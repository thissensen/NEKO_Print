namespace 翻译姬 {
    partial class 正则设置 {
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
            this.正则上半Panel = new Sunny.UI.UIPanel();
            this.机翻后Box = new 翻译姬.组合控件TextBox();
            this.待机翻Box = new 翻译姬.组合控件TextBox();
            this.提示内容Box = new Sunny.UI.UITextBox();
            this.预览Btn = new Sunny.UI.UIButton();
            this.uiSymbolLabel1 = new Sunny.UI.UISymbolLabel();
            this.提取型正则Box = new 翻译姬.组合控件TextBox();
            this.提取前行过滤正则Box = new 翻译姬.组合控件TextBox();
            this.文本过滤正则Box = new 翻译姬.组合控件TextBox();
            this.行过滤正则Box = new 翻译姬.组合控件TextBox();
            this.表格增删改 = new 翻译姬.表格增删改();
            this.查询表格 = new 翻译姬.自定义DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.正则名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.行过滤正则 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.文本过滤正则 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.提取前行过滤正则 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.提取型正则 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.正则上半Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.查询表格)).BeginInit();
            this.SuspendLayout();
            // 
            // 正则上半Panel
            // 
            this.正则上半Panel.Controls.Add(this.机翻后Box);
            this.正则上半Panel.Controls.Add(this.待机翻Box);
            this.正则上半Panel.Controls.Add(this.提示内容Box);
            this.正则上半Panel.Controls.Add(this.预览Btn);
            this.正则上半Panel.Controls.Add(this.uiSymbolLabel1);
            this.正则上半Panel.Controls.Add(this.提取型正则Box);
            this.正则上半Panel.Controls.Add(this.提取前行过滤正则Box);
            this.正则上半Panel.Controls.Add(this.文本过滤正则Box);
            this.正则上半Panel.Controls.Add(this.行过滤正则Box);
            this.正则上半Panel.Controls.Add(this.表格增删改);
            this.正则上半Panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.正则上半Panel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.正则上半Panel.Location = new System.Drawing.Point(0, 0);
            this.正则上半Panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.正则上半Panel.MinimumSize = new System.Drawing.Size(1, 1);
            this.正则上半Panel.Name = "正则上半Panel";
            this.正则上半Panel.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.正则上半Panel.Size = new System.Drawing.Size(987, 336);
            this.正则上半Panel.Style = Sunny.UI.UIStyle.Custom;
            this.正则上半Panel.TabIndex = 1;
            this.正则上半Panel.Text = null;
            this.正则上半Panel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 机翻后Box
            // 
            this.机翻后Box.BackColor = System.Drawing.Color.Transparent;
            this.机翻后Box.CanEmpty = false;
            this.机翻后Box.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.机翻后Box.DecimalPlaces = 2;
            this.机翻后Box.LabelFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.机翻后Box.LabelText = "机翻后文本";
            this.机翻后Box.Label方向 = 翻译姬.数据库控件Label方向.上;
            this.机翻后Box.Location = new System.Drawing.Point(520, 160);
            this.机翻后Box.Maximum = 2147483647D;
            this.机翻后Box.MaxLength = 32767;
            this.机翻后Box.Minimum = -2147483648D;
            this.机翻后Box.MinimumSize = new System.Drawing.Size(0, 29);
            this.机翻后Box.Multiline = true;
            this.机翻后Box.Name = "机翻后Box";
            this.机翻后Box.ReadOnly = true;
            this.机翻后Box.Size = new System.Drawing.Size(455, 128);
            this.机翻后Box.TabIndex = 12;
            this.机翻后Box.Type = Sunny.UI.UITextBox.UIEditType.String;
            this.机翻后Box.Watermark = "";
            this.机翻后Box.主控件Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.机翻后Box.主控件Text = "";
            this.机翻后Box.是否允许为零 = true;
            this.机翻后Box.是否判断条件 = false;
            this.机翻后Box.是否序列化 = false;
            // 
            // 待机翻Box
            // 
            this.待机翻Box.BackColor = System.Drawing.Color.Transparent;
            this.待机翻Box.CanEmpty = false;
            this.待机翻Box.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.待机翻Box.DecimalPlaces = 2;
            this.待机翻Box.LabelFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.待机翻Box.LabelText = "待机翻文本";
            this.待机翻Box.Label方向 = 翻译姬.数据库控件Label方向.上;
            this.待机翻Box.Location = new System.Drawing.Point(520, 12);
            this.待机翻Box.Maximum = 2147483647D;
            this.待机翻Box.MaxLength = 32767;
            this.待机翻Box.Minimum = -2147483648D;
            this.待机翻Box.MinimumSize = new System.Drawing.Size(0, 29);
            this.待机翻Box.Multiline = true;
            this.待机翻Box.Name = "待机翻Box";
            this.待机翻Box.ReadOnly = false;
            this.待机翻Box.Size = new System.Drawing.Size(455, 128);
            this.待机翻Box.TabIndex = 11;
            this.待机翻Box.Type = Sunny.UI.UITextBox.UIEditType.String;
            this.待机翻Box.Watermark = "";
            this.待机翻Box.主控件Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.待机翻Box.主控件Text = "";
            this.待机翻Box.是否允许为零 = true;
            this.待机翻Box.是否判断条件 = false;
            this.待机翻Box.是否序列化 = false;
            // 
            // 提示内容Box
            // 
            this.提示内容Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.提示内容Box.Font = new System.Drawing.Font("微软雅黑", 7F);
            this.提示内容Box.Location = new System.Drawing.Point(13, 194);
            this.提示内容Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.提示内容Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.提示内容Box.Multiline = true;
            this.提示内容Box.Name = "提示内容Box";
            this.提示内容Box.ReadOnly = true;
            this.提示内容Box.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.提示内容Box.ShowText = false;
            this.提示内容Box.Size = new System.Drawing.Size(383, 96);
            this.提示内容Box.Style = Sunny.UI.UIStyle.Custom;
            this.提示内容Box.TabIndex = 10;
            this.提示内容Box.Text = "提取型正则规范：\r\n1、使用()提取，每段正则只允许出现一个，不完全匹配不会提取\r\n    样例：^@name=([^ ]*).*$\r\n2、|符号用于正则的分段\r" +
    "\n    样例：正则段A|正则段B\r\n正则优先级顺序：提取前行过滤正则+提取型正则 -> 行过滤正则 -> 文本过滤正则";
            this.提示内容Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.提示内容Box.Watermark = "";
            // 
            // 预览Btn
            // 
            this.预览Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.预览Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.预览Btn.Location = new System.Drawing.Point(414, 197);
            this.预览Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.预览Btn.Name = "预览Btn";
            this.预览Btn.Radius = 15;
            this.预览Btn.Size = new System.Drawing.Size(100, 35);
            this.预览Btn.Style = Sunny.UI.UIStyle.Custom;
            this.预览Btn.TabIndex = 9;
            this.预览Btn.Text = "预览";
            this.预览Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.预览Btn.Click += new System.EventHandler(this.预览Btn_Click);
            // 
            // uiSymbolLabel1
            // 
            this.uiSymbolLabel1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.uiSymbolLabel1.Location = new System.Drawing.Point(12, 14);
            this.uiSymbolLabel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolLabel1.Name = "uiSymbolLabel1";
            this.uiSymbolLabel1.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.uiSymbolLabel1.Size = new System.Drawing.Size(502, 35);
            this.uiSymbolLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolLabel1.Symbol = 61713;
            this.uiSymbolLabel1.SymbolSize = 12;
            this.uiSymbolLabel1.TabIndex = 8;
            this.uiSymbolLabel1.Text = "右方待机翻填入文本后，点击表中正则，点击预览，预览正则效果";
            this.uiSymbolLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 提取型正则Box
            // 
            this.提取型正则Box.BackColor = System.Drawing.Color.Transparent;
            this.提取型正则Box.CanEmpty = false;
            this.提取型正则Box.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.提取型正则Box.DecimalPlaces = 2;
            this.提取型正则Box.LabelFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.提取型正则Box.LabelText = "提取型正则";
            this.提取型正则Box.Label方向 = 翻译姬.数据库控件Label方向.左;
            this.提取型正则Box.Location = new System.Drawing.Point(12, 160);
            this.提取型正则Box.Maximum = 2147483647D;
            this.提取型正则Box.MaxLength = 32767;
            this.提取型正则Box.Minimum = -2147483648D;
            this.提取型正则Box.MinimumSize = new System.Drawing.Size(0, 29);
            this.提取型正则Box.Multiline = false;
            this.提取型正则Box.Name = "提取型正则Box";
            this.提取型正则Box.ReadOnly = false;
            this.提取型正则Box.Size = new System.Drawing.Size(502, 29);
            this.提取型正则Box.TabIndex = 6;
            this.提取型正则Box.Type = Sunny.UI.UITextBox.UIEditType.String;
            this.提取型正则Box.Watermark = "";
            this.提取型正则Box.主控件Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.提取型正则Box.主控件Text = "";
            this.提取型正则Box.是否允许为零 = true;
            this.提取型正则Box.是否判断条件 = false;
            this.提取型正则Box.是否序列化 = false;
            // 
            // 提取前行过滤正则Box
            // 
            this.提取前行过滤正则Box.BackColor = System.Drawing.Color.Transparent;
            this.提取前行过滤正则Box.CanEmpty = false;
            this.提取前行过滤正则Box.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.提取前行过滤正则Box.DecimalPlaces = 2;
            this.提取前行过滤正则Box.LabelFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.提取前行过滤正则Box.LabelText = "提取前行过滤正则";
            this.提取前行过滤正则Box.Label方向 = 翻译姬.数据库控件Label方向.左;
            this.提取前行过滤正则Box.Location = new System.Drawing.Point(12, 125);
            this.提取前行过滤正则Box.Maximum = 2147483647D;
            this.提取前行过滤正则Box.MaxLength = 32767;
            this.提取前行过滤正则Box.Minimum = -2147483648D;
            this.提取前行过滤正则Box.MinimumSize = new System.Drawing.Size(0, 29);
            this.提取前行过滤正则Box.Multiline = false;
            this.提取前行过滤正则Box.Name = "提取前行过滤正则Box";
            this.提取前行过滤正则Box.ReadOnly = false;
            this.提取前行过滤正则Box.Size = new System.Drawing.Size(502, 29);
            this.提取前行过滤正则Box.TabIndex = 5;
            this.提取前行过滤正则Box.Type = Sunny.UI.UITextBox.UIEditType.String;
            this.提取前行过滤正则Box.Watermark = "";
            this.提取前行过滤正则Box.主控件Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.提取前行过滤正则Box.主控件Text = "";
            this.提取前行过滤正则Box.是否允许为零 = true;
            this.提取前行过滤正则Box.是否判断条件 = false;
            this.提取前行过滤正则Box.是否序列化 = false;
            // 
            // 文本过滤正则Box
            // 
            this.文本过滤正则Box.BackColor = System.Drawing.Color.Transparent;
            this.文本过滤正则Box.CanEmpty = false;
            this.文本过滤正则Box.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.文本过滤正则Box.DecimalPlaces = 2;
            this.文本过滤正则Box.LabelFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.文本过滤正则Box.LabelText = "文本过滤正则";
            this.文本过滤正则Box.Label方向 = 翻译姬.数据库控件Label方向.左;
            this.文本过滤正则Box.Location = new System.Drawing.Point(12, 90);
            this.文本过滤正则Box.Maximum = 2147483647D;
            this.文本过滤正则Box.MaxLength = 32767;
            this.文本过滤正则Box.Minimum = -2147483648D;
            this.文本过滤正则Box.MinimumSize = new System.Drawing.Size(0, 29);
            this.文本过滤正则Box.Multiline = false;
            this.文本过滤正则Box.Name = "文本过滤正则Box";
            this.文本过滤正则Box.ReadOnly = false;
            this.文本过滤正则Box.Size = new System.Drawing.Size(502, 29);
            this.文本过滤正则Box.TabIndex = 4;
            this.文本过滤正则Box.Type = Sunny.UI.UITextBox.UIEditType.String;
            this.文本过滤正则Box.Watermark = "";
            this.文本过滤正则Box.主控件Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.文本过滤正则Box.主控件Text = "";
            this.文本过滤正则Box.是否允许为零 = true;
            this.文本过滤正则Box.是否判断条件 = false;
            this.文本过滤正则Box.是否序列化 = false;
            // 
            // 行过滤正则Box
            // 
            this.行过滤正则Box.BackColor = System.Drawing.Color.Transparent;
            this.行过滤正则Box.CanEmpty = false;
            this.行过滤正则Box.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.行过滤正则Box.DecimalPlaces = 2;
            this.行过滤正则Box.LabelFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.行过滤正则Box.LabelText = "行过滤正则";
            this.行过滤正则Box.Label方向 = 翻译姬.数据库控件Label方向.左;
            this.行过滤正则Box.Location = new System.Drawing.Point(12, 55);
            this.行过滤正则Box.Maximum = 2147483647D;
            this.行过滤正则Box.MaxLength = 32767;
            this.行过滤正则Box.Minimum = -2147483648D;
            this.行过滤正则Box.MinimumSize = new System.Drawing.Size(0, 29);
            this.行过滤正则Box.Multiline = false;
            this.行过滤正则Box.Name = "行过滤正则Box";
            this.行过滤正则Box.ReadOnly = false;
            this.行过滤正则Box.Size = new System.Drawing.Size(502, 29);
            this.行过滤正则Box.TabIndex = 3;
            this.行过滤正则Box.Type = Sunny.UI.UITextBox.UIEditType.String;
            this.行过滤正则Box.Watermark = "";
            this.行过滤正则Box.主控件Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.行过滤正则Box.主控件Text = "";
            this.行过滤正则Box.是否允许为零 = true;
            this.行过滤正则Box.是否判断条件 = false;
            this.行过滤正则Box.是否序列化 = false;
            // 
            // 表格增删改
            // 
            this.表格增删改.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.表格增删改.Location = new System.Drawing.Point(0, 295);
            this.表格增删改.Name = "表格增删改";
            this.表格增删改.Size = new System.Drawing.Size(987, 41);
            this.表格增删改.SQL = null;
            this.表格增删改.TabIndex = 2;
            this.表格增删改.主键 = "ID";
            this.表格增删改.关联表名 = "正则";
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
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.查询表格.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.查询表格.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.查询表格.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.查询表格.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.查询表格.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.查询表格.ColumnHeadersHeight = 32;
            this.查询表格.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.查询表格.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.正则名称,
            this.行过滤正则,
            this.文本过滤正则,
            this.提取前行过滤正则,
            this.提取型正则});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(242)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.查询表格.DefaultCellStyle = dataGridViewCellStyle3;
            this.查询表格.Dock = System.Windows.Forms.DockStyle.Fill;
            this.查询表格.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.查询表格.EnableHeadersVisualStyles = false;
            this.查询表格.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.查询表格.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(199)))), ((int)(((byte)(183)))));
            this.查询表格.Location = new System.Drawing.Point(0, 336);
            this.查询表格.Name = "查询表格";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.查询表格.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.查询表格.RowHeadersWidth = 4;
            this.查询表格.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(242)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.查询表格.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.查询表格.RowTemplate.Height = 23;
            this.查询表格.Size = new System.Drawing.Size(987, 282);
            this.查询表格.TabIndex = 2;
            this.查询表格.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.查询表格_CellClick);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // 正则名称
            // 
            this.正则名称.DataPropertyName = "正则名称";
            this.正则名称.HeaderText = "正则名称";
            this.正则名称.Name = "正则名称";
            this.正则名称.Width = 90;
            // 
            // 行过滤正则
            // 
            this.行过滤正则.DataPropertyName = "行过滤正则";
            this.行过滤正则.HeaderText = "行过滤正则";
            this.行过滤正则.Name = "行过滤正则";
            this.行过滤正则.Width = 200;
            // 
            // 文本过滤正则
            // 
            this.文本过滤正则.DataPropertyName = "文本过滤正则";
            this.文本过滤正则.HeaderText = "文本过滤正则";
            this.文本过滤正则.Name = "文本过滤正则";
            this.文本过滤正则.Width = 290;
            // 
            // 提取前行过滤正则
            // 
            this.提取前行过滤正则.DataPropertyName = "提取前行过滤正则";
            this.提取前行过滤正则.HeaderText = "提取前行过滤正则";
            this.提取前行过滤正则.Name = "提取前行过滤正则";
            this.提取前行过滤正则.Width = 200;
            // 
            // 提取型正则
            // 
            this.提取型正则.DataPropertyName = "提取型正则";
            this.提取型正则.HeaderText = "提取型正则";
            this.提取型正则.Name = "提取型正则";
            this.提取型正则.Width = 200;
            // 
            // 正则设置
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(987, 618);
            this.ControlBoxFillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.Controls.Add(this.查询表格);
            this.Controls.Add(this.正则上半Panel);
            this.Name = "正则设置";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "正则设置";
            this.Page被选中 += new 翻译姬.自定义Page.自定义Page被选中(this.正则设置_Page被选中);
            this.Load += new System.EventHandler(this.正则设置_Load);
            this.Shown += new System.EventHandler(this.正则设置_Shown);
            this.正则上半Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.查询表格)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel 正则上半Panel;
        private 自定义DataGridView 查询表格;
        private 表格增删改 表格增删改;
        private 组合控件TextBox 提取型正则Box;
        private 组合控件TextBox 提取前行过滤正则Box;
        private 组合控件TextBox 文本过滤正则Box;
        private 组合控件TextBox 行过滤正则Box;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn 正则名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 行过滤正则;
        private System.Windows.Forms.DataGridViewTextBoxColumn 文本过滤正则;
        private System.Windows.Forms.DataGridViewTextBoxColumn 提取前行过滤正则;
        private System.Windows.Forms.DataGridViewTextBoxColumn 提取型正则;
        private Sunny.UI.UISymbolLabel uiSymbolLabel1;
        private Sunny.UI.UIButton 预览Btn;
        private Sunny.UI.UITextBox 提示内容Box;
        private 组合控件TextBox 机翻后Box;
        private 组合控件TextBox 待机翻Box;
    }
}