using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;
using System.IO;
using System.Text.RegularExpressions;

namespace 翻译姬;
public partial class 全局设置 : 自定义Page {

    private 全局设置数据 全局设置数据 => 全局数据.全局设置数据;

    public 全局设置() {
        InitializeComponent();
    }

    private void 全局设置_Load(object sender, EventArgs e) {
        //Add会导致数据赋值给控件
        读取目录Box.DataBindings.Add("Text", 全局设置数据, "读取目录", false, DataSourceUpdateMode.OnPropertyChanged);
        读取编码Box.DataBindings.Add("Text", 全局设置数据, "读取编码", false, DataSourceUpdateMode.OnPropertyChanged);
        读取后缀Box.DataBindings.Add("Text", 全局设置数据, "读取后缀", false, DataSourceUpdateMode.OnPropertyChanged);

        机翻接口Box.DataBindings.Add("Text", 全局设置数据, "使用机翻", false, DataSourceUpdateMode.OnPropertyChanged);
        源语言Box.DataBindings.Add("Text", 全局设置数据, "源语言", false, DataSourceUpdateMode.OnPropertyChanged);
        目标语言Box.DataBindings.Add("Text", 全局设置数据, "目标语言", false, DataSourceUpdateMode.OnPropertyChanged);
        机翻空值使用原文Switch.DataBindings.Add("Active", 全局设置数据, "机翻空值使用原文", false, DataSourceUpdateMode.OnPropertyChanged);
        无视返回空值Switch.DataBindings.Add("Active", 全局设置数据, "无视返回空值", false, DataSourceUpdateMode.OnPropertyChanged);
        重复内容跳过Switch.DataBindings.Add("Active", 全局设置数据, "重复内容跳过", false, DataSourceUpdateMode.OnPropertyChanged);
        无视返回原文Switch.DataBindings.Add("Active", 全局设置数据, "无视返回原文", false, DataSourceUpdateMode.OnPropertyChanged);
        文本级多线程Switch.DataBindings.Add("Active", 全局设置数据, "文本级多线程", false, DataSourceUpdateMode.OnPropertyChanged);
        启用单组上限Switch.DataBindings.Add("Active", 全局设置数据, "启用单组上限", false, DataSourceUpdateMode.OnPropertyChanged);
        API单组上限Box.DataBindings.Add("Text", 全局设置数据, "API单组上限", false, DataSourceUpdateMode.OnPropertyChanged);

        写出目录Box.DataBindings.Add("Text", 全局设置数据, "写出目录", false, DataSourceUpdateMode.OnPropertyChanged);
        写出编码Box.DataBindings.Add("Text", 全局设置数据, "写出编码", false, DataSourceUpdateMode.OnPropertyChanged);
        写出格式Box.DataBindings.Add("Text", 全局设置数据, "写出格式", false, DataSourceUpdateMode.OnPropertyChanged);

        使用正则Box.DataBindings.Add("Text", 全局设置数据, "使用正则", false, DataSourceUpdateMode.OnPropertyChanged);
        JSON指令Box.DataBindings.Add("Text", 全局设置数据, "指令集名称", false, DataSourceUpdateMode.OnPropertyChanged);
        Xml指令Box.DataBindings.Add("Text", 全局设置数据, "Xml指令集名称", false, DataSourceUpdateMode.OnPropertyChanged);
        写出后删除源文件Switch.DataBindings.Add("Active", 全局设置数据, "写出后删除源文件", false, DataSourceUpdateMode.OnPropertyChanged);
        内置中括号过滤Switch.DataBindings.Add("Active", 全局设置数据, "内置中括号过滤", false, DataSourceUpdateMode.OnPropertyChanged);
        正则逆向写入Switch.DataBindings.Add("Active", 全局设置数据, "正则逆向写入", false, DataSourceUpdateMode.OnPropertyChanged);

        源语言Box.DataSource = new List<string>() { "日语", "英语", "韩语", "繁中", "简中"};
        目标语言Box.DataSource = new List<string>() { "简中", "繁中", "日语", "英语", "韩语" };
        读取编码Box.DataSource = new List<string>() { "自动识别", "Shift-JIS", "UTF-8", "GBK", "UTF-16LE" };
        写出编码Box.DataSource = new List<string>() { "UTF-16LE", "UTF-8", "GBK" };

        var 机翻类型 = 全局数据.API主表.获取值集合("类型").ToList();
        机翻类型.Add("GPT");
        机翻接口Box.DataSource = 机翻类型.ToArray();
        使用正则Box.DataSource = 数据库.Select($"select 正则名称 from 正则").获取值集合().ToArray();
        JSON指令Box.DataSource = 数据库.Select($"select 名称 from Json指令").获取值集合().ToArray();
        Xml指令Box.DataSource = 数据库.Select($"select 名称 from Xml指令").获取值集合().ToArray();
    }

