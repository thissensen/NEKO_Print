using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace 翻译姬 {
    public class 自定义DataGridView : DataGridView {
        //滚动条相关
        public readonly UIScrollBar VBar = new UIScrollBar();
        public readonly UIHorScrollBarEx HBar = new UIHorScrollBarEx();
        /// <summary>
        /// 将该列所有单元格覆盖为列控件
        /// </summary>
        private Dictionary<string, Type> 列名_列控件 = new Dictionary<string, Type>();
        /// <summary>
        /// 3个列：行下标,列下标,列控件
        /// </summary>
        public DataTable 列控件坐标 = new DataTable();
        /// <summary>
        /// 美化表格高度
        /// </summary>
        public new int Height {
            get {
                return base.Height;
            }
            set {
                base.Height = value;
                if (Dock == DockStyle.Left || Dock == DockStyle.Right || Dock == DockStyle.Fill) {
                    return;
                }
                int num = (Height - ColumnHeadersHeight - HorizontalScrollBar.Height + RowTemplate.DividerHeight) % RowTemplate.Height;
                base.Height += (RowTemplate.Height - num);
            }
        }
        /// <summary>
        /// 是否已Shown
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsShown { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataTable DataTable {
            get {
                return base.DataSource as DataTable;
            }
            set {
                base.DataSource = value;
            }
        }

        public 自定义DataGridView() {
            DoubleBuffered = true;
            RowHeadersWidth = 4;//左边最小化
            BorderStyle = System.Windows.Forms.BorderStyle.None;//无边框
            //禁用修改
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            //AllowUserToResizeColumns = false;
            AllowUserToResizeRows = false;
            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            //设置书写方式
            EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            //单元格文本居中
            //DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //行高
            RowTemplate.Height = 29;
            //列控件相关
            列控件坐标.Columns.Add("行下标", typeof(int));
            列控件坐标.Columns.Add("列下标", typeof(int));
            列控件坐标.Columns.Add("列控件", typeof(列控件));
            RowsAdded += 自定义DataGridView_RowsAdded;
            RowsRemoved += 自定义DataGridView_RowsRemoved;
            DataSourceChanged += 自定义DataGridView_DataSourceChanged;
            CellValueChanged += 自定义DataGridView_CellValueChanged;
            Scroll += 自定义DataGridView_Scroll;
            Sorted += 自定义DataGridView_Sorted;
            //滚动条相关
            VBar.Parent = this;
            VBar.Visible = false;
            HBar.FillColor = VBar.FillColor = UIColor.LightBlue;
            VBar.ForeColor = UIColor.Blue;
            VBar.StyleCustomMode = true;
            VBar.ValueChanged += VBarValueChanged;
            VBar.ShowLeftLine = true;

            HBar.Parent = this;
            HBar.Visible = false;
            HBar.ForeColor = UIColor.Blue;
            HBar.StyleCustomMode = true;
            HBar.ValueChanged += HBar_ValueChanged;

            SetBarPosition();

            VerticalScrollBar.ValueChanged += VerticalScrollBar_ValueChanged;
            HorizontalScrollBar.ValueChanged += HorizontalScrollBar_ValueChanged;
            VerticalScrollBar.VisibleChanged += VerticalScrollBar_VisibleChanged;
            HorizontalScrollBar.VisibleChanged += HorizontalScrollBar_VisibleChanged;
        }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e) {
            if (e.Column.CellType == typeof(DataGridViewComboBoxCell)) {
                //将默认单元格替换为自定义强化单元格
                e.Column.CellTemplate = new DataGridViewComboBoxCellEx();
            }
            base.OnColumnAdded(e);
        }

        public delegate void 自定义DataGridView列控件生成(string 列名, 列控件 列控件);
        public event 自定义DataGridView列控件生成 列控件生成;

        /// <summary>
        /// 窗体Shown时触发
        /// </summary>
        public void Shown() {
            IsShown = true;
        }

        #region 列控件相关函数

        public void 添加列控件(string 列名, Type type) {
            列名_列控件.Add(列名, type);
        }

        public void 删除列控件(string 列名) {
            列名_列控件.Remove(列名);
        }
        //添加行
        private void 自定义DataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) {
            列控件初始化();
        }
        //删除行
        private void 自定义DataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) {
            列控件初始化();
        }
        //排序行
        private void 自定义DataGridView_Sorted(object sender, EventArgs e) {
            if (列名_列控件.Count > 0) {
                throw new Exception("有自定义列控件时，不允许排序");
            }
        }
        //重新赋值
        private void 自定义DataGridView_DataSourceChanged(object sender, EventArgs e) {
            列控件初始化();
        }

        //单元格值修改
        private void 自定义DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (列名_列控件.Count == 0) {
                return;
            }
            //列控件值同步修改
            DataRow row = 列控件坐标.Select($"行下标={e.RowIndex} and 列下标={e.ColumnIndex}").ElementAtOrDefault(0);
            if (row == null) {
                return;
            }
            列控件 con = row["列控件"] as 列控件;
            con.单元格值修改(Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
        }
        //滚动条
        private void 自定义DataGridView_Scroll(object sender, ScrollEventArgs e) {
            if (列名_列控件.Count == 0) {
                return;
            }
            列控件位置刷新();
        }

        public void 列控件初始化() {
            //列控件赋值
            if (列名_列控件.Count == 0) {
                return;
            }
            if (!IsShown) {
                throw new Exception("列控件想正常显示，数据赋值必须在Shown后");
            }

            //清空列控件
            列控件坐标.Rows.Clear();
            List<Control> 移除列 = new List<Control>();
            foreach (Control con in Controls) {
                if (con is 列控件) {
                    移除列.Add(con);
                }
            }
            foreach (Control con in 移除列) {
                Controls.Remove(con);
            }
            //添加列控件
            List<Control> 需添加 = new List<Control>();
            foreach (var kv in 列名_列控件) {
                int 列下标 = this.获取列下标(kv.Key);
                for (int i = 0; i < RowCount; i++) {
                    列控件 l_con = Activator.CreateInstance(kv.Value) as 列控件;
                    l_con.Cell = Rows[i].Cells[列下标];
                    l_con.单元格值修改(l_con.Cell.Value);
                    列控件生成?.Invoke(kv.Key, l_con);
                    需添加.Add(l_con as Control);
                    列控件坐标.Rows.Add(i, 列下标, l_con);
                }
            }
            Controls.AddRange(需添加.ToArray());
            列控件位置刷新();
        }

        public void 列控件位置刷新() {
            foreach (DataRow row in 列控件坐标.Rows) {
                Control con = row["列控件"] as Control;
                con.Visible = false;
            }
            bool 是否显示半格 = true;
            int 显示行下标 = FirstDisplayedScrollingRowIndex;
            int 显示行数 = DisplayedRowCount(是否显示半格);
            for (int i = 显示行下标; i < 显示行下标 + 显示行数; i++) {
                DataRow[] rows = 列控件坐标.Select($"行下标={i}");
                foreach (DataRow row in rows) {
                    int 列下标 = int.Parse(row["列下标"].ToString());
                    列控件 l_con = row["列控件"] as 列控件;
                    Control con = l_con as Control;

                    Rectangle rect = GetCellDisplayRectangle(列下标, i, true);
                    con.Size = new Size(rect.Width - 1, rect.Height - 1 - RowTemplate.DividerHeight);
                    con.Location = rect.Location;
                    con.Visible = true;
                }
            }
            Refresh();
        }

        public void 列控件值刷新() {
            for (int i = 0; i < 列控件坐标.Rows.Count; i++) {
                列控件 con = 列控件坐标.Rows[i]["列控件"] as 列控件;
                con.单元格值修改(con.Cell.Value);
            }
        }

        #endregion

        #region 滚动条相关
        private int scrollBarWidth = 0;
        [Description("垂直滚动条值修改事件")]
        public event EventHandler HorizontalScrollBarChanged;
        [Description("横向滚动条值修改事件")]
        public event EventHandler VerticalScrollBarChanged;
        [DefaultValue(0), Category("SunnyUI"), Description("垂直滚动条宽度，最小为原生滚动条宽度")]
        public int ScrollBarWidth {
            get => scrollBarWidth;
            set {
                scrollBarWidth = value;
                SetScrollInfo();
            }
        }
        private void VerticalScrollBar_ValueChanged(object sender, EventArgs e) {
            VBar.Value = FirstDisplayedScrollingRowIndex;
            VerticalScrollBarChanged?.Invoke(this, e);
        }
        private void HorizontalScrollBar_ValueChanged(object sender, EventArgs e) {
            HBar.Value = HorizontalScrollBar.Value;
        }
        private void HorizontalScrollBar_VisibleChanged(object sender, EventArgs e) {
            SetScrollInfo();
        }

        private void VerticalScrollBar_VisibleChanged(object sender, EventArgs e) {
            SetScrollInfo();
        }
        private void VBarValueChanged(object sender, EventArgs e) {
            if (RowCount == 0) return;
            int idx = VBar.Value;
            if (idx < 0) idx = 0;
            if (idx >= RowCount) idx = RowCount - 1;
            FirstDisplayedScrollingRowIndex = idx;
        }
        private void HBar_ValueChanged(object sender, EventArgs e) {
            HorizontalScrollBar.Value = HBar.Value;
            HorizontalScrollingOffset = HBar.Value;
            HorizontalScrollBarChanged?.Invoke(this, e);
        }
        private void SetBarPosition() {
            if (VBar == null || HBar == null) {
                return;
            }

            int barWidth = Math.Max(ScrollBarInfo.VerticalScrollBarWidth(), ScrollBarWidth);

            if (BorderStyle == BorderStyle.FixedSingle) {
                VBar.Left = Width - barWidth - 2;
                VBar.Top = 1;
                VBar.Width = barWidth + 1;
                VBar.Height = Height - 2;
                VBar.BringToFront();

                HBar.Left = 1;
                HBar.Height = ScrollBarInfo.HorizontalScrollBarHeight() + 1;
                HBar.Width = Width - (VBar.Visible ? VBar.Width : 0) - 2;
                HBar.Top = Height - HBar.Height - 1;
                HBar.BringToFront();
            } else {
                VBar.Left = Width - barWidth - 1;
                VBar.Top = 0;
                VBar.Width = barWidth + 1;
                VBar.Height = Height;
                VBar.BringToFront();

                HBar.Left = 0;
                HBar.Height = ScrollBarInfo.HorizontalScrollBarHeight() + 1;
                HBar.Width = Width - (VBar.Visible ? VBar.Width : 0);
                HBar.Top = Height - HBar.Height;
                HBar.BringToFront();
            }
        }
        public void SetScrollInfo() {
            if (VBar == null || HBar == null) {
                return;
            }

            if (RowCount > DisplayedRowCount(false)) {
                VBar.Maximum = RowCount - DisplayedRowCount(false);
                VBar.Value = FirstDisplayedScrollingRowIndex;
                VBar.Visible = ScrollBars == ScrollBars.Vertical || ScrollBars == ScrollBars.Both;
            } else {
                VBar.Visible = false;
            }

            if (HorizontalScrollBar.Visible) {
                HBar.Maximum = HorizontalScrollBar.Maximum;
                HBar.Value = HorizontalScrollBar.Value;
                HBar.BoundsWidth = HorizontalScrollBar.LargeChange;
                HBar.LargeChange = HorizontalScrollBar.LargeChange;//.Maximum / VisibleColumnCount();
                HBar.Visible = ScrollBars == ScrollBars.Horizontal || ScrollBars == ScrollBars.Both;
            } else {
                HBar.Visible = false;
            }

            SetBarPosition();
        }
        #endregion

    }
}
