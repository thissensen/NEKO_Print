using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Net.Http;

namespace 翻译姬 {
    //QPS:5
    //文档：https://cloud.tencent.com/document/product/551/40566
    public class 腾讯云API : API接口模板 {

        private int 请求异常次数 = 0;

        public 腾讯云API(API信息 data) : base(data) {
        }

        protected override string[] 机翻(string[] 传入文本) {
            string json = HTTP批量机翻(传入文本);
            try {
                var res = JsonConvert.DeserializeObject<腾讯云返回内容>(json)?.Response;
                while (res.error?.Code == "RequestLimitExceeded") {//频率超过限制
                    异常处理.错误处理("腾讯云" + 腾讯云错误提醒(res.error.Code));
                    res = JsonConvert.DeserializeObject<腾讯云返回内容>(json)?.Response;
                }
                if (res == null || !string.IsNullOrEmpty(res.error?.Code)) {
                    throw new Exception_API异常(腾讯云错误提醒(res?.error.Code));
                }
                请求异常次数 = 0;
                return res.TargetTextList;
            } catch (Exception ex) {
                throw new Exception_API异常($"腾讯云机翻错误：{ex.Message}");
            }
        }
        private string 腾讯云错误提醒(string errorStr) {
            return errorStr switch {
                "RequestLimitExceeded" => "请求频率过高",
                "AuthFailure.InvalidSecretId" => "密钥非法（不是云 API 密钥类型）。",
                "AuthFailure.MFAFailure" => "MFA 错误。",
                "AuthFailure.SecretIdNotFound" => "密钥不存在。",
                "AuthFailure.SignatureExpire" => "签名过期。",
                "AuthFailure.SignatureFailure" => "签名错误。",
                "AuthFailure.TokenFailure" => "token 错误。",
                "AuthFailure.UnauthorizedOperation" => "请求未 CAM 授权。",
                "DryRunOperation" => "DryRun 操作，代表请求将会是成功的，只是多传了 DryRun 参数。",
                "FailedOperation" => "操作失败。",
                "InternalError" => "内部错误。",
                "InvalidAction" => "接口不存在。",
                "InvalidParameter" => "参数错误。",
                "InvalidParameterValue" => "参数取值错误。",
                "LimitExceeded" => "超过配额限制。",
                "MissingParameter" => "缺少参数错误。",
                "NoSuchVersion" => "接口版本不存在。",
                "ResourceInUse" => "资源被占用。",
                "ResourceInsufficient" => "资源不足。",
                "ResourceNotFound" => "资源不存在。",
                "ResourceUnavailable" => "资源不可用。",
                "UnauthorizedOperation" => "未授权操作。",
                "UnknownParameter" => "未知参数错误。",
                "UnsupportedOperation" => "操作不支持。",
                "UnsupportedProtocol" => "HTTPS 请求方法错误，只支持 GET 和 POST 请求。",
                "UnsupportedRegion" => "接口不支持所传地域。",
                _ => "错误码：" + errorStr + "，请前往官网查看错误码：https://cloud.tencent.com/document/api/551/30637"
            };
        }
        private string HTTP批量机翻(string[] texts) {
            机翻字符增加(string.Join("", texts).Length);
            string url = "https://tmt.tencentcloudapi.com/";
            DateTime time = DateTime.UtcNow;
            string 产品名 = "tmt";
            string 凭证 = $"{time:yyyy-MM-dd}/{产品名}/tc3_request";
            Dictionary<string, object> body_dic = new Dictionary<string, object>();
            body_dic.Add("Source", data.源语言);
            body_dic.Add("Target", data.目标语言);
            body_dic.Add("ProjectId", 0);
            body_dic.Add("SourceTextList", texts);
            string body = JsonConvert.SerializeObject(body_dic);

            Dictionary<string, string> 请求头 = new Dictionary<string, string>();
            请求头.Add("X-TC-Action", "TextTranslateBatch");
            请求头.Add("X-TC-Region", "ap-shanghai");
            请求头.Add("X-TC-Timestamp", 获取Unix秒级时间戳(time).ToString());
            请求头.Add("X-TC-Version", "2018-03-21");

            #region 签名算法
            string 请求串 = "POST\n/\n\ncontent-type:application/json; charset=utf-8\nhost:tmt.tencentcloudapi.com\n\ncontent-type;host\n" + SHA256加密(body);
            string 签名串 = $"TC3-HMAC-SHA256\n{请求头["X-TC-Timestamp"]}\n{凭证}\n{SHA256加密(请求串)}";
            byte[] secretDate = HMAC_SHA256加密(time.ToString("yyyy-MM-dd"), Encoding.UTF8.GetBytes("TC3" + data.秘钥));
            byte[] secretService = HMAC_SHA256加密(产品名, secretDate);
            byte[] secretSigning = HMAC_SHA256加密("tc3_request", secretService);
            byte[] sign = HMAC_SHA256加密(签名串, secretSigning);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < sign.Length; i++) {
                sb.Append(sign[i].ToString("x2"));
            }
            string 签名 = $"TC3-HMAC-SHA256 Credential={data.KEY}/{凭证}, SignedHeaders=content-type;host, Signature={sb}";
            请求头.Add("Authorization", 签名);
            请求头.Add("X-TC-Language", "zh-CN");
            #endregion

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Timeout = 30000;
            request.KeepAlive = false;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            foreach (var kv in 请求头) {
                request.Headers.Add(kv.Key, kv.Value);
            }
            byte[] buf = Encoding.UTF8.GetBytes(body);
            request.ContentLength = buf.Length;
            using Stream requestStream = request.GetRequestStream();
            requestStream.Write(buf, 0, buf.Length);
            

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
                        异常处理.错误处理($"腾讯云错误：400");
                    } else {
                        throw new Exception($"腾讯云异常：{response.StatusCode}");
                    }
                }
            } catch (Exception ex) {
                异常处理.错误处理($"腾讯云Http错误：{ex.Message}");
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
        /// 获取Unix秒级时间戳
        /// </summary>  
        private long 获取Unix秒级时间戳(DateTime utcTime) {
            DateTime utcStartTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan ts = utcTime - utcStartTime;
            return Convert.ToInt64(ts.TotalSeconds);
        }

        private string SHA256加密(string text) {
            byte[] hash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++) {
                builder.Append(hash[i].ToString("x2"));
            }
            return builder.ToString();
        }

        private byte[] HMAC_SHA256加密(string 文本, byte[] 秘钥) {
            using var hmacsha256 = new HMACSHA256(秘钥);
            return hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(文本));
        }

    }
    public class 腾讯云返回内容 {
        public Response Response { get; set; }
    }
    public class Response {
        public Error error { get; set; }//错误
        public string Source { get; set; }//源语言
        public string Target { get; set; }//目标语言
        public string[] TargetTextList { get; set; }//返回结果
        public string RequestId { get; set; }
    }
    public class Error {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
