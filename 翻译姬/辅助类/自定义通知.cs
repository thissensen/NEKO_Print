using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬 {
    public partial class 自定义通知 : 自定义Form {
        public 自定义通知(string 文本, string 标题) {
            InitializeComponent();
            文字区Label.Text = 文本;
            标题栏Panel.Text = 标题;
        }

        private void 自定义通知_Shown(object sender, EventArgs e) {
            //显示在右下角
            int x = Screen.PrimaryScreen.WorkingArea.Size.Width - Width;
            int y = Screen.PrimaryScreen.WorkingArea.Size.Height - Height;
            Point p = new Point(x, y);
            this.PointToScreen(p);
            this.Location = p;
            TopMost = true;
            TopMost = false;
            定时器.Enabled = true;
        }

        private void 定时器_Tick(object sender, EventArgs e) {
            Close();
        }
    }
}
