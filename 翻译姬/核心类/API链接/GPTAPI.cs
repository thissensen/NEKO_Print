using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Threading;

namespace 翻译姬;
public class GPTAPI : API接口模板 {
    //最后一次返回时的内容
    private static KeyValue<string, string> 上文内容;
    private static GPT设置数据 GPT设置数据 => 全局数据.GPT设置数据;
    public override int QPS显示单位 => 60 * 1000;
    private int 已用Token = 0;//记录单位时间内使用的Token数
    private int 请求异常次数 = 0;
    private int 错行重试次数 = 0;
    private BPE算法 bpe = new BPE算法();

    public GPTAPI(API信息 data) : base(data) {
        data.QPS = GPT设置数据.次数限制;
        GPT设置数据.必要数据验证();
    }

    protected override string[] 机翻(string[] 传入文本) {
        //请求内容计算
        var 请求内容 = 获取请求内容(传入文本);
    GPT机翻开始:
        string[] 译文;
        try {
            //Token限制
            int 使用Token = (int)(bpe.Token计算(JsonConvert.SerializeObject(请求内容)) * 1.02);
            if (使用Token + 已用Token > GPT设置数据.Token限制) {
                计时器.Stop();
                int 所用毫秒 = (int)计时器.ElapsedMilliseconds;
                if (所用毫秒 < QPS显示单位) {
                    Thread.Sleep(QPS显示单位 - 所用毫秒);
                }
                机翻执行次数 = 0;
                已用Token = 0;
                计时器.Restart();
            }
            //错行重试
            var res = JsonConvert.DeserializeObject<GPT返回>(GPT调用(请求内容));
            已用Token += res.Usage.TotalTokens;
            机翻字符增加(res.Usage.TotalTokens);
            var 返回值 = JsonConvert.DeserializeObject<Dictionary<int, string>>(res.Choices[0].Message.Content);
            if (返回值.Count != 传入文本.Length) {
                if (错行重试次数 == GPT设置数据.错行重试数) {
                    错误处理.普通错误处理($"GPT错行次数已达上限，无视本次错行");
                } else {
                    错行重试次数++;
                    机翻执行次数++;
                    goto GPT机翻开始;
                }
            }
            错行重试次数 = 0;
            译文 = 返回值.OrderBy(t => t.Key).Select(t => t.Value).ToArray();
            if (返回值.Count == 传入文本.Length) {
                上文内容 = new KeyValue<string, string> {
                    Key = JsonConvert.SerializeObject(请求内容),
                    Value = JsonConvert.SerializeObject(返回值)
                };
            }
        } catch (JsonSerializationException ex) {
            错误处理.普通错误处理($"GPT返回值非Json，重试");
            goto GPT机翻开始;
        } catch (Exception ex) {
            throw new Exception_API异常(ex.Message);
        }
        return 译文;
    }

    public string GPT调用(List<dynamic> 请求内容) {
        string url = $"https://{GPT设置数据.连接域名}/v1/chat/completions";
        string requestBody = JsonConvert.SerializeObject(new {
            model = GPT设置数据.使用模型,
            messages = 请求内容
        });
        HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        request.KeepAlive = false;
        request.Timeout = GPT设置数据.请求等待延迟 * 1000;
        request.Method = "POST";
        request.ContentType = "application/json";
        request.Headers.Add("Authorization", "Bearer " + data.秘钥);
        byte[] buf = Encoding.UTF8.GetBytes(requestBody);
        request.ContentLength = buf.Length;
        using Stream stream = request.GetRequestStream();
        stream.Write(buf, 0, buf.Length);

        string 返回值 = null;
        try {
            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using Stream myResponseStream = response.GetResponseStream();
            using StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            返回值 = myStreamReader.ReadToEnd();
        } catch (System.Net.WebException ex) {
            错误处理.普通错误处理($"GPT错误：{ex.Message}");
            请求异常次数++;
            if (请求异常次数 <= 3) {
                return GPT调用(请求内容);
            } else {
                throw new Exception_API异常(ex.Message);
            }
        } catch (Exception ex) {
            错误处理.普通错误处理($"GPT错误：{ex.Message}");
            请求异常次数++;
            if (请求异常次数 <= 3) {
                return GPT调用(请求内容);
            } else {
                throw new Exception_API异常(ex.Message);
            }
        } finally {
            request.Abort();
        }
        return 返回值;
    }

    private List<dynamic> 获取请求内容(string[] texts) {
        //请求内容计算
        var 请求内容 = new List<dynamic>();
        请求内容.Add(new { role = "system", content = GPT设置数据.语境 });
        if (GPT设置数据.发送预设) {
            请求内容.Add(new { role = "user", content = GPT设置数据.预设原文 });
            请求内容.Add(new { role = "assistant", content = GPT设置数据.预设返回 });
        }
        if (GPT设置数据.上下文提示 && 上文内容 != null) {
            请求内容.Add(new { role = "user", content = 上文内容.Key });
            请求内容.Add(new { role = "assistant", content = 上文内容.Value });
        }
        var dic = new Dictionary<int, string>();
        int num = 0;
        foreach (var text in texts) {
            dic.Add(num++, text);
        }
        请求内容.Add(new { role = "user", content = JsonConvert.SerializeObject(dic) });
        return 请求内容;
    }

}
public record class GPT_请求 {
    public string role;
    public string content;
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

}
public class UsageEntity {
    [JsonProperty("prompt_tokens")]
    public int PromptTokens { get; set; }
    [JsonProperty("completion_tokens")]
    public int CompletionTokens { get; set; }
    [JsonProperty("total_tokens")]
    public int TotalTokens { get; set; }

}
