namespace 翻译姬;

partial class 正则指令数值更改 {
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
            this.指令集Box = new 翻译姬.组合控件TextBox();
            this.SuspendLayout();
            // 
            // 返回Btn
            // 
            this.返回Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.返回Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.返回Btn.Location = new System.Drawing.Point(338, 292);
            this.返回Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.返回Btn.Name = "返回Btn";
            this.返回Btn.Radius = 15;
            this.返回Btn.Size = new System.Drawing.Size(100, 35);
            this.返回Btn.TabIndex = 17;
            this.返回Btn.Text = "返回";
            this.返回Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.返回Btn.Click += new System.EventHandler(this.返回Btn_Click);
            // 
            // 确定Btn
            // 
            this.确定Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.确定Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.确定Btn.Location = new System.Drawing.Point(175, 292);
            this.确定Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.确定Btn.Name = "确定Btn";
            this.确定Btn.Radius = 15;
            this.确定Btn.Size = new System.Drawing.Size(100, 35);
            this.确定Btn.TabIndex = 16;
            this.确定Btn.Text = "确定";
            this.确定Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.确定Btn.Click += new System.EventHandler(this.确定Btn_Click);
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
            this.指令集Box.Location = new System.Drawing.Point(14, 52);
            this.指令集Box.Maximum = 2147483647D;
            this.指令集Box.MaxLength = 32767;
            this.指令集Box.Minimum = -2147483648D;
            this.指令集Box.MinimumSize = new System.Drawing.Size(0, 29);
            this.指令集Box.Multiline = true;
            this.指令集Box.Name = "指令集Box";
            this.指令集Box.ReadOnly = false;
            this.指令集Box.Size = new System.Drawing.Size(609, 221);
            this.指令集Box.TabIndex = 15;
            this.指令集Box.Type = Sunny.UI.UITextBox.UIEditType.String;
            this.指令集Box.Watermark = "";
            this.指令集Box.主控件Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.指令集Box.主控件Text = "";
            this.指令集Box.是否允许为零 = true;
            this.指令集Box.是否判断条件 = false;
            this.指令集Box.是否序列化 = false;
            // 
            // 正则指令数值更改
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(637, 347);
            this.ControlBox = false;
            this.Controls.Add(this.返回Btn);
            this.Controls.Add(this.确定Btn);
            this.Controls.Add(this.指令集Box);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "正则指令数值更改";
            this.ShowInTaskbar = false;
            this.Text = "正则指令数值更改";
            this.Load += new System.EventHandler(this.正则指令数值更改_Load);
            this.ResumeLayout(false);

    }

    #endregion

    private Sunny.UI.UIButton 返回Btn;
    private Sunny.UI.UIButton 确定Btn;
    private 组合控件TextBox 指令集Box;
}