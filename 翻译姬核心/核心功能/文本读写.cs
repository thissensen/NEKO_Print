using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using System.Xml;
using System.Xml.Linq;

namespace 翻译姬;
public class 文本读写 {

    private static 全局设置数据 全局设置数据 => 全局数据.全局设置数据;

    public static string[] 获取文件列表(string 读取目录 = null) {
        if (读取目录 == null) {
            读取目录 = 全局设置数据.读取目录;
        }
        if (全局设置数据.读取后缀.IsNullOrEmpty()) {
            throw new Exception("读取后缀未填");
        }
        List<string> 文件路径 = new List<string>();
        string[] arr = 全局设置数据.读取后缀.Split('|');
        try {
            foreach (string s in arr) {
                if (s.Trim().IsNullOrEmpty() || !Regex.IsMatch(s, @"\*\.\w")) {
                    string 提示 = """
                        读取后缀不符合规则
                        正确规则例如：*.txt|*.ks
                        *开头，使用|分割
                        """;
                    throw new Exception(提示);
                }
                string[] temp_arr = Directory.GetFiles(读取目录, s, SearchOption.AllDirectories);
                foreach (var path in temp_arr) {
                    文件路径.Add(path);
                }
            }
        } catch (Exception ex) {
            throw new Exception($"目录【{读取目录}】读取错误，{ex.Message}");
        }
        if (文件路径.Count() == 0) {
            throw new Exception("目录下没有文件可读取");
        }
        return 文件路径.ToArray();
    }

