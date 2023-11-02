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
using 翻译姬.Properties;

namespace 翻译姬 {
    internal static class Program {

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
        private static readonly string 软件存储目录 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\翻译姬\";
        private static readonly string 数据库路径 = 软件存储目录 + "翻译姬数据库.db";

        [STAThread]
        static void Main() {
            if (Environment.OSVersion.Version.Major >= 6) {
                SetProcessDPIAware();
            }
            if (!File.Exists(数据库路径)) {
                数据库路径.创建父目录();
                File.WriteAllBytes(数据库路径, Resources.翻译姬数据库);
            }
            全局数据.数据库 = new SQLite数据库(数据库路径);
            全局数据.数据初始化();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new 主界面());
        }
    }
}
