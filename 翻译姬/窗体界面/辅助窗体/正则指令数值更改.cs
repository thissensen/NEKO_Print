using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬;
public partial class 正则指令数值更改 : 自定义Form {

    private DataRow row;
    private string 列名;
    private string[] 指令集Lines {
        get {
            return 指令集Box.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }
        set {
            指令集Box.Text = string.Join(Environment.NewLine, value);
        }
    }

    public 正则指令数值更改(DataRow row, string 列名) {
        InitializeComponent();
        this.row = row;
        this.列名 = 列名;
    }

    private void 正则指令数值更改_Load(object sender, EventArgs e) {
        //显示位置屏幕居中
        var rect = Screen.GetWorkingArea(this);
        var p = new Point((rect.Width - Width) / 2, (rect.Height - Height) / 2);
        Location = p;

        var text = row[列名].ToString();
        指令集Lines = 工具类.正则分割(text); 
    }

    private void 返回Btn_Click(object sender, EventArgs e) {
        Close();
    }

    private void 确定Btn_Click(object sender, EventArgs e) {
        try {
            string[] arr = 指令集Lines;
            for (int i = 0; i < arr.Length; i++) {
                try {
                    new Regex(arr[i]);
                } catch {
                    throw new Exception($"第{i + 1}行正则错误");
                }
            }
            row[列名] = string.Join("|", 指令集Lines);
            Close();
        } catch (Exception ex) {
            MessageBoxEx.Show(ex.Message);
        }
    }

}
