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

        private static object 数据库_lock = new object();
        private static object 进度条_lock = new object();

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
        /// 分割数组，满足任意条件则返回
        /// </summary>
        /// <param name="arr">数据源</param>
        /// <param name="单组上限">一组最高的量</param>
        /// <param name="总字符上限">字符总上限，单条就上限报错</param>
        /// <param name="单条字符上限">每条允许的字符数</param>
        /// <returns></returns>
        public static IEnumerable<string[]> 分割数组(string[] arr, int 单组上限 = -1, int 总字符上限 = -1, int 单条字符上限 = -1) {
            if (单条字符上限 <= 0 && 单组上限 <= 0 && 总字符上限 <= 0) {
                throw new Exception("分割数组：请填写正确参数");
            }
            List<string> res = new List<string>();
            int 已存字符数 = 0;
            foreach (string str in arr) {
                if (总字符上限 > 0) {
                    if (str.Length > 总字符上限) {
                        throw new Exception($"单条文本便已达到总字符上限，文本：{str}");
                    }
                    if (已存字符数 + str.Length > 总字符上限) {
                        yield return res.ToArray();
                        res.Clear();
                        已存字符数 = 0;
                    }
                }
                if (单条字符上限 > 0) {
                    if (str.Length > 单条字符上限) {
                        throw new Exception($"单条文本字符超过上限{单条字符上限}，文本：{str}");
                    }
                }
                res.Add(str);
                已存字符数 += str.Length;
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

        public static string[] 文本置换机翻(string[] arr) {
            bool flag = true;
            string[] res = new string[arr.Length];
            for (int i = 0; i < arr.Length; i++) {
                string str = arr[i];
                string n = null;
                foreach (char c in str) {
                    if (flag) {
                        n += "机";
                        flag = false;
                    } else {
                        n += "翻";
                        flag = true;
                    }
                }
                res[i] = n;
                flag = true;
            }
            return res;
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
            string str = null;
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
            str = p.StandardOutput.ReadToEnd();
            p.StandardError.ReadToEnd();
            p.WaitForExit();
            p.Close();
            return str;
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
