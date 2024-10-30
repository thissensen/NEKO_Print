using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace 翻译姬;
public class Sakura调用 : GPT数据处理接口 {

    public GPT设置数据 GPT设置数据 { get; set; }
    public Dictionary<string, string> 上文内容 { get; set; } = new Dictionary<string, string>();
    public int 错行重试次数 { get; set; }

    public dynamic 文本转请求(文本[] 文本arr) {
        var list = Util.文本提取对话(文本arr);
        var res = new List<string>();
        for (int i = 0; i < list.Count; i++) {
            var kv = list[i];
            var val = kv.Value.Replace("\r\n", "\\n").Replace("\n", "\\n");//防json自带的换行
            res.Add($"{kv.Key}「{val}」");
        }
        return res;
    }

    public void 添加上文内容(dynamic 原请求, dynamic 返回值) {
        var 原数据 = 原请求 as List<string>;
        var 机翻数据 = 返回值 as List<string>;
        var 原str = 原数据.Join("\n");
        var 返回str = 机翻数据.Join("\n");
        if (!上文内容.ContainsKey(原str)) {
            上文内容.Add(原str, 返回str);
        }
    }

    public void 清空上文内容() {
        上文内容.Clear();
    }

    public bool 漏翻检测(dynamic 返回值) {
        var list = 返回值 as List<string>;
        bool 是否漏翻 = false;
        for (int i = 0; i < list.Count; i++) {
            if (Util.漏翻检测(list[i])) {
                list[i] = $"(漏翻){返回值}";
                是否漏翻 = true;
            }
        }
        return 是否漏翻;
    }

    public void 请求写入文本(dynamic 返回值, 文本[] 文本arr) {
        var list = 返回值 as List<string>;
        var res = new List<KeyValue<string, string>>();
        foreach (var item in list) {
            var kv = new KeyValue<string, string>();
            kv.Tag = item.StartsWith("(漏翻)");
            kv.Value = Regex.Replace(item, @"^\(漏翻\)", "");
            res.Add(kv);
        }
        Util.对话写入文本(文本arr, res, true);
    }

    public dynamic 获取待润色数据(dynamic 原请求, dynamic 返回值) {
        return 返回值;
    }

    public List<dynamic> 获取请求内容(bool 是否润色, dynamic 原请求) {
        var GPT请求 = 原请求 as List<string>;
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
        请求内容.Add(new { role = "user", content = GPT请求.Join("\r\n") });
        return 请求内容;
    }
    private string 语境设置词汇表(string 语境, List<string> GPT请求) {
        //词汇表
        var sb = new StringBuilder();
        var 待添加rows = new List<DataRow>();
        foreach (DataRow row in GPT设置数据.GPT词汇表.Rows) {
            var 原文 = row["原文"].ToString();
            bool 是否添加 = false;
            foreach (var 请求内容 in GPT请求) {
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
            sb.Append($"{row["原文"]}->{row["译文"]}");
            if (row["备注"].ToString() != "") {
                sb.AppendLine($" #{row["备注"]}");
            } else {
                sb.AppendLine();
            }
        }
        return 语境.Replace("[Glossary]", sb.ToString());
    }

    public dynamic 返回值解析(string content, dynamic 原请求, bool 是否润色) {
        var GPT请求 = 原请求 as List<string>;
        string text = content;

        var res = new List<string>();
        try {
            var arr = text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length == GPT请求.Count) {
                for (int i = 0; i < arr.Length; i++) {
                    string val = arr[i];
                    val = 文本修正(GPT请求[i], val);
                    res.Add(val);
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
        错行重试次数 = 0;
        return res;
    }
    private static Regex 值提取 = new Regex(@".*?「(?'val'.*?)」[.。]?$");
    private string 文本修正(string 原文, string 译文) {
        if (值提取.IsMatch(译文)) {
            译文 = 值提取.Match(译文).Groups["val"].Value;
            if (原文.Contains("\n")) {//结合原文恢复正常的换行
                int 原文换行次数 = 原文.Count(c => c == '\n');
                译文 = Regex.Replace(译文, @"(?<!\\r)\\n", "\n");
                if (译文.Count(c => c == '\n') > 原文换行次数) {
                    //保证换行一致
                    var val_arr = 译文.Split('\n');
                    译文 = val_arr.Take(原文换行次数).Join("\n") + val_arr.Skip(原文换行次数).Join("\\n");
                }
            }
        }
        译文 = Regex.Replace(译文, @"」[.。]?$", "");
        return 译文;
    }

}
