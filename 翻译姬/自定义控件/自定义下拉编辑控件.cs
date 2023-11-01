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
public partial class 自定义下拉编辑控件 : UserControl {
    public 自定义下拉编辑控件() {
        InitializeComponent();
    }

    private void 自定义下拉编辑控件_Load(object sender, EventArgs e) {
        ComboBox.主题设置();
        ComboBox.DPI设置();
    }
}
