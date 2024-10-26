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
    /*
     * 1、更新内置的Ren'py正则(可前往wiki查看)
     * 2、修复正则在特殊文本下提取到空人名、空对话的情况
     * 3、连续人名且人名在文本最后方这种极端情况下，GPT无法翻译问题
     * 4、读取编码若设为自动，将以第一个识别到的编码为标准读取剩余的文本(原效果：每个文本单独判断编码)
     * 5、GPT新添功能[输出人名优先词汇表]：GPT机翻完毕后，输出人名优先为词汇表中人名
     * 6、修复机翻结束机翻接口是灰色的BUG
     * 7、优化GPT机翻后合并文本的分割问题
     */
    internal static class Program {

        public static readonly string 软件存储目录 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\翻译姬\";
        public static string 数据库路径 = 软件存储目录 + "翻译姬数据库.db";
        //public static readonly string GPT词表路径 = 软件存储目录 + "cl100k_base.tiktoken";
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
                File.WriteAllBytes(数据库路径, Resources.翻译姬数据库);
            }
            /*if (!File.Exists(GPT词表路径)) {
                File.WriteAllBytes(GPT词表路径, Resources.cl100k_base);
            }*/
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
