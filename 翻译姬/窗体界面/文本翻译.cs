using Newtonsoft.Json.Linq;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;

namespace 翻译姬 {
    public partial class 文本翻译 : 自定义Page {

        private 全局设置数据 全局设置数据 => 全局数据.全局设置数据;
        private int 总行数 { get; set; } = 0;
        private 文件结构[] 处理中文件结构 { get; set; }

        private string 起始显示文本 = """
            本工具完全免费，如遇收费，那么你被骗了
            交流群：866373258



            """;

        public 文本翻译() {
            InitializeComponent();
            数据中转.文本显示Box = 文本显示Box;
            数据中转.实际使用字符Label = 实际使用字符Label;
            文本显示Box.ShowScrollBar = true;
            UIToolTip tip = new UIToolTip();
            tip.BackColor = 全局字符串.背景色;
            tip.ForeColor = 全局字符串.主题色;
            tip.RectColor = 全局字符串.主题色;
        }

        private void 文本翻译_Load(object sender, EventArgs e) {
            if (!记住读取目录Switch.Active) {
                Name_Control.Remove(读取目录Box.Name);
            }

            机翻接口Box.DataBindings.Add("Text", 全局设置数据, "使用机翻", false, DataSourceUpdateMode.OnPropertyChanged);
            读取目录Box.DataBindings.Add("Text", 全局设置数据, "读取目录", false, DataSourceUpdateMode.OnPropertyChanged);
            写出目录Box.DataBindings.Add("Text", 全局设置数据, "写出目录", false, DataSourceUpdateMode.OnPropertyChanged);
            使用正则Box.DataBindings.Add("Text", 全局设置数据, "使用正则", false, DataSourceUpdateMode.OnPropertyChanged);
            源语言Box.DataBindings.Add("Text", 全局设置数据, "源语言", false, DataSourceUpdateMode.OnPropertyChanged);
            目标语言Box.DataBindings.Add("Text", 全局设置数据, "目标语言", false, DataSourceUpdateMode.OnPropertyChanged);
            读取编码Box.DataBindings.Add("Text", 全局设置数据, "读取编码", false, DataSourceUpdateMode.OnPropertyChanged);
            写出编码Box.DataBindings.Add("Text", 全局设置数据, "写出编码", false, DataSourceUpdateMode.OnPropertyChanged);

            
            读取方式Box.DataSource = new List<string>() { "本地读取", "Json读取", "Xml读取" };
            机翻方式Box.DataSource = new List<string>() { "标准机翻", "TCP机翻", "不机翻" };
            写出方式Box.DataSource = new List<string>() { "本地写出", "Json写出", "Xml写出", "不写出" };
            源语言Box.DataSource = new List<string>() { "日语", "英语", "韩语", "繁中", "简中" };
            目标语言Box.DataSource = new List<string>() { "简中", "繁中", "日语", "英语", "韩语" };
            读取编码Box.DataSource = new List<string>() { "自动识别", "Shift-JIS", "UTF-8", "GBK", "UTF-16LE" };
            写出编码Box.DataSource = new List<string>() { "UTF-16LE", "UTF-8", "GBK" };

            var 机翻类型 = 全局数据.API主表.获取值集合("类型").ToList();
            机翻类型.Add("GPT");
            机翻接口Box.DataSource = 机翻类型.ToArray();
            使用正则Box.DataSource = 数据库.Select($"select 正则名称 from 正则").获取值集合().ToArray();

        }

        private void 文本翻译_Shown(object sender, EventArgs e) {
            if (写出目录Box.Text == "") {
                写出目录Box.Text = 全局字符串.桌面路径 + "机翻输出\\";
            }
            文本显示Box.Text = 起始显示文本;

            if (!记住读取目录Switch.Active || 读取目录Box.Text == "" || !Directory.Exists(读取目录Box.Text)) {
                读取目录Box.Text = 全局字符串.项目路径;
                return;
            }
        }

        private void 文本翻译_Page被选中() {
            var 机翻类型 = 全局数据.API主表.获取值集合("类型").ToList();
            机翻类型.Add("GPT");
            保留原文设置DataSource(机翻接口Box, 机翻类型.ToArray());
            保留原文设置DataSource(使用正则Box, 数据库.Select($"select 正则名称 from 正则").获取值集合().ToArray());
        }

        private void 保留原文设置DataSource(UIComboBox box, params string[] res) {
            var temp = box.Text;
            box.DataSource = res;
            box.Text = temp;
        }

