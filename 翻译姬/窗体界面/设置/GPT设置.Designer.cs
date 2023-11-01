namespace 翻译姬;

partial class GPT设置 {
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
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.请求等待延迟Box = new Sunny.UI.UITextBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.错行重试数Box = new Sunny.UI.UITextBox();
            this.uiLabel21 = new Sunny.UI.UILabel();
            this.单次机翻行Box = new Sunny.UI.UITextBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.上下文提示Switch = new 翻译姬.自定义Switch();
            this.uiLabel19 = new Sunny.UI.UILabel();
            this.使用多线程Switch = new 翻译姬.自定义Switch();
            this.使用模型Label = new Sunny.UI.UILabel();
            this.使用模型Box = new Sunny.UI.UITextBox();
            this.连接域名Label = new Sunny.UI.UILabel();
            this.连接域名Box = new Sunny.UI.UITextBox();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.uiLabel6 = new Sunny.UI.UILabel();
            this.语境Box = new Sunny.UI.UITextBox();
            this.预设原文Box = new Sunny.UI.UITextBox();
            this.uiLabel7 = new Sunny.UI.UILabel();
            this.预设返回Box = new Sunny.UI.UITextBox();
            this.uiLabel8 = new Sunny.UI.UILabel();
            this.uiLabel9 = new Sunny.UI.UILabel();
            this.发送预设Switch = new 翻译姬.自定义Switch();
            this.模型词表Btn = new Sunny.UI.UIButton();
            this.模型词表Box = new Sunny.UI.UITextBox();
            this.uiLabel10 = new Sunny.UI.UILabel();
            this.下载Btn = new Sunny.UI.UIButton();
            this.uiLabel11 = new Sunny.UI.UILabel();
            this.次数限制Box = new Sunny.UI.UITextBox();
            this.uiLabel12 = new Sunny.UI.UILabel();
            this.Token限制Box = new Sunny.UI.UITextBox();
            this.预设原文选择Box = new Sunny.UI.UIComboBox();
            this.预设返回选择Box = new Sunny.UI.UIComboBox();
            this.SuspendLayout();
            // 
            // uiLabel4
            // 
            this.uiLabel4.AutoSize = true;
            this.uiLabel4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel4.Location = new System.Drawing.Point(330, 85);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(132, 21);
            this.uiLabel4.TabIndex = 107;
            this.uiLabel4.Text = "请求等待延迟(秒)";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 请求等待延迟Box
            // 
            this.请求等待延迟Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.请求等待延迟Box.DoubleValue = 3D;
            this.请求等待延迟Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.请求等待延迟Box.IntValue = 3;
            this.请求等待延迟Box.Location = new System.Drawing.Point(510, 81);
            this.请求等待延迟Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.请求等待延迟Box.Maximum = 10D;
            this.请求等待延迟Box.Minimum = 1D;
            this.请求等待延迟Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.请求等待延迟Box.Name = "请求等待延迟Box";
            this.请求等待延迟Box.ShowText = false;
            this.请求等待延迟Box.Size = new System.Drawing.Size(104, 29);
            this.请求等待延迟Box.TabIndex = 106;
            this.请求等待延迟Box.Text = "3";
            this.请求等待延迟Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.请求等待延迟Box.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.请求等待延迟Box.Watermark = "";
            // 
            // uiLabel2
            // 
            this.uiLabel2.AutoSize = true;
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.Location = new System.Drawing.Point(627, 85);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(90, 21);
            this.uiLabel2.TabIndex = 105;
            this.uiLabel2.Text = "错行重试数";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 错行重试数Box
            // 
            this.错行重试数Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.错行重试数Box.DoubleValue = 1D;
            this.错行重试数Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.错行重试数Box.IntValue = 1;
            this.错行重试数Box.Location = new System.Drawing.Point(721, 81);
            this.错行重试数Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.错行重试数Box.Maximum = 10D;
            this.错行重试数Box.Minimum = 0D;
            this.错行重试数Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.错行重试数Box.Name = "错行重试数Box";
            this.错行重试数Box.ShowText = false;
            this.错行重试数Box.Size = new System.Drawing.Size(75, 29);
            this.错行重试数Box.TabIndex = 104;
            this.错行重试数Box.Text = "1";
            this.错行重试数Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.错行重试数Box.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.错行重试数Box.Watermark = "";
            // 
            // uiLabel21
            // 
            this.uiLabel21.AutoSize = true;
            this.uiLabel21.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel21.Location = new System.Drawing.Point(627, 49);
            this.uiLabel21.Name = "uiLabel21";
            this.uiLabel21.Size = new System.Drawing.Size(90, 21);
            this.uiLabel21.TabIndex = 103;
            this.uiLabel21.Text = "单次机翻行";
            this.uiLabel21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 单次机翻行Box
            // 
            this.单次机翻行Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.单次机翻行Box.DoubleValue = 8D;
            this.单次机翻行Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.单次机翻行Box.IntValue = 8;
            this.单次机翻行Box.Location = new System.Drawing.Point(721, 45);
            this.单次机翻行Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.单次机翻行Box.Maximum = 500D;
            this.单次机翻行Box.Minimum = 1D;
            this.单次机翻行Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.单次机翻行Box.Name = "单次机翻行Box";
            this.单次机翻行Box.ShowText = false;
            this.单次机翻行Box.Size = new System.Drawing.Size(75, 29);
            this.单次机翻行Box.TabIndex = 102;
            this.单次机翻行Box.Text = "8";
            this.单次机翻行Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.单次机翻行Box.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.单次机翻行Box.Watermark = "";
            // 
            // uiLabel1
            // 
            this.uiLabel1.AutoSize = true;
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(330, 121);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(90, 21);
            this.uiLabel1.TabIndex = 100;
            this.uiLabel1.Text = "上下文提示";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 上下文提示Switch
            // 
            this.上下文提示Switch.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.上下文提示Switch.Location = new System.Drawing.Point(428, 117);
            this.上下文提示Switch.MinimumSize = new System.Drawing.Size(1, 1);
            this.上下文提示Switch.Name = "上下文提示Switch";
            this.上下文提示Switch.Size = new System.Drawing.Size(75, 29);
            this.上下文提示Switch.TabIndex = 101;
            this.上下文提示Switch.Text = "自定义Switch1";
            this.上下文提示Switch.ActiveChanged += new System.EventHandler(this.上下文提示Switch_ActiveChanged);
            // 
            // uiLabel19
            // 
            this.uiLabel19.AutoSize = true;
            this.uiLabel19.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel19.Location = new System.Drawing.Point(29, 121);
            this.uiLabel19.Name = "uiLabel19";
            this.uiLabel19.Size = new System.Drawing.Size(90, 21);
            this.uiLabel19.TabIndex = 98;
            this.uiLabel19.Text = "使用多线程";
            this.uiLabel19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 使用多线程Switch
            // 
            this.使用多线程Switch.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.使用多线程Switch.Location = new System.Drawing.Point(143, 117);
            this.使用多线程Switch.MinimumSize = new System.Drawing.Size(1, 1);
            this.使用多线程Switch.Name = "使用多线程Switch";
            this.使用多线程Switch.Size = new System.Drawing.Size(75, 29);
            this.使用多线程Switch.TabIndex = 99;
            this.使用多线程Switch.Text = "自定义Switch1";
            this.使用多线程Switch.ActiveChanged += new System.EventHandler(this.使用多线程Switch_ActiveChanged);
            // 
            // 使用模型Label
            // 
            this.使用模型Label.AutoSize = true;
            this.使用模型Label.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.使用模型Label.Location = new System.Drawing.Point(330, 49);
            this.使用模型Label.Name = "使用模型Label";
            this.使用模型Label.Size = new System.Drawing.Size(74, 21);
            this.使用模型Label.TabIndex = 95;
            this.使用模型Label.Text = "使用模型";
            this.使用模型Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 使用模型Box
            // 
            this.使用模型Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.使用模型Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.使用模型Box.Location = new System.Drawing.Point(428, 45);
            this.使用模型Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.使用模型Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.使用模型Box.Name = "使用模型Box";
            this.使用模型Box.ShowText = false;
            this.使用模型Box.Size = new System.Drawing.Size(186, 29);
            this.使用模型Box.TabIndex = 94;
            this.使用模型Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.使用模型Box.Watermark = "例如：gpt-3.5-turbo";
            // 
            // 连接域名Label
            // 
            this.连接域名Label.AutoSize = true;
            this.连接域名Label.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.连接域名Label.Location = new System.Drawing.Point(29, 13);
            this.连接域名Label.Name = "连接域名Label";
            this.连接域名Label.Size = new System.Drawing.Size(74, 21);
            this.连接域名Label.TabIndex = 93;
            this.连接域名Label.Text = "连接域名";
            this.连接域名Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 连接域名Box
            // 
            this.连接域名Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.连接域名Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.连接域名Box.Location = new System.Drawing.Point(225, 9);
            this.连接域名Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.连接域名Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.连接域名Box.Name = "连接域名Box";
            this.连接域名Box.ShowText = false;
            this.连接域名Box.Size = new System.Drawing.Size(212, 29);
            this.连接域名Box.TabIndex = 92;
            this.连接域名Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.连接域名Box.Watermark = "例如：api.openai.com";
            // 
            // uiLabel3
            // 
            this.uiLabel3.AutoSize = true;
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel3.Location = new System.Drawing.Point(152, 13);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(67, 21);
            this.uiLabel3.TabIndex = 108;
            this.uiLabel3.Text = "https://";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel5
            // 
            this.uiLabel5.AutoSize = true;
            this.uiLabel5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel5.Location = new System.Drawing.Point(440, 13);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(174, 21);
            this.uiLabel5.TabIndex = 109;
            this.uiLabel5.Text = "/v1/chat/completions";
            this.uiLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel6
            // 
            this.uiLabel6.AutoSize = true;
            this.uiLabel6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel6.Location = new System.Drawing.Point(29, 224);
            this.uiLabel6.Name = "uiLabel6";
            this.uiLabel6.Size = new System.Drawing.Size(141, 21);
            this.uiLabel6.TabIndex = 110;
            this.uiLabel6.Text = "语境设置(prompt)";
            this.uiLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 语境Box
            // 
            this.语境Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.语境Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.语境Box.Location = new System.Drawing.Point(33, 250);
            this.语境Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.语境Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.语境Box.Multiline = true;
            this.语境Box.Name = "语境Box";
            this.语境Box.ShowText = false;
            this.语境Box.Size = new System.Drawing.Size(927, 87);
            this.语境Box.TabIndex = 111;
            this.语境Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.语境Box.Watermark = "";
            // 
            // 预设原文Box
            // 
            this.预设原文Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.预设原文Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.预设原文Box.Location = new System.Drawing.Point(33, 386);
            this.预设原文Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.预设原文Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.预设原文Box.Multiline = true;
            this.预设原文Box.Name = "预设原文Box";
            this.预设原文Box.ShowText = false;
            this.预设原文Box.Size = new System.Drawing.Size(444, 218);
            this.预设原文Box.TabIndex = 113;
            this.预设原文Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.预设原文Box.Watermark = "";
            // 
            // uiLabel7
            // 
            this.uiLabel7.AutoSize = true;
            this.uiLabel7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel7.Location = new System.Drawing.Point(29, 353);
            this.uiLabel7.Name = "uiLabel7";
            this.uiLabel7.Size = new System.Drawing.Size(107, 21);
            this.uiLabel7.TabIndex = 112;
            this.uiLabel7.Text = "预设原文Json";
            this.uiLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 预设返回Box
            // 
            this.预设返回Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.预设返回Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.预设返回Box.Location = new System.Drawing.Point(516, 386);
            this.预设返回Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.预设返回Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.预设返回Box.Multiline = true;
            this.预设返回Box.Name = "预设返回Box";
            this.预设返回Box.ShowText = false;
            this.预设返回Box.Size = new System.Drawing.Size(444, 218);
            this.预设返回Box.TabIndex = 115;
            this.预设返回Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.预设返回Box.Watermark = "";
            // 
            // uiLabel8
            // 
            this.uiLabel8.AutoSize = true;
            this.uiLabel8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel8.Location = new System.Drawing.Point(512, 353);
            this.uiLabel8.Name = "uiLabel8";
            this.uiLabel8.Size = new System.Drawing.Size(107, 21);
            this.uiLabel8.TabIndex = 114;
            this.uiLabel8.Text = "预设返回Json";
            this.uiLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel9
            // 
            this.uiLabel9.AutoSize = true;
            this.uiLabel9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel9.Location = new System.Drawing.Point(557, 121);
            this.uiLabel9.Name = "uiLabel9";
            this.uiLabel9.Size = new System.Drawing.Size(74, 21);
            this.uiLabel9.TabIndex = 116;
            this.uiLabel9.Text = "发送预设";
            this.uiLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 发送预设Switch
            // 
            this.发送预设Switch.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.发送预设Switch.Location = new System.Drawing.Point(642, 117);
            this.发送预设Switch.MinimumSize = new System.Drawing.Size(1, 1);
            this.发送预设Switch.Name = "发送预设Switch";
            this.发送预设Switch.Size = new System.Drawing.Size(75, 29);
            this.发送预设Switch.TabIndex = 117;
            this.发送预设Switch.Text = "自定义Switch1";
            this.发送预设Switch.ActiveChanged += new System.EventHandler(this.发送预设Switch_ActiveChanged);
            // 
            // 模型词表Btn
            // 
            this.模型词表Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.模型词表Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.模型词表Btn.Location = new System.Drawing.Point(33, 155);
            this.模型词表Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.模型词表Btn.Name = "模型词表Btn";
            this.模型词表Btn.Radius = 15;
            this.模型词表Btn.Size = new System.Drawing.Size(77, 29);
            this.模型词表Btn.TabIndex = 119;
            this.模型词表Btn.Text = "模型词表";
            this.模型词表Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.模型词表Btn.Click += new System.EventHandler(this.模型词表Btn_Click);
            // 
            // 模型词表Box
            // 
            this.模型词表Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.模型词表Box.Enabled = false;
            this.模型词表Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.模型词表Box.Location = new System.Drawing.Point(33, 192);
            this.模型词表Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.模型词表Box.MaxLength = 10000;
            this.模型词表Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.模型词表Box.Name = "模型词表Box";
            this.模型词表Box.ReadOnly = true;
            this.模型词表Box.ShowText = false;
            this.模型词表Box.Size = new System.Drawing.Size(927, 29);
            this.模型词表Box.TabIndex = 118;
            this.模型词表Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.模型词表Box.Watermark = "";
            // 
            // uiLabel10
            // 
            this.uiLabel10.AutoSize = true;
            this.uiLabel10.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel10.Location = new System.Drawing.Point(310, 159);
            this.uiLabel10.Name = "uiLabel10";
            this.uiLabel10.Size = new System.Drawing.Size(161, 21);
            this.uiLabel10.TabIndex = 120;
            this.uiLabel10.Text = "tip:用于预估Token数";
            this.uiLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 下载Btn
            // 
            this.下载Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.下载Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.下载Btn.Location = new System.Drawing.Point(124, 155);
            this.下载Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.下载Btn.Name = "下载Btn";
            this.下载Btn.Radius = 15;
            this.下载Btn.Size = new System.Drawing.Size(151, 29);
            this.下载Btn.TabIndex = 121;
            this.下载Btn.Text = "cl100k_base下载";
            this.下载Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.下载Btn.Click += new System.EventHandler(this.下载Btn_Click);
            // 
            // uiLabel11
            // 
            this.uiLabel11.AutoSize = true;
            this.uiLabel11.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel11.Location = new System.Drawing.Point(29, 49);
            this.uiLabel11.Name = "uiLabel11";
            this.uiLabel11.Size = new System.Drawing.Size(139, 21);
            this.uiLabel11.TabIndex = 123;
            this.uiLabel11.Text = "调用限制(次/分钟)";
            this.uiLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 次数限制Box
            // 
            this.次数限制Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.次数限制Box.DoubleValue = 3D;
            this.次数限制Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.次数限制Box.IntValue = 3;
            this.次数限制Box.Location = new System.Drawing.Point(225, 45);
            this.次数限制Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.次数限制Box.Maximum = 5000D;
            this.次数限制Box.Minimum = 3D;
            this.次数限制Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.次数限制Box.Name = "次数限制Box";
            this.次数限制Box.ShowText = false;
            this.次数限制Box.Size = new System.Drawing.Size(75, 29);
            this.次数限制Box.TabIndex = 122;
            this.次数限制Box.Text = "3";
            this.次数限制Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.次数限制Box.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.次数限制Box.Watermark = "";
            // 
            // uiLabel12
            // 
            this.uiLabel12.AutoSize = true;
            this.uiLabel12.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel12.Location = new System.Drawing.Point(29, 85);
            this.uiLabel12.Name = "uiLabel12";
            this.uiLabel12.Size = new System.Drawing.Size(170, 21);
            this.uiLabel12.TabIndex = 125;
            this.uiLabel12.Text = "调用限制(Token/分钟)";
            this.uiLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Token限制Box
            // 
            this.Token限制Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Token限制Box.DoubleValue = 150000D;
            this.Token限制Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Token限制Box.IntValue = 150000;
            this.Token限制Box.Location = new System.Drawing.Point(225, 81);
            this.Token限制Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Token限制Box.Maximum = 1000000D;
            this.Token限制Box.Minimum = 10000D;
            this.Token限制Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.Token限制Box.Name = "Token限制Box";
            this.Token限制Box.ShowText = false;
            this.Token限制Box.Size = new System.Drawing.Size(75, 29);
            this.Token限制Box.TabIndex = 124;
            this.Token限制Box.Text = "150000";
            this.Token限制Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.Token限制Box.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.Token限制Box.Watermark = "";
            // 
            // 预设原文选择Box
            // 
            this.预设原文选择Box.DataSource = null;
            this.预设原文选择Box.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.预设原文选择Box.FillColor = System.Drawing.Color.White;
            this.预设原文选择Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.预设原文选择Box.Location = new System.Drawing.Point(143, 349);
            this.预设原文选择Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.预设原文选择Box.MinimumSize = new System.Drawing.Size(63, 0);
            this.预设原文选择Box.Name = "预设原文选择Box";
            this.预设原文选择Box.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.预设原文选择Box.Size = new System.Drawing.Size(150, 29);
            this.预设原文选择Box.TabIndex = 126;
            this.预设原文选择Box.Text = "uiComboBox1";
            this.预设原文选择Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.预设原文选择Box.Watermark = "";
            this.预设原文选择Box.TextChanged += new System.EventHandler(this.预设选择下拉框_TextChanged);
            // 
            // 预设返回选择Box
            // 
            this.预设返回选择Box.DataSource = null;
            this.预设返回选择Box.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.预设返回选择Box.FillColor = System.Drawing.Color.White;
            this.预设返回选择Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.预设返回选择Box.Location = new System.Drawing.Point(629, 349);
            this.预设返回选择Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.预设返回选择Box.MinimumSize = new System.Drawing.Size(63, 0);
            this.预设返回选择Box.Name = "预设返回选择Box";
            this.预设返回选择Box.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.预设返回选择Box.Size = new System.Drawing.Size(150, 29);
            this.预设返回选择Box.TabIndex = 127;
            this.预设返回选择Box.Text = "uiComboBox1";
            this.预设返回选择Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.预设返回选择Box.Watermark = "";
            this.预设返回选择Box.TextChanged += new System.EventHandler(this.预设选择下拉框_TextChanged);
            // 
            // GPT设置
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(987, 618);
            this.Controls.Add(this.预设返回选择Box);
            this.Controls.Add(this.预设原文选择Box);
            this.Controls.Add(this.uiLabel12);
            this.Controls.Add(this.Token限制Box);
            this.Controls.Add(this.uiLabel11);
            this.Controls.Add(this.次数限制Box);
            this.Controls.Add(this.下载Btn);
            this.Controls.Add(this.uiLabel10);
            this.Controls.Add(this.模型词表Btn);
            this.Controls.Add(this.模型词表Box);
            this.Controls.Add(this.uiLabel9);
            this.Controls.Add(this.发送预设Switch);
            this.Controls.Add(this.预设返回Box);
            this.Controls.Add(this.uiLabel8);
            this.Controls.Add(this.预设原文Box);
            this.Controls.Add(this.uiLabel7);
            this.Controls.Add(this.语境Box);
            this.Controls.Add(this.uiLabel6);
            this.Controls.Add(this.uiLabel5);
            this.Controls.Add(this.uiLabel3);
            this.Controls.Add(this.uiLabel4);
            this.Controls.Add(this.请求等待延迟Box);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.错行重试数Box);
            this.Controls.Add(this.uiLabel21);
            this.Controls.Add(this.单次机翻行Box);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.上下文提示Switch);
            this.Controls.Add(this.uiLabel19);
            this.Controls.Add(this.使用多线程Switch);
            this.Controls.Add(this.使用模型Label);
            this.Controls.Add(this.使用模型Box);
            this.Controls.Add(this.连接域名Label);
            this.Controls.Add(this.连接域名Box);
            this.Name = "GPT设置";
            this.Text = "GPT设置";
            this.Page被选中 += new 翻译姬.自定义Page.自定义Page被选中(this.GPT设置_Page被选中);
            this.Load += new System.EventHandler(this.GPT设置_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Sunny.UI.UILabel uiLabel4;
    private Sunny.UI.UITextBox 请求等待延迟Box;
    private Sunny.UI.UILabel uiLabel2;
    private Sunny.UI.UITextBox 错行重试数Box;
    private Sunny.UI.UILabel uiLabel21;
    private Sunny.UI.UITextBox 单次机翻行Box;
    private Sunny.UI.UILabel uiLabel1;
    private 自定义Switch 上下文提示Switch;
    private Sunny.UI.UILabel uiLabel19;
    private 自定义Switch 使用多线程Switch;
    private Sunny.UI.UILabel 使用模型Label;
    private Sunny.UI.UITextBox 使用模型Box;
    private Sunny.UI.UILabel 连接域名Label;
    private Sunny.UI.UITextBox 连接域名Box;
    private Sunny.UI.UILabel uiLabel3;
    private Sunny.UI.UILabel uiLabel5;
    private Sunny.UI.UILabel uiLabel6;
    private Sunny.UI.UITextBox 语境Box;
    private Sunny.UI.UITextBox 预设原文Box;
    private Sunny.UI.UILabel uiLabel7;
    private Sunny.UI.UITextBox 预设返回Box;
    private Sunny.UI.UILabel uiLabel8;
    private Sunny.UI.UILabel uiLabel9;
    private 自定义Switch 发送预设Switch;
    private Sunny.UI.UIButton 模型词表Btn;
    private Sunny.UI.UITextBox 模型词表Box;
    private Sunny.UI.UILabel uiLabel10;
    private Sunny.UI.UIButton 下载Btn;
    private Sunny.UI.UILabel uiLabel11;
    private Sunny.UI.UITextBox 次数限制Box;
    private Sunny.UI.UILabel uiLabel12;
    private Sunny.UI.UITextBox Token限制Box;
    private Sunny.UI.UIComboBox 预设原文选择Box;
    private Sunny.UI.UIComboBox 预设返回选择Box;
}