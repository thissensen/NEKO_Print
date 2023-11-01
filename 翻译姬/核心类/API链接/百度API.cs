using Newtonsoft.Json;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;

namespace 翻译姬 {
    /*
     * 超额不停止
     * 标准版：
     *  QPS：1
     *  5W/月免费
     * 高级版：
     *  QPS：10
     *  100W/月免费
     * 尊享版：
     *  QPS：100
     *  200W/月免费
     *  文档：http://api.fanyi.baidu.com/api/trans/product/apidoc
     */
    public class 百度API : API接口模板 {

        public 百度API(API信息 data) : base(data) {
        }

        /// <summary>
        /// 百度传入文本不能为空，可以全角空格
        /// </summary>
        /// <param name="传入文本"></param>
        /// <returns></returns>
        protected override string[] 机翻(string[] 传入文本) {
            //百度需过滤掉空字符
            for (int i = 0; i < 传入文本.Length; i++) {
                if (传入文本[i].Trim().IsNullOrEmpty()) {
                    传入文本[i] = "　";
                }
                if (传入文本[i].Contains("\\r")) {
                    throw new Exception($"文本中包含\\r符号，百度会造成错行，请在正则中过滤掉");
                }
            }
            string 单条文本 = string.Join("\r", 传入文本);
            string json = Http单条机翻(单条文本);
            百度返回内容 res = JsonConvert.DeserializeObject<百度返回内容>(json);
            while (res.error_code == 52001 || //请求超时
                res.error_code == 52002 ||    //系统错误
                res.error_code == 54003 ||    //访问频率过高 
                res.error_code == 54005) {    //长query请求频繁

                错误处理.普通错误处理("百度" + 百度错误提醒(res.error_code));
                res = JsonConvert.DeserializeObject<百度返回内容>(Http单条机翻(单条文本));
            }
            if (res.error_code != 0 && res.error_code != 52000) {
                throw new Exception_API异常($"百度机翻错误，错误码：{res.error_code}{Environment.NewLine}解释：{百度错误提醒(res.error_code)}");
            } else {
                string[] 机翻结果 = (from result in res.trans_result
                       select result.dst).ToArray();
                return 还原格式(传入文本, 机翻结果);
            }
        }
        private string 百度错误提醒(int code) {
            return code switch {
                52001 => "请求超时",
                52002 => "系统错误",
                54003 => "访问频率过高",
                52003 => "未授权用户",
                54000 => "必填参数为空",
                54001 => "签名错误",
                54004 => "账户余额不足",
                54005 => "长query请求频繁",
                58000 => "客户端IP非法",
                58001 => "译文语言方向不支持",
                58002 => "服务当前已关闭",
                90107 => "认证未通过或未生效",
                _ => "请前往官网查看错误码"
            };
        }
        private string Http单条机翻(string text) {
            机翻字符增加(text.Length);
            Random rd = new Random();
            string salt = rd.Next(100000).ToString();
            string secretKey = data.秘钥;
            string sign = 字符串加密(data.KEY + text + salt + secretKey);
            string url = "http://api.fanyi.baidu.com/api/trans/vip/translate";

            Dictionary<string, string> 请求参数 = new Dictionary<string, string>();
            请求参数.Add("q", HttpUtility.UrlEncode(text));
            请求参数.Add("from", data.源语言);
            请求参数.Add("to", data.目标语言);
            请求参数.Add("appid", data.KEY);
            请求参数.Add("salt", salt);
            请求参数.Add("sign", sign);

            // 将所有请求参数放在请求体中
            StringBuilder 请求体 = new StringBuilder();
            for (int i = 0; i < 请求参数.Count; i++) {
                var kv = 请求参数.ElementAt(i);
                请求体.Append(kv.Key).Append('=').Append(kv.Value);
                if (i != 请求参数.Count - 1) {
                    请求体.Append("&");
                }
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.KeepAlive = false;//是否建立永久链接，容易造成超时
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = null;
            request.Timeout = 5000;
            byte[] requestBody = Encoding.UTF8.GetBytes(请求体.ToString()); // 将请求体转换成字节数组
            request.ContentLength = requestBody.Length; // 设置请求体长度
            using (Stream requestStream = request.GetRequestStream()) {
                requestStream.Write(requestBody, 0, requestBody.Length); // 将请求体写入请求流中
            }
            string 返回值 = null;
            HttpWebResponse response = null;
            try {
                using (response = (HttpWebResponse)request.GetResponse()) {
                    using (Stream myResponseStream = response.GetResponseStream()) {
                        using (StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"))) {
                            返回值 = myStreamReader.ReadToEnd();
                        }
                    }
                }
                if ("".Equals(返回值) || '{' != 返回值.Trim()[0]) {
                    错误处理.普通错误处理("百度返回错误：" + 返回值);
                    返回值 = Http单条机翻(text);
                }
            } catch (Exception ex) {
                错误处理.普通错误处理($"百度Http错误：{ex.Message},错误码:{response?.StatusCode}");
                返回值 = Http单条机翻(text);
            } finally {
                request.Abort();
            }
            return 返回值;
        }
        private string 字符串加密(string str) {
            MD5 md5 = MD5.Create();
            // 将字符串转换成字节数组
            byte[] byteOld = Encoding.UTF8.GetBytes(str);
            // 调用加密方法
            byte[] byteNew = md5.ComputeHash(byteOld);
            // 将加密结果转换为字符串
            StringBuilder sb = new StringBuilder();
            foreach (byte b in byteNew) {
                // 将字节转换成16进制表示的字符串，
                sb.Append(b.ToString("x2"));
            }
            // 返回加密的字符串
            return sb.ToString();
        }
        private string[] 还原格式(string[] 传入文本, string[] 机翻结果) {
            //传入文本可能有\r，进行文本对齐
            int 取值下标 = 0;
            string[] res = new string[传入文本.Length];
            for (int i = 0; i < 传入文本.Length; i++) {
                string[] arr = 传入文本[i].Split('\r');
                res[i] = string.Join("\r", 机翻结果.Skip(取值下标).Take(arr.Length));
                取值下标 += arr.Length;
            }
            return res;
        }

        public class 百度返回文本信息 {
            public string src { get; set; }//原文
            public string dst { get; set; }//译文
        }

        public class 百度返回内容 {
            public string from { get; set; }
            public string to { get; set; }
            public int error_code { get; set; }
            public List<百度返回文本信息> trans_result { get; set; }
        }
    }
}
