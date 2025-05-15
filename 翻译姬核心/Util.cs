using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace 翻译姬 {
    public class Util {

        private static 全局设置数据 全局设置数据 => 全局数据.全局设置数据;
        private static GPT设置数据 GPT设置数据 => 全局数据.GPT设置数据;

        private static Dictionary<string, Regex> 语言范围 = new() {
            ["简中"] = new Regex(@"[\u4e00-\u9fa5]"),
            ["日语"] = new Regex(@"[\u0800-\u4e00]"),
            ["英语"] = new Regex(@"[[a-zA-Zａ-ｚＡ-Ｚ]]"),
            ["韩语"] = new Regex(@"[\x3130-\x318F\xAC00-\xD7A3]"),
            ["俄语"] = new Regex(@"[а-яА-Я]")
        };
        /// <summary>
        /// 漏翻了就返回true
        /// </summary>
        public static bool 漏翻检测(string text) {
            try {
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
            } catch (Exception ex) {
                throw new Exception($"漏翻检测BUG：{ex.Message}");
            }
        }

        private static object 数据库_lock = new object();
        public static void 多线程数据库Exec(string sql) {
            lock (数据库_lock) {
                全局数据.数据库.Execute(sql);
            }
        }

        private static object 进度条_lock = new object();
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

        public static DataTable 词汇表读取(string path) {
            using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, false);
            using StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            string t = sr.ReadLine();
            var dt = new DataTable();
            dt.Columns.Add("原文");
            dt.Columns.Add("译文");
            dt.Columns.Add("备注");
            while ((t = sr.ReadLine()) != null) {
                if (t.Trim() == "") {
                    continue;
                }
                var arr = t.Split('\t');
                if (arr.Length < 2) {
                    throw new Exception("csv格式异常，请从[数据处理]界面导出后修改再导入");
                }
                if (arr[0].ToString().Length == 0) {
                    throw new Exception("原文不能为空");
                }
                var dr = dt.NewRow();
                dr["原文"] = arr[0];
                dr["译文"] = arr[1];
                dr["备注"] = string.Join("\t", arr.Skip(2));
                dt.Rows.Add(dr);
            }
            return dt;
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

        public static List<KeyValue<string, string>> 文本提取对话(文本[] 文本arr) {
            //文本下标 - 下标对应的文本组
            var 下标分组 = (from 文本 in 文本arr
                        where 文本.文本类型 != 文本类型.人名
                        group 文本 by 文本.文本下标 into g
                        select g).ToDictionary(t => t.Key, t => t);
            var res = new List<KeyValue<string, string>>();
            int 上级文本下标 = -2;
            文本 人名文本 = null;
            for (int i = 0; i < 文本arr.Length; i++) {
                var 文本 = 文本arr[i];
                if (文本.文本类型 == 文本类型.人名) {
                    人名文本 = 文本;
                    continue;
                }
                if (GPT设置数据.连续对话合并 && 文本.文本下标 == 上级文本下标) {
                    //不进行左右重复合并保存
                    continue;
                }
                if (GPT设置数据.连续对话合并) {
                    if (GPT设置数据.相邻对话合并 && 文本.文本下标 == 上级文本下标 + 1) {
                        //连续对话，当前内容合并到上级
                        var 最后kv = res.Last();
                        最后kv.Value += GPT设置数据.合并分隔符 + 文本.原文;
                        res[res.Count - 1] = 最后kv;
                        上级文本下标 = 文本.文本下标;
                        continue;
                    }
                }
                //非连续，正常新添
                var kv = new KeyValue<string, string>();
                kv.Key = 人名文本?.原文;
                if (GPT设置数据.连续对话合并) {
                    var 原文组 = 下标分组[文本.文本下标].Select(t => t.原文);
                    kv.Value = string.Join(GPT设置数据.合并分隔符, 原文组);
                } else {
                    kv.Value = 文本.原文;
                }
                上级文本下标 = 文本.文本下标;
                res.Add(kv);
                人名文本 = null;
            }
            return res;
        }

        public static void 对话写入文本(文本[] 文本arr, List<KeyValue<string, string>> 对话list, bool 返回值换行符修正) {
            try {
                var 下标分组 = (from 文本 in 文本arr
                            where 文本.文本类型 != 文本类型.人名
                            group 文本 by 文本.文本下标 into g
                            select g).ToDictionary(t => t.Key, t => t);
                int 请求id = 0, 上级文本下标 = -2;
                文本 最后文本 = null;
                var 读取中结果 = new Queue<string>();
                for (int i = 0; i < 文本arr.Length; i++) {
                    var 文本 = 文本arr[i];
                    if (文本.文本类型 == 文本类型.人名) {
                        if (全局数据.GPT设置数据.输出人名优先词汇表) {
                            //GPT从GPT词汇表尝试读取
                            var 词汇表row = 全局数据.GPT设置数据.GPT词汇表.AsEnumerable().Where(r => r["原文"].ToString() == 文本.原文).LastOrDefault();
                            if (词汇表row != null) {
                                文本.译文 = 词汇表row["译文"].ToString();
                            }
                        }
                        if (文本.译文.IsNullOrEmpty()) {
                            文本.译文 = 文本.原文;
                        }
                        continue;
                    }
                    if (GPT设置数据.连续对话合并 && 文本.文本下标 == 上级文本下标) {
                        //不进行左右重复合并保存
                        continue;
                    }
                    if (GPT设置数据.连续对话合并) {
                        if (GPT设置数据.相邻对话合并 && 文本.文本下标 == 上级文本下标 + 1) {
                            //连续对话，当前内容合并到上级
                            var 原文组 = 下标分组[文本.文本下标];
                            var 上级文本 = 下标分组[上级文本下标].Last();
                            文本 临时最后文本 = null;
                            foreach (var 原文文本 in 原文组) {
                                if (读取中结果.Count > 0) {
                                    原文文本.译文 = 读取中结果.Dequeue();
                                    原文文本.异常状态 = 上级文本.异常状态;
                                    临时最后文本 = 原文文本;
                                }
                            }
                            上级文本下标 = 文本.文本下标;
                            continue;
                        }
                    }
                    //非连续，正常读取赋值
                    var kv = 对话list[请求id++];
                    bool 是否漏翻 = (bool)kv.Tag;
                    if (GPT设置数据.连续对话合并) {//从左往右，从上往下，取值赋值
                        var 原文组 = 下标分组[文本.文本下标];
                        string 请求结果 = kv.Value;
                        if (返回值换行符修正) {
                            请求结果 = 换行符修正(请求结果);
                        }
                        //判断多换行符
                        if (读取中结果.Count > 0) {
                            if (最后文本 != null) {
                                最后文本.异常状态 = 文本异常状态.多换行符;
                                while (读取中结果.Count != 0) {
                                    最后文本.译文 += 读取中结果.Dequeue();//剩余的拼接在最后的文本中
                                }
                            } else {
                                throw new Exception("算法异常，最后文本为null");
                            }
                            读取中结果.Clear();
                        }
                        //a-b  -  c-d
                        string[] gpt译文 = Regex.Split(请求结果, Regex.Escape(GPT设置数据.合并分隔符));
                        foreach (var 译文 in gpt译文) {
                            读取中结果.Enqueue(译文);
                        }
                        foreach (var 原文文本 in 原文组) {
                            if (读取中结果.Count > 0) {
                                原文文本.译文 = 读取中结果.Dequeue();
                                if (是否漏翻) {
                                    原文文本.异常状态 = 文本异常状态.存在漏翻;
                                }
                            }
                            最后文本 = 原文文本;
                        }
                    } else {
                        文本.译文 = kv.Value;
                        if (是否漏翻) {
                            文本.异常状态 = 文本异常状态.存在漏翻;
                        }
                        最后文本 = 文本;
                    }
                    上级文本下标 = 文本.文本下标;
                }
            } catch (Exception ex) {
                throw new Exception($"对话写入文本错误：{ex.Message}");
            }
        }

        //废弃的原算法
        /*public static List<KeyValue<string, string>> 文本提取对话3(文本[] 文本arr) {
            //文本下标 - 下标对应的文本组
            //目前是合并左右对话组，上下也要合并
            var 下标分组 = (from 文本 in 文本arr
                        where 文本.文本类型 != 文本类型.人名
                        group 文本 by 文本.文本下标 into g
                        select g).ToDictionary(t => t.Key, t => t);
            var res = new List<KeyValue<string, string>>();
            int 最后合并id = -1;
            文本 人名文本 = null;
            for (int i = 0; i < 文本arr.Length; i++) {
                var 文本 = 文本arr[i];
                if (文本.文本类型 == 文本类型.人名) {
                    人名文本 = 文本;
                    continue;
                }
                if (GPT设置数据.连续对话合并 && 文本.文本下标 == 最后合并id) {
                    //不进行重复合并保存
                    continue;
                }
                var kv = new KeyValue<string, string>();
                kv.Key = 人名文本?.原文;
                if (GPT设置数据.连续对话合并) {
                    var 原文组 = 下标分组[文本.文本下标];
                    kv.Value = string.Join(GPT设置数据.合并分隔符, 原文组);
                    最后合并id = 文本.文本下标;
                } else {
                    kv.Value = 文本.原文;
                }
                res.Add(kv);
                人名文本 = null;
            }
            return res;
        }


        public static void 对话写入文本3(文本[] 文本arr, List<KeyValue<string, string>> 对话list, bool 返回值换行符修正) {
            var 下标分组 = from 文本 in 文本arr
                       where 文本.文本类型 != 文本类型.人名
                       group 文本 by 文本.文本下标 into g
                       select g;
            int 请求id = 0, 最后合并id = -1;
            for (int i = 0; i < 文本arr.Length; i++) {
                var 文本 = 文本arr[i];
                if (文本.文本类型 == 文本类型.人名) {
                    continue;
                }
                if (GPT设置数据.连续对话合并 && 文本.文本下标 == 最后合并id) {
                    //不进行重复合并保存
                    continue;
                }
                var kv = 对话list[请求id++];
                bool 是否漏翻 = (bool)kv.Tag;
                if (GPT设置数据.连续对话合并) {
                    var 原文组 = 下标分组.Single(t => t.Key == 文本.文本下标).Select(t => t).ToArray();
                    string 请求结果 = kv.Value;
                    if (返回值换行符修正) {
                        请求结果 = 换行符修正(请求结果);
                    }
                    string[] gpt译文 = Regex.Split(请求结果, Regex.Escape(GPT设置数据.合并分隔符));
                    int j;
                    for (j = 0; j < 原文组.Length; j++) {
                        string val = gpt译文.ElementAtOrDefault(j) ?? "";
                        原文组[j].译文 = val;
                        if (是否漏翻) {
                            原文组[j].异常状态 = 文本异常状态.存在漏翻;
                        }
                    }
                    if (j < gpt译文.Length) {//没问题就是等于
                                           //gpt多换行符                        
                        原文组.Last().译文 += string.Join("", gpt译文.Skip(j));
                        原文组.Last().异常状态 = 文本异常状态.多换行符;
                    }

                    最后合并id = 文本.文本下标;
                } else {
                    文本.译文 = kv.Value;
                    if (是否漏翻) {
                        文本.异常状态 = 文本异常状态.存在漏翻;
                    }
                }
            }
        }
*/
        private static Regex 换行符_n = new Regex(@"(?<!\\r)\\n");
        private static Regex 换行符_r = new Regex(@"\\r(?!\\n)");
        private static string 换行符修正(string src) {
            try {
                src = Regex.Replace(src, @"\\*\r\\*\n", @"\r\n");
                src = Regex.Replace(src, @"\\+r", @"\r");
                src = Regex.Replace(src, @"\\+n", @"\n");
                if (换行符_n.IsMatch(src)) {
                    src = 换行符_n.Replace(src, @"\r\n");
                }
                if (换行符_r.IsMatch(src)) {
                    src = 换行符_r.Replace(src, @"\r\n");
                }
                return src;
            } catch (Exception ex) {
                throw new Exception($"换行符修正错误：{ex.Message}");
            }
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
