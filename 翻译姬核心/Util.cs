using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace 翻译姬 {
    /// <summary>
    /// 引用程序集：
    /// System.IO.Compression
    /// System.IO.Compression.FileSystem
    /// </summary>

    
    public class Util {

        private static 全局设置数据 全局设置数据 => 全局数据.全局设置数据;

        private static object 数据库_lock = new object();
        private static object 进度条_lock = new object();

        private static Dictionary<string, Regex> 语言范围 = new() {
            ["简中"] = new Regex(@"[\u4e00-\u9fa5]"),
            ["日语"] = new Regex(@"[\u0800-\u4e00]"),
            ["英语"] = new Regex(@"[[a-zA-Zａ-ｚＡ-Ｚ]]"),
            ["韩语"] = new Regex(@"[\x3130-\x318F\xAC00-\xD7A3]")
        };

        /// <summary>
        /// 漏翻了就返回true
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool 漏翻检测(string text) {
            string 替换后 = Regex.Replace(text, @"[\p{P}\p{N}\p{M}\p{S}\p{Z}\p{C}]", "");//去符号
            替换后 = 替换后.Replace("ー", "");//日文的常见特殊符号
            if (全局设置数据.目标语言 != "英语") {
                替换后 = Regex.Replace(替换后, @"[a-zA-Zａ-ｚＡ-Ｚ]+", "");
            }
            if (替换后.Length == 0) return false;
            decimal 不符合数量 = 0;
            foreach (var s in 替换后) {
                if (!语言范围[全局设置数据.目标语言].IsMatch(s.ToString())) {
                    不符合数量++;
                }
            }
            return 不符合数量 / 替换后.Length > 0.3m;
        }


        public static void 多线程数据库Exec(string sql) {
            lock (数据库_lock) {
                全局数据.数据库.Execute(sql);
            }
        }
        public delegate void 多线程机翻完成行数(int 增加值);
        public static event 多线程机翻完成行数 机翻完成行数;
        public static void 多线程进度条增加(int 增加值) {
            lock (进度条_lock) {
                机翻完成行数?.Invoke(增加值);
            }
        }

        public static string 文本编码识别(string path) {
            List<string> 编码列表 = new List<string>();
            编码列表.Add("UTF-8");
            编码列表.Add("UTF-16LE");
            编码列表.Add("Shift-JIS");
            编码列表.Add("GBK");
            List<char> 乱码字符表 = new List<char>();
            乱码字符表.AddRange(new char[] { '�', '?', '丄', '丅', '亀', '亁', '碁', '瞁', '†', 'ൻ', '・' });
            DataTable dt = new DataTable();
            dt.Columns.Add("编码");
            dt.Columns.Add("乱码次数", typeof(int));
            dt.Columns.Add("文本长度", typeof(int));
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)) {
                byte[] buf = new byte[4096];
                int num = fs.Read(buf, 0, buf.Length);
                foreach (string 编码 in 编码列表) {
                    string text = Encoding.GetEncoding(编码).GetString(buf, 0, num);
                    int 乱码次数 = (from char c in text
                                where 乱码字符表.Contains(c)
                                select c).Count();
                    //韩文+装饰符号+杂项符号
                    乱码次数 += Regex.Matches(text, @"[\uAC00-\uD7AF\u1100-\u11FF\u3130-\u318F\u2600-\u27BF]").Count;
                    DataRow row = dt.NewRow();
                    row["编码"] = 编码;
                    row["乱码次数"] = 乱码次数;
                    row["文本长度"] = text.Length;
                    dt.Rows.Add(row);
                }
                return (from row in dt.AsEnumerable()
                               orderby int.Parse(row["乱码次数"].ToString()), int.Parse(row["文本长度"].ToString())
                               select row["编码"].ToString()).First();
            }
        }

        

        /// <summary>
        /// 分割原文，满足任意条件则返回
        /// </summary>
        /// <param name="arr">数据源</param>
        /// <param name="单组上限">一组最高的量</param>
        /// <param name="总字符上限">字符总上限，单条就上限报错</param>
        /// <param name="单条字符上限">每条允许的字符数</param>
        /// <returns></returns>
        public static IEnumerable<文本[]> 分割数组(文本[] arr, int 单组上限 = -1, int 总字符上限 = -1, int 单条字符上限 = -1, bool 百度模式 = false) {
            if (单条字符上限 <= 0 && 单组上限 <= 0 && 总字符上限 <= 0) {
                throw new Exception($"分割数组：请填写正确参数");
            }
            var res = new List<文本>();
            int 已存字符数 = 0;
            foreach (文本 文本 in arr) {
                string str = 文本.原文;
                int len = str.Length;
                if (百度模式) {
                    if (string.IsNullOrEmpty(str.Trim())) {
                        len = 1;
                    }
                    len += 1;
                }
                if (总字符上限 > 0) {
                    if (len > 总字符上限) {
                        throw new Exception($"单条文本便已达到总字符上限，文本：{str}");
                    }
                    if (已存字符数 + len > 总字符上限) {
                        yield return res.ToArray();
                        res.Clear();
                        已存字符数 = 0;
                    }
                }
                if (单条字符上限 > 0) {
                    if (len > 单条字符上限) {
                        throw new Exception($"单条文本字符超过上限{单条字符上限}，文本：{str}");
                    }
                }
                res.Add(文本);
                已存字符数 += len;
                if (单组上限 > 0) {
                    if (res.Count == 单组上限) {
                        yield return res.ToArray();
                        res.Clear();
                        已存字符数 = 0;
                    }
                }
            }
            if (res.Count > 0) {//返回剩余的
                yield return res.ToArray();
            }
        }

        public static string[] 正则分割(string text) {
            var res = new List<string>();
            var 已读字符 = new StringBuilder();
            for (int i = 0; i < text.Length; i++) {
                char c = text[i];
                已读字符.Append(c);
                if (c != '|') {
                    continue;
                }
                //判断|是否被转义
                if (转义判断(text, i)) {
                    //被转义了无视
                    continue;
                }
                //判断是否被()包裹
                if (!括号判断(text, i)) {
                    if (已读字符.Length > 0) {
                        已读字符.Remove(已读字符.Length - 1, 1);
                    }
                    res.Add(已读字符.ToString());
                    已读字符.Clear();
                }
            }
            if (已读字符.Length > 0) {
                res.Add(已读字符.ToString());
            }
            return res.ToArray();
        }
        //判断某字符是否被转义，隐患：判断转义符号本身时
        private static bool 转义判断(string text, int 字符下标) {
            // \ 符号是否被
            var 转义符数量 = 0;
            for (int i = 字符下标 - 1; i > -1; i--) {
                if (text[i] == '\\') {
                    转义符数量++;
                } else {
                    break;
                }
            }
            if (转义符数量 % 2 == 1) {
                return true;
            } else {
                return false;
            }
        }
        //判断该字符是否被未被转义括号包裹
        private static bool 括号判断(string text, int 字符下标) {
            //前括号判断
            var 包含前括号 = false;
            for (int i = 字符下标 - 1; i > -1; i--) {
                if (text[i] == ')' && !转义判断(text, i)) {
                    break;
                }
                if (text[i] == '(' && !转义判断(text, i)) {
                    包含前括号 = true;
                    break;
                }
            }
            if (!包含前括号) {
                return false;
            }
            //后括号判断
            var 包含后括号 = false;
            for (int i = 字符下标; i < text.Length; i++) {
                if (text[i] == '(' && !转义判断(text, i)) {
                    break;
                }
                if (text[i] == ')' && !转义判断(text, i)) {
                    包含后括号 = true;
                    break;
                }
            }
            if (!包含后括号) {
                return false;
            }
            return true;
        }

        public static 文本[] 文本置换机翻(文本[] arr) {
            bool flag = true;
            文本[] res = new 文本[arr.Length];
            for (int i = 0; i < arr.Length; i++) {
                if (arr[i].文本类型 == 文本类型.人名) {
                    res[i] = new 文本(0, arr[i].原文) { 译文 = $"name:" + arr[i].原文 };
                    continue;
                }
                string str = arr[i].原文;
                string n = "";
                foreach (char c in str) {
                    if (flag) {
                        n += "机";
                        flag = false;
                    } else {
                        n += "翻";
                        flag = true;
                    }
                }
                res[i] = new 文本(0, str) { 译文 = n };
                flag = true;
            }
            return res;
        }

        public static string 换行符修正(string src) {
            var reg_n = new Regex(@"(?<!\r)\n");
            if (reg_n.IsMatch(src)) {
                src = reg_n.Replace(src, "\r\n");
            }
            var reg_r = new Regex(@"\r(?!\n)");
            if (reg_r.IsMatch(src)) {
                src = reg_r.Replace(src, "\r\n");
            }
            src = Regex.Replace(src, @"\\*\r\\*\n", @"\r\n");
            src = Regex.Replace(src, @"\\+r", @"\r");
            src = Regex.Replace(src, @"\\+n", @"\n");
            return src;
        }

        /// <summary>
        /// 防止文件存在，后面加(1) (2)之类
        /// </summary>
        public static string 获取不重复路径(string path, int 已存在个数 = 0) {
            FileInfo info = new FileInfo(path);
            if (!info.Exists) {
                return path;
            }
            已存在个数++;//已存在该文件
            if (已存在个数 == 1) {//初次后面加
                path = info.FullName.Substring(0, info.FullName.Length - info.Extension.Length) + $" ({已存在个数}){info.Extension}";
            } else {//比如即存在原来的又存在(1)，把1改成2
                path = info.FullName.Substring(0, info.FullName.Length - info.Extension.Length - (已存在个数 - 1).ToString().Length - 3) + $" ({已存在个数}){info.Extension}";
            }
            if (!File.Exists(path)) {
                return path;
            }
            return 获取不重复路径(path, 已存在个数);
        }
    }


}
