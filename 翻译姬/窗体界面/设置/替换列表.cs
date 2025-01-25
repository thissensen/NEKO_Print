using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace 翻译姬 {
    public partial class 替换列表 : 自定义Page {
        public 替换列表() {
            InitializeComponent();
            查询表格.禁用排序();
            查询表格.RowHeadersVisible = false;
        }

        private void 替换列表_Page被选中() {
            //查询表格.CurrentCell = null;
            string sql = $"select * from 替换列表";
            表格增删改.SQL = sql;
            if (CanSelect) {
                查询表格.DataTable = 数据库.Select(sql);
            } else {
                BeginInvoke(new Action(() => {//异步，使控件正常显示
                    查询表格.DataTable = 数据库.Select(sql);
                }));
            }
        }

        private void 替换列表_Load(object sender, EventArgs e) {
            替换时机.DataSource = new List<string>() { "机翻前", "机翻后" };
            匹配行为.DataSource = new List<string>() { "包含", "正则", "全字" };
            替换列表_Page被选中();
        }

        private void 替换列表_Shown(object sender, EventArgs e) {
            查询表格.CurrentCell = null;
            查询表格.添加列控件("是否启用", typeof(列控件Switch));
            查询表格.列控件初始化();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            try {
                if (全局字符串.键盘按下按钮组.Count == 2 &&
                    全局字符串.键盘按下按钮组.Contains(Keys.LShiftKey) &&
                    全局字符串.键盘按下按钮组.Contains(Keys.C)) {//复制

                    Focus();
                    查询表格.EndEdit();
                    var row = 查询表格.获取所在行();
                    if (row == null) {
                        消息框帮助.轻便消息("光标选中具体行再复制", 查询表格, UINotifierType.WARNING);
                    } else {
                        Clipboard.SetText(配置文件操作.常规写出(row));
                        消息框帮助.轻便消息($"已复制[{row["名称"]}]", 查询表格);
                    }
                } else if (全局字符串.键盘按下按钮组.Count == 2 &&
                    全局字符串.键盘按下按钮组.Contains(Keys.LShiftKey) &&
                    全局字符串.键盘按下按钮组.Contains(Keys.V)) {//粘贴

                    Focus();
                    查询表格.EndEdit();
                    string text = Clipboard.GetText(TextDataFormat.Text);
                    string[] arr = text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    var rows = 配置文件操作.常规读取(查询表格.DataTable, arr);
                    if (rows.Length > 0) {
                        消息框帮助.轻便消息($"已读取[{string.Join(",", rows.Select(r => r["名称"]))}]", 查询表格);
                    }
                }
            } catch (Exception ex) {
                MessageBoxEx.Show(ex.Message);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void 查询表格_DataError(object sender, DataGridViewDataErrorEventArgs e) {
        }

        private void 查询表格_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
            if (查询表格.CurrentCell == null) {
                return;
            }
            string 表格值 = 查询表格.CurrentCell.Value.ToString();
            if (查询表格.Columns[查询表格.CurrentCell.ColumnIndex].HeaderText == "替换时机") {
                var cb = (e.Control as DataGridViewComboBoxEditingControlEx).ComboBox;
                cb.DataSource = new List<string>() { "机翻前", "机翻后" };
                cb.SelectedItem = 表格值;
            } else if (查询表格.Columns[查询表格.CurrentCell.ColumnIndex].HeaderText == "匹配行为") {
                var cb = (e.Control as DataGridViewComboBoxEditingControlEx).ComboBox;
                cb.DataSource = new List<string>() { "包含", "正则", "全字" };
                cb.SelectedItem = 表格值;
            }
        }

        private void 表格增删改_新添行后执行(DataRow row) {
            row["是否启用"] = false;
            row["替换时机"] = "机翻前";
            row["匹配行为"] = "包含";
            row["替换列表"] = "{}";
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
            if (row["名称"].ToString().Trim().IsNullOrEmpty()) {
                throw new Exception("名称未填写");
            }
            if (row["替换列表"].ToString() == "{}") {
                throw new Exception("替换列表未填写");
            }
        }

        private void 查询表格_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || 查询表格.Columns[e.ColumnIndex].HeaderText != 替换列表Col.HeaderText) {
                return;
            }
            DataRow row = (查询表格.Rows[e.RowIndex].DataBoundItem as DataRowView).Row;
            //弹出替换列表修改窗
            替换列表数值更改 f = new 替换列表数值更改(row);
            f.ShowDialog();
        }
    }
}
