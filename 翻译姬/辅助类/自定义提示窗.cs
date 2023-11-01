using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬 {
    public partial class 自定义提示窗 : 自定义Form {

        public bool 是否确认 = false;
        private string 提示内容;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "PostMessage")]
        private extern static void PostMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        //Panel控件,鼠标点击时移动窗口位置,MouseDown事件
        private void 移动窗口_MouseDown(object sender, MouseEventArgs e) { ReleaseCapture(); PostMessage(this.Handle, 0x112, 0xf012, 0); }

        public 自定义提示窗(string 内容, string 标题, 提示窗按钮 显示按钮) {
            InitializeComponent();
            提示内容 = 内容;
            标题栏Panel.Text = 标题;
            if (显示按钮 == 提示窗按钮.确认) {
                确认Btn.Width = 取消Btn.Location.X + 取消Btn.Width - 确认Btn.Location.X;
                取消Btn.Visible = false;
            }
        }

        private void 自定义提示窗_Load(object sender, EventArgs e) {
            //显示位置屏幕居中
            var rect = Screen.GetWorkingArea(this);
            var p = new Point((rect.Width - Width) / 2, (rect.Height - Height) / 2);
            Location = p;
        }

        private void 自定义提示窗_Shown(object sender, EventArgs e) {
            确认Btn.Select();
            /*//计算一行显示多少字
            int 文字宽度 = TextRenderer.MeasureText(提示内容, 文字区Panel.Font).Width;
            if (文字宽度 > 文字区Panel.Width) {
                string[] arr = Regex.Split(提示内容.Trim(), Environment.NewLine);
                decimal 已用长度 = 0;
                List<string> 文本行 = new List<string>();
                string temp = null;
                foreach (string str in arr) {
                    foreach (char c in str) {
                        decimal 所需长度 = TextRenderer.MeasureText(c.ToString(), 文字区Panel.Font).Width / 1.3m;
                        temp += c;
                        if (所需长度 + 已用长度 > 文字区Panel.Width) {
                            //换行
                            文本行.Add(temp);
                            temp = null;
                            已用长度 = 0;
                        } else {
                            已用长度 += 所需长度;
                        }
                    }
                    if (temp != null) {
                        文本行.Add(temp);
                        temp = null;
                        已用长度 = 0;
                    }
                }
                文字区Panel.Text = string.Join(Environment.NewLine, 文本行);
            } else {
                文字区Panel.Text = 提示内容;
            }*/
            lbMsg.Text = 提示内容;
        }

        private void 确认Btn_Click(object sender, EventArgs e) {
            是否确认 = true;
            Close();
        }

        private void 取消Btn_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
