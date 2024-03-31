using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace 翻译姬;
public class 火山API : API接口模板 {

    private int 请求异常次数 = 0;

    public 火山API(API信息 data) : base(data) {
    }

    protected override string[] 机翻(string[] 传入文本) {
        string json = HTTP批量机翻(传入文本);
        try {
            var res = JsonConvert.DeserializeObject<火山返回内容>(json);
            while (res.ResponseMetadata.Error?.Code == "FlowLimitExceeded") {//频率超过限制
                异常处理.错误处理("火山" + 火山错误提醒(res.ResponseMetadata.Error.Code));
                res = JsonConvert.DeserializeObject<火山返回内容>(json);
            }
            if (res == null || res.ResponseMetadata.Error != null) {
                throw new Exception_API异常(火山错误提醒(res.ResponseMetadata.Error.Code));
            }
            请求异常次数 = 0;
            return res.TranslationList.Select(t => t.Translation).ToArray();
        } catch (Exception ex) {
            throw new Exception_API异常($"火山机翻错误：{ex.Message}");
        }
    }

    private string 火山错误提醒(string code) {
        return code switch {
            "FlowLimitExceeded" => "请求过于频繁",
            "MissingParameter" => "关键参数缺失",
            "MissingRequestInfo" => "缺少请求必要信息",
            "MissingAuthenticationToken" => "缺少身份认证的必要信息",
            "InvalidTimestamp" => "请求过期或请求的签名时间来自未来",
            "InvalidAccessKey" => "Access Key不合法，请检查是否有多余空格",
            "SignatureDoesNotMatch" => "签名结果不正确",
            "InvalidAuthorization" => "Authorization头格式错误",
            "InvalidCredential" => "Authorization头中的Credential格式错误",
            "InvalidSecretToken" => "错误的STS（临时安全凭证）",
            _ => "请前往官网查看错误码"
        };
    }

    //API对接：https://www.volcengine.com/docs/6369/67268
    public string HTTP批量机翻(string[] texts) {

        机翻字符增加(string.Join("", texts).Length);

        var 请求体 = new Dictionary<string, object>();
        请求体.Add("SourceLanguage", data.源语言);
        请求体.Add("TargetLanguage", data.目标语言);
        请求体.Add("TextList", texts);
        string body = JsonConvert.SerializeObject(请求体);

        string QueryString = "Action=TranslateText&Version=2020-06-01";
        string url = "http://translate.volcengineapi.com/?" + QueryString;
        HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        request.Timeout = 30000;
        request.KeepAlive = false;
        request.Method = "POST";
        request.ContentType = "application/json";
        request.Accept = "application/json";
        request.Headers.Add("X-Date", DateTime.UtcNow.ToString("yyyyMMddTHHmmssZ"));
        request.Headers.Add("X-Content-Sha256", 获取SHA256(body));
        
        #region 签名计算
        var 签名头 = new Dictionary<string, string>();
        签名头.Add("Content-Type", request.ContentType);
        签名头.Add("HOST", request.Host);
        签名头.Add("X-Content-Sha256", request.Headers.Get("X-Content-Sha256"));
        签名头.Add("X-Date", request.Headers.Get("X-Date"));
        var CanonicalHeaders = new StringBuilder();
        foreach (var kv in 签名头) {
            CanonicalHeaders.Append(kv.Key.ToLower()).Append(':');
            CanonicalHeaders.Append(kv.Value.Trim()).Append('\n');
        }
        string 小写签名头 = string.Join(";", 签名头.Keys.Select(t => t.ToLower()));
        string CanonicalRequest =
            "POST" + '\n' +
            "/" + '\n' +
            QueryString + '\n' +
            CanonicalHeaders + '\n' +
            小写签名头 + '\n' +
            获取SHA256(body);

        string StringToSign = "HMAC-SHA256" + '\n' +
            签名头["X-Date"] + '\n' +
            $"{DateTime.UtcNow:yyyyMMdd}/cn-north-1/translate/request" + '\n' +
            获取SHA256(CanonicalRequest);

        var Signingkey = HMAC(HMAC(HMAC(HMAC(Encoding.UTF8.GetBytes(data.秘钥), DateTime.UtcNow.ToString("yyyyMMdd")), "cn-north-1"), "translate"), "request");
        var Signature = BitConverter.ToString(HMAC(Signingkey, StringToSign)).Replace("-", "").ToLower();

        string 签名串 = $"HMAC-SHA256 Credential={data.KEY}/{DateTime.UtcNow:yyyyMMdd}/cn-north-1/translate/request, "
            + $"SignedHeaders={小写签名头}, Signature={Signature}";
        #endregion

        request.Headers.Add("Authorization", 签名串);
        var buf = Encoding.UTF8.GetBytes(body);
        request.ContentLength = buf.Length;
        using var reqS = request.GetRequestStream();
        reqS.Write(buf, 0, buf.Length);

        string 返回值 = null;
        try {
            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK) {//200
                using Stream myResponseStream = response.GetResponseStream();
                using StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                返回值 = myStreamReader.ReadToEnd();
            }
        } catch (Exception ex) {
            异常处理.错误处理($"火山Http错误：{ex.Message}");
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
    //HMACSHA256加密
    private static byte[] HMAC(byte[] key, string text) {
        HMACSHA256 hmac = new HMACSHA256();
        hmac.Key = key;
        byte[] dataBuffer = Encoding.UTF8.GetBytes(text);
        return hmac.ComputeHash(dataBuffer);
    }

    private string 获取SHA256(string str) {
        byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
        SHA256Managed Sha256 = new SHA256Managed();
        byte[] by = Sha256.ComputeHash(SHA256Data);
        return BitConverter.ToString(by).Replace("-", "").ToLower();
    }

}
public class 火山返回内容 {
    public List<TranslationListItem> TranslationList { get; set; }
    public ResponseMetadata ResponseMetadata { get; set; }
}
public class TranslationListItem {
    /// <summary>
    /// 机翻内容
    /// </summary>
    public string Translation { get; set; }
    /// <summary>
    /// 源语言
    /// </summary>
    public string DetectedSourceLanguage { get; set; }
}

public class ResponseMetadata {
    public string RequestId { get; set; }
    public string Action { get; set; }
    public string Version { get; set; }
    public string Service { get; set; }
    public string Region { get; set; }
    public ResponseMetadataError Error { get; set; }
}
public class ResponseMetadataError {
    public string Code { get; set; }
    public string Message { get; set; }
}