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
public partial class 跳转到 : 自定义Form {

    public int 所选数值 = -1;

    public 跳转到() {
        InitializeComponent();
        TopMost = true;
    }

    private void 返回Btn_Click(object sender, EventArgs e) {
        Close();
    }

    private void 确定Btn_Click(object sender, EventArgs e) {
        所选数值 = 页码Box.TextBox.IntValue;
        Close();
    }
}
