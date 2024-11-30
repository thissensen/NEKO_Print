namespace 翻译姬;

partial class GPT请求参数 {
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
            this.文本1Box = new Sunny.UI.UILabel();
            this.frequency_penalty = new Sunny.UI.UITextBox();
            this.提示内容1Box = new Sunny.UI.UITextBox();
            this.提示内容2Box = new Sunny.UI.UITextBox();
            this.文本2Box = new Sunny.UI.UILabel();
            this.temperature = new Sunny.UI.UITextBox();
            this.提示内容3Box = new Sunny.UI.UITextBox();
            this.文本3Box = new Sunny.UI.UILabel();
            this.top_p = new Sunny.UI.UITextBox();
            this.确定Btn = new Sunny.UI.UIButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.默认Btn = new Sunny.UI.UIButton();
            this.精准Btn = new Sunny.UI.UIButton();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // 文本1Box
            // 
            this.文本1Box.AutoSize = true;
            this.文本1Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.文本1Box.Location = new System.Drawing.Point(17, 82);
            this.文本1Box.Name = "文本1Box";
            this.文本1Box.Size = new System.Drawing.Size(223, 21);
            this.文本1Box.TabIndex = 46;
            this.文本1Box.Text = "退化处理(frequency_penalty)";
            this.文本1Box.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frequency_penalty
            // 
            this.frequency_penalty.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.frequency_penalty.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.frequency_penalty.Location = new System.Drawing.Point(274, 78);
            this.frequency_penalty.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.frequency_penalty.Maximum = 2D;
            this.frequency_penalty.Minimum = -2D;
            this.frequency_penalty.MinimumSize = new System.Drawing.Size(1, 16);
            this.frequency_penalty.Name = "frequency_penalty";
            this.frequency_penalty.ShowText = false;
            this.frequency_penalty.Size = new System.Drawing.Size(59, 29);
            this.frequency_penalty.TabIndex = 45;
            this.frequency_penalty.Text = "0.00";
            this.frequency_penalty.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.frequency_penalty.Type = Sunny.UI.UITextBox.UIEditType.Double;
            this.frequency_penalty.Watermark = "";
            // 
            // 提示内容1Box
            // 
            this.提示内容1Box.AutoSize = true;
            this.提示内容1Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.提示内容1Box.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.提示内容1Box.Location = new System.Drawing.Point(17, 53);
            this.提示内容1Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.提示内容1Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.提示内容1Box.Name = "提示内容1Box";
            this.提示内容1Box.ReadOnly = true;
            this.提示内容1Box.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.提示内容1Box.ShowText = false;
            this.提示内容1Box.Size = new System.Drawing.Size(259, 23);
            this.提示内容1Box.TabIndex = 47;
            this.提示内容1Box.Text = "默认0,处理退化(文本重复),越高频率越低";
            this.提示内容1Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.提示内容1Box.Watermark = "";
            // 
            // 提示内容2Box
            // 
            this.提示内容2Box.AutoSize = true;
            this.提示内容2Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.提示内容2Box.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.提示内容2Box.Location = new System.Drawing.Point(17, 113);
            this.提示内容2Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.提示内容2Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.提示内容2Box.Name = "提示内容2Box";
            this.提示内容2Box.ReadOnly = true;
            this.提示内容2Box.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.提示内容2Box.ShowText = false;
            this.提示内容2Box.Size = new System.Drawing.Size(164, 23);
            this.提示内容2Box.TabIndex = 50;
            this.提示内容2Box.Text = "默认1,越高输出越随机";
            this.提示内容2Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.提示内容2Box.Watermark = "";
            // 
            // 文本2Box
            // 
            this.文本2Box.AutoSize = true;
            this.文本2Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.文本2Box.Location = new System.Drawing.Point(17, 142);
            this.文本2Box.Name = "文本2Box";
            this.文本2Box.Size = new System.Drawing.Size(227, 21);
            this.文本2Box.TabIndex = 49;
            this.文本2Box.Text = "文本输出随机性(temperature)";
            this.文本2Box.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // temperature
            // 
            this.temperature.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.temperature.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.temperature.Location = new System.Drawing.Point(274, 138);
            this.temperature.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.temperature.Maximum = 2D;
            this.temperature.Minimum = 0D;
            this.temperature.MinimumSize = new System.Drawing.Size(1, 16);
            this.temperature.Name = "temperature";
            this.temperature.ShowText = false;
            this.temperature.Size = new System.Drawing.Size(59, 29);
            this.temperature.TabIndex = 48;
            this.temperature.Text = "0.00";
            this.temperature.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.temperature.Type = Sunny.UI.UITextBox.UIEditType.Double;
            this.temperature.Watermark = "";
            // 
            // 提示内容3Box
            // 
            this.提示内容3Box.AutoSize = true;
            this.提示内容3Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.提示内容3Box.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.提示内容3Box.Location = new System.Drawing.Point(17, 173);
            this.提示内容3Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.提示内容3Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.提示内容3Box.Name = "提示内容3Box";
            this.提示内容3Box.ReadOnly = true;
            this.提示内容3Box.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.提示内容3Box.ShowText = false;
            this.提示内容3Box.Size = new System.Drawing.Size(222, 23);
            this.提示内容3Box.TabIndex = 53;
            this.提示内容3Box.Text = "默认1,0.1表示选取前10%的答案";
            this.提示内容3Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.提示内容3Box.Watermark = "";
            // 
            // 文本3Box
            // 
            this.文本3Box.AutoSize = true;
            this.文本3Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.文本3Box.Location = new System.Drawing.Point(17, 202);
            this.文本3Box.Name = "文本3Box";
            this.文本3Box.Size = new System.Drawing.Size(127, 21);
            this.文本3Box.TabIndex = 52;
            this.文本3Box.Text = "质量采样(top_p)";
            this.文本3Box.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // top_p
            // 
            this.top_p.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.top_p.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.top_p.Location = new System.Drawing.Point(274, 198);
            this.top_p.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.top_p.Maximum = 1D;
            this.top_p.Minimum = 0D;
            this.top_p.MinimumSize = new System.Drawing.Size(1, 16);
            this.top_p.Name = "top_p";
            this.top_p.ShowText = false;
            this.top_p.Size = new System.Drawing.Size(59, 29);
            this.top_p.TabIndex = 51;
            this.top_p.Text = "0.00";
            this.top_p.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.top_p.Type = Sunny.UI.UITextBox.UIEditType.Double;
            this.top_p.Watermark = "";
            // 
            // 确定Btn
            // 
            this.确定Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.确定Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.确定Btn.Location = new System.Drawing.Point(127, 243);
            this.确定Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.确定Btn.Name = "确定Btn";
            this.确定Btn.Radius = 15;
            this.确定Btn.Size = new System.Drawing.Size(100, 35);
            this.确定Btn.TabIndex = 54;
            this.确定Btn.Text = "确定";
            this.确定Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.确定Btn.Click += new System.EventHandler(this.确定Btn_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uiLabel1);
            this.panel1.Controls.Add(this.精准Btn);
            this.panel1.Controls.Add(this.默认Btn);
            this.panel1.Controls.Add(this.确定Btn);
            this.panel1.Controls.Add(this.frequency_penalty);
            this.panel1.Controls.Add(this.提示内容3Box);
            this.panel1.Controls.Add(this.temperature);
            this.panel1.Controls.Add(this.提示内容2Box);
            this.panel1.Controls.Add(this.文本1Box);
            this.panel1.Controls.Add(this.文本3Box);
            this.panel1.Controls.Add(this.文本2Box);
            this.panel1.Controls.Add(this.提示内容1Box);
            this.panel1.Controls.Add(this.top_p);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(361, 294);
            this.panel1.TabIndex = 55;
            // 
            // 默认Btn
            // 
            this.默认Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.默认Btn.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.默认Btn.Location = new System.Drawing.Point(153, 10);
            this.默认Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.默认Btn.Name = "默认Btn";
            this.默认Btn.Radius = 15;
            this.默认Btn.Size = new System.Drawing.Size(74, 29);
            this.默认Btn.TabIndex = 55;
            this.默认Btn.Text = "默认";
            this.默认Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.默认Btn.Click += new System.EventHandler(this.默认Btn_Click);
            // 
            // 精准Btn
            // 
            this.精准Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.精准Btn.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.精准Btn.Location = new System.Drawing.Point(259, 10);
            this.精准Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.精准Btn.Name = "精准Btn";
            this.精准Btn.Radius = 15;
            this.精准Btn.Size = new System.Drawing.Size(74, 29);
            this.精准Btn.TabIndex = 56;
            this.精准Btn.Text = "精准";
            this.精准Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.精准Btn.Click += new System.EventHandler(this.精准Btn_Click);
            // 
            // uiLabel1
            // 
            this.uiLabel1.AutoSize = true;
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(17, 14);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(74, 21);
            this.uiLabel1.TabIndex = 57;
            this.uiLabel1.Text = "预设参数";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GPT请求参数
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(363, 331);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GPT请求参数";
            this.Padding = new System.Windows.Forms.Padding(1, 36, 1, 1);
            this.ShowInTaskbar = false;
            this.Text = "GPT请求参数";
            this.Load += new System.EventHandler(this.GPT请求参数_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion
    private Sunny.UI.UILabel 文本1Box;
    private Sunny.UI.UITextBox frequency_penalty;
    private Sunny.UI.UITextBox 提示内容1Box;
    private Sunny.UI.UITextBox 提示内容2Box;
    private Sunny.UI.UILabel 文本2Box;
    private Sunny.UI.UITextBox temperature;
    private Sunny.UI.UITextBox 提示内容3Box;
    private Sunny.UI.UILabel 文本3Box;
    private Sunny.UI.UITextBox top_p;
    private Sunny.UI.UIButton 确定Btn;
    private System.Windows.Forms.Panel panel1;
    private Sunny.UI.UIButton 默认Btn;
    private Sunny.UI.UIButton 精准Btn;
    private Sunny.UI.UILabel uiLabel1;
}