using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬 {
    public class Start {

        static Start() {

#if !DEBUG
            //设置应用程序处理异常方式：ThreadException处理
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
#endif
        }
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e) => MessageBoxEx.Show(GetExceptionMsg(e.Exception, e.ToString()));
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) => MessageBoxEx.Show(GetExceptionMsg(e.ExceptionObject as Exception, e.ToString()));
        private static string GetExceptionMsg(Exception ex, string backStr) {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("系统异常，请联系开发者");
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null) {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            } else {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }


        public static 数据库连接 数据库 {
            get {
                return 全局数据.数据库;
            }
            set {
                全局数据.数据库 = value;
            }
        }

        /// <summary>
        /// 登录界面窗体
        /// </summary>
        public static Form 登录窗体 { get; set; }

        /// <summary>
        /// 主界面窗体
        /// </summary>
        public static Form 主界面窗体 { get; set; }

        /// <summary>
        /// true：则禁止重复启动窗体
        /// </summary>
        public static bool 是否禁止重复启动 = false;

        /// <summary>
        /// 开启后会强制出现管理员相关界面，可设置账号密码窗体等
        /// </summary>
        public static bool 是否开启默认管理员窗体 = false;

        public static void 启动窗体() {
            //防止重复启动
            if (是否禁止重复启动) {
                //获取欲启动进程名
                string strProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                //检查进程是否已经启动，已经启动则显示报错信息退出程序。 
                if (!全局字符串.允许重复启动 && System.Diagnostics.Process.GetProcessesByName(strProcessName).Length > 1) {
                    MessageBoxEx.Show("客户端已经运行，请勿重复启动！");
                    Environment.Exit(0);
                    return;
                }
            }
            //必要设置判断
            if (主界面窗体 == null) {
                throw new Exception("程序必须有主界面窗体，请设置");
            }
            if (数据库 == null) {
                throw new Exception("请设置好相应数据库(至少设置[全局数据.数据库])");
            }
            //启动窗体
            Application.Run(主界面窗体);
        }
    }
}
