namespace 翻译姬;

partial class 数据处理 {
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
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.缓存导出Btn = new Sunny.UI.UISymbolButton();
            this.人名导出Btn = new Sunny.UI.UISymbolButton();
            this.数据转换Btn = new Sunny.UI.UISymbolButton();
            this.下移Btn = new Sunny.UI.UISymbolButton();
            this.上移Btn = new Sunny.UI.UISymbolButton();
            this.全部保存Btn = new Sunny.UI.UISymbolButton();
            this.返回Btn = new Sunny.UI.UISymbolButton();
            this.uiLabel9 = new Sunny.UI.UILabel();
            this.只显示未完成数据Switch = new 翻译姬.自定义Switch();
            this.文件列表Panel = new Sunny.UI.UIFlowLayoutPanel();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.表格Panel = new Sunny.UI.UIPanel();
            this.表格 = new 翻译姬.自定义DataGridView();
            this.uiPanel3 = new Sunny.UI.UIPanel();
            this.跳转Btn = new Sunny.UI.UISymbolButton();
            this.异常行Btn = new Sunny.UI.UISymbolButton();
            this.重翻Btn = new Sunny.UI.UISymbolButton();
            this.保存Btn = new Sunny.UI.UISymbolButton();
            this.页数显示Label = new Sunny.UI.UILabel();
            this.下一页Btn = new Sunny.UI.UISymbolButton();
            this.上一页Btn = new Sunny.UI.UISymbolButton();
            this.异常 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.原文 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.译文 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiPanel1.SuspendLayout();
            this.uiPanel2.SuspendLayout();
            this.表格Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.表格)).BeginInit();
            this.uiPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.缓存导出Btn);
            this.uiPanel1.Controls.Add(this.人名导出Btn);
            this.uiPanel1.Controls.Add(this.数据转换Btn);
            this.uiPanel1.Controls.Add(this.下移Btn);
            this.uiPanel1.Controls.Add(this.上移Btn);
            this.uiPanel1.Controls.Add(this.全部保存Btn);
            this.uiPanel1.Controls.Add(this.返回Btn);
            this.uiPanel1.Controls.Add(this.uiLabel9);
            this.uiPanel1.Controls.Add(this.只显示未完成数据Switch);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel1.Location = new System.Drawing.Point(0, 0);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel1.Size = new System.Drawing.Size(1280, 59);
            this.uiPanel1.TabIndex = 1;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.移动窗口_MouseDown);
            // 
            // 缓存导出Btn
            // 
            this.缓存导出Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.缓存导出Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.缓存导出Btn.Location = new System.Drawing.Point(776, 12);
            this.缓存导出Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.缓存导出Btn.Name = "缓存导出Btn";
            this.缓存导出Btn.Radius = 25;
            this.缓存导出Btn.Size = new System.Drawing.Size(100, 35);
            this.缓存导出Btn.Symbol = 61686;
            this.缓存导出Btn.SymbolOffset = new System.Drawing.Point(3, 1);
            this.缓存导出Btn.TabIndex = 86;
            this.缓存导出Btn.Text = "缓存导出";
            this.缓存导出Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.缓存导出Btn.Click += new System.EventHandler(this.缓存导出Btn_Click);
            // 
            // 人名导出Btn
            // 
            this.人名导出Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.人名导出Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.人名导出Btn.Location = new System.Drawing.Point(1032, 12);
            this.人名导出Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.人名导出Btn.Name = "人名导出Btn";
            this.人名导出Btn.Radius = 25;
            this.人名导出Btn.Size = new System.Drawing.Size(100, 35);
            this.人名导出Btn.Symbol = 362748;
            this.人名导出Btn.SymbolOffset = new System.Drawing.Point(0, 1);
            this.人名导出Btn.TabIndex = 54;
            this.人名导出Btn.Text = "人名导出";
            this.人名导出Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.人名导出Btn.Click += new System.EventHandler(this.人名导出Btn_Click);
            // 
            // 数据转换Btn
            // 
            this.数据转换Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.数据转换Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.数据转换Btn.Location = new System.Drawing.Point(648, 12);
            this.数据转换Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.数据转换Btn.Name = "数据转换Btn";
            this.数据转换Btn.Radius = 25;
            this.数据转换Btn.Size = new System.Drawing.Size(100, 35);
            this.数据转换Btn.Symbol = 61473;
            this.数据转换Btn.SymbolOffset = new System.Drawing.Point(2, 1);
            this.数据转换Btn.TabIndex = 53;
            this.数据转换Btn.Text = "数据转换";
            this.数据转换Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.数据转换Btn.Click += new System.EventHandler(this.数据转换Btn_Click);
            // 
            // 下移Btn
            // 
            this.下移Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.下移Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.下移Btn.Location = new System.Drawing.Point(376, 12);
            this.下移Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.下移Btn.Name = "下移Btn";
            this.下移Btn.Radius = 15;
            this.下移Btn.Size = new System.Drawing.Size(84, 35);
            this.下移Btn.Symbol = 61611;
            this.下移Btn.TabIndex = 85;
            this.下移Btn.Text = "下移";
            this.下移Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.下移Btn.TipsText = "提示文本";
            this.下移Btn.Visible = false;
            this.下移Btn.Click += new System.EventHandler(this.下移Btn_Click);
            // 
            // 上移Btn
            // 
            this.上移Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.上移Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.上移Btn.Location = new System.Drawing.Point(275, 12);
            this.上移Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.上移Btn.Name = "上移Btn";
            this.上移Btn.Radius = 15;
            this.上移Btn.Size = new System.Drawing.Size(84, 35);
            this.上移Btn.Symbol = 61610;
            this.上移Btn.TabIndex = 84;
            this.上移Btn.Text = "上移";
            this.上移Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.上移Btn.TipsText = "提示文本";
            this.上移Btn.Visible = false;
            this.上移Btn.Click += new System.EventHandler(this.上移Btn_Click);
            // 
            // 全部保存Btn
            // 
            this.全部保存Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.全部保存Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.全部保存Btn.Location = new System.Drawing.Point(904, 12);
            this.全部保存Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.全部保存Btn.Name = "全部保存Btn";
            this.全部保存Btn.Radius = 25;
            this.全部保存Btn.Size = new System.Drawing.Size(100, 35);
            this.全部保存Btn.Symbol = 61639;
            this.全部保存Btn.SymbolOffset = new System.Drawing.Point(2, 1);
            this.全部保存Btn.TabIndex = 51;
            this.全部保存Btn.Text = "全部保存";
            this.全部保存Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.全部保存Btn.Click += new System.EventHandler(this.全部保存Btn_Click);
            // 
            // 返回Btn
            // 
            this.返回Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.返回Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.返回Btn.Location = new System.Drawing.Point(1160, 12);
            this.返回Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.返回Btn.Name = "返回Btn";
            this.返回Btn.Radius = 25;
            this.返回Btn.Size = new System.Drawing.Size(100, 35);
            this.返回Btn.Symbol = 61579;
            this.返回Btn.TabIndex = 50;
            this.返回Btn.Text = "返回";
            this.返回Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.返回Btn.Click += new System.EventHandler(this.返回Btn_Click);
            // 
            // uiLabel9
            // 
            this.uiLabel9.AutoSize = true;
            this.uiLabel9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel9.Location = new System.Drawing.Point(27, 19);
            this.uiLabel9.Name = "uiLabel9";
            this.uiLabel9.Size = new System.Drawing.Size(138, 21);
            this.uiLabel9.TabIndex = 49;
            this.uiLabel9.Text = "只显示未完成数据";
            this.uiLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 只显示未完成数据Switch
            // 
            this.只显示未完成数据Switch.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.只显示未完成数据Switch.Location = new System.Drawing.Point(171, 15);
            this.只显示未完成数据Switch.MinimumSize = new System.Drawing.Size(1, 1);
            this.只显示未完成数据Switch.Name = "只显示未完成数据Switch";
            this.只显示未完成数据Switch.Size = new System.Drawing.Size(75, 29);
            this.只显示未完成数据Switch.TabIndex = 48;
            this.只显示未完成数据Switch.Text = "自定义Switch1";
            this.只显示未完成数据Switch.ActiveChanged += new System.EventHandler(this.只显示未完成数据Switch_ActiveChanged);
            // 
            // 文件列表Panel
            // 
            this.文件列表Panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.文件列表Panel.Dock = System.Windows.Forms.DockStyle.Left;
            this.文件列表Panel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.文件列表Panel.Location = new System.Drawing.Point(0, 59);
            this.文件列表Panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.文件列表Panel.MinimumSize = new System.Drawing.Size(1, 1);
            this.文件列表Panel.Name = "文件列表Panel";
            this.文件列表Panel.Padding = new System.Windows.Forms.Padding(2);
            this.文件列表Panel.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.文件列表Panel.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.文件列表Panel.ShowText = false;
            this.文件列表Panel.Size = new System.Drawing.Size(275, 661);
            this.文件列表Panel.TabIndex = 2;
            this.文件列表Panel.Text = null;
            this.文件列表Panel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiPanel2
            // 
            this.uiPanel2.Controls.Add(this.表格Panel);
            this.uiPanel2.Controls.Add(this.uiPanel3);
            this.uiPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel2.Location = new System.Drawing.Point(275, 59);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel2.Size = new System.Drawing.Size(1005, 661);
            this.uiPanel2.TabIndex = 3;
            this.uiPanel2.Text = null;
            this.uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 表格Panel
            // 
            this.表格Panel.Controls.Add(this.表格);
            this.表格Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.表格Panel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.表格Panel.Location = new System.Drawing.Point(0, 53);
            this.表格Panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.表格Panel.MinimumSize = new System.Drawing.Size(1, 1);
            this.表格Panel.Name = "表格Panel";
            this.表格Panel.Padding = new System.Windows.Forms.Padding(2);
            this.表格Panel.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.表格Panel.Size = new System.Drawing.Size(1005, 608);
            this.表格Panel.TabIndex = 1;
            this.表格Panel.Text = "uiPanel4";
            this.表格Panel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 表格
            // 
            this.表格.AllowUserToAddRows = false;
            this.表格.AllowUserToDeleteRows = false;
            this.表格.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.表格.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.表格.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.表格.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.表格.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.表格.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.表格.ColumnHeadersHeight = 32;
            this.表格.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.表格.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.异常,
            this.原文,
            this.译文});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.表格.DefaultCellStyle = dataGridViewCellStyle3;
            this.表格.Dock = System.Windows.Forms.DockStyle.Fill;
            this.表格.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.表格.EnableHeadersVisualStyles = false;
            this.表格.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.表格.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(173)))), ((int)(((byte)(255)))));
            this.表格.Location = new System.Drawing.Point(2, 2);
            this.表格.Name = "表格";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.表格.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.表格.RowHeadersVisible = false;
            this.表格.RowHeadersWidth = 4;
            this.表格.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.表格.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.表格.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Blue;
            this.表格.RowTemplate.Height = 29;
            this.表格.Size = new System.Drawing.Size(1001, 604);
            this.表格.TabIndex = 6;
            // 
            // uiPanel3
            // 
            this.uiPanel3.Controls.Add(this.跳转Btn);
            this.uiPanel3.Controls.Add(this.异常行Btn);
            this.uiPanel3.Controls.Add(this.重翻Btn);
            this.uiPanel3.Controls.Add(this.保存Btn);
            this.uiPanel3.Controls.Add(this.页数显示Label);
            this.uiPanel3.Controls.Add(this.下一页Btn);
            this.uiPanel3.Controls.Add(this.上一页Btn);
            this.uiPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiPanel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel3.Location = new System.Drawing.Point(0, 0);
            this.uiPanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel3.Name = "uiPanel3";
            this.uiPanel3.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel3.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.uiPanel3.Size = new System.Drawing.Size(1005, 53);
            this.uiPanel3.TabIndex = 0;
            this.uiPanel3.Text = null;
            this.uiPanel3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 跳转Btn
            // 
            this.跳转Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.跳转Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.跳转Btn.Location = new System.Drawing.Point(341, 8);
            this.跳转Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.跳转Btn.Name = "跳转Btn";
            this.跳转Btn.Radius = 15;
            this.跳转Btn.Size = new System.Drawing.Size(84, 35);
            this.跳转Btn.Symbol = 61540;
            this.跳转Btn.TabIndex = 87;
            this.跳转Btn.Text = "跳转";
            this.跳转Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.跳转Btn.TipsText = "提示文本";
            this.跳转Btn.Click += new System.EventHandler(this.跳转Btn_Click);
            // 
            // 异常行Btn
            // 
            this.异常行Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.异常行Btn.Enabled = false;
            this.异常行Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.异常行Btn.Location = new System.Drawing.Point(677, 8);
            this.异常行Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.异常行Btn.Name = "异常行Btn";
            this.异常行Btn.Radius = 15;
            this.异常行Btn.Size = new System.Drawing.Size(84, 35);
            this.异常行Btn.Symbol = 61609;
            this.异常行Btn.TabIndex = 86;
            this.异常行Btn.Text = "异常行";
            this.异常行Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.异常行Btn.TipsText = "提示文本";
            this.异常行Btn.Click += new System.EventHandler(this.异常行Btn_Click);
            // 
            // 重翻Btn
            // 
            this.重翻Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.重翻Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.重翻Btn.Location = new System.Drawing.Point(789, 8);
            this.重翻Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.重翻Btn.Name = "重翻Btn";
            this.重翻Btn.Radius = 15;
            this.重翻Btn.Size = new System.Drawing.Size(84, 35);
            this.重翻Btn.Symbol = 61473;
            this.重翻Btn.TabIndex = 83;
            this.重翻Btn.Text = "重翻";
            this.重翻Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.重翻Btn.TipsText = "提示文本";
            this.重翻Btn.Click += new System.EventHandler(this.重翻Btn_Click);
            // 
            // 保存Btn
            // 
            this.保存Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.保存Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.保存Btn.Location = new System.Drawing.Point(901, 8);
            this.保存Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.保存Btn.Name = "保存Btn";
            this.保存Btn.Radius = 10;
            this.保存Btn.Size = new System.Drawing.Size(84, 35);
            this.保存Btn.Symbol = 61639;
            this.保存Btn.TabIndex = 82;
            this.保存Btn.Text = "保存";
            this.保存Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.保存Btn.Click += new System.EventHandler(this.保存Btn_Click);
            // 
            // 页数显示Label
            // 
            this.页数显示Label.AutoSize = true;
            this.页数显示Label.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.页数显示Label.Location = new System.Drawing.Point(36, 16);
            this.页数显示Label.Name = "页数显示Label";
            this.页数显示Label.Size = new System.Drawing.Size(67, 21);
            this.页数显示Label.TabIndex = 81;
            this.页数显示Label.Text = "第0/0页";
            this.页数显示Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 下一页Btn
            // 
            this.下一页Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.下一页Btn.Enabled = false;
            this.下一页Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.下一页Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.下一页Btn.Location = new System.Drawing.Point(565, 8);
            this.下一页Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.下一页Btn.Name = "下一页Btn";
            this.下一页Btn.Radius = 10;
            this.下一页Btn.Size = new System.Drawing.Size(84, 35);
            this.下一页Btn.Symbol = 61701;
            this.下一页Btn.TabIndex = 80;
            this.下一页Btn.Text = "下一页";
            this.下一页Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.下一页Btn.TipsText = "提示文本";
            this.下一页Btn.Click += new System.EventHandler(this.下一页Btn_Click);
            // 
            // 上一页Btn
            // 
            this.上一页Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.上一页Btn.Enabled = false;
            this.上一页Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.上一页Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.上一页Btn.Location = new System.Drawing.Point(453, 8);
            this.上一页Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.上一页Btn.Name = "上一页Btn";
            this.上一页Btn.Radius = 10;
            this.上一页Btn.Size = new System.Drawing.Size(84, 35);
            this.上一页Btn.Symbol = 61700;
            this.上一页Btn.TabIndex = 79;
            this.上一页Btn.Text = "上一页";
            this.上一页Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.上一页Btn.TipsText = "提示文本";
            this.上一页Btn.Click += new System.EventHandler(this.上一页Btn_Click);
            // 
            // 异常
            // 
            this.异常.DataPropertyName = "异常";
            this.异常.HeaderText = "";
            this.异常.Name = "异常";
            this.异常.ReadOnly = true;
            this.异常.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.异常.Width = 82;
            // 
            // 原文
            // 
            this.原文.DataPropertyName = "原文";
            this.原文.HeaderText = "原文";
            this.原文.Name = "原文";
            this.原文.ReadOnly = true;
            this.原文.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.原文.Width = 450;
            // 
            // 译文
            // 
            this.译文.DataPropertyName = "译文";
            this.译文.HeaderText = "译文";
            this.译文.Name = "译文";
            this.译文.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.译文.Width = 450;
            // 
            // 数据处理
            // 
            this.AllowShowTitle = false;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.uiPanel2);
            this.Controls.Add(this.文件列表Panel);
            this.Controls.Add(this.uiPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "数据处理";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.ShowInTaskbar = false;
            this.ShowRadius = false;
            this.ShowTitle = false;
            this.Text = "数据处理";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.数据处理_FormClosed);
            this.Load += new System.EventHandler(this.数据处理_Load);
            this.uiPanel1.ResumeLayout(false);
            this.uiPanel1.PerformLayout();
            this.uiPanel2.ResumeLayout(false);
            this.表格Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.表格)).EndInit();
            this.uiPanel3.ResumeLayout(false);
            this.uiPanel3.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private Sunny.UI.UIPanel uiPanel1;
    private Sunny.UI.UIFlowLayoutPanel 文件列表Panel;
    private Sunny.UI.UIPanel uiPanel2;
    private Sunny.UI.UILabel uiLabel9;
    private 自定义Switch 只显示未完成数据Switch;
    private Sunny.UI.UIPanel uiPanel3;
    private Sunny.UI.UIPanel 表格Panel;
    public 自定义DataGridView 表格;
    private Sunny.UI.UISymbolButton 返回Btn;
    private Sunny.UI.UISymbolButton 保存Btn;
    private Sunny.UI.UILabel 页数显示Label;
    public Sunny.UI.UISymbolButton 下一页Btn;
    public Sunny.UI.UISymbolButton 上一页Btn;
    public Sunny.UI.UISymbolButton 重翻Btn;
    private Sunny.UI.UISymbolButton 全部保存Btn;
    public Sunny.UI.UISymbolButton 下移Btn;
    public Sunny.UI.UISymbolButton 上移Btn;
    public Sunny.UI.UISymbolButton 异常行Btn;
    private Sunny.UI.UISymbolButton 数据转换Btn;
    private Sunny.UI.UISymbolButton 人名导出Btn;
    public Sunny.UI.UISymbolButton 跳转Btn;
    private Sunny.UI.UISymbolButton 缓存导出Btn;
    private System.Windows.Forms.DataGridViewTextBoxColumn 异常;
    private System.Windows.Forms.DataGridViewTextBoxColumn 原文;
    private System.Windows.Forms.DataGridViewTextBoxColumn 译文;
}