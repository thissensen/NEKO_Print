using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace 翻译姬 {
    //QPS:50
    //文档：https://help.aliyun.com/document_detail/201590.html?spm=a2c4g.158291.0.0.606957ddUscJJt
    public class 阿里云API : API接口模板 {

        private int 请求异常次数 = 0;

        public 阿里云API(API信息 data) : base(data) {
        }

        /// <summary>
        /// 传入文本不能有空，全角空格也不行，可以用&
        /// </summary>
        /// <param name="传入文本"></param>
        /// <returns></returns>
        /// <exception cref="Exception_一般错误"></exception>
        protected override string[] 机翻(string[] 传入文本) {
            //阿里云需过滤掉空字符
            for (int i = 0; i < 传入文本.Length; i++) {
                if (传入文本[i].Trim().IsNullOrEmpty()) {
                    传入文本[i] = "&";
                }
            }
            string json = HTTP批量机翻(传入文本);
            阿里云返回内容 res = JsonConvert.DeserializeObject<阿里云返回内容>(json);
            while (res.Code == 101) {
                异常处理.错误处理("阿里云" + 阿里云错误提醒(101));
                res = JsonConvert.DeserializeObject<阿里云返回内容>(HTTP批量机翻(传入文本));
            }
            if (res.Code != 200) {
                throw new Exception_API异常($"阿里云机翻错误，错误码：{res.Code}，提示：{res.Message}{Environment.NewLine}解释：{阿里云错误提醒(res.Code)}");
            }
            List<string> arr = new List<string>();
            foreach (var 单条文本 in res.TranslatedList.OrderBy(t => int.Parse(t.index))) {
                arr.Add(单条文本.translated);
            }
            请求异常次数 = 0;
            return arr.ToArray();
        }
        private string 阿里云错误提醒(int code) {
            return code switch {
                101 => "连接超时",
                110 => "账号没有开通服务",
                113 => "账号服务没有开通或者欠费",
                102 => "系统错误",
                106 => "语种识别错误",
                105 => "该语向不支持",
                104 => "参数错误",
                108 => "字符过长",
                109 => "子账号没有权限",
                111 => "子账号服务失败",
                107 => "翻译错误",
                112 => "翻译服务调用失败",
                199 => "未知错误",
                103 => "URL编码错误",
                19999 => "文本错误，可能存在特殊符号等异常",
                _ => "请前往官网查看错误码：https://api.aliyun.com/document/alimt/2018-10-12/errorCode"
            };
        }
        public string HTTP批量机翻(string[] texts) {
            机翻字符增加(string.Join("", texts).Length);
            string url = "http://mt.cn-hangzhou.aliyuncs.com/api/translate/web/ecommerce";
            //设置SourceText
            Dictionary<string, string> ID_文本 = new Dictionary<string, string>();
            int index = 0;
            foreach (string text in texts) {
                ID_文本.Add((index++).ToString(), text);
            }
            #region 计算请求参数
            Dictionary<string, string> 请求参数 = new Dictionary<string, string>();
            //请求参数.Add("Action", "GetBatchTranslate");
            请求参数.Add("FormatType", "text");
            请求参数.Add("TargetLanguage", data.目标语言);
            请求参数.Add("SourceLanguage", data.源语言);
            请求参数.Add("Scene", "general");//专业版社交：social，通用版：general
            请求参数.Add("ApiType", "translate_standard");//通用版：translate_standard，专业版：translate_ecommerce
            请求参数.Add("SourceText", JsonConvert.SerializeObject(ID_文本));
            请求参数.Add("Action", "GetBatchTranslate");
            请求参数.Add("Version", "2018-10-12");
            请求参数.Add("Format", "JSON");
            请求参数.Add("AccessKeyId", data.KEY);
            请求参数.Add("SignatureNonce", 随机数生成(14));
            请求参数.Add("Timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")); // 使用UTC时间
            请求参数.Add("SignatureMethod", "HMAC-SHA1");
            请求参数.Add("SignatureVersion", "1.0");
            #region 签名算法
            //首字母排序b,a,C => C,a,b，然后进行RFC3896的UTF-8编码
            Dictionary<string, string> 临时请求参数 = (from kv in 请求参数
                                                 orderby kv.Key[0], kv.Key
                                                 select kv).ToDictionary(t1 => RFC3986编码(t1.Key), t2 => RFC3986编码(t2.Value));
            StringBuilder 公共参数体 = new StringBuilder();
            for (int i = 0; i < 临时请求参数.Count; i++) {
                var kv = 临时请求参数.ElementAt(i);
                公共参数体.Append(kv.Key).Append("=").Append(kv.Value);
                if (i != 临时请求参数.Count - 1) {
                    公共参数体.Append("&");
                }
            }
            string 签名字符串 = $"POST&{RFC3986编码("/")}&{RFC3986编码(公共参数体.ToString())}";
            string signature = HMACSHA1加密(签名字符串, data.秘钥 + "&");//用秘钥加密签名串并返回BASE64
            #endregion
            请求参数.Add("Signature", RFC3986编码(signature));//请求签名
            #endregion

            // 将所有请求参数放在请求体中
            StringBuilder 请求体 = new StringBuilder();
            for (int i = 0; i < 请求参数.Count; i++) {
                var kv = 请求参数.ElementAt(i);
                请求体.Append(kv.Key).Append('=').Append(kv.Value);
                if (i != 请求参数.Count - 1) {
                    请求体.Append("&");
                }
            }

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Timeout = 30000;
            request.KeepAlive = false;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded"; // 设置Content-Type
            byte[] requestBody = Encoding.UTF8.GetBytes(请求体.ToString()); // 将请求体转换成字节数组
            request.ContentLength = requestBody.Length; // 设置请求体长度
            using (Stream requestStream = request.GetRequestStream()) {
                requestStream.Write(requestBody, 0, requestBody.Length); // 将请求体写入请求流中
            }

            string 返回值 = null;
            try {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
                    if (response.StatusCode == HttpStatusCode.OK) {//200
                        using (Stream myResponseStream = response.GetResponseStream()) {
                            using (StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"))) {
                                返回值 = myStreamReader.ReadToEnd();
                            }
                        }
                    } else if (response.StatusCode == HttpStatusCode.BadRequest) {//400
                        异常处理.错误处理($"阿里云错误：400");
                    } else {
                        throw new Exception($"阿里云异常：{response.StatusCode}");
                    }
                }
            } catch (Exception ex) {
                异常处理.错误处理($"阿里云Http错误：{ex.Message}");
                请求异常次数++;
                if (请求异常次数 != 10) {
                    return HTTP批量机翻(texts);
                }
                throw new Exception_API异常(ex.Message);
            } finally {
                request.Abort();
            }
            return 返回值;
        }
        /// <summary>
        /// HMACSHA1加密
        /// </summary>
        /// <param name="text">要加密的原串</param>
        ///<param name="key">私钥</param>
        /// <returns></returns>
        private string HMACSHA1加密(string text, string key) {
            //HMACSHA1加密
            HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = System.Text.Encoding.UTF8.GetBytes(key);

            byte[] dataBuffer = System.Text.Encoding.UTF8.GetBytes(text);
            byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);

            return Convert.ToBase64String(hashBytes);

        }
        private string 随机数生成(int 长度) {
            string buffer = "0123456789";// 随机字符中也可以为汉字（任何）
            StringBuilder sb = new StringBuilder();
            Random r = new Random();
            int range = buffer.Length;
            for (int i = 0; i < 长度; i++) {
                sb.Append(buffer.Substring(r.Next(range), 1));
            }
            return sb.ToString();
        }
        private string RFC3986编码(string str) {
            StringBuilder sb = new StringBuilder();
            var urf8Bytes = Encoding.UTF8.GetBytes(str);
            foreach (var item in urf8Bytes) {
                if (IsReverseChar((char)item)) {
                    sb.Append('%');
                    sb.Append(item.ToString("X2"));
                } else
                    sb.Append((char)item);
            }

            return sb.ToString();
        }
        private bool IsReverseChar(char c) {
            return !((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9')
                    || c == '-' || c == '_' || c == '.' || c == '~');
        }

    }
    public class 阿里云返回内容 {
        public int Code { get; set; }
        public string Message { get; set; }
        public string RequestId { get; set; }
        public Translated[] TranslatedList { get; set; }
    }
    public class Translated {
        public string code { get; set; }//状态码
        public string wordCount { get; set; }//文档页？
        public string index { get; set; }//数组下标，对应请求体对应内容
        public string translated { get; set; }//中文
    }
    
}