        private void 开始Btn_Click(object sender, EventArgs e) {
            机翻前();
            Task.Run(() => {
                try {
                    //预显示
                    预显示Btn_Click(null, null);
                    if (总行数 == 0) {
                        return;
                    }
                    //数据准备
                    读取方式 读取 = (读取方式)Enum.Parse(typeof(读取方式), 读取方式Box.Text);
                    机翻方式 机翻 = (机翻方式)Enum.Parse(typeof(机翻方式), 机翻方式Box.Text);
                    写出方式 写出 = (写出方式)Enum.Parse(typeof(写出方式), 写出方式Box.Text);
                    数据中转.进度条当前值设定(0);
                    数据中转.进度条最大值设定(总行数);

                    foreach (var 文件 in 处理中文件结构) {
                            调用管理.文本机翻(机翻, 文件);
                        try {
                        } catch (Exception ex) {
                            数据中转.文本显示Append($"机翻异常：{ex.Message}");
                            if (MessageBoxEx.Show("机翻异常，发现已有机翻数据，是否写出不完整机翻文本？", "警告", 提示窗按钮.确认取消)) {
                                调用管理.文本写出(写出, 文件);
                                数据中转.文本显示Append($"已强制写出：{文件.文件名}");
                            }
                            break;
                        }
                        调用管理.文本写出(写出, 文件);
                    }

                    if (处理中文件结构.Any(t => t.文本组.Any(t2 => !t2.完成状态))) {
                        MessageBoxEx.Show("发现未完成异常数据，可点击数据处理进行手动更正");
                    }

                    BeginInvoke(() => 消息框帮助.通知栏消息("机翻完成！"));
                } catch (Exception ex) {
                    MessageBoxEx.Show(ex.Message);
                } finally {
                    机翻后();
                }
            });
        }

        private void 预显示Btn_Click(object sender, EventArgs e) {
            try {

                Invoke(() => 文本显示Box.Text = "");

                读取方式 读取 = (读取方式)Enum.Parse(typeof(读取方式), 读取方式Box.Text);
                处理中文件结构 = 调用管理.文本读取(读取).ToArray();

                int num = 0, 总字符 = 0;
                if (机翻接口Box.Text == "GPT") {
                    总字符 += GPT_Token计算(处理中文件结构);
                }
                总行数 = 0;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("\"机翻\"字符表示将会被机翻的内容");
                sb.AppendLine("如显示乱码，或机翻到了不该机翻的内容，请勿机翻");
                sb.AppendLine("*****************************************************************");
                foreach (var 文件 in 处理中文件结构) {
                    调用管理.文本机翻(机翻方式.不机翻, 文件);
                    //模拟机翻
                    var 译文组 = new List<string>();
                    foreach (var 文本 in 文件.文本组) {
                        foreach (var text in 文本.原文) {
                            if (机翻接口Box.Text != "GPT") {
                                总字符 += text.Length;
                            }
                        }
                        总行数 += 文本.原文.Length;
                        译文组.AddRange(工具类.文本置换机翻(文本.原文));
                    }
                    string[] 写入后文本 = 正则读写.正则文本写入(文件.原文本行, 译文组.ToArray(), 文件.处理数据.正则row);
                    for (int i = 0; num != 5 && i < 写入后文本.Length; i++) {
                        if (写入后文本[i] != 文件.原文本行[i]) {
                            sb.Append("【原文】:").AppendLine(文件.原文本行[i]);
                            sb.Append("【译文】:").AppendLine(写入后文本[i]);
                            sb.AppendLine();
                            num++;
                        }
                    }
                }
                sb.AppendLine("*****************************************************************");
                string 预计字符 = "预计使用字符：" + (int)(总字符 * 1.02);
                if (机翻接口Box.Text == "GPT") {
                    预计字符 += "(GPT预估)";
                }
                Invoke(() => 预计使用字符Label.Text = 预计字符);
                文本显示追加(起始显示文本 + sb.ToString());
            } catch (Exception ex) {
                消息框帮助.轻便消息(ex.Message, this, UINotifierType.WARNING);
            }
        }

        private void 读取目录Btn_Click(object sender, EventArgs e) {
            string[] paths = 工具类.选择文件夹("选择待机翻目录");
            if (paths.Length == 0) {
                return;
            }
            读取目录Box.Text = paths[0] + "\\";
        }

        private void 写出目录Btn_Click(object sender, EventArgs e) {
            string[] paths = 工具类.选择文件夹("选择机翻输出目录");
            if (paths.Length == 0) {
                return;
            }
            写出目录Box.Text = paths[0] + "\\";
        }

