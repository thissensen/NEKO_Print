using Newtonsoft.Json;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬 {
    /*
     * 1、Shift+C、V的复制粘贴功能修改部分参数，使其不与正则的[]混淆
     * 2、修复续翻会从头开始的问题
     * 3、修复GPTAPI和火山API的UI显示问题
     * 4、重绘表格，修复部分UI问题
     */
    internal static class Program {

        public static string 软件存储目录 = 全局数据.软件存储目录;
        public static string 数据库路径 {
            get => 全局数据.数据库路径;
            set => 全局数据.数据库路径 = value;
        }
        public static string GPT词表路径 => 全局数据.GPT词表路径;
        //public static readonly string 缓存数据 = 软件存储目录 + "未完成缓存数据.json";

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [STAThread]
        static void Main(string[] args) {
            if (args.Length != 0 && File.Exists(args[0])) {
                数据库路径 = args[0];
            }
            if (Environment.OSVersion.Version.Major >= 6) {
                SetProcessDPIAware();
            }
            if (!File.Exists(数据库路径)) {
                数据库路径.创建父目录();
                File.WriteAllBytes(数据库路径, 翻译姬.Properties.Resources.翻译姬数据库);
            }
            if (!File.Exists(GPT词表路径)) {
                File.WriteAllBytes(GPT词表路径, 翻译姬.Properties.Resources.cl100k_base);
            }
            全局数据.缓存数据路径 = 软件存储目录 + "未完成缓存数据.json";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            try {
                全局数据.数据库 = new SQLite数据库(数据库路径);
            } catch (Exception ex) {
                MessageBoxEx.Show($"数据库异常，翻译姬启动失败:{ex.Message}");
                Environment.Exit(0);
            }
#if DEBUG
            全局数据.数据库 = new SqlServer数据库(".", "翻译姬", "sa", "123456");
#endif
            全局数据.数据初始化();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new 主界面());
        }
    }
}
