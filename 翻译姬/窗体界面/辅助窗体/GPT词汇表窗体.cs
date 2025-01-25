using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬;
public partial class GPT词汇表窗体 : 自定义Form {

    private static GPT设置数据 GPT设置数据 => 全局数据.GPT设置数据;

    public GPT词汇表窗体() {
        InitializeComponent();
        this.窗体相对屏幕居中();
    }

    private void GPT词汇表窗体_Load(object sender, EventArgs e) {
        if (DesignMode) {
            return;
        }
        词汇表.DataTable = GPT设置数据.GPT词汇表;
    }

    private void 导入Btn_Click(object sender, EventArgs e) {
        try {
            var path = 工具类.选择文件("选择词汇表", "csv", "*.csv", 全局字符串.桌面路径).ElementAtOrDefault(0);
            if (path == null) {
                return;
            }
            var dt = Util.词汇表读取(path);
            词汇表.DataTable = dt;
            GPT设置数据.GPT词汇表 = dt;
        } catch (Exception ex) {
            MessageBoxEx.Show(ex.Message);
        }
    }

    private void 导出Btn_Click(object sender, EventArgs e) {
        try {
            string file = 工具类.选择保存目录("人名导出", 全局字符串.桌面路径 + "词汇表.csv", "CSV", "*.csv");
            if (file == null) {
                return;
            }
            var sb = new StringBuilder("原文\t译文\t备注\r\n");
            foreach (DataRow row in GPT设置数据.GPT词汇表.Rows) {
                sb.AppendLine($"{row["原文"]}\t{row["译文"]}\t{row["备注"]}");
            }
            using var fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write);
            using var sw = new StreamWriter(fs, Encoding.UTF8);
            sw.Write(sb.ToString());
            sw.Flush();
            消息框帮助.轻便消息("导出成功", this);
        } catch (Exception ex) {
            MessageBoxEx.Show(ex.Message);
        }
    }

    private void 清空Btn_Click(object sender, EventArgs e) {
        try {
            if (!MessageBoxEx.Show("确认清空？", 显示按钮: 提示窗按钮.确认取消)){
                return;
            }
            词汇表.DataTable.Rows.Clear();
            GPT设置数据.GPT词汇表.Rows.Clear();
        } catch (Exception ex) {
            MessageBoxEx.Show(ex.Message);
        }
    }

    private void 返回Btn_Click(object sender, EventArgs e) {
        Close();
    }
}
