using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Sunny.UI;
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

namespace 翻译姬;
public class 文本读写 {

    private static 全局设置数据 全局设置数据 => 全局数据.全局设置数据;

    public static string[] 获取文件列表() {
        if (全局设置数据.读取后缀.IsNullOrEmpty()) {
            throw new Exception("读取后缀未填");
        }
        List<string> 文件路径 = new List<string>();
        string[] arr = 全局设置数据.读取后缀.Split('|');
        try {
            foreach (string s in arr) {
                if (s.Trim().IsNullOrEmpty() || !Regex.IsMatch(s, @"\*\.\w")) {
                    throw new Exception($"读取后缀不符合规则，正确规则例如：*.txt|*.lua，使用|分割");
                }
                string[] temp_arr = Directory.GetFiles(全局设置数据.读取目录, s, SearchOption.AllDirectories);
                foreach (var path in temp_arr) {
                    文件路径.Add(path);
                }
            }
        } catch (Exception ex) {
            throw new Exception($"目录【{全局设置数据.读取目录}】读取错误，{ex.Message}");
        }
        if (文件路径.Count() == 0) {
            throw new Exception("目录下没有文件可读取");
        }
        return 文件路径.ToArray();
    }

    public static string[] 读取文本行(string 路径) {
        string 读取编码 = 全局设置数据.读取编码;
        if (读取编码 == "自动识别") {
            读取编码 = 工具类.文本编码识别(路径);
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
    public static void 写出文本行(string 路径, string[] 文本行) {
        写出文本(路径, string.Join(Environment.NewLine, 文本行));
    }
    public static string 读取文本(string 路径) {
        //文本读取
        string 读取编码 = 全局设置数据.读取编码;
        if (读取编码 == "自动识别") {
            读取编码 = 工具类.文本编码识别(路径);
        }
        using FileStream fs = new FileStream(路径, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, false);
        using StreamReader sr = new StreamReader(fs, Encoding.GetEncoding(读取编码));
        return sr.ReadToEnd();
    }
    public static void 写出文本(string 路径, string 文本) {
        string 写出路径;
        if (路径 == null) {//从Tcp而来
            路径 = (全局设置数据.读取目录 + "TCP读取数据.txt").获取不重复路径();
        }
        写出路径 = 目录改变(路径).创建父目录();
        using FileStream fs = new FileStream(写出路径, FileMode.Create, FileAccess.Write, FileShare.None);
        using StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding(全局设置数据.写出编码));
        sw.Write(文本);
        sw.Flush();
        if (全局设置数据.写出后删除源文件 && File.Exists(路径)) {
            File.Delete(路径);
        }
    }

    public static string[] Json提取(string Json文本, DataRow Json指令row) {
        //指令集提取
        string[] 正则指令集 = 获取正则指令集(Json指令row);

        //文本提取
        JToken token = null;
        try {
            token = JToken.Parse(Json文本);
        } catch (Exception e) {
            throw new Exception($"Json识别失败：{e.Message}");
        }
        List<string> res = new List<string>();
        JsonReader reader = token.CreateReader();
        while (reader.Read()) {
            if (reader.TokenType == JsonToken.String && reader.Value != null && reader.Value.ToString().Trim() != "") {
                foreach (string 正则指令 in 正则指令集) {
                    if (Regex.IsMatch(reader.Path, 正则指令)) {
                        res.Add(reader.Value.ToString().Trim());
                        break;
                    }
                }
            }
        }
        return res.ToArray();
    }
    public static string Json写入(string Json文本, string[] 文本行, DataRow Json指令row) {
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
        while (reader.Read()) {
            if (reader.TokenType == JsonToken.String && reader.Value != null && reader.Value.ToString().Trim() != "") {
                foreach (string 正则指令 in 正则指令集) {
                    if (Regex.IsMatch(reader.Path, 正则指令)) {
                        if (取值下标 == 文本行.Length) {
                            throw new Exception($"文本不符合此Json");
                        }
                        res.SelectToken(reader.Path).Replace(文本行[取值下标++]);
                        break;
                    }
                }
            }
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
        string 序列化指令 = Regex.Escape(指令);
        return "^" + 序列化指令.Replace(@"\[\*]", @"\[\d+]") + "$";
    }

    public static string[] Xml提取(string Xml文本, DataRow Xml指令row) {
        //指令集提取
        string[] 指令集 = Xml指令row["指令集"].ToString().Split('|');
        //文本提取
        XmlDocument xml = new XmlDocument();
        try {
            xml.LoadXml(Xml文本);
        } catch (Exception ex) { 
            throw new Exception($"Xml识别失败：{ex.Message}"); 
        }
        List<string> res = new List<string>();
        var stack = new Stack<XmlNode>();
        stack.Push(xml.DocumentElement);
        while (stack.Count > 0) {
            var node = stack.Pop();
            if (node.HasChildNodes) {
                for (int i = node.ChildNodes.Count - 1; i >= 0; i--) {
                    stack.Push(node.ChildNodes[i]);
                }
            } else if (指令集.Contains(node.获取XPath())) {
                res.Add(node.InnerText);
            }
            
        }
        return res.ToArray();
    }
    public static string Xml写入(string Xml文本, string[] 文本行, DataRow Xml指令row) {
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
        int 取值下标 = 0;
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
                if (指令集.Contains(xPath)) {
                    node.InnerText = 文本行[取值下标++];
                }
            }
        }
        return res.OuterXml.格式化Xml();
    }

    /// <summary>
    /// 把文件的根目录改为写出目录
    /// </summary>
    /// <returns></returns>
    private static string 目录改变(string 路径) {
        Regex reg = new Regex(Regex.Escape(全局设置数据.读取目录));
        return reg.Replace(路径, 全局设置数据.写出目录, 1);
    }

}
