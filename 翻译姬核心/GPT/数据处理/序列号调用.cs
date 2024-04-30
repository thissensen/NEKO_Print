using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace 翻译姬;
public class 序列号调用 : GPT数据处理接口 {

    public GPT设置数据 GPT设置数据 { get; set; }
    public Dictionary<string, string> 上文内容 { get; set; } = new Dictionary<string, string>();
    public int 错行重试次数 { get; set; }

    public dynamic 文本转请求(文本[] 文本arr) {
        var list = Util.文本提取对话(文本arr);
        var res = new List<KeyValue<int, string>>();
        for (int i = 0; i < list.Count; i++) {
            var kv = new KeyValue<int, string>();
            kv.Key = i;
            kv.Value = list[i].Value;
            res.Add(kv);
        }
        return res;
    }

    public void 添加上文内容(dynamic 原请求, dynamic 返回值) {
        var 原数据 = 原请求 as List<KeyValue<int, string>>;
        var 机翻数据 = 返回值 as List<KeyValue<int, string>>;
        var 原dic = 原数据.ToDictionary(t => t.Key, t => t.Value);
        var 返回dic = 机翻数据.ToDictionary(t => t.Key, t => t.Value);
        var 原json = JsonConvert.SerializeObject(原dic);
        if (!上文内容.ContainsKey(原json)) {
            上文内容.Add(原json, JsonConvert.SerializeObject(返回dic));
        }
    }

    public void 清空上文内容() {
        上文内容.Clear();
    }

    public bool 漏翻检测(dynamic 返回值) {
        var dic = 返回值 as List<KeyValue<int, string>>;
        bool result = false;
        for (int i = 0; i < dic.Count; i++) {
            var kv = dic[i];
            if (Util.漏翻检测(kv.Value)) {
                kv.Tag = true;
                result = true;
            } else {
                kv.Tag = false;
            }
        }
        return result;
    }

    public dynamic 获取待润色数据(dynamic 原请求, dynamic 返回值) {
        return 返回值;
    }

    public List<dynamic> 获取请求内容(bool 是否润色, dynamic 原请求) {
        var GPT请求 = 原请求 as List<KeyValue<int, string>>;
        var 请求dic = GPT请求.ToDictionary(t => t.Key, t => t.Value);
        string json = JsonConvert.SerializeObject(请求dic);
        //请求内容计算
        var 请求内容 = new List<dynamic>();
        if (!是否润色) {
            请求内容.Add(new { role = "system", content = 语境设置词汇表(GPT设置数据.语境, GPT请求) });
        } else {
            请求内容.Add(new { role = "system", content = 语境设置词汇表(GPT设置数据.润色语境, GPT请求) });
        }
        if (GPT设置数据.上下文提示 && 上文内容.Count > 0) {
            var 取值深度 = 上文内容.Count < GPT设置数据.上下文深度 ? 上文内容.Count : GPT设置数据.上下文深度;
            var res = 上文内容.Skip(上文内容.Count - 取值深度);
            foreach (var kv in res) {
                请求内容.Add(new { role = "user", content = kv.Key });
                请求内容.Add(new { role = "assistant", content = kv.Value });
            }
        }
        请求内容.Add(new { role = "user", content = json });
        return 请求内容;
    }
    private string 语境设置词汇表(string 语境, List<KeyValue<int, string>> GPT请求) {
        语境 = 语境.Replace("[行数]", GPT请求.Count.ToString());
        var 请求内容arr = GPT请求.Select(kv => kv.Value).ToList();
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

    public void 请求写入文本(dynamic 返回值, 文本[] 文本arr) {
        var list = 返回值 as List<KeyValue<int, string>>;
        var res = new List<KeyValue<string, string>>();
        foreach (var item in list) {
            var kv = new KeyValue<string, string>();
            kv.Value = item.Value;
            kv.Tag = item.Tag ?? false;
            res.Add(kv);
        }
        Util.对话写入文本(文本arr, res, true);
    }

    public dynamic 返回值解析(string content, dynamic 原请求) {
        
        string text = Regex.Replace(content, @"^`+[^{\[]+", "");
        text = Regex.Replace(text, @"[\r\n]", "");
        text = Regex.Replace(text, @"`+$", "");
        var res = new List<KeyValue<int, string>>();
        try {
            var dic = JsonConvert.DeserializeObject<Dictionary<int, string>>(text);
            var temp = from kv in dic
                    orderby kv.Key
                    let t = new KeyValue<int, string>() { Key = kv.Key, Value = kv.Value }
                    select t;
            res.AddRange(temp);
        } catch { }
        try {
            if (res.Count == 0) {
                //1：「xxx」\n2：
                var matches = Regex.Matches(text, @"(?'id'\d+)[:：] ?");
                int 当前取值下标 = 0, 上级取值长度 = -1, id取值长度 = 0;
                var id组 = new List<int>();
                var 数据组 = new List<string>();
                foreach (Match match in matches) {
                    int.TryParse(match.Groups["id"].Value, out int id);
                    id组.Add(id);
                    if (上级取值长度 != -1) {
                        //取上一次的循环内容
                        string 内容 = text.Substring(当前取值下标 + id取值长度, match.Index - 当前取值下标 - id取值长度);
                        内容 = Regex.Replace(内容, @"^「", "");
                        内容 = Regex.Replace(内容, @"」?\\?n?$", "");
                        if (!内容.IsNullOrEmpty()) {
                            数据组.Add(内容);
                        }
                    }
                    上级取值长度 = match.Index - 当前取值下标;
                    当前取值下标 = match.Index;
                    id取值长度 = match.Length;
                }
                string temp = text.Substring(当前取值下标 + id取值长度);
                temp = Regex.Replace(temp, @"^「", "");
                temp = Regex.Replace(temp, @"」?\\?n?$", "");
                if (!temp.IsNullOrEmpty()) {
                    数据组.Add(temp);
                }

                if (id组.Count != 0 && id组.Count == 数据组.Count) {
                    for (int i = 0; i < id组.Count; i++) {
                        var kv = new KeyValue<int, string>();
                        kv.Key = id组[i];
                        kv.Value = 数据组[i];
                        res.Add(kv);
                    }
                }
            }
        } catch { }

        if (res.Count != 原请求.Count) {
            if (错行重试次数 == GPT设置数据.错行重试数) {
                throw new Exception_API异常("错行次数已达上限");
            } else {
                错行重试次数++;
                throw new Exception($"返回内容错行");
            }
        }
        foreach (var kv in res) {
            if (kv.Value == null) {
                throw new Exception("未返回机翻内容");
            }
        }
        for (int i = 0; i < 原请求.Count; i++) {
            var 结果 = res.Where(t => t.Key == i).Select(t => t);
            if (结果.Count() == 0) {
                throw new Exception("返回错误id");
            }
        }
        错行重试次数 = 0;
        return res.OrderBy(kv => kv.Key).ToList();
    }
}