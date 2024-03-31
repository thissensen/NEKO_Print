namespace 翻译姬;

partial class 跳转到 {
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
            this.页码Box = new 翻译姬.组合控件TextBox();
            this.SuspendLayout();
            // 
            // 返回Btn
            // 
            this.返回Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.返回Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.返回Btn.Location = new System.Drawing.Point(205, 100);
            this.返回Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.返回Btn.Name = "返回Btn";
            this.返回Btn.Radius = 15;
            this.返回Btn.Size = new System.Drawing.Size(100, 35);
            this.返回Btn.TabIndex = 14;
            this.返回Btn.Text = "返回";
            this.返回Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.返回Btn.Click += new System.EventHandler(this.返回Btn_Click);
            // 
            // 确定Btn
            // 
            this.确定Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.确定Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.确定Btn.Location = new System.Drawing.Point(42, 100);
            this.确定Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.确定Btn.Name = "确定Btn";
            this.确定Btn.Radius = 15;
            this.确定Btn.Size = new System.Drawing.Size(100, 35);
            this.确定Btn.TabIndex = 13;
            this.确定Btn.Text = "确定";
            this.确定Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.确定Btn.Click += new System.EventHandler(this.确定Btn_Click);
            // 
            // 页码Box
            // 
            this.页码Box.BackColor = System.Drawing.Color.Transparent;
            this.页码Box.CanEmpty = false;
            this.页码Box.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.页码Box.DecimalPlaces = 2;
            this.页码Box.LabelFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.页码Box.LabelText = "页码";
            this.页码Box.Label方向 = 翻译姬.数据库控件Label方向.左;
            this.页码Box.Location = new System.Drawing.Point(23, 54);
            this.页码Box.Maximum = 2147483647D;
            this.页码Box.MaxLength = 32767;
            this.页码Box.Minimum = 1D;
            this.页码Box.MinimumSize = new System.Drawing.Size(0, 29);
            this.页码Box.Multiline = false;
            this.页码Box.Name = "页码Box";
            this.页码Box.ReadOnly = false;
            this.页码Box.Size = new System.Drawing.Size(269, 29);
            this.页码Box.TabIndex = 15;
            this.页码Box.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.页码Box.Watermark = "";
            this.页码Box.主控件Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.页码Box.主控件Text = "1";
            this.页码Box.是否允许为零 = true;
            this.页码Box.是否判断条件 = false;
            this.页码Box.是否序列化 = false;
            // 
            // 跳转到
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(340, 150);
            this.ControlBox = false;
            this.Controls.Add(this.页码Box);
            this.Controls.Add(this.返回Btn);
            this.Controls.Add(this.确定Btn);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "跳转到";
            this.ShowInTaskbar = false;
            this.Text = "跳转到";
            this.ResumeLayout(false);

    }

    #endregion

    private Sunny.UI.UIButton 返回Btn;
    private Sunny.UI.UIButton 确定Btn;
    public 组合控件TextBox 页码Box;
}