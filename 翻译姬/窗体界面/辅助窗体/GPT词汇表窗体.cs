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
            using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, false);
            using StreamReader sr = new StreamReader(fs, Encoding.Default);
            string t = sr.ReadLine();
            var dt = new DataTable();
            dt.Columns.Add("原文");
            dt.Columns.Add("译文");
            dt.Columns.Add("备注");
            while ((t = sr.ReadLine()) != null) {
                if (t.Trim() == "") {
                    continue;
                }
                var arr = t.Split('\t');
                if (arr.Length < 2) {
                    throw new Exception("csv格式异常，请从[数据处理]界面导出后修改再导入");
                }
                var dr = dt.NewRow();
                dr["原文"] = arr[0];
                dr["译文"] = arr[1];
                dr["备注"] = string.Join("\t", arr.Skip(2));
                dt.Rows.Add(dr);
            }
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
            var sb = new StringBuilder("原文,译文,备注\r\n");
            foreach (DataRow row in GPT设置数据.GPT词汇表.Rows) {
                sb.AppendLine($"{row["原文"]},{row["译文"]},{row["备注"]}");
            }
            using var fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write);
            using var sw = new StreamWriter(fs, Encoding.Default);
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
