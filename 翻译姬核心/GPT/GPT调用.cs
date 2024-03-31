using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using 翻译姬;

namespace 翻译姬;
public class GPT调用 {

    private HttpClient client = new HttpClient();

    public bool 是否Https { get; set; }
    public string 域名 { get; set; }
    public string 模型 { get; set; }
    public int 等待超时 { get; set; } = 60;//秒
    public string 令牌 { get; set; }

    public GPT调用(bool 是否Https, string 域名, string 模型, int 等待超时, string 令牌) {
        this.是否Https = 是否Https;
        this.域名 = 域名;
        this.模型 = 模型;
        this.等待超时 = 等待超时;
        this.令牌 = 令牌;
    }

    public GPT返回 调用(List<dynamic> 请求内容) {
        string 连接前缀 = 是否Https ? "https" : "http";
        string url = $"{连接前缀}://{域名}/v1/chat/completions";
        var res = new Dictionary<string, dynamic>();
        res.Add("model", 模型);
        //res.Add("response_format", new { type = "json_object" });//强制Json
        res.Add("messages", 请求内容);
        string body = JsonConvert.SerializeObject(res);
        var rep = client.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json")).Result;
        var json = rep.Content.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject<GPT返回>(json);
    }

}
public class GPT返回 {
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("object")]
    public string Object { get; set; }
    [JsonProperty("created")]
    public long Created { get; set; }
    [JsonProperty("model")]
    public string Model { get; set; }
    [JsonProperty("choices")]
    public List<ChoicesEntity> Choices { get; set; }
    [JsonProperty("usage")]
    public UsageEntity Usage { get; set; }

}
public class ChoicesEntity {
    [JsonProperty("index")]
    public long Index { get; set; }
    [JsonProperty("message")]
    public MessageEntity Message { get; set; }
    [JsonProperty("finish_reason")]
    public string FinishReason { get; set; }

}
public class MessageEntity {
    [JsonProperty("role")]
    public string Role { get; set; }
    [JsonProperty("content")]
    public string Content { get; set; }
    [JsonProperty("function_call")]
    public Function_callEntity Function_call { get; set; }
}
public class Function_callEntity {
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("arguments")]
    public string Arguments { get; set; }
}
public class UsageEntity {
    [JsonProperty("prompt_tokens")]
    public int PromptTokens { get; set; }
    [JsonProperty("completion_tokens")]
    public int CompletionTokens { get; set; }
    [JsonProperty("total_tokens")]
    public int TotalTokens { get; set; }

}