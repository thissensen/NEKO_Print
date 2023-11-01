namespace 翻译姬 {
    partial class 替换列表数值更改 {
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
            this.字典选择Btn = new Sunny.UI.UIButton();
            this.提取型正则Box = new 翻译姬.组合控件TextBox();
            this.uiSymbolLabel1 = new Sunny.UI.UISymbolLabel();
            this.替换列表Box = new 翻译姬.组合控件TextBox();
            this.确定Btn = new Sunny.UI.UIButton();
            this.返回Btn = new Sunny.UI.UIButton();
            this.uiSymbolLabel2 = new Sunny.UI.UISymbolLabel();
            this.SuspendLayout();
            // 
            // 字典选择Btn
            // 
            this.字典选择Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.字典选择Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.字典选择Btn.Location = new System.Drawing.Point(279, 47);
            this.字典选择Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.字典选择Btn.Name = "字典选择Btn";
            this.字典选择Btn.Radius = 15;
            this.字典选择Btn.Size = new System.Drawing.Size(100, 35);
            this.字典选择Btn.Style = Sunny.UI.UIStyle.Custom;
            this.字典选择Btn.TabIndex = 0;
            this.字典选择Btn.Text = "字典选择";
            this.字典选择Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.字典选择Btn.Click += new System.EventHandler(this.字典选择Btn_Click);
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
            this.提取型正则Box.Location = new System.Drawing.Point(14, 88);
            this.提取型正则Box.Maximum = 2147483647D;
            this.提取型正则Box.MaxLength = 32767;
            this.提取型正则Box.Minimum = -2147483648D;
            this.提取型正则Box.MinimumSize = new System.Drawing.Size(0, 29);
            this.提取型正则Box.Multiline = false;
            this.提取型正则Box.Name = "提取型正则Box";
            this.提取型正则Box.ReadOnly = false;
            this.提取型正则Box.Size = new System.Drawing.Size(365, 29);
            this.提取型正则Box.TabIndex = 1;
            this.提取型正则Box.Type = Sunny.UI.UITextBox.UIEditType.String;
            this.提取型正则Box.Watermark = "";
            this.提取型正则Box.主控件Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.提取型正则Box.主控件Text = "(.+)=(.*)";
            this.提取型正则Box.是否允许为零 = true;
            this.提取型正则Box.是否判断条件 = false;
            this.提取型正则Box.是否序列化 = true;
            // 
            // uiSymbolLabel1
            // 
            this.uiSymbolLabel1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.uiSymbolLabel1.Location = new System.Drawing.Point(14, 38);
            this.uiSymbolLabel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolLabel1.Name = "uiSymbolLabel1";
            this.uiSymbolLabel1.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.uiSymbolLabel1.Size = new System.Drawing.Size(225, 25);
            this.uiSymbolLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolLabel1.Symbol = 61713;
            this.uiSymbolLabel1.SymbolSize = 12;
            this.uiSymbolLabel1.TabIndex = 9;
            this.uiSymbolLabel1.Text = "提取型正则需要有2个()用于提取";
            this.uiSymbolLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 替换列表Box
            // 
            this.替换列表Box.BackColor = System.Drawing.Color.Transparent;
            this.替换列表Box.CanEmpty = false;
            this.替换列表Box.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.替换列表Box.DecimalPlaces = 2;
            this.替换列表Box.LabelFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.替换列表Box.LabelText = "替换列表(a=b格式)";
            this.替换列表Box.Label方向 = 翻译姬.数据库控件Label方向.上;
            this.替换列表Box.Location = new System.Drawing.Point(14, 123);
            this.替换列表Box.Maximum = 2147483647D;
            this.替换列表Box.MaxLength = 32767;
            this.替换列表Box.Minimum = -2147483648D;
            this.替换列表Box.MinimumSize = new System.Drawing.Size(0, 29);
            this.替换列表Box.Multiline = true;
            this.替换列表Box.Name = "替换列表Box";
            this.替换列表Box.ReadOnly = false;
            this.替换列表Box.Size = new System.Drawing.Size(365, 321);
            this.替换列表Box.TabIndex = 10;
            this.替换列表Box.Type = Sunny.UI.UITextBox.UIEditType.String;
            this.替换列表Box.Watermark = "";
            this.替换列表Box.主控件Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.替换列表Box.主控件Text = "";
            this.替换列表Box.是否允许为零 = true;
            this.替换列表Box.是否判断条件 = false;
            this.替换列表Box.是否序列化 = false;
            // 
            // 确定Btn
            // 
            this.确定Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.确定Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.确定Btn.Location = new System.Drawing.Point(70, 460);
            this.确定Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.确定Btn.Name = "确定Btn";
            this.确定Btn.Radius = 15;
            this.确定Btn.Size = new System.Drawing.Size(100, 35);
            this.确定Btn.Style = Sunny.UI.UIStyle.Custom;
            this.确定Btn.TabIndex = 11;
            this.确定Btn.Text = "确定";
            this.确定Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.确定Btn.Click += new System.EventHandler(this.确定Btn_Click);
            // 
            // 返回Btn
            // 
            this.返回Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.返回Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.返回Btn.Location = new System.Drawing.Point(233, 460);
            this.返回Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.返回Btn.Name = "返回Btn";
            this.返回Btn.Radius = 15;
            this.返回Btn.Size = new System.Drawing.Size(100, 35);
            this.返回Btn.Style = Sunny.UI.UIStyle.Custom;
            this.返回Btn.TabIndex = 12;
            this.返回Btn.Text = "返回";
            this.返回Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.返回Btn.Click += new System.EventHandler(this.返回Btn_Click);
            // 
            // uiSymbolLabel2
            // 
            this.uiSymbolLabel2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.uiSymbolLabel2.Location = new System.Drawing.Point(14, 60);
            this.uiSymbolLabel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolLabel2.Name = "uiSymbolLabel2";
            this.uiSymbolLabel2.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.uiSymbolLabel2.Size = new System.Drawing.Size(225, 25);
            this.uiSymbolLabel2.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolLabel2.Symbol = 61713;
            this.uiSymbolLabel2.SymbolSize = 12;
            this.uiSymbolLabel2.TabIndex = 13;
            this.uiSymbolLabel2.Text = "若重复，则后来者优先";
            this.uiSymbolLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 替换列表数值更改
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(396, 510);
            this.ControlBox = false;
            this.Controls.Add(this.uiSymbolLabel2);
            this.Controls.Add(this.返回Btn);
            this.Controls.Add(this.确定Btn);
            this.Controls.Add(this.替换列表Box);
            this.Controls.Add(this.uiSymbolLabel1);
            this.Controls.Add(this.提取型正则Box);
            this.Controls.Add(this.字典选择Btn);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "替换列表数值更改";
            this.ShowInTaskbar = false;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "替换列表数值更改";
            this.Load += new System.EventHandler(this.替换列表数值更改_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIButton 字典选择Btn;
        private 组合控件TextBox 提取型正则Box;
        private Sunny.UI.UISymbolLabel uiSymbolLabel1;
        private 组合控件TextBox 替换列表Box;
        private Sunny.UI.UIButton 确定Btn;
        private Sunny.UI.UIButton 返回Btn;
        private Sunny.UI.UISymbolLabel uiSymbolLabel2;
    }
}