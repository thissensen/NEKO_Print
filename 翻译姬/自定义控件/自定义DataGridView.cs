using Sunny.UI;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace 翻译姬 {
    public class 自定义DataGridView: DataGridView {
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

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e) {
            if (DesignMode) {
                base.OnCellPainting(e);
                return;
            }
            //ComboBox列的单元格绘制
            if (e.RowIndex > -1 && e.ColumnIndex > -1) {
                //绘制string[]类型
                var 列名 = Columns[e.ColumnIndex].Name;
                var 数据row = this.获取行(e.RowIndex);
                if (数据row != null && 数据row.Table.Columns.Contains(列名) && 数据row[列名] is string[] arr) {
                    单元格覆盖绘制($"[ {arr.Join(", ", "\"", "\"")}]", e);//模拟JArray的默认显示
                } else {
                    var 内容 = Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue?.ToString() ?? "";
                    单元格覆盖绘制(内容, e);
                }
            } else {
                单元格覆盖绘制(e.FormattedValue?.ToString() ?? "", e);
            }
            //e.Handled = true;
            base.OnCellPainting(e);
        }
        private void 单元格覆盖绘制(string 内容, DataGridViewCellPaintingEventArgs e) {
            //特殊类型不绘制
            if (e.ColumnIndex > -1) {
                if (Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn ||
                    Columns[e.ColumnIndex] is DataGridViewButtonColumn) {
                    return;
                }
            }
            SizeF 文字大小F = e.Graphics.MeasureString(内容, e.CellStyle.Font);
            var 文字大小 = new Size((int)Math.Ceiling(文字大小F.Width), (int)Math.Ceiling(文字大小F.Height));
            if (文字大小.Width > e.CellBounds.Width || 文字大小.Height > e.CellBounds.Height) {
                //文字超范围，调整到极限大小
                内容 = 内容.Replace("\r\n", "\n");
                内容 = 内容.Replace("\r", "\n");
                var 单字高度 = e.Graphics.MeasureString("正", e.CellStyle.Font).Height;
                int 最大行数 = Math.Max(1, (int)(e.CellBounds.Height / 单字高度));//最多几行
                var 文本行 = new List<string>();
                string temp = null;
                float 已用宽度 = 0;
                foreach (char c in 内容) {
                    char c2 = c;
                    if (c == '\n') {
                        c2 = ' ';
                    }
                    var 所需宽度 = e.Graphics.MeasureString(c.ToString(), e.CellStyle.Font).Width;
                    if (所需宽度 + 已用宽度 > e.CellBounds.Width) {
                        文本行.Add(temp);
                        temp = null;
                        已用宽度 = 0;
                    }
                    temp += c2;
                    已用宽度 += 所需宽度;
                }
                if (temp != null) {
                    文本行.Add(temp);
                }
                if (文本行.Count > 最大行数) {
                    文本行 = 文本行.Take(最大行数).ToList();
                    var len = 文本行[文本行.Count - 1].Length;
                    //最后字符替换为...
                    文本行[文本行.Count - 1] = 文本行[文本行.Count - 1].Substring(0, len - 1) + "…";
                }
                内容 = string.Join(Environment.NewLine, 文本行);
            }

            var 背景色 = 全局字符串.背景色;
            var 字体色 = 全局字符串.主题色;
            if (e.State.HasFlag(DataGridViewElementStates.Selected)) {
                背景色 = 全局字符串.深级主题色;
            }
            //画矩形
            using Brush backBrush = new SolidBrush(背景色);
            e.Graphics.FillRectangle(backBrush, e.CellBounds);
            //画底线+右线
            using Pen pen = new Pen(全局字符串.主题色);
            e.Graphics.DrawLine(pen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
            e.Graphics.DrawLine(pen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
            //画标题头线
            if (e.RowIndex == -1) {
                e.Graphics.DrawLine(pen, e.CellBounds.Left, e.CellBounds.Top, e.CellBounds.Right, e.CellBounds.Top);
            }
            //绘制字符串
            var size = e.Graphics.MeasureString(内容, e.CellStyle.Font);
            (float x, float y) = 获取绘制字符串坐标(内容, e);
            using Brush foreBrush = new SolidBrush(字体色);
            e.Graphics.DrawString(内容, e.CellStyle.Font, foreBrush, x, y);
            e.Handled = true;

        }
        private (float x, float y) 获取绘制字符串坐标(string 内容, DataGridViewCellPaintingEventArgs e) {
            var size = e.Graphics.MeasureString(内容, e.CellStyle.Font);
            float x, y;
            switch (e.CellStyle.Alignment) {
                case DataGridViewContentAlignment.TopLeft:
                case DataGridViewContentAlignment.MiddleLeft:
                case DataGridViewContentAlignment.BottomLeft:
                    x = e.CellBounds.Left - 1;
                    break;
                case DataGridViewContentAlignment.TopRight:
                case DataGridViewContentAlignment.MiddleRight:
                case DataGridViewContentAlignment.BottomRight:
                    x = e.CellBounds.Left - 1 + (e.CellBounds.Width - size.Width);
                    break;
                default:
                    x = (e.CellBounds.Width - size.Width) / 2 + e.CellBounds.Left - 1;
                    break;
            }
            switch (e.CellStyle.Alignment) {
                case DataGridViewContentAlignment.TopLeft:
                case DataGridViewContentAlignment.TopCenter:
                case DataGridViewContentAlignment.TopRight:
                    y = e.CellBounds.Top;
                    break;
                case DataGridViewContentAlignment.BottomLeft:
                case DataGridViewContentAlignment.BottomCenter:
                case DataGridViewContentAlignment.BottomRight:
                    y = e.CellBounds.Height - size.Height + e.CellBounds.Top;
                    break;
                default:
                    y = (e.CellBounds.Height - size.Height) / 2 + e.CellBounds.Top;
                    break;
            }
            return (x, y);
        }
        protected override void OnColumnAdded(DataGridViewColumnEventArgs e) {
            //将默认单元格替换为自定义强化单元格
            if (e.Column.CellType == typeof(DataGridViewComboBoxCell)) {
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
