using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using 翻译姬;

namespace 翻译姬;
public class GPT调用 {

    static GPT调用() {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
    }

    private HttpClient client = new HttpClient();
    private Stopwatch 计时器;//与api类共用计时器，同时对qps和token生效

    public bool 是否Https { get; set; }
    public string 域名 { get; set; }
    public string 路由 { get; set; }
    public string 模型 { get; set; }
    public double frequency_penalty { get; set; }
    public double temperature { get; set; }
    public double top_p { get; set; }
    public int 等待超时 { get; set; } = 60;//秒
    public string 令牌 { get; set; }

    public int 已用token { get; set; }
    public int token限制 { get; set; }
    public int QPS显示单位 { get; set; } = 60000;//token的限制为每60秒
    public bool 已进行token限制 { get; set; } = false;

    public GPT调用(Stopwatch 计时器, int token限制, bool 是否Https, string 域名, string 路由, string 模型, double frequency_penalty, double temperature, double top_p, int 等待超时, string 令牌) {
        this.计时器 = 计时器;
        this.token限制 = token限制;
        this.是否Https = 是否Https;
        this.域名 = 域名;
        this.路由 = 路由;
        this.模型 = 模型;
        this.frequency_penalty = frequency_penalty;
        this.temperature = temperature;
        this.top_p = top_p;
        this.等待超时 = 等待超时;
        this.令牌 = 令牌;
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + 令牌);
    }

    public GPT返回 调用(List<dynamic> 请求内容, int 最大token) {
        //做Token限制
        if (已用token > token限制) {
            计时器.Stop();
            var num = (int)计时器.ElapsedMilliseconds;
            if (num < QPS显示单位) {
                Thread.Sleep(QPS显示单位 - num);
            }
            已用token = 0;
            已进行token限制 = true;
            计时器.Restart();
        }
        string json;
        try {
            string 连接前缀 = 是否Https ? "https" : "http";
            string url = $"{连接前缀}://{域名}{路由}";
            var res = new Dictionary<string, dynamic>();
            //res.Add("response_format", new { type = "json_object" });//强制Json
            res.Add("model", 模型);
            res.Add("messages", 请求内容);
            res.Add("frequency_penalty", frequency_penalty);//减少模型重复输出，-2到2，默认0，处理退化(返回值一直是最大token)则往上加0.1，
            res.Add("temperature", temperature);//采样度，0到2，默认1，越高输出越随机
            res.Add("top_p", top_p);//质量采样，0到1默认1，0.1表示只会用前10质量的文本。与上方参数二选一
            res.Add("max_tokens", 最大token);
            string body = JsonConvert.SerializeObject(res);
            var rep = client.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json")).Result;
            json = rep.Content.ReadAsStringAsync().Result;
        } catch (AggregateException ex) {
            var 异常信息 = new List<string>();
            ex.Handle(x =>  {
                异常信息.Add(x.Message);
                 return true;
             });
            throw new Exception($"http请求错误:{异常信息.Join("\r\n")}");
        } catch (Exception ex) {
            throw new Exception($"http请求错误:{ex.Message}");
        }
        try {
            GPT返回 返回;
            try {
                返回 = JsonConvert.DeserializeObject<GPT返回>(json);
            } catch {
                throw new Exception($"GPT返回值非Json");
            }
            if (返回 == null || 返回.Usage == null || 返回.Choices == null) {
                throw new Exception($"GPT返回数据异常");
            }
            if (返回.Choices.Count == 0) {
                throw new Exception($"GPT返回结果为空");
            }
            if (返回.Choices[0].Message == null) {
                throw new Exception($"GPT返回的Message为空");
            }
            if (返回.Choices[0].Message.Content.IsNullOrEmpty()) {
                throw new Exception($"GPT返回的Content为空");
            }
            return 返回;
        } catch (Exception ex) {
            throw new Exception($"{ex.Message}:{json}");
        }
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