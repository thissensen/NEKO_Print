using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬 {

    /// <summary>
    /// 
    /// 可能BUG：
    /// 进行Filter后，获取显示表，index下标
    /// </summary>
    public static class DataGridView拓展 {

        #region 表格设定
        public static void 初始化列(this DataGridView view, DataTable dt) {
            for (int i = 0; i < dt.Columns.Count; i++) {
                //已存在过滤
                if (view.Columns.Contains(dt.Columns[i].ColumnName)) {
                    continue;
                }
                DataGridViewColumn data;
                if (dt.Columns[i].DataType == typeof(byte[])) {
                    data = new DataGridViewImageColumn();
                    ((DataGridViewImageColumn)data).ImageLayout = DataGridViewImageCellLayout.Zoom;
                } else {
                    data = new DataGridViewTextBoxColumn();
                }
                data.DataPropertyName = dt.Columns[i].ColumnName;
                data.HeaderText = dt.Columns[i].ColumnName;
                data.Name = dt.Columns[i].ColumnName;
                if (dt.Columns[i].DataType == typeof(DateTime)) {
                    data.DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                }
                view.Columns.Add(data);
            }
        }
        public static void 禁用排序(this DataGridView view) {
            for (int i = 0; i < view.Columns.Count; i++) {
                view.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                view.Columns[i].Resizable = DataGridViewTriState.False;
            }
        }
        public static bool ContainsHeaderText(this DataGridView view, string HeaderText) {
            foreach (DataGridViewColumn col in view.Columns) {
                if (col.HeaderText == HeaderText) {
                    return true;
                }
            }
            return false;
        }
        public static void 取消选中(this DataGridView view) {
            for (int i = 0; i < view.Rows.Count; i++) {
                view.Rows[i].Selected = false;
            }
        }
        public static int[] 获取列宽(this DataGridView view) {
            List<int> res = new List<int>();
            for (int i = 0; i < view.Columns.Count; i++) {
                res.Add(view.Columns[i].Width);
            }
            return res.ToArray();
        }
        public static void 设置列宽(this DataGridView view, int[] 列宽) {
            if (列宽 == null) {
                return;
            }
            for (int i = 0; i < view.Columns.Count; i++) {
                if (i < 列宽.Length) {
                    view.Columns[i].Width = 列宽[i];
                }
            }
        }
        public static void 屏蔽列(this DataGridView view, string[] 列名) {
            if (列名 == null) {
                return;
            }
            for (int i = 0; i < view.Columns.Count; i++) {
                if (列名.Contains(view.Columns[i].HeaderText)) {
                    view.Columns[i].Visible = false;
                }
            }
        }
        public static void 显示所有列(this DataGridView view) {
            for (int i = 0; i < view.Columns.Count; i++) {
                view.Columns[i].Visible = true;
            }
        }
        public static void 表格固定列(this DataGridView view, int index) {
            if (view.Columns.Count - 1 < index) {
                view.Columns[index].Frozen = true;
            }
        }
        public static int 获取列下标(this DataGridView view, string 列名) {
            for (int i = 0; i < view.Columns.Count; i++) {
                if (view.Columns[i].HeaderText == 列名) {
                    return i;
                }
            }
            return -1;
        }
        #endregion

        /**
        * 功能列表：
        * 获取行
        * 获取所选行，获取所选行下标
        * 获取所选多行，获取所选多行下标
        * 获取所在行，获取所在行下标
        * 获取所在多行，获取所在多行下标
        */
        #region 获取数据(内部数据DataTable)
        public static int 获取行所在下标(this DataGridView view, DataRow row) {
            for (int i = 0; i < view.Rows.Count; i++) {
                if (view.获取行(i) == row) {
                    return i;
                }
            }
            return -1;
        }
        public static DataRow 获取行(this DataGridView view, int index) {
            try {
                if (index > view.Rows.Count - 1) {
                    return null;
                }
                return (view.Rows[index].DataBoundItem as DataRowView)?.Row;
            } catch { return null; }
        }
        public static DataRow 获取所选行(this DataGridView view) {
            return 获取所选多行(view)?.ElementAtOrDefault(0);
        }
        public static int 获取所选行下标(this DataGridView view) {
            var res = 获取所选多行下标(view);
            if (res.Count() == 0) {
                return -1;
            } else {
                return res.ElementAt(0);
            }
        }
        public static IEnumerable<DataRow> 获取所选多行(this DataGridView view) {
            DataGridViewSelectedRowCollection rows = view.SelectedRows;
            if (rows == null || rows.Count == 0) {
                yield break;
            }
            foreach (DataGridViewRow row in rows) {
                yield return 获取行(view, row.Index);
            }
        }
        public static IEnumerable<int> 获取所选多行下标(this DataGridView view) {
            DataGridViewSelectedRowCollection rows = view.SelectedRows;
            if (rows == null || rows.Count == 0) {
                yield break;
            }
            foreach (DataGridViewRow row in rows) {
                yield return row.Index;
            }
        }
        public static IEnumerable<DataGridViewCell> 获取所选单元格(this DataGridView view) {
            if (view.SelectedCells == null || view.SelectedCells.Count == 0) {
                yield break;
            }
            foreach (DataGridViewCell cell in view.SelectedCells) {
                yield return cell;
            }
        }
        public static DataRow 获取所在行(this DataGridView view) {
            if (view.SelectedCells.Count == 0) {
                return null;
            }
            try {
                DataGridViewCell cell = view.SelectedCells[0];
                return (view.Rows[cell.RowIndex].DataBoundItem as DataRowView)?.Row;
            } catch { return null; }
        }
        public static int 获取所在行下标(this DataGridView view) {
            if (view.SelectedCells.Count == 0) {
                return -1;
            }
            DataGridViewCell cell = view.SelectedCells[0];
            return cell.RowIndex;
        }
        public static int 获取所在列下标(this DataGridView view) {
            if (view.SelectedCells.Count == 0) {
                return -1;
            }
            DataGridViewCell cell = view.SelectedCells[0];
            return cell.ColumnIndex;
        }
        public static IEnumerable<DataRow> 获取所在多行(this DataGridView view) {
            DataGridViewSelectedCellCollection cells = view.SelectedCells;
            if (cells == null || cells.Count == 0) {
                yield break;
            }
            //记录下标
            List<int> indexs = new List<int>();
            foreach (DataGridViewCell cell in cells) {
                if (!indexs.Contains(cell.RowIndex)) {
                    indexs.Add(cell.RowIndex);
                    yield return 获取行(view, cell.RowIndex);
                }
            }
        }
        public static IEnumerable<int> 获取所在多行下标(this DataGridView view) {
            DataGridViewSelectedCellCollection cells = view.SelectedCells;
            if (cells == null || cells.Count == 0) {
                yield break;
            }
            //记录下标
            List<int> indexs = new List<int>();
            foreach (DataGridViewCell cell in cells) {
                if (cell.RowIndex < 0) {
                    continue;
                }
                if (!indexs.Contains(cell.RowIndex)) {
                    indexs.Add(cell.RowIndex);
                    yield return cell.RowIndex;
                }
            }
        }
        #endregion
        /**
        * 功能列表：
        * 获取显示表
        * 获取显示行
        * 获取所选显示行
        * 获取所选显示多行
        * 获取所在显示行
        * 获取所在显示多行
        */
        #region 获取显示数据(页面真实数据，看到啥就是啥)
        public static DataTable 获取显示表(this DataGridView view, params int[] indexs) {
            DataTable dt = new DataTable();
            //创建头
            foreach (DataGridViewColumn col in view.Columns) {
                dt.Columns.Add(col.HeaderText, typeof(object));
            }
            //数据赋值
            if (indexs.Length == 0) {
                foreach (DataGridViewRow row in view.Rows) {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < row.Cells.Count; i++) {
                        dr[i] = row.Cells[i].EditedFormattedValue;
                    }
                    dt.Rows.Add(dr);
                }
            } else {
                foreach (int index in indexs) {
                    if (index == view.Rows.Count) {//防止越界
                        break;
                    }
                    DataGridViewRow row = view.Rows[index];
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < row.Cells.Count; i++) {
                        dr[i] = row.Cells[i].EditedFormattedValue;
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        public static DataRow 获取显示行(this DataGridView view, int index) {
            if (index > view.Rows.Count - 1) {
                return null;
            }
            return 获取显示表(view, index).AsEnumerable().ElementAtOrDefault(0);
        }
        public static DataRow 获取所选显示行(this DataGridView view) {
            return 获取显示行(view, 获取所选行下标(view));
        }
        public static IEnumerable<DataRow> 获取所选显示多行(this DataGridView view) {
            var indexs = 获取所选多行下标(view).ToArray();
            foreach (DataRow row in 获取显示表(view, indexs).AsEnumerable()) {
                yield return row;
            }
        }
        public static DataRow 获取所在显示行(this DataGridView view) {
            return 获取显示行(view, 获取所在行下标(view));
        }

        public static IEnumerable<DataRow> 获取所在显示多行(this DataGridView view) {
            var indexs = 获取所在多行下标(view).ToArray();
            foreach (DataRow row in 获取显示表(view, indexs).AsEnumerable()) {
                yield return row;
            }
        }
        #endregion

        #region 勾选框相关
        /// <param name="headText">标题显示内容</param>
        /// <param name="index">添加的位置</param>
        /// <param name="width">列宽</param>
        public static void 添加勾选框(this DataGridView view, string headText = "选择", int index = 0, int width = 50) {
            DataGridViewCheckBoxColumn checkbox = new DataGridViewCheckBoxColumn();
            checkbox.HeaderText = headText;//列显示名称
            checkbox.Name = headText;
            checkbox.TrueValue = true;//设置勾选状态初始值
            checkbox.FalseValue = false;//设置取消状态初始值
            checkbox.DataPropertyName = headText;
            checkbox.Width = width;//列宽
            checkbox.Resizable = DataGridViewTriState.False;//列大小不改变
            view.Columns.Insert(index, checkbox);//添加的checkbox在第一列
        }
        /// <param name="index">勾选的列所在下标</param>
        public static void 全选(this DataGridView view, int index = 0) {
            for (int i = 0; i < view.Rows.Count; i++) {
                if (view.Rows[i].Visible == false) {
                    continue;
                }
                if (Convert.ToBoolean(view.Rows[i].Cells[index].Value) == false) {
                    view.Rows[i].Cells[index].Value = true;
                } else {
                    continue;
                }
            }
        }

        /// <param name="index">勾选的列所在下标</param>
        public static void 全不选(this DataGridView view, int index = 0) {
            for (int i = 0; i < view.Rows.Count; i++) {
                if (view.Rows[i].Visible == false) {
                    continue;
                }
                if (Convert.ToBoolean(view.Rows[i].Cells[index].Value) == true) {
                    view.Rows[i].Cells[index].Value = false;
                } else {
                    continue;
                }
            }
        }
        /// <param name="checkListRowIndex">勾选的行下标列</param>
        /// <param name="index">勾选的列所在下标</param>
        public static void 勾选(this DataGridView view, List<int> checkListRowIndex, int index = 0) {
            foreach (int i in checkListRowIndex) {
                if (i == view.RowCount) {
                    return;
                }
                view.Rows[i].Cells[index].Value = true;
            }
        }
        /// <param name="checkListRowIndex">勾选的行下标列</param>
        /// <param name="index">勾选的列所在下标</param>
        public static void 反选(this DataGridView view, List<int> checkListRowIndex, int index = 0) {
            foreach (int i in checkListRowIndex) {
                if ((bool)view.Rows[i].Cells[index].FormattedValue == true) {
                    view.Rows[i].Cells[index].Value = false;
                } else {
                    view.Rows[i].Cells[index].Value = true;
                }
            }
        }
        /// <param name="index">勾选的行</param>
        public static IEnumerable<DataRow> 获取勾选行(this DataGridView view, int index = 0) {
            for (int i = 0; i < view.Rows.Count; i++) {
                if (Convert.ToBoolean(view.Rows[i].Cells[index].Value) == true) {
                    yield return (view.Rows[i].DataBoundItem as DataRowView).Row;
                } else {
                    continue;
                }
            }
            yield break;
        }
        /// <param name="index">勾选的列所在下标</param>
        public static IEnumerable<int> 获取勾选行下标(this DataGridView view, int index = 0) {
            for (int i = 0; i < view.Rows.Count; i++) {
                if (Convert.ToBoolean(view.Rows[i].Cells[index].Value) == true) {
                    yield return i;
                } else {
                    continue;
                }
            }
            yield break;
        }
        #endregion

    }
}
