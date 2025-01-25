using Newtonsoft.Json;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using 翻译姬.Properties;
using static System.Net.Mime.MediaTypeNames;

namespace 翻译姬 {
    public partial class 替换列表数值更改 : 自定义Form {

        private DataRow 替换列表row;
        private string[] 替换列表Lines {
            get {
                return 替换列表Box.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }
            set {
                替换列表Box.Text = string.Join(Environment.NewLine, value);
            }
        }

        public 替换列表数值更改(DataRow 替换列表row) {
            InitializeComponent();
            this.替换列表row = 替换列表row;
            替换列表Box.TextBox.ShowScrollBar = true;
        }

        private void 替换列表数值更改_Load(object sender, EventArgs e) {
            Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(替换列表row["替换列表"].ToString());
            StringBuilder sb = new StringBuilder();
            foreach (var kv in dic) {
                sb.Append(kv.Key).Append('=').AppendLine(kv.Value);
            }
            替换列表Box.Text = sb.ToString();

            //显示位置屏幕居中
            var rect = Screen.GetWorkingArea(this);
            var p = new Point((rect.Width - Width) / 2, (rect.Height - Height) / 2);
            Location = p;
        }

        private void 字典选择Btn_Click(object sender, EventArgs e) {
            try {
                if (提取型正则Box.Text.Trim().IsNullOrEmpty()) {
                    throw new Exception("提取型正则未填");
                }
                try {
                    if (!Regex.IsMatch(提取型正则Box.Text, @"^\(.*\).*\(.*\)$")) {
                        throw new Exception();
                    }
                } catch { throw new Exception("提取型正则格式错误，需要有2个()"); }
                string[] paths = 工具类.选择文件("选择字典文件", "字典", "*.txt");
                if (paths.Length == 1) {
                    string[] lines = File.ReadAllLines(paths[0], Encoding.GetEncoding(Util.文本编码识别(paths[0])));
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    foreach (string line in lines) {
                        var groups = Regex.Match(line, 提取型正则Box.Text).Groups;
                        if (groups.Count == 3) {
                            if (dic.ContainsKey(groups[1].Value)) {
                                dic[groups[1].Value] = groups[2].Value;
                            } else {
                                dic.Add(groups[1].Value, groups[2].Value);
                            }
                        }
                    }
                    StringBuilder sb = new StringBuilder();
                    foreach (var kv in dic) {
                        sb.Append(kv.Key).Append('=').AppendLine(kv.Value);
                    }
                    替换列表Box.Text = sb.ToString();
                }
            } catch (Exception ex) {
                消息框帮助.轻便消息(ex.Message, this, UINotifierType.WARNING);
            }
        }

        private void 返回Btn_Click(object sender, EventArgs e) {
            Close();
        }

        private void 确定Btn_Click(object sender, EventArgs e) {
            try {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                var 是否正则 = 替换列表row["匹配行为"].ToString() == "正则";
                string[] 替换列表arr = 替换列表Lines;
                for (int i = 0; i < 替换列表arr.Length; i++) {
                    string line = 替换列表arr[i];
                    if (!line.Contains("=")) {
                        throw new Exception("替换列表所有值必须为a=b(b可以不填)格式");
                    }
                    int index = line.IndexOf('=');
                    string[] arr = new string[] { line.Substring(0, index), line.Substring(index + 1) };
                    if (dic.ContainsKey(arr[0])) {
                        throw new Exception($"【{arr[0]}】已存在，不可重复");
                    } else {
                        if (是否正则) {
                            try {
                                Regex.IsMatch("", arr[1]);
                            } catch { throw new Exception($"第{i + 1}行不符合正则规则"); }
                        }
                        dic.Add(arr[0], arr[1]);
                    }
                }
                替换列表row["替换列表"] = JsonConvert.SerializeObject(dic);
                Close();
            } catch (Exception ex) {
                消息框帮助.轻便消息(ex.Message, this, UINotifierType.WARNING);
            }
        }
    }
}
