namespace 翻译姬 {
    partial class 通用API窗体 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.表格增删改 = new 翻译姬.表格增删改();
            this.查询表格 = new 翻译姬.自定义DataGridView();
            this.QPS_bs = new System.Windows.Forms.BindingSource(this.components);
            this.检测可用Btn = new Sunny.UI.UISymbolButton();
            this.通用API控件区Panel = new Sunny.UI.UIPanel();
            this.前往注册Btn = new Sunny.UI.UISymbolButton();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.类型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.是否启用 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KEY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.秘钥 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QPS = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.已用额度 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.可用额度 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.查询表格)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QPS_bs)).BeginInit();
            this.通用API控件区Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // 表格增删改
            // 
            this.表格增删改.Dock = System.Windows.Forms.DockStyle.Left;
            this.表格增删改.Location = new System.Drawing.Point(0, 0);
            this.表格增删改.Name = "表格增删改";
            this.表格增删改.Size = new System.Drawing.Size(701, 41);
            this.表格增删改.SQL = null;
            this.表格增删改.TabIndex = 0;
            this.表格增删改.主键 = "ID";
            this.表格增删改.关联表名 = "API明细";
            this.表格增删改.表格 = this.查询表格;
            this.表格增删改.表格行移动中 = false;
            this.表格增删改.新添行后执行 += new 翻译姬.表格增删改.表格增删改_新添行后执行(this.表格增删改_新添行后执行);
            this.表格增删改.保存前验证 += new 翻译姬.表格增删改.表格增删改_保存前验证(this.表格增删改_保存前验证);
            // 
            // 查询表格
            // 
            this.查询表格.AllowUserToAddRows = false;
            this.查询表格.AllowUserToDeleteRows = false;
            this.查询表格.AllowUserToResizeColumns = false;
            this.查询表格.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.查询表格.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.查询表格.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.查询表格.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.查询表格.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.查询表格.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.查询表格.ColumnHeadersHeight = 32;
            this.查询表格.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.查询表格.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.类型,
            this.是否启用,
            this.KEY,
            this.秘钥,
            this.QPS,
            this.已用额度,
            this.可用额度});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.查询表格.DefaultCellStyle = dataGridViewCellStyle3;
            this.查询表格.Dock = System.Windows.Forms.DockStyle.Fill;
            this.查询表格.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.查询表格.EnableHeadersVisualStyles = false;
            this.查询表格.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.查询表格.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(173)))), ((int)(((byte)(255)))));
            this.查询表格.Location = new System.Drawing.Point(0, 41);
            this.查询表格.MultiSelect = false;
            this.查询表格.Name = "查询表格";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.查询表格.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.查询表格.RowHeadersWidth = 4;
            this.查询表格.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.查询表格.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.查询表格.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))), ((int)(((byte)(188)))));
            this.查询表格.RowTemplate.Height = 29;
            this.查询表格.Size = new System.Drawing.Size(987, 577);
            this.查询表格.TabIndex = 3;
            this.查询表格.列控件生成 += new 翻译姬.自定义DataGridView.自定义DataGridView列控件生成(this.查询表格_列控件生成);
            this.查询表格.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.查询表格_DataError);
            this.查询表格.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.查询表格_EditingControlShowing);
            // 
            // 检测可用Btn
            // 
            this.检测可用Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.检测可用Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.检测可用Btn.Location = new System.Drawing.Point(707, 3);
            this.检测可用Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.检测可用Btn.Name = "检测可用Btn";
            this.检测可用Btn.Radius = 15;
            this.检测可用Btn.Size = new System.Drawing.Size(100, 35);
            this.检测可用Btn.Symbol = 361613;
            this.检测可用Btn.TabIndex = 1;
            this.检测可用Btn.Text = "检测可用";
            this.检测可用Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.检测可用Btn.TipsText = "提示文本";
            this.检测可用Btn.Click += new System.EventHandler(this.检测可用Btn_Click);
            // 
            // 通用API控件区Panel
            // 
            this.通用API控件区Panel.Controls.Add(this.前往注册Btn);
            this.通用API控件区Panel.Controls.Add(this.表格增删改);
            this.通用API控件区Panel.Controls.Add(this.检测可用Btn);
            this.通用API控件区Panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.通用API控件区Panel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.通用API控件区Panel.Location = new System.Drawing.Point(0, 0);
            this.通用API控件区Panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.通用API控件区Panel.MinimumSize = new System.Drawing.Size(1, 1);
            this.通用API控件区Panel.Name = "通用API控件区Panel";
            this.通用API控件区Panel.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.通用API控件区Panel.Size = new System.Drawing.Size(987, 41);
            this.通用API控件区Panel.TabIndex = 2;
            this.通用API控件区Panel.Text = null;
            this.通用API控件区Panel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 前往注册Btn
            // 
            this.前往注册Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.前往注册Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.前往注册Btn.Location = new System.Drawing.Point(838, 3);
            this.前往注册Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.前往注册Btn.Name = "前往注册Btn";
            this.前往注册Btn.Radius = 15;
            this.前往注册Btn.Size = new System.Drawing.Size(100, 35);
            this.前往注册Btn.Symbol = 61633;
            this.前往注册Btn.TabIndex = 2;
            this.前往注册Btn.Text = "前往注册";
            this.前往注册Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.前往注册Btn.TipsText = "提示文本";
            this.前往注册Btn.Click += new System.EventHandler(this.前往注册Btn_Click);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            this.ID.Width = 51;
            // 
            // 类型
            // 
            this.类型.DataPropertyName = "类型";
            this.类型.HeaderText = "类型";
            this.类型.Name = "类型";
            this.类型.ReadOnly = true;
            this.类型.Visible = false;
            // 
            // 是否启用
            // 
            this.是否启用.DataPropertyName = "是否启用";
            this.是否启用.HeaderText = "是否启用";
            this.是否启用.Name = "是否启用";
            this.是否启用.ReadOnly = true;
            this.是否启用.Width = 80;
            // 
            // KEY
            // 
            this.KEY.DataPropertyName = "KEY";
            this.KEY.HeaderText = "KEY";
            this.KEY.Name = "KEY";
            this.KEY.Width = 300;
            // 
            // 秘钥
            // 
            this.秘钥.DataPropertyName = "秘钥";
            this.秘钥.HeaderText = "秘钥";
            this.秘钥.Name = "秘钥";
            this.秘钥.Width = 300;
            // 
            // QPS
            // 
            this.QPS.DataPropertyName = "QPS";
            this.QPS.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.QPS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.QPS.HeaderText = "QPS";
            this.QPS.Name = "QPS";
            this.QPS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.QPS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.QPS.Width = 80;
            // 
            // 已用额度
            // 
            this.已用额度.DataPropertyName = "已用额度";
            this.已用额度.HeaderText = "已用额度";
            this.已用额度.Name = "已用额度";
            this.已用额度.Width = 110;
            // 
            // 可用额度
            // 
            this.可用额度.DataPropertyName = "可用额度";
            this.可用额度.HeaderText = "可用额度";
            this.可用额度.Name = "可用额度";
            this.可用额度.Width = 110;
            // 
            // 通用API窗体
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(987, 618);
            this.Controls.Add(this.查询表格);
            this.Controls.Add(this.通用API控件区Panel);
            this.Name = "通用API窗体";
            this.Text = "通用API窗体";
            this.Page被选中 += new 翻译姬.自定义Page.自定义Page被选中(this.通用API窗体_Page被选中);
            this.Load += new System.EventHandler(this.通用API窗体_Load);
            this.Shown += new System.EventHandler(this.通用API窗体_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.查询表格)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QPS_bs)).EndInit();
            this.通用API控件区Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIPanel 通用API控件区Panel;
        private System.Windows.Forms.BindingSource QPS_bs;
        public 自定义DataGridView 查询表格;
        public Sunny.UI.UISymbolButton 检测可用Btn;
        public Sunny.UI.UISymbolButton 前往注册Btn;
        public 表格增删改 表格增删改;
        public System.Windows.Forms.DataGridViewTextBoxColumn ID;
        public System.Windows.Forms.DataGridViewTextBoxColumn 类型;
        public System.Windows.Forms.DataGridViewTextBoxColumn 是否启用;
        public System.Windows.Forms.DataGridViewTextBoxColumn KEY;
        public System.Windows.Forms.DataGridViewTextBoxColumn 秘钥;
        public System.Windows.Forms.DataGridViewComboBoxColumn QPS;
        public System.Windows.Forms.DataGridViewTextBoxColumn 已用额度;
        public System.Windows.Forms.DataGridViewTextBoxColumn 可用额度;
    }
}