    private void 全局设置_Page被选中() {
        var 机翻类型 = 全局数据.API主表.获取值集合("类型").ToList();
        机翻类型.Add("GPT");
        保留原文设置DataSource(机翻接口Box, 机翻类型.ToArray());
        保留原文设置DataSource(使用正则Box, 数据库.Select($"select 正则名称 from 正则").获取值集合().ToArray());
        保留原文设置DataSource(JSON指令Box, 数据库.Select($"select 名称 from Json指令").获取值集合().ToArray());
        保留原文设置DataSource(Xml指令Box, 数据库.Select($"select 名称 from Xml指令").获取值集合().ToArray());
        foreach (var kv in this.Name_Control) {
            if (kv.Value is UITextBox box) {
                var temp = box.Text;
                box.Text = "";
                box.Text = temp;
            } else if (kv.Value is UIComboBox cBox) {
                var temp = cBox.Text;
                cBox.Text = "";
                cBox.Text = temp;
            }
        }
    }

    private void 保留原文设置DataSource(UIComboBox box, params string[] res) {
        var temp = box.Text;
        box.DataSource = res;
        box.Text = temp;
    }

    private void 读取目录Btn_Click(object sender, EventArgs e) {
        string[] paths = 工具类.选择文件夹("选择待机翻目录");
        if (paths.Length == 0) {
            return;
        }
        读取目录Box.Text = paths[0] + "\\";
    }

    private void 后缀验证Btn_Click(object sender, EventArgs e) {
        if (读取后缀Box.Text.Trim() == "") {
            return;
        }
        try {
            string[] arr = 读取后缀Box.Text.Split('|');
            foreach (string s in arr) {
                if (s.Trim().IsNullOrEmpty() || !Regex.IsMatch(s, @"\*\.\w")) {
                    string 提示 = """
                        读取后缀不符合规则
                        正确规则例如：*.txt|*.ks
                        *开头，使用|分割
                        """;
                    throw new Exception(提示);
                }
            }
            消息框帮助.轻便消息("验证成功", this);
        } catch (Exception ex) {
            MessageBoxEx.Show(ex.Message);
        }
    }

    private void 写出目录Btn_Click(object sender, EventArgs e) {
        string[] paths = 工具类.选择文件夹("选择机翻输出目录");
        if (paths.Length == 0) {
            return;
        }
        写出目录Box.Text = paths[0] + "\\";
    }

    private void 本机IPBtn_Click(object sender, EventArgs e) {
        MessageBoxEx.Show(获取本机IP地址());
    }
    public static string 获取本机IP地址() {
        IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
        List<string> ipv6 = new List<string>();
        List<string> ipv4 = new List<string>();
        foreach (var ip in ipEntry.AddressList) {
            if (ip.AddressFamily == AddressFamily.InterNetwork) {
                ipv4.Add(ip.ToString());
            } else if (ip.AddressFamily == AddressFamily.InterNetworkV6) {
                ipv6.Add(ip.ToString());
            }
        }
        StringBuilder sb = new StringBuilder();
        if (ipv4.Count > 0) {
            foreach (var v4 in ipv4) {
                sb.AppendLine($"IPv4 --- {v4}");
            }
        }
        if (ipv6.Count > 2) {
            foreach (var v6 in ipv6) {
                sb.AppendLine($"IPv6 --- {v6}");
            }
            //sb.AppendLine($"本地IPv6 --- {ipv6[0]}");
            //sb.AppendLine($"临时IPv6 --- {ipv6[1]}");
        }
        return sb.ToString().Trim();
    }

    private void 机翻空值使用原文Switch_ActiveChanged(object sender, EventArgs e) {
        BeginInvoke(() => {
            if (机翻空值使用原文Switch.Active) {
                无视返回空值Switch.Active = false;
            }
        });
    }

    private void 无视返回空值Switch_ActiveChanged(object sender, EventArgs e) {
        BeginInvoke(() => {
            if (无视返回空值Switch.Active) {
                机翻空值使用原文Switch.Active = false;
            }
        });
    }

    private void 启用单组上限Switch_ActiveChanged(object sender, EventArgs e) {
        if (启用单组上限Switch.Active) {
            API单组上限Box.Enabled = true;
        } else {
            API单组上限Box.Enabled = false;
        }
    }

    private void 文本级多线程Switch_ActiveChanged(object sender, EventArgs e) {
        BeginInvoke(() => {
            if (!文本级多线程Switch.Active) {
                全局数据.GPT设置数据.上下文提示 = false;
                启用单组上限Switch.Active = false;
            }
        });
    }
}
