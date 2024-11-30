using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬;
public partial class 数组数据数值更改 : 自定义Form {

    public string[] 数据 { get; set; }

    public 数组数据数值更改() {
        InitializeComponent();
        Load += 数组数据数值更改_Load;
    }

    private void 数组数据数值更改_Load(object sender, EventArgs e) {
        //显示位置屏幕居中
        var rect = Screen.GetWorkingArea(this);
        var p = new Point((rect.Width - Width) / 2, (rect.Height - Height) / 2);
        Location = p;

        数据Box.Text = string.Join("\r\n", 数据);
    }

    private void 确定Btn_Click(object sender, EventArgs e) {
        数据 = 数据Box.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        Close();
    }

    private void 返回Btn_Click(object sender, EventArgs e) {
        Close();
    }
}
