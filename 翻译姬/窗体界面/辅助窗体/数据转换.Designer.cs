namespace 翻译姬;

partial class 数据转换 {
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
            this.转换类型Box = new Sunny.UI.UIComboBox();
            this.类型预览Box = new Sunny.UI.UITextBox();
            this.全部导入Btn = new Sunny.UI.UISymbolButton();
            this.全部导出Btn = new Sunny.UI.UISymbolButton();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.返回Btn = new Sunny.UI.UIButton();
            this.SuspendLayout();
            // 
            // 转换类型Box
            // 
            this.转换类型Box.DataSource = null;
            this.转换类型Box.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.转换类型Box.FillColor = System.Drawing.Color.White;
            this.转换类型Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.转换类型Box.Location = new System.Drawing.Point(105, 54);
            this.转换类型Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.转换类型Box.MinimumSize = new System.Drawing.Size(63, 0);
            this.转换类型Box.Name = "转换类型Box";
            this.转换类型Box.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.转换类型Box.Size = new System.Drawing.Size(225, 29);
            this.转换类型Box.TabIndex = 46;
            this.转换类型Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.转换类型Box.Watermark = "";
            this.转换类型Box.TextChanged += new System.EventHandler(this.转换类型Box_TextChanged);
            // 
            // 类型预览Box
            // 
            this.类型预览Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.类型预览Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.类型预览Box.Location = new System.Drawing.Point(26, 97);
            this.类型预览Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.类型预览Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.类型预览Box.Multiline = true;
            this.类型预览Box.Name = "类型预览Box";
            this.类型预览Box.ReadOnly = true;
            this.类型预览Box.ShowText = false;
            this.类型预览Box.Size = new System.Drawing.Size(304, 341);
            this.类型预览Box.TabIndex = 112;
            this.类型预览Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.类型预览Box.Watermark = "";
            // 
            // 全部导入Btn
            // 
            this.全部导入Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.全部导入Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.全部导入Btn.Location = new System.Drawing.Point(372, 97);
            this.全部导入Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.全部导入Btn.Name = "全部导入Btn";
            this.全部导入Btn.Radius = 25;
            this.全部导入Btn.Size = new System.Drawing.Size(100, 35);
            this.全部导入Btn.Symbol = 362829;
            this.全部导入Btn.SymbolOffset = new System.Drawing.Point(5, 1);
            this.全部导入Btn.TabIndex = 114;
            this.全部导入Btn.Text = "全部导入";
            this.全部导入Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.全部导入Btn.Click += new System.EventHandler(this.全部导入Btn_Click);
            // 
            // 全部导出Btn
            // 
            this.全部导出Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.全部导出Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.全部导出Btn.Location = new System.Drawing.Point(372, 48);
            this.全部导出Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.全部导出Btn.Name = "全部导出Btn";
            this.全部导出Btn.Radius = 25;
            this.全部导出Btn.Size = new System.Drawing.Size(100, 35);
            this.全部导出Btn.Symbol = 362836;
            this.全部导出Btn.SymbolOffset = new System.Drawing.Point(5, 1);
            this.全部导出Btn.TabIndex = 113;
            this.全部导出Btn.Text = "全部导出";
            this.全部导出Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.全部导出Btn.Click += new System.EventHandler(this.全部导出Btn_Click);
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.Location = new System.Drawing.Point(24, 58);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(74, 21);
            this.uiLabel2.TabIndex = 115;
            this.uiLabel2.Text = "转换类型";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 返回Btn
            // 
            this.返回Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.返回Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.返回Btn.Location = new System.Drawing.Point(372, 403);
            this.返回Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.返回Btn.Name = "返回Btn";
            this.返回Btn.Radius = 15;
            this.返回Btn.Size = new System.Drawing.Size(100, 35);
            this.返回Btn.TabIndex = 117;
            this.返回Btn.Text = "返回";
            this.返回Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.返回Btn.Click += new System.EventHandler(this.返回Btn_Click);
            // 
            // 数据转换
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(516, 450);
            this.ControlBox = false;
            this.Controls.Add(this.返回Btn);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.全部导入Btn);
            this.Controls.Add(this.全部导出Btn);
            this.Controls.Add(this.类型预览Box);
            this.Controls.Add(this.转换类型Box);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "数据转换";
            this.ShowInTaskbar = false;
            this.ShowRadius = false;
            this.Text = "数据转换";
            this.Load += new System.EventHandler(this.数据导出Form_Load);
            this.ResumeLayout(false);

    }

    #endregion
    private Sunny.UI.UIComboBox 转换类型Box;
    private Sunny.UI.UITextBox 类型预览Box;
    private Sunny.UI.UISymbolButton 全部导入Btn;
    private Sunny.UI.UISymbolButton 全部导出Btn;
    private Sunny.UI.UILabel uiLabel2;
    private Sunny.UI.UIButton 返回Btn;
}