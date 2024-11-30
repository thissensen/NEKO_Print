using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace 翻译姬;
public class 正则读写 {

    private static 全局设置数据 全局设置数据 => 全局数据.全局设置数据;

    public static 文本[] 文本过滤正则(文本 文本, Regex 行过滤正则, Regex 文本过滤正则) {
        if (行过滤正则 == null && 文本过滤正则 == null) {
            return new[] { 文本 };
        }
        var 文本arr = 正则识别分割(文本.原文, 文本.文本下标, 行过滤正则, 文本过滤正则, null, null); 
        if (文本arr == null) {
            return null;
        }
        var res = new List<文本>();
        foreach (var item in 文本arr) {
            res.Add(item);
        }
        return res.ToArray();
    }
    public static 文本[] 正则文本提取(string[] 原文本行, DataRow 正则row) {
        #region 正则预加载，提升速度
        Regex 行过滤正则 = null;
        Regex 文本过滤正则 = null;
        Regex 提取前行过滤正则 = null;
        Regex[] 提取型正则arr = null;

        if (!正则row["行过滤正则"].ToString().IsNullOrEmpty()) {
            行过滤正则 = new Regex(正则row["行过滤正则"].ToString());
        }

        if (!正则row["文本过滤正则"].ToString().IsNullOrEmpty()) {
            文本过滤正则 = new Regex(正则row["文本过滤正则"].ToString());
        }

        if (!正则row["提取前行过滤正则"].ToString().IsNullOrEmpty()) {
            提取前行过滤正则 = new Regex(正则row["提取前行过滤正则"].ToString());
        }

        if (!正则row["提取型正则"].ToString().IsNullOrEmpty()) {
            string[] 提取正则arr = Util.正则分割(正则row["提取型正则"].ToString());
            var 提取正则list = new List<Regex>();
            foreach (var 正则 in 提取正则arr) {
                提取正则list.Add(new Regex(正则));
            }
            提取型正则arr = 提取正则list.ToArray();
        }
        #endregion
        if (行过滤正则 == null && 文本过滤正则 == null && 提取前行过滤正则 == null && 提取型正则arr == null) {
            var temp = new List<文本>();
            for (int i = 0; i < 原文本行.Length; i++) {
                temp.Add(new 文本(i, 原文本行[i]));
            }
            return temp.ToArray();
        }
        List<文本> res = new List<文本>();
        for (int i = 0; i < 原文本行.Length; i++) {
            string text = 原文本行[i];
            文本[] 提取文本 = 正则识别分割(text, i, 行过滤正则, 文本过滤正则, 提取前行过滤正则, 提取型正则arr);
            if (提取文本 != null) {
                res.AddRange(提取文本);
            }
        }
        return res.ToArray();
    }
    public static string[] 正则文本写入(string[] 原文本行, 文本[] 译文, DataRow 正则row) {

        if (原文本行.Length == 0 || 译文.Length == 0) {
            return 原文本行;
        }
        #region 正则预加载，提升速度
        Regex 行过滤正则 = null;
        Regex 文本过滤正则 = null;
        Regex 提取前行过滤正则 = null;
        Regex[] 提取型正则arr = null;

        if (!正则row["行过滤正则"].ToString().IsNullOrEmpty()) {
            行过滤正则 = new Regex(正则row["行过滤正则"].ToString());
        }

        if (!正则row["文本过滤正则"].ToString().IsNullOrEmpty()) {
            文本过滤正则 = new Regex(正则row["文本过滤正则"].ToString());
        }

        if (!正则row["提取前行过滤正则"].ToString().IsNullOrEmpty()) {
            提取前行过滤正则 = new Regex(正则row["提取前行过滤正则"].ToString());
        }

        if (!正则row["提取型正则"].ToString().IsNullOrEmpty()) {
            string[] 提取正则arr = Util.正则分割(正则row["提取型正则"].ToString());
            var 提取正则list = new List<Regex>();
            foreach (var 正则 in 提取正则arr) {
                提取正则list.Add(new Regex(正则));
            }
            提取型正则arr = 提取正则list.ToArray();
        }
        #endregion
        var res = new List<string>();
        if (行过滤正则 == null && 文本过滤正则 == null && 提取前行过滤正则 == null && 提取型正则arr == null) {
            for (int i = 0; i < 译文.Length; i++) {
                res.Add(写出格式替换(原文本行[i], 译文[i].译文, i));
            }
            return res.ToArray();
        }

        int 译文下标 = 0, 写出格式id = 1;
        for (int i = 0; i < 原文本行.Length; i++) {
            string text = 原文本行[i];
            文本[] 提取文本 = 正则识别分割(text, i, 行过滤正则, 文本过滤正则, 提取前行过滤正则, 提取型正则arr);
            if (提取文本 != null) {
                //替换为译文
                string 译文text = text;
                if (!全局设置数据.正则逆向写入) {
                    int 搜索位置 = 0;
                    for (int j = 0; j < 提取文本.Length; j++) {
                        string 分割译文text = 译文[译文下标++].译文;
                        译文text = new Regex(Regex.Escape(提取文本[j].原文)).Replace(译文text, m => {
                            搜索位置 = m.Index + 分割译文text.Length;//正序：会匹配包括m.idnex的字符
                            return 分割译文text;
                        }, 1, 搜索位置);
                    }
                } else {
                    int 搜索位置 = 译文text.Length;
                    var 待替换译文 = new List<string>();
                    for (int j = 0; j < 提取文本.Length; j++) {
                        string 分割译文text = 译文[译文下标++].译文;
                        待替换译文.Add(分割译文text);
                    }
                    for (int j = 提取文本.Length - 1; j >= 0; j--) {
                        var 分割译文text = 待替换译文[j];
                        译文text = new Regex(Regex.Escape(提取文本[j].原文), RegexOptions.RightToLeft)
                            .Replace(译文text, m => {
                                //从右往左，不包括m.idnex的字符
                                搜索位置 = m.Index;
                                return 分割译文text;
                            }, 1, 搜索位置);
                    }
                }
                //进行写出格式替换
                res.Add(写出格式替换(text, 译文text, 写出格式id));
                写出格式id++;
            } else {
                res.Add(text);
            }
        }
        return res.ToArray();
    }
    /// <summary>
    /// // 默认Trim()
    /// </summary>
    /// <returns>要么null，要么一定有至少一个值</returns>
    private static 文本[] 正则识别分割(string text, int 文本下标, Regex 行过滤正则, Regex 文本过滤正则, Regex 提取前行过滤正则, Regex[] 提取型正则arr) {
        //先提取
        List<文本> 结果 = new List<文本>();
        if (提取型正则arr != null && 提取型正则arr.Length > 0 && !(提取前行过滤正则 != null && 提取前行过滤正则.IsMatch(text))) {
            //有提取型正则，并没被提取前行过滤掉
            //string[] 提取正则arr = Util.正则分割(提取型正则);
            foreach (var 提取正则 in 提取型正则arr) {
                GroupCollection groups = 提取正则.Match(text).Groups;
                var 提取结果 = groups.获取提取结果();
                if (提取结果.Count == 0) {
                    continue;
                }
                foreach (var kv in 提取结果) {
                    string 提取name = kv.Key;
                    string 提取text = kv.Value;
                    if (提取text.IsNullOrEmpty()) {
                        continue;//提取了个空气，主要是*造成的，+就没事
                    }
                    string[] 分割后arr;
                    if (提取name != "name" && 文本过滤正则 != null) {
                        分割后arr = 文本过滤(提取text, 文本过滤正则);
                    } else {
                        分割后arr = new string[] { 提取text };
                    }
                    foreach (string str in 分割后arr) {
                        if (str.Trim() == "") {
                            continue;
                        }
                        文本 t = new 文本(文本下标, str);
                        if (提取name == "name") {
                            t.人名 = t.原文;
                            t.文本类型 = 文本类型.人名;
                        }
                        结果.Add(t);
                    }
                }
                //能提取到则不进行普通提取
                if (结果.Count > 0) {
                    return 结果.ToArray();
                }
            }
        }
        //提取不到再普通
        if (行过滤正则 != null && 行过滤正则.IsMatch(text)) {//符合行过滤不执行
            return null;
        }
        //正则分割
        string[] arr;
        if (文本过滤正则 != null) {
            arr = 文本过滤(text, 文本过滤正则);
        } else {
            arr = new string[] { text };
        }
        foreach (string str in arr) {
            if (str.Trim() == "") {
                continue;
            }
            文本 t = new 文本(文本下标, str);
            结果.Add(t);
        }
        if (结果.Count > 0) {
            return 结果.ToArray();
        }
        return null;
    }
    private static Regex 单文件行reg = new Regex(@"\[单文件行顺序\+?(-?\d{1,8})?\]");
    private static Regex 全局顺序reg = new Regex(@"\[全局顺序\+?(-?\d{1,8})?\]");
    private static string 写出格式替换(string 原文text, string 译文text, int 单文件行顺序) {
        if (全局设置数据.写出格式.IsNullOrEmpty()) {
            return 译文text;
        }
        string res = 全局设置数据.写出格式.Replace("[原文]", 原文text);
        res = res.Replace("[译文]", 译文text);
        if (单文件行reg.IsMatch(全局设置数据.写出格式)) {
            int.TryParse(单文件行reg.Match(全局设置数据.写出格式).Groups[1].ToString(), out int 变量值);
            res = 单文件行reg.Replace(res, (单文件行顺序 + 变量值).ToString());
        }
        if (全局顺序reg.IsMatch(全局设置数据.写出格式)) {
            int.TryParse(全局顺序reg.Match(全局设置数据.写出格式).Groups[1].ToString(), out int 变量值);
            res = 全局顺序reg.Replace(res, (变量值 + 全局数据.写出格式全局变量).ToString());
            全局数据.写出格式全局变量++;
        }
        return res;
    }
    private static string[] 文本过滤(string 文本, Regex 文本过滤正则) {
        string[] 原文本;
        if (全局设置数据.内置中括号过滤) {
            原文本 = 中括号分割(文本);
        } else {
            原文本 = new string[] { 文本 };
        }
        var res = new List<string>();
        foreach (var 分割后 in 原文本) {
            var s = 分割后.Trim();
            if (s == "") {
                continue;
            }
            var arr = 文本过滤正则.Split(s);
            res.AddRange(arr);
        }
        return res.ToArray();
    }
    private static string[] 中括号分割(string text) {//完美的[]解决方案
        List<string> textList = new List<string>();
        if (!text.Contains("[") && !text.Contains("]")) {
            return new string[] { text };
        }
        char[] chars = text.ToCharArray();
        bool flag = false;//字符串提取判断
        int count = 0;
        int start = chars.Length;//]下标，
        int end = -1;//[下
        for (int i = 0; i < chars.Length; i++) {
            if (chars[i] == '[') {
                count++;
                if (flag) {
                    end = i;//表示字符串结束了，开始提取
                }
            } else if (chars[i] == ']') {
                count--;
            }
            if (count == 0 && i != chars.Length - 1) {
                //表示该处为正常文本，记录需要提取的真正下标
                if (!flag) {
                    if (i != 0) {
                        start = i + 1;
                    } else {//如果是开头，则从0开始提取
                        start = 0;
                    }
                }
                //告诉上方要开始记录[位置了
                flag = true;
            }
            if (end > -1) {
                //提取
                char[] tempChar = new char[end - start];
                text.CopyTo(start, tempChar, 0, tempChar.Length);
                textList.Add(new string(tempChar));
                end = -1;
                start = chars.Length;//提取完了，归位默认值
                flag = false;//通知提取完了
            } else if (i == chars.Length - 1) {
                char[] tempChar = new char[chars.Length - start];
                text.CopyTo(start, tempChar, 0, tempChar.Length);
                textList.Add(new string(tempChar));
            }
        }
        if (count == 0) {
            return textList.ToArray();
        } else {//前后括号数量不一致
            return new string[0];
        }
    }

}
