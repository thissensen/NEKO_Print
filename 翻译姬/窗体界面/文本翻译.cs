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
using System.Drawing.Printing;

namespace 翻译姬 {
    public partial class 文本翻译 : 自定义Page {

        public static bool 机翻中 { get; private set; } = false;
        private 全局设置数据 全局设置数据 => 全局数据.全局设置数据;
        private int 总行数 { get; set; } = 0;
        //public static bool 是否中止 = false;
        public static 文件结构[] 处理中文件结构 {
            get => 全局数据.处理中文件结构;
            set => 全局数据.处理中文件结构 = value;
        }

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
            缓存续翻Btn.Text = "缓存\r\n续翻";
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


            读取方式Box.DataSource = new List<string>() { "文本读取", "Json读取", "Xml读取" };
            机翻方式Box.DataSource = new List<string>() { "标准机翻", "不机翻" };
            写出方式Box.DataSource = new List<string>() { "文本写出", "Json写出", "Xml写出" };
            源语言Box.DataSource = new List<string>() { "日语", "英语", "韩语", "繁中", "简中" };
            目标语言Box.DataSource = new List<string>() { "简中", "日语", "英语", "韩语" };
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
            }

            if (File.Exists(全局数据.缓存数据路径)) {
                缓存续翻Btn.Enabled = true;
                try {
                    string json = File.ReadAllText(全局数据.缓存数据路径);
                    处理中文件结构 = JsonConvert.DeserializeObject<文件结构[]>(json);
                    文本组反向覆盖有效文本(处理中文件结构);
                } catch {
                    throw new Exception("缓存数据解析失败");
                }
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
            if (开始Btn.Text == "中止") {
                if (MessageBoxEx.Show("机翻中，确定中止？", "提示", 提示窗按钮.确认取消)) {
                    数据中转.文本显示AppendLine("中止进行中……");
                    开始Btn.Enabled = false;
                    全局数据.是否中止 = true;
                }
                return;
            }
            机翻前();
            Task.Run(() => {
                try {
                    //预显示
                    预显示Btn_Click(null, null);
                    if (总行数 == 0) {
                        return;
                    }
                    if (File.Exists(全局数据.缓存数据路径) &&
                    !MessageBoxEx.Show("检测到有未完成的缓存数据，继续机翻将覆盖已缓存数据", 显示按钮: 提示窗按钮.确认取消, 确认按钮文本: "继续")) {
                        return;
                    }
                    开始Btn.Invoke(() => {
                        开始Btn.Text = "中止";
                        开始Btn.Enabled = true;
                    });
                    //数据准备
                    机翻方式 机翻 = (机翻方式)Enum.Parse(typeof(机翻方式), 机翻方式Box.Text);
                    数据中转.进度条当前值设定(0);
                    数据中转.进度条最大值设定(总行数);

                    try {
                        //机翻-替换-文本检查-完成状态检查
                        foreach (var 文件 in 处理中文件结构) {
                            文件.文件完成机翻 = () => {
                                文件.机翻后数据处理(机翻);
                                if (机翻 != 机翻方式.不机翻) {
                                    文件.文本组.强制生成机翻();//写出前必做
                                }
                                文件.写出();
                            };
                        }
                        调用管理.文本机翻(机翻, 处理中文件结构);

                        /*foreach (var 文件 in 处理中文件结构) {
                            调用管理.文本机翻(机翻, 文件);
                            if (机翻 != 机翻方式.不机翻) {
                                文件.文本组.强制生成机翻();
                            }
                            文件.写出();
                        }*/
                    } catch (Exception ex) {
                        数据中转.文本显示AppendLine(ex.Message);
                    }

                    if (处理中文件结构.Any(t => t.文本组.Any(t2 => !t2.完成状态))) {
                        MessageBoxEx.Show("发现未完成异常数据，可点击数据处理进行手动更正");
                    }

                    BeginInvoke(() => 消息框帮助.通知栏消息("机翻结束！"));
                } catch (Exception ex) {
                    if (ex.Message.Contains("正在中止线程")) {
                        MessageBoxEx.Show("中止成功");
                    } else {
                        MessageBoxEx.Show(ex.Message);
                    }
                } finally {
                    机翻后();
                }
            });
        }

