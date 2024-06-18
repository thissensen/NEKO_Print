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
    public int 错行重试次数 { get; set; } = 0;

    //返回 List<dynamic>
    public List<dynamic> 获取请求内容(bool 是否润色, dynamic data) {
        var GPT请求 = data as List<Src_Dst请求>;
        //请求内容计算
        var 请求内容 = new List<dynamic>();
        if (!是否润色) {
            请求内容.Add(new { role = "system", content = 语境设置词汇表(GPT设置数据.语境,  GPT请求) });
        } else {
            请求内容.Add(new { role = "system", content = 语境设置词汇表(GPT设置数据.润色语境,  GPT请求) });
        }
        if (GPT设置数据.上下文提示 && 上文内容.Count > 0) {
            var 取值深度 = 上文内容.Count < GPT设置数据.上下文深度 ? 上文内容.Count : GPT设置数据.上下文深度;
            var res = 上文内容.Skip(上文内容.Count - 取值深度);
            foreach (var kv in res) {
                请求内容.Add(new { role = "user", content = kv.Key });
                请求内容.Add(new { role = "assistant", content = kv.Value });
            }
        }
        string json = JsonConvert.SerializeObject(GPT请求);
        请求内容.Add(new { role = "user", content = json });
        return 请求内容;
    }
    private string 语境设置词汇表(string 语境, List<Src_Dst请求> GPT请求) {
        //基础
        /*string json = JsonConvert.SerializeObject(GPT请求);
        语境 = 语境.Replace("[Input]", json);*/
        //语境 = 语境.Replace("[Input]", "");

        语境 = 语境.Replace("[SourceLang]", 预设语言[全局数据.全局设置数据.源语言]);
        语境 = 语境.Replace("[TargetLang]", 预设语言[全局数据.全局设置数据.目标语言]);
        //人名动态prompt
        var 人名arr = (from 请求 in GPT请求
                     where 请求.name != null && 请求.name != ""
                     select 请求.name).Distinct();
        if (人名arr.Count() > 0) {
            var match = Regex.Match(语境, @"\[NamePrompt:(.*?)]");
            var nameprompt = match.Groups[0].Value;
            var 值 = match.Groups[1].Value;
            if (nameprompt != "") {
                语境 = 语境.Replace(nameprompt, 值);
            }
            //string.Replace的值长度不能为0，故没有else
        }
        //词汇表设置
        var 文本arr = (from 请求 in GPT请求
                     select 请求.src).Distinct();
        var 请求内容arr = 人名arr.Concat(文本arr).Distinct();
        var sb = new StringBuilder();
        sb.AppendLine("# Glossary");
        sb.AppendLine("| Src | Dst(/Dst2/..) | Note |");
        sb.AppendLine("| --- | --- | --- |");
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
        return 语境.Replace("[Glossary]", sb.ToString());
    }
    private Dictionary<string, string> 预设语言 = new Dictionary<string, string> {
        ["日语"] = "Japanese",
        ["英语"] = "English",
        ["韩语"] = "Korean",
        ["繁中"] = "Traditional Chinese",
        ["简中"] = "Simplified Chinese"
    };

    public dynamic 返回值解析(string content, dynamic GPT请求, bool 是否润色) {
        var 原请求 = GPT请求 as List<Src_Dst请求>;
        string text = Regex.Replace(content, @"^`+[^{\[]+", "");//去除开头的`
        text = Regex.Replace(text, @"^jsonline", "");//去除开头的jsonline
        text = Regex.Replace(text, @"[\r\n]", "");//去除莫名的换行
        text = Regex.Replace(text, @"`+$", "");//去除结尾的`
        text = Regex.Replace(text, @"^\[|\]$", "");//删除左右[]
        text = Regex.Replace(text, @"(?<=""|,)}?\]?\s*?[，,]?\s*?\[?{", "}\r\n{");//修正}, {的异常
        if (!text.EndsWith("}")) {
            text += "}";
        }
        var res = new List<Src_Dst请求>();
        try {
            string[] arr = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string 取值目标 = 是否润色 ? "newdst" : "dst";
            var reg_dst = new Regex(@$".*?(?:""id"".+?(?'id'\d+))?.*?""{取值目标}"".*?""(?'dst'.*?)[""}}]");
            var reg_src = new Regex(@".*?(?:""id"".+?(?'id'\d+))?.*?""src"".*?""(?'src'.*?)[""}]");
            int num = 0;
            foreach (var s in arr) {
                GroupCollection g = null;
                int id = -1;
                string dst = null;
                if (reg_dst.IsMatch(s)) {
                    g = reg_dst.Match(s).Groups;
                    dst = g["dst"].ToString().Trim();
                    if (dst != "") {
                        if (g["id"].ToString() == "") {
                            id = num++;
                        } else {
                            id = int.Parse(g["id"].ToString());
                        }
                        goto 返回值提取成功;
                    }
                    dst = null;
                }
                //src提取解析
                g = reg_src.Match(s).Groups;
                if (g["id"].ToString() == "") {
                    id = num++;
                } else {
                    id = int.Parse(g["id"].ToString());
                }
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

        if (res.Count != 原请求.Count) {
            if (错行重试次数 == GPT设置数据.错行重试数) {
                throw new Exception_API异常("GPT错行次数已达上限");
            } else {
                错行重试次数++;
                throw new Exception($"GPT错行");
            }
        }
        foreach (var g in res) {
            if (g.dst == null) {
                throw new Exception("GPT未返回dst");
            }
        }
        for (int i = 0; i < 原请求.Count; i++) {
            if (res.FirstOrDefault(t => t.id == i) == null) {
                throw new Exception("GPT返回错误id");
            }
        }
        错行重试次数 = 0;
        var temp = res.OrderBy(t => t.id).ToList();
        Name对齐( 原请求, temp);//name可能缺失，进行还原
        return temp;
    }
    private void Name对齐(List<Src_Dst请求> 原请求, List<Src_Dst请求> 返回值) {
        for (int i = 0; i < 返回值.Count; i++) {
            返回值[i].name = 原请求[i].name;
        }
    }

    public dynamic 文本转请求(文本[] 文本arr) {
        var list = Util.文本提取对话(文本arr);
        var res = new List<Src_Dst请求>();
        for (int i = 0; i < list.Count; i++) {
            var 请求 = new Src_Dst请求();
            请求.id = i;
            请求.name = list[i].Key;
            请求.src = list[i].Value;
            res.Add(请求);
        }
        return res;
    }

    public void 请求写入文本(dynamic 返回值, 文本[] 文本arr) {
        var list = 返回值 as List<Src_Dst请求>;
        var res = new List<KeyValue<string, string>>();
        foreach (var item in list) {
            var kv = new KeyValue<string, string>();
            //kv.Key = item.name;
            kv.Value = item.dst;
            kv.Tag = item.是否漏翻;
            res.Add(kv);
        }
        Util.对话写入文本(文本arr, res, true);
    }

    public dynamic 获取待润色数据(dynamic 原请求, dynamic 返回值) {
        var res = new List<Src_Dst请求>();
        var 原数据 = 原请求 as List<Src_Dst请求>;
        var 返回数据 = 返回值 as List<Src_Dst请求>;
        for (int i = 0; i < 原数据.Count; i++) {
            var data = 原数据[i] with { dst = 返回数据[i].dst };
            res.Add(data);
        }
        return res;
    }

    public bool 漏翻检测(dynamic 返回值) {
        var res = 返回值 as List<Src_Dst请求>;
        bool result = false;
        foreach (var item in res) {
            if (Util.漏翻检测(item.dst)) {
                item.是否漏翻 = true;
                result = true;
            }
        }
        return result;
    }
    
    public void 添加上文内容(dynamic 原请求, dynamic 机翻后) {
        var 原数据 = 原请求 as List<Src_Dst请求>;
        var 机翻数据 = 机翻后 as List<Src_Dst请求>;
        var 理想机翻数据 = new List<Src_Dst请求>();
        for (int i = 0; i < 原数据.Count; i++) {
            var data = 原数据[i] with { src = null, dst = 机翻数据[i].dst };
            理想机翻数据.Add(data);
        }
        var 原json = JsonConvert.SerializeObject(原数据);
        if (!上文内容.ContainsKey(原json)) {
            上文内容.Add(原json, JsonConvert.SerializeObject(理想机翻数据));
        }
    }

    public void 清空上文内容() {
        上文内容.Clear();
    }
}
public record class Src_Dst请求 {
    public int id { get; set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string name { get; set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string src { get; set; }//原文
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string dst { get; set; }//译文
    [JsonIgnore]
    public bool 是否漏翻 = false;
}
