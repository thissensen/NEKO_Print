using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace 翻译姬;
public class 正则读写 {

    private static 全局设置数据 全局设置数据 => 全局数据.全局设置数据;

    public static string[] 正则文本提取(string[] 原文本行, DataRow 正则row) {
        string 行过滤正则 = 正则row["行过滤正则"].ToString();
        string 文本过滤正则 = 正则row["文本过滤正则"].ToString();
        string 提取前行过滤正则 = 正则row["提取前行过滤正则"].ToString();
        string 提取型正则 = 正则row["提取型正则"].ToString();
        if (行过滤正则.IsNullOrEmpty() &&
            文本过滤正则.IsNullOrEmpty() &&
            提取前行过滤正则.IsNullOrEmpty() &&
            提取型正则.IsNullOrEmpty()) {
            return 原文本行;
        }
        if (原文本行.Length == 0) {
            return 原文本行;
        }
        List<string> res = new List<string>();
        foreach (string text in 原文本行) {
            string[] 提取文本 = 正则识别分割(text, 行过滤正则, 文本过滤正则, 提取前行过滤正则, 提取型正则);
            if (提取文本 != null) {
                res.AddRange(提取文本);
            }
        }
        return res.ToArray();
    }
    public static string[] 正则文本写入(string[] 原文本行, string[] 译文, DataRow 正则row) {
        string 行过滤正则 = 正则row["行过滤正则"].ToString();
        string 文本过滤正则 = 正则row["文本过滤正则"].ToString();
        string 提取前行过滤正则 = 正则row["提取前行过滤正则"].ToString();
        string 提取型正则 = 正则row["提取型正则"].ToString();
        if (行过滤正则.IsNullOrEmpty() &&
            文本过滤正则.IsNullOrEmpty() &&
            提取前行过滤正则.IsNullOrEmpty() &&
            提取型正则.IsNullOrEmpty()) {
            for (int i = 0; i < 译文.Length; i++) {
                译文[i] = 写出格式替换(原文本行[i], 译文[i]);
            }
            return 译文;
        }
        if (原文本行.Length == 0 || 译文.Length == 0) {
            return 原文本行;
        }
        int 译文下标 = 0;
        List<string> res = new List<string>();
        foreach (string text in 原文本行) {
            string[] 提取文本 = 正则识别分割(text, 行过滤正则, 文本过滤正则, 提取前行过滤正则, 提取型正则);
            if (提取文本 != null) {
                //替换为译文
                string 译文text = text;
                for (int i = 0; i < 提取文本.Length; i++) {
                    string 分割译文text = 译文[译文下标++];
                    译文text = new Regex(Regex.Escape(提取文本[i])).Replace(译文text, 分割译文text, 1);
                }
                //进行写出格式替换
                res.Add(写出格式替换(text, 译文text));
            } else {
                res.Add(text);
            }
        }
        return res.ToArray();
    }
    /// <summary>
    /// 默认Trim()
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private static string[] 正则识别分割(string text, string 行过滤正则, string 文本过滤正则, string 提取前行过滤正则, string 提取型正则) {
        //先提取
        List<string> 结果 = new List<string>();
        if (提取型正则 != "" && !(提取前行过滤正则 != "" && Regex.IsMatch(text, 提取前行过滤正则))) {
            //有提取型正则，并没被提取前行过滤掉
            string[] 提取正则arr = 提取型正则.Split('|');
            foreach (var 提取正则 in 提取正则arr) {
                GroupCollection groups = Regex.Match(text, 提取正则).Groups;
                if (groups.Count < 1) {
                    continue;
                }
                string groupText = groups[1].Value;
                if (groupText == null) {
                    continue;//null：提取了个空气，主要是*造成的，+就没事
                }
                string[] 分割后arr;
                if (文本过滤正则 != "") {
                    分割后arr = Regex.Split(groupText, 文本过滤正则);
                } else {
                    分割后arr = new string[] { groupText };
                }
                foreach (string str in 分割后arr) {
                    if (str.Trim() != "") {
                        结果.Add(str);
                    }
                }
            }
            //能提取到则不进行普通提取
            if (结果.Count > 0) {
                return 结果.ToArray();
            }
        }
        //提取不到再普通
        if (行过滤正则 != "" && Regex.IsMatch(text, 行过滤正则)) {//符合行过滤不执行
            return null;
        }
        //正则分割
        string[] arr;
        if (文本过滤正则 != "") {
            arr = Regex.Split(text, 文本过滤正则);
        } else {
            arr = new string[] { text };
        }
        foreach (string str in arr) {
            if (str.Trim() != "") {
                结果.Add(str);
            }
        }
        if (结果.Count > 0) {
            return 结果.ToArray();
        }
        return null;
    }
    private static string 写出格式替换(string 原文text, string 译文text) {
        if (全局设置数据.写出格式.IsNullOrEmpty()) {
            return 译文text;
        }
        string res = 全局设置数据.写出格式.Replace("[原文]", 原文text);
        return res.Replace("[译文]", 译文text);
    }

}
