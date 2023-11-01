using Sunny.UI;
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

namespace 翻译姬 {
    public partial class 正则设置 : 自定义Page {

        private string[] 待机翻Lines {
            get {
                return 待机翻Box.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }
            set {
                待机翻Box.Text = string.Join(Environment.NewLine, value);
            }
        }

        private string[] 机翻后Lines {
            get {
                return 机翻后Box.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }
            set {
                机翻后Box.Text = string.Join(Environment.NewLine, value);
            }
        }

        public 正则设置() {
            InitializeComponent();
            查询表格.禁用排序();
            待机翻Box.TextBox.ShowScrollBar = true;
            机翻后Box.TextBox.ShowScrollBar = true;
        }

        private void 正则设置_Page被选中() {
            查询表格.CurrentCell = null;
        }

        private void 正则设置_Load(object sender, EventArgs e) {
            Name_Control.Remove(提示内容Box.Name);
            string sql = "select * from 正则";
            查询表格.DataTable = 数据库.Select(sql);
            表格增删改.SQL = sql;
        }

        private void 正则设置_Shown(object sender, EventArgs e) {
            查询表格.CurrentCell = null;
        }

        private bool 表格增删改_新添前行验证(DataRow row) {
            try {
                行验证(row);
            } catch (Exception ex) {
                消息框帮助.轻便消息(ex.Message, 查询表格, UINotifierType.WARNING);
                return false;
            }
            return true;
        }

        private bool 表格增删改_保存前验证() {
            try {
                foreach (DataRow row in 查询表格.DataTable.Rows) {
                    行验证(row);
                }
            } catch (Exception ex) {
                消息框帮助.轻便消息(ex.Message, 查询表格, UINotifierType.WARNING);
                return false;
            }
            return true;
        }

        private void 行验证(DataRow row) {
            if (row.RowState == DataRowState.Deleted) {
                return;
            }
            if (row[正则名称.DataPropertyName].ToString().Trim().IsNullOrEmpty()) {
                throw new Exception("正则名称未填写");
            }
            /*if (row[行过滤正则.DataPropertyName].ToString().IsNullOrEmpty() &&
                row[文本过滤正则.DataPropertyName].ToString().IsNullOrEmpty() &&
                row[提取前行过滤正则.DataPropertyName].ToString().IsNullOrEmpty() &&
                row[提取型正则.DataPropertyName].ToString().IsNullOrEmpty()
                ) {
                throw new Exception("正则未填写");
            }*/
            for (int i = 0; i < 查询表格.Columns.Count; i++) {
                string 列名 = 查询表格.Columns[i].HeaderText;
                if (!列名.EndsWith("正则")) {
                    continue;
                }
                string 正则 = row[i].ToString();
                try {
                    Regex.Match("", 正则);
                } catch {
                    throw new Exception($"【{列名}】正则格式有误");
                }
            }
        }

        private void 查询表格_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) {
                return;
            }
            DataRow row = 查询表格.获取所在行();
            行过滤正则Box.Text = row[行过滤正则.DataPropertyName].ToString();
            文本过滤正则Box.Text = row[文本过滤正则.DataPropertyName].ToString();
            提取前行过滤正则Box.Text = row[提取前行过滤正则.DataPropertyName].ToString();
            提取型正则Box.Text = row[提取型正则.DataPropertyName].ToString();
        }

        private void 预览Btn_Click(object sender, EventArgs e) {
            try {
                if (待机翻Box.Text.Trim().IsNullOrEmpty()) {
                    消息框帮助.轻便消息("请在右上窗口填入测试文本", this, UINotifierType.WARNING);
                    return;
                }
                DataRow 正则row = 查询表格.DataTable.Clone().NewRow();
                正则row["行过滤正则"] = 行过滤正则Box.Text;
                正则row["文本过滤正则"] = 文本过滤正则Box.Text;
                正则row["提取前行过滤正则"] = 提取前行过滤正则Box.Text;
                正则row["提取型正则"] = 提取型正则Box.Text;

                string[] 提取文本 = 正则读写.正则文本提取(待机翻Lines, 正则row);
                string[] 机翻后 = 工具类.文本置换机翻(提取文本);
                机翻后Lines = 正则读写.正则文本写入(待机翻Lines, 机翻后, 正则row);

            } catch (Exception ex) {
                消息框帮助.轻便消息(ex.Message, this, UINotifierType.WARNING);
            }

        }
    }
}
