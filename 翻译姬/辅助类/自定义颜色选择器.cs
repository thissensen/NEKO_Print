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
public partial class 自定义颜色选择器 : 自定义Form {

    public bool 是否确认 = false;
    public Color 所选颜色;

    public 自定义颜色选择器(Color 所选颜色) {
        InitializeComponent();
        this.所选颜色 = 所选颜色;
        //edtA.DataBindings.Add("Text", ABar, "Value", false, DataSourceUpdateMode.OnPropertyChanged);
        edtR.DataBindings.Add("Text", RBar, "Value", false, DataSourceUpdateMode.OnPropertyChanged);
        edtG.DataBindings.Add("Text", GBar, "Value", false, DataSourceUpdateMode.OnPropertyChanged);
        edtB.DataBindings.Add("Text", BBar, "Value", false, DataSourceUpdateMode.OnPropertyChanged);
    }

    private void 自定义颜色选择器_Load(object sender, EventArgs e) {
    }

    private void 自定义颜色选择器_Shown(object sender, EventArgs e) {
        //edtA.IntValue = 所选颜色.A;
        edtR.IntValue = 所选颜色.R;
        edtG.IntValue = 所选颜色.G;
        edtB.IntValue = 所选颜色.B;
    }

    private void btnOK_Click(object sender, EventArgs e) {
        是否确认 = true;
        所选颜色 = 颜色框.FillColor;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e) {
        Close();
    }

    private void 自定义颜色选择器_Leave(object sender, EventArgs e) {
        Close();
    }

    private void 颜色变更_Changed(object sender, EventArgs e) {
        var color = Color.FromArgb(edtR.IntValue, edtG.IntValue, edtB.IntValue);
        颜色框.FillColor = color;
    }
}
