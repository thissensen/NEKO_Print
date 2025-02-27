using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace 翻译姬 {
    public partial class Json指令 : 自定义Page {

        protected virtual string 关联表 => "Json指令";

        protected virtual Type 指令集数值更改窗 => typeof(Json指令集数值更改);

        protected string[] 指令集Lines {
            get {
                return 指令集Box.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }
            set {
                指令集Box.Text = string.Join(Environment.NewLine, value);
            }
        }

        public Json指令() {
            InitializeComponent();
            提取结果Box.TextBox.ShowScrollBar = true;
            查询表格.禁用排序();
            指令表格.禁用排序();
        }

        private void Json指令_Page被选中() {
            //查询表格.CurrentCell = null;
        }

        private void Json指令_Load(object sender, EventArgs e) {
            if (DesignMode) {
                return;
            }
            string sql = "select * from " + 关联表;
            查询表格.DataTable = 数据库.Select(sql);
            表格增删改.SQL = sql;
            //指令表初始化
            DataTable dt = new DataTable();
            dt.Columns.Add("指令");
            dt.Columns.Add("内容");
            指令表格.DataTable = dt;
        }

        private void Json指令_Shown(object sender, EventArgs e) {
            查询表格.CurrentCell = null;
            //不序列化
            筛选Box.Text = "";
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
                    } else {
                        消息框帮助.轻便消息($"识别失败", 查询表格);
                    }
                }
            } catch (Exception ex) {
                MessageBoxEx.Show(ex.Message);
            }
            return base.ProcessCmdKey(ref msg, keyData);
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
                var res = from row in 查询表格.DataTable.AsEnumerable()
                          where row.RowState != DataRowState.Deleted
                          group row by row["名称"].ToString() into g
                          where g.Count() > 1
                          select g.Key;
                if (res.Count() > 0) {
                    throw new Exception($"名称【{string.Join(",", res)}】重复");
                }
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
            if (row[名称.DataPropertyName].ToString().Trim().IsNullOrEmpty()) {
                throw new Exception("名称未填写");
            }
            if (row[指令集.DataPropertyName].ToString().Trim().IsNullOrEmpty()) {
                throw new Exception("指令集未填写");
            }
        }

        protected virtual void 选择JsonBtn_Click(object sender, EventArgs e) {
            string[] paths = 工具类.选择文件("请选择Json文件", "Json文件", "*.txt;*.json");
            if (paths.Length == 0) {
                return;
            }
            try {
                string json = File.ReadAllText(paths[0], Encoding.GetEncoding(Util.文本编码识别(paths[0])));
                JToken token;
                try {
                    token = JToken.Parse(json);
                } catch { throw new Exception("文本非Json格式"); }
                指令表格.Tag = json;
                JsonReader jr = token.CreateReader();
                DataTable dt = 指令表格.DataTable.Clone();
                while (jr.Read()) {
                    if (jr.TokenType == JsonToken.String && jr.Value != null) {
                        DataRow row = dt.NewRow();
                        row["指令"] = jr.Path;
                        row["内容"] = jr.Value.ToString();
                        dt.Rows.Add(row);
                    }
                }
                指令表格.DataTable = dt;
            } catch (Exception ex) {
                消息框帮助.轻便消息(ex.Message, 查询表格, UINotifierType.WARNING);
            }
        }

        protected virtual void 指令表格_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) {
                return;
            }
            string 指令 = 指令表格.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            //转换通用指令
            int count = Regex.Matches(指令, @"\[\d+]").Count - 1;
            if (!指令.EndsWith("]")) {//非数组选择全提取
                count++;
            }
            string 通用指令 = new Regex(@"\[\d+]").Replace(指令, "[*]", count);
            if (!指令集Lines.Contains(通用指令)) {
                指令集Lines = 指令集Lines.AddRange(通用指令);
            }
        }

        private void 筛选Box_KeyPress(object sender, KeyPressEventArgs e) {
            if (筛选Box.Text.IsNullOrEmpty()) {
                指令表格.DataTable.DefaultView.RowFilter = null;
            } else 
                指令表格.DataTable.DefaultView.RowFilter = $"内容='{筛选Box.Text}'";
            }

        private void 筛选Box_TextChanged(object sender, EventArgs e) {
            if (!IsShown) {
                return;
            }
            if (筛选Box.Text.IsNullOrEmpty()) {
                指令表格.DataTable.DefaultView.RowFilter = null;
            } else {
                指令表格.DataTable.DefaultView.RowFilter = $"内容 like '%{筛选Box.Text}%'";
            }
        }

        protected virtual void 提取Btn_Click(object sender, EventArgs e) {
            try {
                if (指令表格.DataTable.Rows.Count == 0) {
                    throw new Exception("请先选择Json文件");
                }
                if (指令集Box.Text.IsNullOrEmpty()) {
                    throw new Exception("指令集未填写");
                }
                DataRow json指令row = 查询表格.DataTable.Clone().NewRow();
                json指令row["指令集"] = string.Join("|", 指令集Lines);
                文本[] res = 文本读写.Json提取(指令表格.Tag.ToString(), json指令row);
                提取结果Box.Text = string.Join(Environment.NewLine, res.获取原文组());

            } catch(Exception ex) {
                消息框帮助.轻便消息(ex.Message, 查询表格, UINotifierType.WARNING);
            }
        }

        private void 查询表格_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) {
                return;
            } else if (查询表格.Columns[e.ColumnIndex].HeaderText == 指令集.HeaderText) { 
                //点到了指令集
                DataRow row = (查询表格.Rows[e.RowIndex].DataBoundItem as DataRowView).Row;
                var f = Activator.CreateInstance(指令集数值更改窗, row) as Form;
                f.ShowDialog();
            } else {
                //点到了其他
                DataRow row = (查询表格.Rows[e.RowIndex].DataBoundItem as DataRowView).Row;
                指令集Lines = row["指令集"].ToString().Split('|');
            }
        }
    }
}
