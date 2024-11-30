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
public partial class GPT请求参数 : 自定义Form {

    private GPT设置数据 GPT设置数据 => 全局数据.GPT设置数据;

    public GPT请求参数() {
        InitializeComponent();
    }

    private void GPT请求参数_Load(object sender, EventArgs e) {
        try {
            frequency_penalty.DataBindings.Add("Text", GPT设置数据, "frequency_penalty", false, DataSourceUpdateMode.OnPropertyChanged);
            temperature.DataBindings.Add("Text", GPT设置数据, "temperature", false, DataSourceUpdateMode.OnPropertyChanged);
            top_p.DataBindings.Add("Text", GPT设置数据, "top_p", false, DataSourceUpdateMode.OnPropertyChanged);
        } catch (Exception ex) {
            MessageBoxEx.Show(ex.Message);
        }
    }

    private void 默认Btn_Click(object sender, EventArgs e) {
        frequency_penalty.Text = "0.3";
        temperature.Text = "0.4";
        top_p.Text = "0.95";
    }

    private void 精准Btn_Click(object sender, EventArgs e) {
        frequency_penalty.Text = "0.1";
        temperature.Text = "0.1";
        top_p.Text = "0.8";
    }

    private void 确定Btn_Click(object sender, EventArgs e) {
        Close();
    }
}