        private void 机翻前() {
            Invoke(new Action(() => {
                实际使用字符Label.Text = "实际使用字符：0";
                控件区Panel.Enabled = false; 
            }));
        }

        private void 机翻后() {
            Invoke(new Action(() => 控件区Panel.Enabled = true));
        }

        private void 文本显示追加(string text) {
            Invoke(new Action(() => 文本显示Box.AppendText(text + Environment.NewLine)));
        }

        private KeyValue<string, string> 上文内容;
        private int GPT_Token计算(文件结构[] 文件组) {
            全局数据.GPT设置数据.必要数据验证();
            var bpe = new BPE算法();
            var 额外请求内容 = new List<dynamic>();
            额外请求内容.Add(new { role = "system", content = 全局数据.GPT设置数据.语境 });
            if (全局数据.GPT设置数据.发送预设) {
                额外请求内容.Add(new { role = "user", content = 全局数据.GPT设置数据.预设原文 });
                额外请求内容.Add(new { role = "assistant", content = 全局数据.GPT设置数据.预设返回 });
            }
            int 单次请求额外token = bpe.Token计算(JsonConvert.SerializeObject(额外请求内容));
            int 上下文额外token = 0;
            int 总token = 0;
            foreach (var 文件 in 文件组) {
                if (文件.文本组.Length == 0) {
                    文件.生成机翻前文本组();
                }
                foreach (var 文本 in 文件.文本组) {
                    if (全局数据.GPT设置数据.上下文提示 && 上文内容 != null && 上下文额外token == 0) {
                        var temp = new List<dynamic>();
                        temp.Add(new { role = "user", content = 上文内容.Key });
                        temp.Add(new { role = "assistant", content = 上文内容.Value });
                        上下文额外token = bpe.Token计算(JsonConvert.SerializeObject(temp));
                    }
                    var 请求内容 = 获取请求内容(文本.原文);
                    string 请求内容Json = JsonConvert.SerializeObject(请求内容);
                    总token += bpe.Token计算(请求内容Json) * 2;//假设返回的为相同内容
                    总token += 单次请求额外token;
                    总token += 上下文额外token;
                    上文内容 = new KeyValue<string, string> {
                        Key = 请求内容Json,
                        Value = 请求内容Json
                    };
                }
            }
            上文内容 = null;
            return 总token;
        }
        private List<dynamic> 获取请求内容(string[] texts) {
            //请求内容计算
            var 请求内容 = new List<dynamic>();
            var dic = new Dictionary<int, string>();
            int num = 0;
            foreach (var text in texts) {
                dic.Add(num++, text);
            }
            请求内容.Add(new { role = "user", content = JsonConvert.SerializeObject(dic) });
            return 请求内容;
        }

        private void 机翻方式Box_TextChanged(object sender, EventArgs e) {
            if (机翻方式Box.Text == "标准机翻") {
                机翻接口Box.Enabled = true;
            } else {
                机翻接口Box.Enabled = false;
            }
        }

        private void 读取方式Box_TextChanged(object sender, EventArgs e) {
            if (读取方式Box.Text == "TCP读取") {
                读取目录Box.Enabled = false;
            } else {
                读取目录Box.Enabled = true;
            }
            if (读取方式Box.Text == "Json读取") {
                写出方式Box.Text = "Json写出";
            } else if (读取方式Box.Text == "Xml读取") { 
                写出方式Box.Text = "Xml写出";
            } else {
                写出方式Box.Text = "本地写出";
            }
        }

        private void 写出方式Box_TextChanged(object sender, EventArgs e) {
            if (写出方式Box.Text == "本地写出" || 写出方式Box.Text == "Json写出" || 写出方式Box.Text == "Xml写出") {
                写出目录Box.Enabled = true;
            } else {
                写出目录Box.Enabled = false;
            }
            if (写出方式Box.Text == "Json写出") {
                读取方式Box.Text = "Json读取";
            } else if (写出方式Box.Text == "Xml写出") { 
                读取方式Box.Text = "Xml读取";
            } else {
                读取方式Box.Text = "本地读取";
            }
        }

        private void 数据处理Btn_Click(object sender, EventArgs e) {
            new 数据处理(处理中文件结构).ShowDialog();
        }

        private void 文本翻译_FormClosed(object sender, FormClosedEventArgs e) {
            //晚于主界面的关闭，进行数据存储
            全局数据.保存设置数据();
        }
    }
}