        private void 缓存续翻Btn_Click(object sender, EventArgs e) {
            机翻前();
            Task.Run(() => {
                try {
                    if (!File.Exists(全局数据.缓存数据路径)) {
                        throw new Exception("不存在缓存数据");
                    }
                    文件结构[] 文件结构组;
                    try {
                        string json = File.ReadAllText(全局数据.缓存数据路径);
                        文件结构组 = JsonConvert.DeserializeObject<文件结构[]>(json);
                        文本组反向覆盖有效文本(文件结构组);
                    } catch {
                        throw new Exception("缓存数据解析失败");
                    }
                    开始Btn.Invoke(() => {
                        开始Btn.Text = "中止";
                        开始Btn.Enabled = true;
                    });
                    总行数 = 0;
                    foreach (var 文件 in 处理中文件结构) {
                        foreach (var 文本组 in 文件.文本组) {
                            if (文本组.机翻状态) {
                                continue;
                            }
                            总行数 += 文本组.文本.Length;
                        }
                    }
                    数据中转.进度条当前值设定(0);
                    数据中转.进度条最大值设定(总行数);
                    机翻方式 机翻 = (机翻方式)Enum.Parse(typeof(机翻方式), 机翻方式Box.Text);

                    处理中文件结构 = 文件结构组;
                    try {
                        foreach (var 文件 in 处理中文件结构) {
                            文件.文件完成机翻 = () => {
                                文件.机翻后数据处理(机翻);
                                文件.文本组.强制生成机翻();
                                文件.写出();
                            };
                        }
                        调用管理.文本机翻(机翻, 处理中文件结构);


                        /*foreach (var 文件 in 处理中文件结构) {
                            标准机翻.机翻(文件);
                            文件.机翻后数据处理(机翻);
                            文件.文本组.强制生成机翻();
                            文件.写出();
                        }*/
                    } catch (Exception ex) {
                        数据中转.文本显示AppendLine(ex.Message);
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
        //Json转化而来后那俩就不是同一个地址了，单独反向覆盖保留机翻状态
        private void 文本组反向覆盖有效文本(文件结构[] 文件组) {
            foreach (var 文件 in 文件组) {
                var list = new List<文本>();
                foreach (var 文本组 in 文件.文本组) {
                    foreach (var 文本 in 文本组.文本) {
                        list.Add(文本);
                    }
                }
                文件.有效文本 = list.OrderBy(t => t.文本下标).ToArray();
            }

        }

        private void 预显示Btn_Click(object sender, EventArgs e) {
            try {
                总行数 = 0;

                //路径验证
                if (!Directory.Exists(全局设置数据.读取目录)) {
                    读取目录Box.Text = 全局字符串.项目路径;
                    throw new Exception("读取目录不存在，已设置为默认路径");
                }
                if (!Directory.Exists(全局设置数据.写出目录) && 全局设置数据.写出目录 != 全局字符串.桌面路径 + "机翻输出\\") {
                    写出目录Box.Text = 全局字符串.桌面路径 + "机翻输出\\";
                    throw new Exception("写出目录不存在，已设置为默认路径");
                }

                Invoke(() => 文本显示Box.Text = "");

                读取方式 读取 = (读取方式)Enum.Parse(typeof(读取方式), 读取方式Box.Text);
                处理中文件结构 = 调用管理.文本读取(读取).ToArray();

                int num = 0, 总字符 = 0;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("\"机翻\"字符表示将会被机翻的内容");
                sb.AppendLine("如显示乱码，或机翻到了不该机翻的内容，请勿开始");
                sb.AppendLine("*****************************************************************");
                foreach (var 文件 in 处理中文件结构) {
                    调用管理.文本机翻(机翻方式.不机翻, 文件);
                    //模拟机翻
                    var 译文组 = new List<文本>();
                    foreach (var 文本组 in 文件.文本组) {
                        if (机翻接口Box.Text != "GPT") {
                            foreach (var 文本 in 文本组.文本) {
                                string text = 文本.原文;
                                if (机翻接口Box.Text == "百度") {
                                    总字符 += text.Length + 1;
                                } else {
                                    总字符 += text.Length;
                                }
                            }
                        }
                        总行数 += 文本组.文本.Length;
                        译文组.AddRange(Util.文本置换机翻(文本组.文本));//区别于源文件，这个是复制出来的
                    }
                    string[] 写入前文本;
                    string[] 写入后文本;
                    if (读取 == 读取方式.文本读取) {
                        写入后文本 = 正则读写.正则文本写入(文件.原文本行, 译文组.ToArray(), 文件.处理数据.正则.Rows[0]);
                        写入前文本 = 文件.原文本行;
                    } else {
                        写入前文本 = 译文组.ToArray().获取原文组();
                        写入后文本 = 译文组.ToArray().获取译文组();
                    }
                    //模拟输出5行
                    for (int i = 0; num != 5 && i < 写入后文本.Length; i++) {
                        if (写入后文本[i] != 写入前文本[i]) {
                            sb.Append("【原文】:").AppendLine(写入前文本[i]);
                            sb.Append("【译文】:").AppendLine(写入后文本[i]);
                            sb.AppendLine();
                            num++;
                        }
                    }
                }
                sb.AppendLine("*****************************************************************");
                string 预计字符 = "预计使用字符：" + (int)(总字符 * 1.02);
                if (机翻接口Box.Text == "GPT") {
                    预计字符 = "预计使用字符：(无法预估，请以实际消耗为准)";
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
                数据中转.数据处理?.Close();
                机翻中 = true;
                全局数据.是否中止 = false;
                实际使用字符Label.Text = "实际使用字符：0";
                var cons = 控件组();
                foreach (var c in cons) {
                    c.Enabled = false;
                }
                开始Btn.Enabled = false;
                数据处理Btn.Enabled = true;
                主界面.界面组["全局设置"].Enabled = false;
                主界面.界面组["GPT设置"].Enabled = false;
            }));
        }

        private void 机翻后() {
            BeginInvoke(new Action(() => {
                机翻中 = false;
                全局数据.是否中止 = true;
                var cons = 控件组();
                foreach (var c in cons) {
                    c.Enabled = true;
                }
                开始Btn.Text = "开始";
                开始Btn.Enabled = true;
                主界面.界面组["全局设置"].Enabled = true;
                主界面.界面组["GPT设置"].Enabled = true;
                if (File.Exists(全局数据.缓存数据路径)) {
                    缓存续翻Btn.Enabled = true;
                } else {
                    缓存续翻Btn.Enabled = false;
                }
            }));
        }

        private Control[] 控件组() {
            var res = new List<Control>();
            foreach (Control con in 控件区Panel.Controls) {
                res.Add(con);
            }
            res.Remove(开始Btn);
            return res.ToArray();
        }

        private void 文本显示追加(string text) {
            Invoke(new Action(() => 文本显示Box.AppendText(text + Environment.NewLine)));
        }

        public Dictionary<string, string> 上文内容 = new Dictionary<string, string>();
        /*private int GPT_Token计算(文件结构 文件) {
            上文内容.Clear();
            int token = 0;
            foreach (var 文本组 in 文件.文本组) {
                全局数据.GPT设置数据.必要数据验证();
                var 请求arr = 工具类.文本转请求(文本组.文本);
                string json = JsonConvert.SerializeObject(请求arr);
                var 请求内容 = new List<dynamic>();
                请求内容.Add(new { role = "system", content = GPT设置数据.语境.Replace("[Input]", json) });
                if (GPT设置数据.上下文提示 && 上文内容.Count > 0) {
                    for (int i = 0; i < GPT设置数据.上下文深度 && i < 上文内容.Count; i++) {
                        var kv = 上文内容.ElementAt(i);
                        请求内容.Add(new { role = "user", content = kv.Key });
                        请求内容.Add(new { role = "assistant", content = kv.Value });
                    }
                }
                token += bpe.Token计算(json);//返回消耗的token
                token += bpe.Token计算(JsonConvert.SerializeObject(请求内容));//请求消耗的token
            }
            return token;
        }*/

        /*private void 机翻接口Box_TextChanged(object sender, EventArgs e) {
            if (机翻接口Box.Text == "GPT") {
                源语言Box.Enabled = false;
                //目标语言Box.Enabled = false;
            } else {
                源语言Box.Enabled = true;
                //目标语言Box.Enabled = true;
            }
        }*/

        private void 机翻方式Box_TextChanged(object sender, EventArgs e) {
            if (机翻方式Box.Text == "标准机翻") {
                机翻接口Box.Enabled = true;
            } else {
                机翻接口Box.Enabled = false;
            }
        }

        private void 读取方式Box_TextChanged(object sender, EventArgs e) {
            if (读取方式Box.Text == "Json读取") {
                写出方式Box.Text = "Json写出";
            } else if (读取方式Box.Text == "Xml读取") {
                写出方式Box.Text = "Xml写出";
            } else {
                写出方式Box.Text = "文本写出";
            }
        }

        private void 写出方式Box_TextChanged(object sender, EventArgs e) {
            if (写出方式Box.Text == "Json写出") {
                读取方式Box.Text = "Json读取";
            } else if (写出方式Box.Text == "Xml写出") {
                读取方式Box.Text = "Xml读取";
            } else {
                读取方式Box.Text = "文本读取";
            }
        }

        private void 数据处理Btn_Click(object sender, EventArgs e) {
            if (数据中转.数据处理 == null) {
                new 数据处理(处理中文件结构).Show();
            } else {
                数据中转.数据处理.TopMost = true;
                数据中转.数据处理.TopMost = false;
            }
        }

        private void 文本翻译_FormClosed(object sender, FormClosedEventArgs e) {
            //晚于主界面的关闭，进行数据存储
            全局数据.保存设置数据();
        }
    }
}
