namespace 翻译姬;

partial class 数组数据数值更改 {
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
            this.返回Btn = new Sunny.UI.UIButton();
            this.确定Btn = new Sunny.UI.UIButton();
            this.数据Box = new 翻译姬.组合控件TextBox();
            this.SuspendLayout();
            // 
            // 返回Btn
            // 
            this.返回Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.返回Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.返回Btn.Location = new System.Drawing.Point(206, 347);
            this.返回Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.返回Btn.Name = "返回Btn";
            this.返回Btn.Radius = 15;
            this.返回Btn.Size = new System.Drawing.Size(100, 35);
            this.返回Btn.TabIndex = 20;
            this.返回Btn.Text = "返回";
            this.返回Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.返回Btn.Click += new System.EventHandler(this.返回Btn_Click);
            // 
            // 确定Btn
            // 
            this.确定Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.确定Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.确定Btn.Location = new System.Drawing.Point(43, 347);
            this.确定Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.确定Btn.Name = "确定Btn";
            this.确定Btn.Radius = 15;
            this.确定Btn.Size = new System.Drawing.Size(100, 35);
            this.确定Btn.TabIndex = 19;
            this.确定Btn.Text = "确定";
            this.确定Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.确定Btn.Click += new System.EventHandler(this.确定Btn_Click);
            // 
            // 数据Box
            // 
            this.数据Box.BackColor = System.Drawing.Color.Transparent;
            this.数据Box.CanEmpty = false;
            this.数据Box.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.数据Box.DecimalPlaces = 2;
            this.数据Box.LabelFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.数据Box.LabelText = "数据";
            this.数据Box.Label方向 = 翻译姬.数据库控件Label方向.上;
            this.数据Box.Location = new System.Drawing.Point(21, 58);
            this.数据Box.Maximum = 2147483647D;
            this.数据Box.MaxLength = 32767;
            this.数据Box.Minimum = -2147483648D;
            this.数据Box.MinimumSize = new System.Drawing.Size(0, 29);
            this.数据Box.Multiline = true;
            this.数据Box.Name = "数据Box";
            this.数据Box.ReadOnly = false;
            this.数据Box.Size = new System.Drawing.Size(319, 270);
            this.数据Box.TabIndex = 18;
            this.数据Box.Type = Sunny.UI.UITextBox.UIEditType.String;
            this.数据Box.Watermark = "";
            this.数据Box.主控件Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.数据Box.主控件Text = "";
            this.数据Box.是否允许为零 = true;
            this.数据Box.是否判断条件 = false;
            this.数据Box.是否序列化 = false;
            // 
            // 数组数据数值更改
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(360, 406);
            this.ControlBox = false;
            this.Controls.Add(this.返回Btn);
            this.Controls.Add(this.确定Btn);
            this.Controls.Add(this.数据Box);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "数组数据数值更改";
            this.ShowInTaskbar = false;
            this.Text = "数组数据数值更改";
            this.ResumeLayout(false);

    }

    #endregion

    private Sunny.UI.UIButton 返回Btn;
    private Sunny.UI.UIButton 确定Btn;
    private 组合控件TextBox 数据Box;
}