using Sunny.UI;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

//JsonPath和XmlPath：https://www.cnblogs.com/youring2/p/10942728.html
namespace 翻译姬 {
    public partial class 关于翻译姬 : 自定义Page {

        [DllImport("Kernel32.dll")]
        public static extern bool SetLocalTime(ref SYSTEMTIME Time);

        public 关于翻译姬() {
            InitializeComponent();
        }

        private void 关于翻译姬_Load(object sender, EventArgs e) {
            Name_Control.Remove(介绍内容Box.Name);//不要序列化
            刷新Panel颜色();
            string 版本 = FileVersionInfo.GetVersionInfo(Process.GetCurrentProcess().MainModule.FileName).FileVersion;
            当前版本Label.Text = $"当前版本：V{版本}";
        }

        private async void 关于翻译姬_Shown(object sender, EventArgs e) {
            //版本检查
            最新版本Label.Text = await 获取版本号();
        }

        private void 主题色Btn_Click(object sender, EventArgs e) {
            var f = new 自定义颜色选择器(全局字符串.主题色);
            f.Location = 获取窗体位置(主题色Btn);
            f.FormClosed += (_, _) => {
                if (f.是否确认) {
                    全局字符串.主题色 = f.所选颜色;
                    刷新Panel颜色();
                    消息框帮助.轻便消息("重启后生效", this);
                }
            };
            f.Show();
        }

        private void 背景色Btn_Click(object sender, EventArgs e) {
            var f = new 自定义颜色选择器(全局字符串.背景色);
            f.Location = 获取窗体位置(背景色Btn);
            f.FormClosed += (_, _) => {
                if (f.是否确认) {
                    全局字符串.背景色 = f.所选颜色;
                    刷新Panel颜色();
                    消息框帮助.轻便消息("重启后生效", this);
                }
            };
            f.Show();
        }

        private void 次级主题色Btn_Click(object sender, EventArgs e) {
            var f = new 自定义颜色选择器(全局字符串.次级主题色);
            f.Location = 获取窗体位置(次级主题色Btn);
            f.FormClosed += (_, _) => {
                if (f.是否确认) {
                    全局字符串.次级主题色 = f.所选颜色;
                    刷新Panel颜色();
                    消息框帮助.轻便消息("重启后生效", this);
                }
            };
            f.Show();
        }

        private void 深级主题色Btn_Click(object sender, EventArgs e) {
            var f = new 自定义颜色选择器(全局字符串.深级主题色);
            f.Location = 获取窗体位置(深级主题色Btn);
            f.FormClosed += (_, _) => {
                if (f.是否确认) {
                    全局字符串.深级主题色 = f.所选颜色;
                    刷新Panel颜色();
                    消息框帮助.轻便消息("重启后生效", this);
                }
            };
            f.Show();
        }

        private void 不可用时颜色Btn_Click(object sender, EventArgs e) {
            var f = new 自定义颜色选择器(全局字符串.不可用时颜色);
            f.Location = 获取窗体位置(不可用时颜色Btn);
            f.FormClosed += (_, _) => {
                if (f.是否确认) {
                    全局字符串.不可用时颜色 = f.所选颜色;
                    刷新Panel颜色();
                    消息框帮助.轻便消息("重启后生效", this);
                }
            };
            f.Show();
        }

        private void 刷新Panel颜色() {
            主题色Panel.FillColor = 全局字符串.主题色;
            背景色Panel.FillColor = 全局字符串.背景色;
            次级主题色Panel.FillColor = 全局字符串.次级主题色;
            深级主题色Panel.FillColor = 全局字符串.深级主题色;
            不可用时颜色Panel.FillColor = 全局字符串.不可用时颜色;
        }

        private Point 获取窗体位置(UIButton btn) {
            var p = PointToScreen(btn.Location);
            return new Point(p.X, p.Y + btn.Height);
        }

        private bool CanPing(string url) {
            using Ping ping = new Ping();
            try {
                PingReply pr = ping.Send(url, 500);
                if (pr.Status == IPStatus.Success) {
                    return true;
                } else {
                    return false;
                }
            } catch {
                return false;
            }
        }

        private async Task<string> 获取版本号() {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://gitee.com/this_sensen/NEKO_Print/blob/master/README.md");
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;
            request.Timeout = 3000;
            try {
                using WebResponse wp = await request.GetResponseAsync();
                using Stream stream = wp.GetResponseStream();
                using StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("utf-8"));
                string[] arr = reader.ReadToEnd().Split('\n');
                string text = arr.First(s => s.Contains("最新版本：")).Trim();
                return Regex.Match(text, @"(最新版本：V\d\.\d\.\d\.\d)").Groups[1].Value;
            } catch {
                return "最新版本：获取失败";
            }
        }

        private async void 检查更新Btn_Click(object sender, EventArgs e) {
            检查更新Btn.Click -= 检查更新Btn_Click;
            最新版本Label.Text = "最新版本：获取中…";
            最新版本Label.Text = await 获取版本号();
            检查更新Btn.Click += 检查更新Btn_Click;
        }

