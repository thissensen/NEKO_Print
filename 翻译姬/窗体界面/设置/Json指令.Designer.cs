namespace 翻译姬 {
    partial class Json指令 {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Json上半Panel = new Sunny.UI.UIPanel();
            this.筛选Box = new Sunny.UI.UITextBox();
            this.uiSymbolLabel1 = new Sunny.UI.UISymbolLabel();
            this.提取Btn = new Sunny.UI.UIButton();
            this.指令表格 = new 翻译姬.自定义DataGridView();
            this.指令 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.内容 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.提取结果Box = new 翻译姬.组合控件TextBox();
            this.选择JsonBtn = new Sunny.UI.UIButton();
            this.指令集Box = new 翻译姬.组合控件TextBox();
            this.表格增删改 = new 翻译姬.表格增删改();
            this.查询表格 = new 翻译姬.自定义DataGridView();
            this.名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.指令集 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.备注 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Json上半Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.指令表格)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.查询表格)).BeginInit();
            this.SuspendLayout();
            // 
            // Json上半Panel
            // 
            this.Json上半Panel.Controls.Add(this.筛选Box);
            this.Json上半Panel.Controls.Add(this.uiSymbolLabel1);
            this.Json上半Panel.Controls.Add(this.提取Btn);
            this.Json上半Panel.Controls.Add(this.指令表格);
            this.Json上半Panel.Controls.Add(this.提取结果Box);
            this.Json上半Panel.Controls.Add(this.选择JsonBtn);
            this.Json上半Panel.Controls.Add(this.指令集Box);
            this.Json上半Panel.Controls.Add(this.表格增删改);
            this.Json上半Panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Json上半Panel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Json上半Panel.Location = new System.Drawing.Point(0, 0);
            this.Json上半Panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Json上半Panel.MinimumSize = new System.Drawing.Size(1, 1);
            this.Json上半Panel.Name = "Json上半Panel";
            this.Json上半Panel.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.Json上半Panel.Size = new System.Drawing.Size(987, 336);
            this.Json上半Panel.Style = Sunny.UI.UIStyle.Custom;
            this.Json上半Panel.TabIndex = 0;
            this.Json上半Panel.Text = null;
            this.Json上半Panel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 筛选Box
            // 
            this.筛选Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.筛选Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.筛选Box.Location = new System.Drawing.Point(315, 21);
            this.筛选Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.筛选Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.筛选Box.Name = "筛选Box";
            this.筛选Box.ShowText = false;
            this.筛选Box.Size = new System.Drawing.Size(150, 29);
            this.筛选Box.Style = Sunny.UI.UIStyle.Custom;
            this.筛选Box.TabIndex = 15;
            this.筛选Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.筛选Box.Watermark = "输入内容进行筛选";
            this.筛选Box.TextChanged += new System.EventHandler(this.筛选Box_TextChanged);
            // 
            // uiSymbolLabel1
            // 
            this.uiSymbolLabel1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.uiSymbolLabel1.Location = new System.Drawing.Point(118, 18);
            this.uiSymbolLabel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolLabel1.Name = "uiSymbolLabel1";
            this.uiSymbolLabel1.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.uiSymbolLabel1.Size = new System.Drawing.Size(190, 35);
            this.uiSymbolLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolLabel1.Symbol = 61713;
            this.uiSymbolLabel1.SymbolSize = 12;
            this.uiSymbolLabel1.TabIndex = 14;
            this.uiSymbolLabel1.Text = "点击指令可在右方生产指令集";
            this.uiSymbolLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 提取Btn
            // 
            this.提取Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.提取Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.提取Btn.Location = new System.Drawing.Point(471, 18);
            this.提取Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.提取Btn.Name = "提取Btn";
            this.提取Btn.Radius = 15;
            this.提取Btn.Size = new System.Drawing.Size(100, 35);
            this.提取Btn.Style = Sunny.UI.UIStyle.Custom;
            this.提取Btn.TabIndex = 13;
            this.提取Btn.Text = "提取";
            this.提取Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.提取Btn.Click += new System.EventHandler(this.提取Btn_Click);
            // 
            // 指令表格
            // 
            this.指令表格.AllowUserToAddRows = false;
            this.指令表格.AllowUserToDeleteRows = false;
            this.指令表格.AllowUserToResizeColumns = false;
            this.指令表格.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.指令表格.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.指令表格.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.指令表格.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.指令表格.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.指令表格.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.指令表格.ColumnHeadersHeight = 32;
            this.指令表格.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.指令表格.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.指令,
            this.内容});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.指令表格.DefaultCellStyle = dataGridViewCellStyle3;
            this.指令表格.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.指令表格.EnableHeadersVisualStyles = false;
            this.指令表格.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.指令表格.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(173)))), ((int)(((byte)(255)))));
            this.指令表格.Location = new System.Drawing.Point(12, 66);
            this.指令表格.Name = "指令表格";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.指令表格.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.指令表格.RowHeadersVisible = false;
            this.指令表格.RowHeadersWidth = 4;
            this.指令表格.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.指令表格.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.指令表格.RowTemplate.Height = 29;
            this.指令表格.Size = new System.Drawing.Size(559, 223);
            this.指令表格.TabIndex = 12;
            this.指令表格.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.指令表格_CellMouseClick);
            // 
            // 指令
            // 
            this.指令.DataPropertyName = "指令";
            this.指令.HeaderText = "指令";
            this.指令.Name = "指令";
            this.指令.ReadOnly = true;
            this.指令.Width = 275;
            // 
            // 内容
            // 
            this.内容.DataPropertyName = "内容";
            this.内容.HeaderText = "内容";
            this.内容.Name = "内容";
            this.内容.Width = 275;
            // 
            // 提取结果Box
            // 
            this.提取结果Box.BackColor = System.Drawing.Color.Transparent;
            this.提取结果Box.CanEmpty = false;
            this.提取结果Box.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.提取结果Box.DecimalPlaces = 2;
            this.提取结果Box.LabelFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.提取结果Box.LabelText = "提取结果";
            this.提取结果Box.Label方向 = 翻译姬.数据库控件Label方向.上;
            this.提取结果Box.Location = new System.Drawing.Point(577, 126);
            this.提取结果Box.Maximum = 2147483647D;
            this.提取结果Box.MaxLength = 32767;
            this.提取结果Box.Minimum = -2147483648D;
            this.提取结果Box.MinimumSize = new System.Drawing.Size(0, 29);
            this.提取结果Box.Multiline = true;
            this.提取结果Box.Name = "提取结果Box";
            this.提取结果Box.ReadOnly = false;
            this.提取结果Box.Size = new System.Drawing.Size(375, 163);
            this.提取结果Box.TabIndex = 11;
            this.提取结果Box.Type = Sunny.UI.UITextBox.UIEditType.String;
            this.提取结果Box.Watermark = "";
            this.提取结果Box.主控件Font = new System.Drawing.Font("微软雅黑", 8F);
            this.提取结果Box.主控件Text = "";
            this.提取结果Box.是否允许为零 = true;
            this.提取结果Box.是否判断条件 = false;
            this.提取结果Box.是否序列化 = false;
            // 
            // 选择JsonBtn
            // 
            this.选择JsonBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.选择JsonBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.选择JsonBtn.Location = new System.Drawing.Point(12, 18);
            this.选择JsonBtn.MinimumSize = new System.Drawing.Size(1, 1);
            this.选择JsonBtn.Name = "选择JsonBtn";
            this.选择JsonBtn.Radius = 15;
            this.选择JsonBtn.Size = new System.Drawing.Size(100, 35);
            this.选择JsonBtn.Style = Sunny.UI.UIStyle.Custom;
            this.选择JsonBtn.TabIndex = 10;
            this.选择JsonBtn.Text = "选择Json";
            this.选择JsonBtn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.选择JsonBtn.Click += new System.EventHandler(this.选择JsonBtn_Click);
            // 
            // 指令集Box
            // 
            this.指令集Box.BackColor = System.Drawing.Color.Transparent;
            this.指令集Box.CanEmpty = false;
            this.指令集Box.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.指令集Box.DecimalPlaces = 2;
            this.指令集Box.LabelFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.指令集Box.LabelText = "指令集";
            this.指令集Box.Label方向 = 翻译姬.数据库控件Label方向.上;
            this.指令集Box.Location = new System.Drawing.Point(577, 12);
            this.指令集Box.Maximum = 2147483647D;
            this.指令集Box.MaxLength = 32767;
            this.指令集Box.Minimum = -2147483648D;
            this.指令集Box.MinimumSize = new System.Drawing.Size(0, 29);
            this.指令集Box.Multiline = true;
            this.指令集Box.Name = "指令集Box";
            this.指令集Box.ReadOnly = false;
            this.指令集Box.Size = new System.Drawing.Size(375, 108);
            this.指令集Box.TabIndex = 2;
            this.指令集Box.Type = Sunny.UI.UITextBox.UIEditType.String;
            this.指令集Box.Watermark = "";
            this.指令集Box.主控件Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.指令集Box.主控件Text = "";
            this.指令集Box.是否允许为零 = true;
            this.指令集Box.是否判断条件 = false;
            this.指令集Box.是否序列化 = false;
            // 
            // 表格增删改
            // 
            this.表格增删改.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.表格增删改.Location = new System.Drawing.Point(0, 295);
            this.表格增删改.Name = "表格增删改";
            this.表格增删改.Size = new System.Drawing.Size(987, 41);
            this.表格增删改.SQL = null;
            this.表格增删改.TabIndex = 1;
            this.表格增删改.主键 = "名称";
            this.表格增删改.关联表名 = "Json指令";
            this.表格增删改.表格 = this.查询表格;
            this.表格增删改.表格行移动中 = false;
            this.表格增删改.新添前行验证 += new 翻译姬.表格增删改.表格增删改_新添前行验证(this.表格增删改_新添前行验证);
            this.表格增删改.保存前验证 += new 翻译姬.表格增删改.表格增删改_保存前验证(this.表格增删改_保存前验证);
            // 
            // 查询表格
            // 
            this.查询表格.AllowUserToAddRows = false;
            this.查询表格.AllowUserToDeleteRows = false;
            this.查询表格.AllowUserToResizeColumns = false;
            this.查询表格.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.查询表格.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.查询表格.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.查询表格.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.查询表格.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.查询表格.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.查询表格.ColumnHeadersHeight = 32;
            this.查询表格.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.查询表格.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.名称,
            this.指令集,
            this.备注});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.查询表格.DefaultCellStyle = dataGridViewCellStyle8;
            this.查询表格.Dock = System.Windows.Forms.DockStyle.Fill;
            this.查询表格.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.查询表格.EnableHeadersVisualStyles = false;
            this.查询表格.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.查询表格.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(173)))), ((int)(((byte)(255)))));
            this.查询表格.Location = new System.Drawing.Point(0, 336);
            this.查询表格.Name = "查询表格";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.查询表格.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.查询表格.RowHeadersVisible = false;
            this.查询表格.RowHeadersWidth = 4;
            this.查询表格.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.查询表格.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.查询表格.RowTemplate.Height = 29;
            this.查询表格.Size = new System.Drawing.Size(987, 282);
            this.查询表格.TabIndex = 2;
            this.查询表格.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.查询表格_CellMouseClick);
            // 
            // 名称
            // 
            this.名称.DataPropertyName = "名称";
            this.名称.HeaderText = "名称";
            this.名称.Name = "名称";
            this.名称.Width = 200;
            // 
            // 指令集
            // 
            this.指令集.DataPropertyName = "指令集";
            this.指令集.HeaderText = "指令集";
            this.指令集.Name = "指令集";
            this.指令集.ReadOnly = true;
            this.指令集.Width = 350;
            // 
            // 备注
            // 
            this.备注.DataPropertyName = "备注";
            this.备注.HeaderText = "备注";
            this.备注.Name = "备注";
            this.备注.Width = 427;
            // 
            // Json指令
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(987, 618);
            this.Controls.Add(this.查询表格);
            this.Controls.Add(this.Json上半Panel);
            this.Name = "Json指令";
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "Json指令";
            this.Page被选中 += new 翻译姬.自定义Page.自定义Page被选中(this.Json指令_Page被选中);
            this.Load += new System.EventHandler(this.Json指令_Load);
            this.Shown += new System.EventHandler(this.Json指令_Shown);
            this.Json上半Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.指令表格)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.查询表格)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel Json上半Panel;
        private 表格增删改 表格增删改;
        private Sunny.UI.UIButton 提取Btn;
        private Sunny.UI.UISymbolLabel uiSymbolLabel1;
        private Sunny.UI.UITextBox 筛选Box;
        private System.Windows.Forms.DataGridViewTextBoxColumn 指令;
        private System.Windows.Forms.DataGridViewTextBoxColumn 内容;
        private System.Windows.Forms.DataGridViewTextBoxColumn 名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 指令集;
        private System.Windows.Forms.DataGridViewTextBoxColumn 备注;
        public Sunny.UI.UIButton 选择JsonBtn;
        public 自定义DataGridView 查询表格;
        public 自定义DataGridView 指令表格;
        public 组合控件TextBox 指令集Box;
        public 组合控件TextBox 提取结果Box;
    }
}