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

namespace 翻译姬 {
    public partial class 表格增删改 : UserControl {

        private 数据库连接 数据库 => 全局数据.数据库;

        [Category("自定义"), Description("绑定的表格")]
        public 自定义DataGridView 表格 { get; set; }

        [Category("自定义"), Description("表格的主键名称")]
        public string 主键 { get; set; }

        [Category("自定义"), Description("表格对应表名")]
        public string 关联表名 { get; set; }

        [Category("自定义"), Description("表格当前所对应的SQL")]
        public string SQL { get; set; }

        /// <summary>
        /// 表格行是否在上下移动中
        /// </summary>
        public bool 表格行移动中 { get; set; }

        public delegate bool 表格增删改_新添前行验证(DataRow row);
        public delegate void 表格增删改_新添行后执行(DataRow row);
        public delegate bool 表格增删改_保存前验证();
        public delegate void 表格增删改_保存后执行();

        public event 表格增删改_新添前行验证 新添前行验证;
        public event 表格增删改_新添行后执行 新添行后执行;
        public event 表格增删改_保存前验证 保存前验证;
        public event 表格增删改_保存后执行 保存后执行;

        public 表格增删改() {
            InitializeComponent();
        }

        public void 新添Btn_Click(object sender, EventArgs e) {
            if (新添前行验证 != null && 表格.DataTable.Rows.Count > 0 && !新添前行验证.Invoke(表格.DataTable.Rows[表格.DataTable.Rows.Count - 1])) {
                return;
            }
            DataRow row = 表格.DataTable.NewRow();
            表格.DataTable.Rows.Add(row);
            新添行后执行?.Invoke(row);
        }

        public void 保存Btn_Click(object sender, EventArgs e) {
            if (保存前验证 != null && !保存前验证.Invoke()) {
                return;
            }
            表格.CurrentCell = null;
            //脱离DataGridView后再更新
            DataTable dt = 表格.DataTable;
            表格.DataTable = dt.Clone();
            数据库.Update(SQL, dt);
            //更新数据
            表格.DataTable = 数据库.Select(SQL);
            表格.DataTable.AcceptChanges();
            消息框帮助.轻便消息("保存成功", 表格);
            保存后执行?.Invoke();
        }

        public void 删除Btn_Click(object sender, EventArgs e) {
            表格.获取所在行()?.Delete();
            表格.CurrentCell = null;
        }

        public void 上移Btn_Click(object sender, EventArgs e) {
            try {
                //选中所在行
                int 所在行下标 = 表格.获取所在行下标();
                if (表格.Rows.Count == 0 || 所在行下标 == -1) {
                    return;
                }
                if (表格是否变动()) {
                    throw new Exception("请保存后再点击");
                }
                表格.CurrentCell = null;
                表格.Rows[所在行下标].Selected = true;
                if (所在行下标 == 0) {
                    return;
                }
                //数据变更
                //获取选中行及上方数据
                表格行移动中 = true;
                List<object[]> 行数据 = new List<object[]>();//除去主键
                for (int i = 所在行下标; i >= 0; i--) {
                    DataRow row = 表格.DataTable.Rows[i];
                    object[] data = new object[表格.DataTable.Columns.Count - 1];
                    int 已存下标 = 0;
                    for (int j = 0; j < row.Table.Columns.Count; j++) {
                        if (row.Table.Columns[j].ColumnName.Equals(主键, StringComparison.CurrentCultureIgnoreCase)) {
                            continue;
                        }
                        data[已存下标++] = row[j];
                    }
                    行数据.Add(data);
                }
                //上移一行
                object[] temp = 行数据[0];
                行数据[0] = 行数据[1];
                行数据[1] = temp;
                //数据覆盖
                int 行数据取值下标 = 0;
                for (int i = 所在行下标; i >= 0; i--) {
                    DataRow row = 表格.DataTable.Rows[i];
                    object[] 行数据data = 行数据[行数据取值下标++];
                    int 已取下标 = 0;
                    for (int j = 0; j < 表格.DataTable.Columns.Count; j++) {
                        if (row.Table.Columns[j].ColumnName.Equals(主键, StringComparison.CurrentCultureIgnoreCase)) {
                            continue;
                        }
                        //非主键赋值
                        row[j] = 行数据data[已取下标++];
                    }
                }
                //选中行变更
                表格.Rows[所在行下标].Selected = false;
                表格.Rows[所在行下标 - 1].Selected = true;
                //值刷新
                表格.列控件值刷新();
                //表格.CurrentCell = null;
            } catch (Exception ex) {
                消息框帮助.轻便消息(ex.Message, this, UINotifierType.WARNING);
            } finally {
                表格行移动中 = false;
            }
        }

        public void 下移Btn_Click(object sender, EventArgs e) {
            try {
                //选中所在行
                int 所在行下标 = 表格.获取所在行下标();
                if (表格.Rows.Count == 0 || 所在行下标 == -1) {
                    return;
                }
                if (表格是否变动()) {
                    throw new Exception("请保存后再点击");
                }
                表格.CurrentCell = null;
                表格.Rows[所在行下标].Selected = true;
                if (所在行下标 == 表格.DataTable.Rows.Count - 1) {
                    return;
                }
                //数据变更
                表格行移动中 = true;
                //获取选中行及下方数据
                List<object[]> 行数据 = new List<object[]>();//除去主键
                for (int i = 所在行下标; i < 表格.DataTable.Rows.Count; i++) {
                    DataRow row = 表格.DataTable.Rows[i];
                    object[] data = new object[表格.DataTable.Columns.Count - 1];
                    int 已存下标 = 0;
                    for (int j = 0; j < row.Table.Columns.Count; j++) {
                        if (row.Table.Columns[j].ColumnName.Equals(主键, StringComparison.CurrentCultureIgnoreCase)) {
                            continue;
                        }
                        data[已存下标++] = row[j];
                    }
                    行数据.Add(data);
                }
                //下移一行
                object[] temp = 行数据[0];
                行数据[0] = 行数据[1];
                行数据[1] = temp;
                //数据覆盖
                int 行数据取值下标 = 0;
                for (int i = 所在行下标; i < 表格.DataTable.Rows.Count; i++) {
                    DataRow row = 表格.DataTable.Rows[i];
                    object[] 行数据data = 行数据[行数据取值下标++];
                    int 已取下标 = 0;
                    for (int j = 0; j < 表格.DataTable.Columns.Count; j++) {
                        if (row.Table.Columns[j].ColumnName.Equals(主键, StringComparison.CurrentCultureIgnoreCase)) {
                            continue;
                        }
                        //非主键赋值
                        row[j] = 行数据data[已取下标++];
                    }
                }
                //选中行变更
                表格.Rows[所在行下标].Selected = false;
                表格.Rows[所在行下标 + 1].Selected = true;
                //值刷新
                表格.列控件值刷新();
                //表格.CurrentCell = null;
            } catch (Exception ex) {
                消息框帮助.轻便消息(ex.Message, this, UINotifierType.WARNING);
            } finally {
                表格行移动中 = false;
            }
        }

        public bool 表格是否变动() {
            foreach (DataRow row in 表格.DataTable.Rows) {
                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Deleted) {
                    return true;
                }
            }
            return false;
        }

    }
}