    public static string[] 读取文本行(string 路径) {
        string 读取编码 = 全局设置数据.读取编码;
        if (读取编码 == "自动识别") {
            读取编码 = Util.文本编码识别(路径);
        }
        using FileStream fs = new FileStream(路径, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, false);
        using StreamReader sr = new StreamReader(fs, Encoding.GetEncoding(读取编码));
        string t = null;
        List<string> list = new List<string>();
        while ((t = sr.ReadLine()) != null) {
            list.Add(t);
        }
        return list.ToArray();
    }
    public static void 写出文本行(文件结构 文件, string[] 文本行) {
        写出文本(文件, string.Join(Environment.NewLine, 文本行));
    }
    public static string 读取文本(string 路径) {
        //文本读取
        string 读取编码 = 全局设置数据.读取编码;
        if (读取编码 == "自动识别") {
            读取编码 = Util.文本编码识别(路径);
        }
        using FileStream fs = new FileStream(路径, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, false);
        using StreamReader sr = new StreamReader(fs, Encoding.GetEncoding(读取编码));
        return sr.ReadToEnd();
    }
    public static void 写出文本(文件结构 文件, string 文本) {
        string 写出路径 = (全局设置数据.写出目录 + 文件.相对路径).创建父目录();
        if (!全局设置数据.写出后缀.IsNullOrEmpty()) {
            var f = new FileInfo(写出路径);
            写出路径 = Path.Combine(f.DirectoryName, Regex.Replace(f.Name, @$"{f.Extension}$", $".{全局设置数据.写出后缀.Trim()}"));
        }
        using FileStream fs = new FileStream(写出路径, FileMode.Create, FileAccess.Write, FileShare.None);
        using StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding(全局设置数据.写出编码));
        sw.Write(文本);
        sw.Flush();
        if (全局设置数据.写出后删除源文件 && File.Exists(文件.路径)) {
            File.Delete(文件.路径);
        }
    }

    public static 文本[] Json提取(string Json文本, DataRow Json指令row, DataRow 正则指令row = null) {
        //指令集提取
        string[] 正则指令集 = 获取正则指令集(Json指令row);
        //文本提取
        JToken token = null;
        try {
            token = JToken.Parse(Json文本);
        } catch (Exception e) {
            throw new Exception($"Json识别失败：{e.Message}");
        }
        var res = new List<文本>();
        JsonReader reader = token.CreateReader();
        int 文本下标 = 0;
        while (reader.Read()) {
            if (reader.TokenType != JsonToken.String || reader.Value == null || reader.Value.ToString().Trim() == "") {
                continue;
            }
            foreach (string 正则指令 in 正则指令集) {
                if (!Regex.IsMatch(reader.Path, 正则指令)) {
                    continue;
                }
                var 是否人名 = Regex.Match(reader.Path, 正则指令).Groups["name"].ToString() != "";
                var 文本 = new 文本(文本下标, reader.Value.ToString().Trim());
                if (正则指令row != null) {
                    foreach (var t in 正则读写.文本过滤正则(文本, 正则指令row)) {
                        if (是否人名) {
                            t.文本类型 = 文本类型.人名;
                            t.人名 = 文本.原文;
                        }
                        res.Add(t);
                    }
                } else {
                    if (是否人名) {
                        文本.文本类型 = 文本类型.人名;
                        文本.人名 = 文本.原文;
                    }
                    res.Add(文本);
                }
                break;
            }
            文本下标++;
        }
        return res.ToArray();
    }
    public static string Json写入(string Json文本, 文本[] 文本行, DataRow Json指令row, DataRow 正则指令row = null) {
        //指令集提取
        string[] 正则指令集 = 获取正则指令集(Json指令row);
        //写入
        JToken token = null, res = null;
        try {
            token = JToken.Parse(Json文本);
            res = token.DeepClone();
        } catch (Exception e) {
            throw new Exception($"Json识别失败：{e.Message}");
        }
        int 取值下标 = 0;
        JsonReader reader = token.CreateReader();
        int 文本下标 = 0;
        while (reader.Read()) {
            if (reader.TokenType != JsonToken.String || reader.Value == null || reader.Value.ToString().Trim() == "") {
                continue;
            }
            foreach (string 正则指令 in 正则指令集) {
                if (!Regex.IsMatch(reader.Path, 正则指令)) {
                    continue;
                }
                string text = reader.Value.ToString().Trim();
                var 文本 = new 文本(文本下标, text);
                if (正则指令row != null) {
                    foreach (var t in 正则读写.文本过滤正则(文本, 正则指令row)) {
                        文本 待替换 = 文本行.ElementAtOrDefault(取值下标++);
                        text = new Regex(Regex.Escape(待替换.原文)).Replace(text, 待替换.译文, 1);
                    }
                } else {
                    文本 待替换 = 文本行.ElementAtOrDefault(取值下标++);
                    text = new Regex(Regex.Escape(待替换.原文)).Replace(text, 待替换.译文, 1);
                }
                res.SelectToken(reader.Path).Replace(text);
                break;
            }
            文本下标++;
        }
        return res.ToString();
    }
    private static string[] 获取正则指令集(DataRow Json指令row) {
        List<string> list = new List<string>();
        foreach (string 指令 in Json指令row["指令集"].ToString().Split('|')) {
            string 正则指令 = 指令正则化(指令);
            try {
                new Regex(正则指令);
            } catch (Exception ex) {
                throw new Exception($"指令【{指令}】不符合要求");
            }
            list.Add(正则指令);
        }
        return list.ToArray();
    }
    private static string 指令正则化(string 指令) {
        指令 = 指令.Replace("[", @"\[");
        指令 = 指令.Replace("]", @"\]");
        指令 = 指令.Replace(@"\[*\]", @"\[\d+\]");
        指令 = Regex.Replace(指令, @"\.(?!\*)", @"\.");
        return $"^{指令}$";
    }

    public static 文本[] Xml提取(string Xml文本, DataRow Xml指令row, DataRow 正则指令row = null) {
        //指令集提取
        string[] 指令集 = Xml指令row["指令集"].ToString().Split('|');
        //文本提取
        XmlDocument xml = new XmlDocument();
        try {
            xml.LoadXml(Xml文本);
        } catch (Exception ex) { 
            throw new Exception($"Xml识别失败：{ex.Message}"); 
        }
        var res = new List<文本>();
        var stack = new Stack<XmlNode>();
        stack.Push(xml.DocumentElement);
        int 文本下标 = 0;
        while (stack.Count > 0) {
            var node = stack.Pop();
            if (node.HasChildNodes) {
                for (int i = node.ChildNodes.Count - 1; i >= 0; i--) {
                    stack.Push(node.ChildNodes[i]);
                }
            } else { 
                if (指令集.Contains(node.获取XPath()) && node.InnerText.Trim() != "") {
                    var 文本 = new 文本(文本下标, node.InnerText.Trim());
                    if (正则指令row != null) {
                        foreach (var t in 正则读写.文本过滤正则(文本, 正则指令row)) {
                            res.Add(t);
                        }
                    } else {
                        res.Add(文本);
                    }
                }
                文本下标++;
            }
            
        }
        return res.ToArray();
    }
    public static string Xml写入(string Xml文本, 文本[] 文本行, DataRow Xml指令row, DataRow 正则指令row = null) {
        //指令集提取
        string[] 指令集 = Xml指令row["指令集"].ToString().Split('|');
        //文本提取
        XmlDocument xml = new XmlDocument();
        XmlNode res = null;
        try {
            xml.LoadXml(Xml文本);
            res = xml.Clone();
        } catch (Exception ex) {
            throw new Exception($"Xml识别失败：{ex.Message}");
        }
        int 取值下标 = 0, 文本下标 = 0;
        var stack = new Stack<XmlNode>();
        stack.Push(res);
        while (stack.Count > 0) {
            var node = stack.Pop();
            if (node.HasChildNodes) {
                for (int i = node.ChildNodes.Count - 1; i >= 0; i--) {
                    stack.Push(node.ChildNodes[i]);
                }
            } else {
                string xPath = node.获取XPath();
                if (指令集.Contains(xPath) && node.InnerText.Trim() != "") {
                    string text = node.InnerText.Trim();
                    var 文本 = new 文本(文本下标, text);
                    if (正则指令row != null) {
                        foreach (var t in 正则读写.文本过滤正则(文本, 正则指令row)) {
                            文本 待替换 = 文本行.ElementAtOrDefault(取值下标++);
                            text = new Regex(Regex.Escape(待替换.原文)).Replace(text, 待替换.译文, 1);
                        }
                    } else {
                        文本 待替换 = 文本行.ElementAtOrDefault(取值下标++);
                        text = new Regex(Regex.Escape(待替换.原文)).Replace(text, 待替换.译文, 1);
                    }
                    node.InnerText = text;
                }
                文本下标++;
            }
        }
        return res.OuterXml.格式化Xml();
    }

}
