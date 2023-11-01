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
    public partial class Json指令集数值更改 : 自定义Form {

        private DataRow 指令row;
        private string[] 指令集Lines {
            get {
                return 指令集Box.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }
            set {
                指令集Box.Text = string.Join(Environment.NewLine, value);
            }
        }

        public Json指令集数值更改(DataRow 指令row) {
            InitializeComponent();
            this.指令row = 指令row;
        }

        private void Json指令集数值更改_Load(object sender, EventArgs e) {
            //显示位置屏幕居中
            var rect = Screen.GetWorkingArea(this);
            var p = new Point((rect.Width - Width) / 2, (rect.Height - Height) / 2);
            Location = p;

            string text = 指令row["指令集"].ToString();
            指令集Lines = text.Split('|');
        }

        private void 返回Btn_Click(object sender, EventArgs e) {
            Close();
        }

        private void 确定Btn_Click(object sender, EventArgs e) {
            指令row["指令集"] = string.Join("|", 指令集Lines);
            Close();
        }
    }
}
