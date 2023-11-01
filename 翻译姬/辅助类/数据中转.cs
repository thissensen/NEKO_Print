using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬 {
    public static class 数据中转 {

        public static Form 主窗体 { get; set; }
        public static UIProcessBar 进度条 { get; set; }
        public static UITextBox 文本显示Box { get; set; }
        public static UILabel 实际使用字符Label { get; set; }

        public static void 文本显示Append(string text) {
            文本显示Box?.BeginInvoke(new Action(() => {
                文本显示Box.AppendText(text);
            }));
        }
        public static void 文本显示AppendLine(string text) {
            文本显示Append(text + Environment.NewLine);
        }
        public static void 进度条最大值设定(int 最大值) {
            进度条.Invoke(new Action(() => {
                进度条.Maximum = 最大值;
            }));
        }
        public static void 进度条当前值设定(int 当前值) {
            进度条.Invoke(new Action(() => {
                进度条.Value = 当前值;
            }));
        }
        public static void 进度条当前值增加(int 增加值) {
            进度条.BeginInvoke(new Action(() => {
                进度条.Value += 增加值;
            }));
        }
        public static void 使用字符增加(int 增加值) {
            实际使用字符Label.BeginInvoke(new Action(() => {
                int 当前数 = int.Parse(实际使用字符Label.Text.Substring(实际使用字符Label.Text.IndexOf("：") + 1));
                实际使用字符Label.Text = $"实际使用字符：{当前数 + 增加值}";
            }));
        }
    }
}
