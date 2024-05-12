using Sunny.UI;
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
public partial class GPT : 通用API窗体 {

    public override Dictionary<string, int> QPS数据 => new Dictionary<string, int> {
        ["自定义"] = 0
    };

    public GPT() {
        InitializeComponent();
        QPS.ReadOnly = true;
        KEY.Visible = false;
        秘钥.Width += KEY.Width;
        秘钥.HeaderText = "令牌";
        可用额度.ReadOnly = true;
    }
    protected override void QPS列数据变化(DataGridViewRow row, int 值) {
        if (row == null) {
            return;
        }
        row.Cells[可用额度.DataPropertyName].Value = 0;
    }
    protected override void 前往注册Btn_Click(object sender, EventArgs e) {
        MessageBoxEx.Show("GPT注册请自行解决");
    }
    protected override void 检测可用Btn_Click(object sender, EventArgs e) {
        //MessageBoxEx.Show("GPT无法检测可用状态");
    }
    protected override void 是否启用列数据变化(UISwitch sw, DataRow row) {
        表格增删改.保存Btn.PerformClick();
    }
}