        private void 前往下载Btn_Click(object sender, EventArgs e) {
            Process.Start("https://gitee.com/this_sensen/NEKO_Print/releases");
        }

        private void 项目地址Btn_Click(object sender, EventArgs e) {
            Process.Start("https://gitee.com/this_sensen/NEKO_Print/");
        }

        private void 交流群Btn_Click(object sender, EventArgs e) {
            Process.Start("https://qm.qq.com/cgi-bin/qm/qr?authKey=uJu9cCUREatWVmk6Sc%2F8hfigYi%2F%2B9BhAapMbrEr4U%2FXVValM3xRB%2FrzKCX1uWpLh&k=pYjvXcwknhVTViXPYPWr6a2LXZ3VaSD5&noverify=0");
        }

        private void 使用手册Btn_Click(object sender, EventArgs e) {
            Process.Start("https://gitee.com/this_sensen/NEKO_Print/wikis/");
        }

        private void 时间校准Btn_Click(object sender, EventArgs e) {
            string str = GetNetDateTime();
            if (str.IsNullOrEmpty()) {
                消息框帮助.轻便消息("校准失败", this);
            } else {
                DateTime time = Convert.ToDateTime(str);
                SYSTEMTIME t = new SYSTEMTIME();
                t.wYear = (short)time.Year;
                t.wMonth = (short)time.Month;
                t.wDayOfWeek = (short)time.DayOfWeek;
                t.wDay = (short)time.Day;
                t.wHour = (short)time.Hour;
                t.wMinute = (short)time.Minute;
                t.wSecond = (short)time.Second;
                t.wMilliseconds = (short)time.Millisecond;
                if (SetLocalTime(ref t)) {
                    string 提示 = $"""
                        系统时间校准成功
                        {time:yyyy-MM-dd HH:mm:ss}
                        """;
                    MessageBoxEx.Show(提示);
                } else {
                    MessageBoxEx.Show("校准失败，请检查是否被杀毒软件拦截");
                }
            }
        }
        private string GetNetDateTime() {
            WebRequest request = null;
            WebResponse response = null;
            WebHeaderCollection headerCollection = null;
            string datetime = string.Empty;
            try {
                request = WebRequest.Create("https://www.baidu.com");
                request.Timeout = 3000;
                request.Credentials = CredentialCache.DefaultCredentials;
                response = (WebResponse)request.GetResponse();
                headerCollection = response.Headers;
                foreach (var h in headerCollection.AllKeys) {
                    if (h == "Date") {
                        datetime = headerCollection[h];
                    }
                }
                return datetime;
            } catch (Exception) {
                return datetime;
            } finally {
                if (request != null) {
                    request.Abort();
                }
                if (response != null) {
                    response.Close();
                }
                if (headerCollection != null) {
                    headerCollection.Clear();
                }
            }
        }

        private void 初始化Btn_Click(object sender, EventArgs e) {
            if (MessageBoxEx.Show("仅清空默认设置，不影响账号、正则、替换列表等数据", "提示", 提示窗按钮.确认取消)) {
                数据库.Execute("delete from 窗体序列化");
                Environment.Exit(0);
            }
        }

        private void 清空所有Btn_Click(object sender, EventArgs e) {
            if (MessageBoxEx.Show("将清空所有数据！", "提示", 提示窗按钮.确认取消)) {
#if DEBUG
#else
                var con = 全局数据.数据库 as SQLite数据库;
                con.con.Close();
                con.con.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                try {
                    File.Delete(Program.数据库路径);
                    Environment.Exit(0);
                } catch (Exception ex) {
                    MessageBoxEx.Show($"删除失败：{ex.Message}");
                }
#endif
            }
        }

        private void 本机数据Btn_Click(object sender, EventArgs e) {
            工具类.CMD异步调用($"explorer \"{Program.软件存储目录}\"");
        }

        private void 导出数据Btn_Click(object sender, EventArgs e) {
            try {
                string file = 工具类.选择保存目录("数据保存目录", $"{全局字符串.桌面路径}{DateTime.Now:yyyy-MM-dd}.db", "文件", "*.db");
                if (file == null) {
                    return;
                }
                File.Copy(Program.数据库路径, file, false);
                消息框帮助.轻便消息("保存成功", this);
            } catch (Exception ex) {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private void 使用指定数据启动翻译姬Btn_Click(object sender, EventArgs e) {
            try {
                string path = 工具类.选择文件("请选择由翻译姬导出的db文件", "文件", "*.db").FirstOrDefault();
                if (path == null) {
                    return;
                }
                string 翻译姬路径 = $"{全局字符串.项目路径 + 全局字符串.软件名称}.exe";
                Task.Run(() => {
                    工具类.CMD异步调用(翻译姬路径, path);
                });
            } catch (Exception ex) {
                MessageBoxEx.Show(ex.Message);
            }
        }
    }
}
