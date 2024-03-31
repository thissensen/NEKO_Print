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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬 {
    /// <summary>
    /// 引用程序集：
    /// System.IO.Compression
    /// System.IO.Compression.FileSystem
    /// </summary>
    public class 工具类 {

        private static 全局设置数据 全局设置数据 => 全局数据.全局设置数据;
        private static GPT设置数据 GPT设置数据 => 全局数据.GPT设置数据;

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

        public static void 多线程进度条增加(int 增加值) {
            lock (进度条_lock) {
                数据中转.进度条当前值增加(增加值);
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

        public static List<Src_Dst请求> 文本转请求(文本[] 文本arr) {
            var 下标分组 = from 文本 in 文本arr
                       where 文本.文本类型 != 文本类型.人名
                       group 文本 by 文本.文本下标 into g
                       select g;
            var res = new List<Src_Dst请求>();
            int 请求id = 0, 最后合并id = -1;
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
                var gpt请求 = new Src_Dst请求();
                gpt请求.id = 请求id++;
                gpt请求.name = 人名文本?.原文;
                if (GPT设置数据.连续对话合并) {
                    var 原文组 = 下标分组.Single(t => t.Key == 文本.文本下标).Select(t => t.原文);
                    gpt请求.src = string.Join(GPT设置数据.合并分隔符, 原文组);
                    最后合并id = 文本.文本下标;
                } else {
                    gpt请求.src = 文本.原文;
                }
                res.Add(gpt请求);
                人名文本 = null;
            }
            return res;
        }

        public static void 请求写入文本(文本[] 文本arr, List<Src_Dst请求> GPT请求list, bool 返回值换行符修正) {
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
                var gpt请求 = GPT请求list[请求id++];
                if (GPT设置数据.连续对话合并) {
                    var 原文组 = 下标分组.Single(t => t.Key == 文本.文本下标).Select(t => t).ToArray();
                    string 请求结果 = gpt请求.dst;
                    if (返回值换行符修正) {
                        请求结果 = 换行符修正(请求结果);
                    }
                    string[] gpt译文 = Regex.Split(请求结果, Regex.Escape(GPT设置数据.合并分隔符));
                    int j;
                    for (j = 0; j < 原文组.Length; j++) {
                        string val = gpt译文.ElementAtOrDefault(j) ?? "";
                        原文组[j].译文 = val;
                        if (gpt请求.是否漏翻) {
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
                    文本.译文 = gpt请求.dst;
                    if (gpt请求.是否漏翻) {
                        文本.异常状态 = 文本异常状态.存在漏翻;
                    }
                }
            }
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

        public static void 异步调用(string exe名, params string[] 参数) {
            异步调用(exe名, false, 参数);
        }
        public static void 异步调用(string exe名, bool 是否显示窗口 = false, params string[] 参数) {
            Process p = new Process();
            p.StartInfo.FileName = exe名;
            p.StartInfo.Arguments = string.Join(" ", 参数);
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = !是否显示窗口;
            p.Start();
            p.Close();
        }
        public static void 同步调用(string exe名, params string[] 参数) {
            同步调用(exe名, false, 参数);
        }
        public static void 同步调用(string exe名, bool 是否显示窗口 = false, params string[] 参数) {
            Process p = new Process();
            p.StartInfo.FileName = exe名;
            p.StartInfo.Arguments = string.Join(" ", 参数);
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = !是否显示窗口;
            p.Start();
            p.WaitForExit();
            p.Close();
        }
        public static string CMD同步调用(params string[] 参数) {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.StandardOutputEncoding = Encoding.Default;
            p.Start();
            p.StandardInput.WriteLine(string.Join(" ", 参数));
            p.StandardInput.AutoFlush = true;
            p.StandardInput.WriteLine("exit");
            string str = p.StandardOutput.ReadToEnd();
            p.StandardError.ReadToEnd();
            p.WaitForExit();
            p.Close();
            return str;
        }
        public static void CMD异步调用(params string[] 参数) {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.StandardOutputEncoding = Encoding.Default;
            p.Start();
            p.StandardInput.WriteLine(string.Join(" ", 参数));
            p.StandardInput.AutoFlush = true;
            p.StandardInput.WriteLine("exit");
            string str = p.StandardOutput.ReadToEnd();
            p.StandardError.ReadToEnd();
            p.Close();
        }

        /// <param name="标题">标题栏显示的标题</param>
        /// <param name="选择前缀">选择按钮上方显示的名字</param>
        /// <param name="filter">过滤的后缀，使用;分隔进行多选</param>
        /// <param name="起始目录">起始显示的目录</param>
        /// <param name="是否多选">是否允许多选</param>
        /// <returns></returns>
        public static string[] 选择文件(string 标题 = null, string 选择前缀 = "文件", string filter = null, string 起始目录 = null, bool 是否多选 = false) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = 标题 == null ? "请选择文件" : 标题;
            if (起始目录 != null) {
                dialog.InitialDirectory = 起始目录;
            }
            dialog.Filter = filter == null ? 选择前缀 + "|*.*" : 选择前缀 + $"|{filter}";
            dialog.Multiselect = 是否多选;
            dialog.ShowDialog();
            return dialog.FileNames;
        }
        /// <returns>没选返回""</returns>
        public static string[] 选择文件夹(string 标题 = null, string 起始目录 = null, bool 是否多选 = false) {
            FolderSelectDialog dialog = new FolderSelectDialog();
            dialog.Title = 标题 == null ? "请选择文件夹" : 标题;
            if (起始目录 != null) {
                dialog.InitialDirectory = 起始目录;
            }
            dialog.Multiselect = 是否多选;
            if (dialog.ShowDialog()) {
                return dialog.FileNames;
            } else {
                return new string[0];
            }
        }
        public static string 选择保存目录(string 标题 = null, string 起始目录 = null, string 文件前缀 = "文件", string filter = null) {
            var info = new SaveFileDialog();
            info.Title = 标题;
            info.Filter = filter == null ? 文件前缀 + "|*.*" : 文件前缀 + $"|{filter}";
            info.AddExtension = true;
            info.AutoUpgradeEnabled = true;
            info.FileName = 起始目录;
            var r = info.ShowDialog();
            if (r != DialogResult.OK) {
                return null;
            }
            return info.FileName;
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


    #region 文件夹选择器
    /// <summary>
    /// Wraps System.Windows.Forms.OpenFileDialog to make it present
    /// a vista-style dialog.
    /// </summary>
    public class FolderSelectDialog {
        // Wrapped dialog
        System.Windows.Forms.OpenFileDialog ofd = null;

        /// <summary>
        /// Default constructor
        /// </summary>
        public FolderSelectDialog() {
            ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.AddExtension = false;
            ofd.CheckFileExists = false;
        }

        #region Properties

        /// <summary>
        /// Gets/Sets the initial folder to be selected. A null value selects the current directory.
        /// </summary>
        public string InitialDirectory {
            get { return ofd.InitialDirectory; }
            set { ofd.InitialDirectory = value == null || value.Length == 0 ? Environment.CurrentDirectory : value; }
        }

        /// <summary>
        /// Gets/Sets the title to show in the dialog
        /// </summary>
        public string Title {
            get { return ofd.Title; }
            set { ofd.Title = value == null ? "请选择文件夹" : value; }
        }

        /// <summary>
        /// Gets the selected folder
        /// </summary>
        public string FileName {
            get { return ofd.FileName; }
        }

        public bool Multiselect {
            get { return ofd.Multiselect; }
            set { ofd.Multiselect = value; }
        }

        public string[] FileNames {
            get { return ofd.FileNames; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Shows the dialog
        /// </summary>
        /// <returns>True if the user presses OK else false</returns>
        public bool ShowDialog() {
            return ShowDialog(IntPtr.Zero);
        }

        /// <summary>
        /// Shows the dialog
        /// </summary>
        /// <param name="hWndOwner">Handle of the control to be parent</param>
        /// <returns>True if the user presses OK else false</returns>
        public bool ShowDialog(IntPtr hWndOwner) {
            bool flag = false;

            if (Environment.OSVersion.Version.Major >= 6) {
                var r = new Reflector("System.Windows.Forms");

                uint num = 0;
                Type typeIFileDialog = r.GetType("FileDialogNative.IFileDialog");
                object dialog = r.Call(ofd, "CreateVistaDialog");
                r.Call(ofd, "OnBeforeVistaDialog", dialog);

                uint options = (uint)r.CallAs(typeof(System.Windows.Forms.FileDialog), ofd, "GetOptions");
                options |= (uint)r.GetEnum("FileDialogNative.FOS", "FOS_PICKFOLDERS");
                r.CallAs(typeIFileDialog, dialog, "SetOptions", options);

                object pfde = r.New("FileDialog.VistaDialogEvents", ofd);
                object[] parameters = new object[] { pfde, num };
                r.CallAs2(typeIFileDialog, dialog, "Advise", parameters);
                num = (uint)parameters[1];
                try {
                    int num2 = (int)r.CallAs(typeIFileDialog, dialog, "Show", hWndOwner);
                    flag = 0 == num2;
                } finally {
                    r.CallAs(typeIFileDialog, dialog, "Unadvise", num);
                    GC.KeepAlive(pfde);
                }
            } else {
                var fbd = new FolderBrowserDialog();
                fbd.Description = this.Title;
                fbd.SelectedPath = this.InitialDirectory;
                fbd.ShowNewFolderButton = false;
                if (fbd.ShowDialog(new WindowWrapper(hWndOwner)) != DialogResult.OK) return false;
                ofd.FileName = fbd.SelectedPath;
                flag = true;
            }

            return flag;
        }

        #endregion
    }
    /// <summary>
    /// Creates IWin32Window around an IntPtr
    /// </summary>
    public class WindowWrapper : System.Windows.Forms.IWin32Window {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="handle">Handle to wrap</param>
        public WindowWrapper(IntPtr handle) {
            _hwnd = handle;
        }

        /// <summary>
        /// Original ptr
        /// </summary>
        public IntPtr Handle {
            get { return _hwnd; }
        }

        private IntPtr _hwnd;
    }
    /// <summary>
    /// This class is from the Front-End for Dosbox and is used to present a 'vista' dialog box to select folders.
    /// Being able to use a vista style dialog box to select folders is much better then using the shell folder browser.
    /// http://code.google.com/p/fed/
    ///
    /// Example:
    /// var r = new Reflector("System.Windows.Forms");
    /// </summary>
    public class Reflector {
        #region variables

        string m_ns;
        Assembly m_asmb;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ns">The namespace containing types to be used</param>
        public Reflector(string ns)
            : this(ns, ns) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="an">A specific assembly name (used if the assembly name does not tie exactly with the namespace)</param>
        /// <param name="ns">The namespace containing types to be used</param>
        public Reflector(string an, string ns) {
            m_ns = ns;
            m_asmb = null;
            foreach (AssemblyName aN in Assembly.GetExecutingAssembly().GetReferencedAssemblies()) {
                if (aN.FullName.StartsWith(an)) {
                    m_asmb = Assembly.Load(aN);
                    break;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Return a Type instance for a type 'typeName'
        /// </summary>
        /// <param name="typeName">The name of the type</param>
        /// <returns>A type instance</returns>
        public Type GetType(string typeName) {
            Type type = null;
            string[] names = typeName.Split('.');

            if (names.Length > 0)
                type = m_asmb.GetType(m_ns + "." + names[0]);

            for (int i = 1; i < names.Length; ++i) {
                type = type.GetNestedType(names[i], BindingFlags.NonPublic);
            }
            return type;
        }

        /// <summary>
        /// Create a new object of a named type passing along any params
        /// </summary>
        /// <param name="name">The name of the type to create</param>
        /// <param name="parameters"></param>
        /// <returns>An instantiated type</returns>
        public object New(string name, params object[] parameters) {
            Type type = GetType(name);

            ConstructorInfo[] ctorInfos = type.GetConstructors();
            foreach (ConstructorInfo ci in ctorInfos) {
                try {
                    return ci.Invoke(parameters);
                } catch { }
            }

            return null;
        }

        /// <summary>
        /// Calls method 'func' on object 'obj' passing parameters 'parameters'
        /// </summary>
        /// <param name="obj">The object on which to excute function 'func'</param>
        /// <param name="func">The function to execute</param>
        /// <param name="parameters">The parameters to pass to function 'func'</param>
        /// <returns>The result of the function invocation</returns>
        public object Call(object obj, string func, params object[] parameters) {
            return Call2(obj, func, parameters);
        }

        /// <summary>
        /// Calls method 'func' on object 'obj' passing parameters 'parameters'
        /// </summary>
        /// <param name="obj">The object on which to excute function 'func'</param>
        /// <param name="func">The function to execute</param>
        /// <param name="parameters">The parameters to pass to function 'func'</param>
        /// <returns>The result of the function invocation</returns>
        public object Call2(object obj, string func, object[] parameters) {
            return CallAs2(obj.GetType(), obj, func, parameters);
        }

        /// <summary>
        /// Calls method 'func' on object 'obj' which is of type 'type' passing parameters 'parameters'
        /// </summary>
        /// <param name="type">The type of 'obj'</param>
        /// <param name="obj">The object on which to excute function 'func'</param>
        /// <param name="func">The function to execute</param>
        /// <param name="parameters">The parameters to pass to function 'func'</param>
        /// <returns>The result of the function invocation</returns>
        public object CallAs(Type type, object obj, string func, params object[] parameters) {
            return CallAs2(type, obj, func, parameters);
        }

        /// <summary>
        /// Calls method 'func' on object 'obj' which is of type 'type' passing parameters 'parameters'
        /// </summary>
        /// <param name="type">The type of 'obj'</param>
        /// <param name="obj">The object on which to excute function 'func'</param>
        /// <param name="func">The function to execute</param>
        /// <param name="parameters">The parameters to pass to function 'func'</param>
        /// <returns>The result of the function invocation</returns>
        public object CallAs2(Type type, object obj, string func, object[] parameters) {
            MethodInfo methInfo = type.GetMethod(func, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return methInfo.Invoke(obj, parameters);
        }

        /// <summary>
        /// Returns the value of property 'prop' of object 'obj'
        /// </summary>
        /// <param name="obj">The object containing 'prop'</param>
        /// <param name="prop">The property name</param>
        /// <returns>The property value</returns>
        public object Get(object obj, string prop) {
            return GetAs(obj.GetType(), obj, prop);
        }

        /// <summary>
        /// Returns the value of property 'prop' of object 'obj' which has type 'type'
        /// </summary>
        /// <param name="type">The type of 'obj'</param>
        /// <param name="obj">The object containing 'prop'</param>
        /// <param name="prop">The property name</param>
        /// <returns>The property value</returns>
        public object GetAs(Type type, object obj, string prop) {
            PropertyInfo propInfo = type.GetProperty(prop, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return propInfo.GetValue(obj, null);
        }

        /// <summary>
        /// Returns an enum value
        /// </summary>
        /// <param name="typeName">The name of enum type</param>
        /// <param name="name">The name of the value</param>
        /// <returns>The enum value</returns>
        public object GetEnum(string typeName, string name) {
            Type type = GetType(typeName);
            FieldInfo fieldInfo = type.GetField(name);
            return fieldInfo.GetValue(null);
        }

        #endregion

    }
    #endregion
}
