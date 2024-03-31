using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace 翻译姬;
public class Src_Dst调用 : GPT数据处理接口 {

    public GPT设置数据 GPT设置数据 { get ; set; }
    public Dictionary<string, string> 上文内容 { get; set; } = new Dictionary<string, string>();
    public bool 润色模式 { get; set; }

    public List<dynamic> 获取请求内容(params dynamic[] data) {
        string json = data[0];
        List<Src_Dst请求> GPT请求 = data[1];
        //请求内容计算
        var 请求内容 = new List<dynamic>();
        if (!润色模式) {
            请求内容.Add(new { role = "system", content = 语境设置词汇表(GPT设置数据.语境, GPT请求) });
        } else {
            请求内容.Add(new { role = "system", content = 语境设置词汇表(GPT设置数据.润色语境, GPT请求) });
        }
        if (GPT设置数据.上下文提示 && 上文内容.Count > 0) {
            var 取值深度 = 上文内容.Count < GPT设置数据.上下文深度 ? 上文内容.Count : GPT设置数据.上下文深度;
            var res = 上文内容.Skip(上文内容.Count - 取值深度);
            foreach (var kv in res) {
                请求内容.Add(new { role = "user", content = kv.Key });
                //请求内容.Add(new { role = "system", content = kv.Value });
                请求内容.Add(new { role = "assistant", content = kv.Value });
            }
        }
        请求内容.Add(new { role = "user", content = json });
        return 请求内容;
    }
    private string 语境设置词汇表(string 语境, List<Src_Dst请求> GPT请求) {
        语境 = 语境.Replace("[行数]", GPT请求.Count.ToString());
        var 人名arr = (from 请求 in GPT请求
                     where 请求.name != null && 请求.name != ""
                     select 请求.name).Distinct();
        var 文本arr = (from 请求 in GPT请求
                     select 请求.src).Distinct();
        var 请求内容arr = 人名arr.Concat(文本arr).Distinct();
        var sb = new StringBuilder();
        var 待添加rows = new List<DataRow>();
        foreach (DataRow row in GPT设置数据.GPT词汇表.Rows) {
            var 原文 = row["原文"].ToString();
            bool 是否添加 = false;
            foreach (var 请求内容 in 请求内容arr) {
                if (请求内容.Contains(原文)) {
                    是否添加 = true;
                    break;
                }
            }
            if (是否添加 && !待添加rows.Contains(row)) {
                待添加rows.Add(row);
            }
        }
        foreach (var row in 待添加rows) {
            sb.AppendLine($"| {row["原文"]} | {row["译文"]} | {row["备注"]} |");
        }
        if (sb.Length == 0) {
            return 语境.Replace("[词汇表]", "");
        } else {
            return 语境.Replace("[词汇表]", sb.ToString());
        }
    }

    public dynamic 返回值解析(string content) {
        string text = Regex.Replace(content, @"^`+[^{\[]+", "");
        text = Regex.Replace(text, @"[\r\n]", "");
        text = Regex.Replace(text, @"`+$", "");
        text = Regex.Replace(text, @"^\[|\]$", "");//删除左右[]
        text = Regex.Replace(text, @"(?<=""|,)}?\]?\s*?[，,]?\s*?\[?{", "}\r\n{");
        if (!text.EndsWith("}")) {
            text += "}";
        }
        var res = new List<Src_Dst请求>();
        try {
            string[] arr = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var reg_dst = new Regex(@".*?""id"".+?(?'id'\d+).*?""dst"".*?""(?'dst'.*?)[""}]");
            var reg_src = new Regex(@".*?""id"".+?(?'id'\d+).*?""src"".*?""(?'src'.*?)[""}]");
            foreach (var s in arr) {
                GroupCollection g = null;
                int id = -1;
                string dst = null;
                if (reg_dst.IsMatch(s)) {
                    g = reg_dst.Match(s).Groups;
                    id = int.Parse(g["id"].ToString());
                    dst = g["dst"].ToString().Trim();
                    if (dst != "") {
                        goto 返回值提取成功;
                    }
                    dst = null;
                }
                //src提取解析
                g = reg_src.Match(s).Groups;
                id = int.Parse(g["id"].ToString());
                var src = g["src"].ToString().Trim();
                if (src != "" && !Util.漏翻检测(src)) {
                    dst = src;
                }
            返回值提取成功:
                var gpt = new Src_Dst请求();
                gpt.id = id;
                gpt.dst = dst;
                res.Add(gpt);
            }
        } catch { }
        /*foreach (var g in res) {
            if (g.dst == null) {
                异常处理.错误处理($"GPT未返回dst，重试");
                机翻执行次数++;
                var 返回 = 机翻(GPT请求);
                return 返回值解析(返回.Choices[0].Message.Content, GPT请求);
            }
        }
        if (res.Count != GPT请求.Count) {
            if (错行重试次数 == GPT设置数据.错行重试数) {
                throw new Exception($"GPT错行次数已达上限");
            } else {
                异常处理.错误处理($"GPT错行，异常内容：{原始text}\r\n");
                错行重试次数++;
                机翻执行次数++;
                var 返回 = 机翻(GPT请求);
                return 返回值解析(返回.Choices[0].Message.Content, GPT请求);
            }
        }
        for (int i = 0; i < GPT请求.Count; i++) {
            if (res.FirstOrDefault(t => t.id == i) == null) {
                异常处理.错误处理($"GPT返回错误id，异常内容：{原始text}");
                机翻执行次数++;
                var 返回 = 机翻(GPT请求);
                return 返回值解析(返回.Choices[0].Message.Content, GPT请求);
            }
        }*/
        return res.OrderBy(t => t.id).ToList();
    }
}
public class Src_Dst请求 {
    public int id { get; set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string name { get; set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string src { get; set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string dst { get; set; }//翻后
    [JsonIgnore]
    public bool 是否漏翻 = false;
}
