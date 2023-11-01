using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬 {
    public partial class 通用API窗体 : 自定义Page {

        public string API类型 => GetType().Name;
        public Dictionary<string, int> QPS数据  = new Dictionary<string, int>();

        public 通用API窗体() {
            InitializeComponent();
            QPS.DisplayMember = "Key";
            QPS.ValueMember = "Value";
            查询表格.禁用排序();
            查询表格.CellEndEdit += 查询表格_CellEndEdit;
        }

        private void 查询表格_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            string 列名 = 查询表格.Columns[e.ColumnIndex].DataPropertyName;
            if (列名 != "已用额度" && 列名 != "可用额度") {
                return;
            }
            int.TryParse(查询表格.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString(), out int num);
            if (num < 0) {
                num = 0;
            }
            查询表格.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = num;
        }

        private void 查询表格_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            if (查询表格.Columns[e.ColumnIndex].DataPropertyName == "已用额度") {
                MessageBoxEx.Show("请填写正确的数量");
                查询表格.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
            } else if (查询表格.Columns[e.ColumnIndex].DataPropertyName == "可用额度") {
                MessageBoxEx.Show("请填写正确的数量,设置无限，请输入-1");
                查询表格.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
            }
        }

        private void 通用API窗体_Page被选中() {
            string sql = $"select * from API明细 where 类型='{API类型}'";
            表格增删改.SQL = sql;
            if (CanSelect) {
                查询表格.DataTable = 数据库.Select(sql);
            } else {
                BeginInvoke(new Action(() => {//异步，使控件正常显示
                    查询表格.DataTable = 数据库.Select(sql);
                }));
            }
        }

        private void 通用API窗体_Load(object sender, EventArgs e) {
            if (DesignMode) {
                return;
            }
            QPS_bs.DataSource = QPS数据;
            通用API窗体_Page被选中();
        }

        private void 通用API窗体_Shown(object sender, EventArgs e) {
            if (DesignMode) {
                return;
            }
            查询表格.添加列控件("是否启用", typeof(列控件Switch));
            查询表格.列控件初始化();
        }

        private void 查询表格_列控件生成(string 列名, 列控件 列控件) {
            UISwitch sw = 列控件 as UISwitch;
            sw.ActiveChanged += (s, e) => {
                DataRow row = (查询表格.Rows[列控件.Cell.RowIndex].DataBoundItem as DataRowView).Row;
                是否启用列数据变化(sw, row);
            };
        }

        private void 表格增删改_新添行后执行(DataRow row) {
            row["类型"] = API类型;
            row["是否启用"] = false;
            row["QPS"] = 0;
            row["已用额度"] = 0;
            row["可用额度"] = 0;
            QPS列数据变化(查询表格.Rows[查询表格.Rows.Count - 1], 0);
        }

        private void 查询表格_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
            if (查询表格.CurrentCell == null) {
                return;
            }
            if (查询表格.Columns[查询表格.CurrentCell.ColumnIndex].HeaderText != QPS.HeaderText) {
                return;
            }
            int 表格值 = int.Parse(查询表格.CurrentCell.Value.ToString());
            //得到QPS的ComboBox单元格，共用同一个ComboBox
            var cb = (e.Control as DataGridViewComboBoxEditingControlEx).ComboBox;
            cb.DisplayMember = "key";
            cb.ValueMember = "value";
            cb.DataSource = QPS_bs;
            cb.SelectedValue = 表格值;
            cb.SelectedIndexChanged += QPS_SelectedIndexChanged;
            cb.Leave += (s, e2) => cb.SelectedValueChanged -= QPS_SelectedIndexChanged;
        }

        private void QPS_SelectedIndexChanged(object sender, EventArgs e) {
            if (查询表格.CurrentCell == null) {
                return;
            }
            var cb = sender as 自定义ComboBox;
            int num = int.Parse(cb.SelectedValue.ToString());
            QPS列数据变化(查询表格.CurrentCell.OwningRow, num);
        }

        protected virtual void 检测可用Btn_Click(object sender, EventArgs e) {
            DataRow 主表row = 全局数据.API主表.Select($"类型='{API类型}'")[0];
            int 成功次数 = 0;
            API接口模板.是否更新字符数 = false;
            foreach (DataRow row in 查询表格.DataTable.Rows) {
                if (检测可用(row, 主表row)) {
                    成功次数++;
                }
            }
            if (成功次数 > 0) {
                表格增删改.保存Btn.PerformClick();
            }
            API接口模板.是否更新字符数 = true;
        }

        protected virtual void 前往注册Btn_Click(object sender, EventArgs e) {
            try {
                string 地址 = 全局数据.API主表.Select($"类型='{API类型}'")[0]["注册地址"].ToString();
                Process.Start(地址);
            } catch { }
        }

        protected virtual void 是否启用列数据变化(UISwitch sw, DataRow row) {
            if (!sw.Active) {
                return;
            }
            if (表格增删改.表格行移动中) {
                return;
            }
            API接口模板.是否更新字符数 = false;
            DataRow 主表row = 全局数据.API主表.Select($"类型='{API类型}'")[0];
            bool flag = 检测可用(row, 主表row, sw);
            if (flag) {
                表格增删改.保存Btn.PerformClick();
            }
            API接口模板.是否更新字符数 = true;
        }

        private bool 检测可用(DataRow row, DataRow 主表row, UISwitch sw = null) {
            //启用，检测可用
            Type type = 文件结构.获取API类型(API类型);
            int 已用 = int.Parse(row["已用额度"].ToString());
            API信息 data = API信息.Parse(row);
            data.源语言 = 主表row["英语"].ToString();
            data.目标语言 = 主表row["简中"].ToString();
            API接口模板 api = Activator.CreateInstance(type, data) as API接口模板;
            try {
                api.文本机翻(new string[] { "Hi" });
                row["是否启用"] = true;
                row["已用额度"] = 已用 + 2;
                return true;
            } catch (Exception_API异常 api_ex) {
                try {
                    数据库.Execute($"update API明细 set 是否启用=0 where ID={row["ID"]}");
                } catch { }
                row["是否启用"] = false;
                if (sw != null) {
                    sw.Active = false;
                }
                消息框帮助.轻便消息(api_ex.Message, this, UINotifierType.WARNING);
            } catch (Exception ex) {
                消息框帮助.轻便消息(ex.Message, this, UINotifierType.WARNING);
            }
            return false;
        }

        protected virtual void QPS列数据变化(DataGridViewRow row, int 值) {

        }
    }
}
