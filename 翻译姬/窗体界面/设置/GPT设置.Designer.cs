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
            this.components = new System.ComponentModel.Container();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.请求等待延迟Box = new Sunny.UI.UITextBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.错行重试数Box = new Sunny.UI.UITextBox();
            this.uiLabel21 = new Sunny.UI.UILabel();
            this.单次机翻行Box = new Sunny.UI.UITextBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.上下文提示Switch = new 翻译姬.自定义Switch();
            this.使用模型Label = new Sunny.UI.UILabel();
            this.使用模型Box = new Sunny.UI.UITextBox();
            this.连接域名Label = new Sunny.UI.UILabel();
            this.连接域名Box = new Sunny.UI.UITextBox();
            this.uiLabel6 = new Sunny.UI.UILabel();
            this.语境Box = new Sunny.UI.UITextBox();
            this.uiLabel11 = new Sunny.UI.UILabel();
            this.次数限制Box = new Sunny.UI.UITextBox();
            this.uiLabel12 = new Sunny.UI.UILabel();
            this.Token限制Box = new Sunny.UI.UITextBox();
            this.uiLabel13 = new Sunny.UI.UILabel();
            this.异常重试上限Box = new Sunny.UI.UITextBox();
            this.uiLabel15 = new Sunny.UI.UILabel();
            this.上下文深度Box = new Sunny.UI.UITextBox();
            this.uiLabel14 = new Sunny.UI.UILabel();
            this.连续对话合并Switch = new 翻译姬.自定义Switch();
            this.uiLabel16 = new Sunny.UI.UILabel();
            this.合并分隔符Box = new Sunny.UI.UITextBox();
            this.uiLabel7 = new Sunny.UI.UILabel();
            this.漏翻重试次数Box = new Sunny.UI.UITextBox();
            this.uiLabel8 = new Sunny.UI.UILabel();
            this.漏翻检测Switch = new 翻译姬.自定义Switch();
            this.uiLabel9 = new Sunny.UI.UILabel();
            this.润色语境Box = new Sunny.UI.UITextBox();
            this.uiLabel17 = new Sunny.UI.UILabel();
            this.翻后润色Switch = new 翻译姬.自定义Switch();
            this.词汇表设置Btn = new Sunny.UI.UIButton();
            this.uiLabel18 = new Sunny.UI.UILabel();
            this.错误跳过Switch = new 翻译姬.自定义Switch();
            this.Http设置Btn = new Sunny.UI.UIButton();
            this.uiLine1 = new Sunny.UI.UILine();
            this.uiLine2 = new Sunny.UI.UILine();
            this.uiLine3 = new Sunny.UI.UILine();
            this.uiLine4 = new Sunny.UI.UILine();
            this.uiLine5 = new Sunny.UI.UILine();
            this.连接路由Box = new Sunny.UI.UITextBox();
            this.修改Btn = new Sunny.UI.UIButton();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.简易模式Switch = new 翻译姬.自定义Switch();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.相邻对话合并Switch = new 翻译姬.自定义Switch();
            this.uiLabel10 = new Sunny.UI.UILabel();
            this.输出人名优先词汇表Switch = new 翻译姬.自定义Switch();
            this.uiLabel19 = new Sunny.UI.UILabel();
            this.Sakura机翻Switch = new 翻译姬.自定义Switch();
            this.Sakura和简易模式按钮判定Timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // uiLabel4
            // 
            this.uiLabel4.AutoSize = true;
            this.uiLabel4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel4.Location = new System.Drawing.Point(29, 560);
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
            this.请求等待延迟Box.Location = new System.Drawing.Point(225, 556);
            this.请求等待延迟Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.请求等待延迟Box.Maximum = 360D;
            this.请求等待延迟Box.Minimum = 1D;
            this.请求等待延迟Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.请求等待延迟Box.Name = "请求等待延迟Box";
            this.请求等待延迟Box.ShowText = false;
            this.请求等待延迟Box.Size = new System.Drawing.Size(75, 29);
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
            this.uiLabel2.Location = new System.Drawing.Point(29, 488);
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
            this.错行重试数Box.Location = new System.Drawing.Point(225, 484);
            this.错行重试数Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.错行重试数Box.Maximum = 100D;
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
            this.uiLabel21.Location = new System.Drawing.Point(29, 524);
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
            this.单次机翻行Box.Location = new System.Drawing.Point(225, 520);
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
            this.uiLabel1.Location = new System.Drawing.Point(29, 136);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(90, 21);
            this.uiLabel1.TabIndex = 100;
            this.uiLabel1.Text = "上下文提示";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 上下文提示Switch
            // 
            this.上下文提示Switch.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.上下文提示Switch.Location = new System.Drawing.Point(225, 132);
            this.上下文提示Switch.MinimumSize = new System.Drawing.Size(1, 1);
            this.上下文提示Switch.Name = "上下文提示Switch";
            this.上下文提示Switch.Size = new System.Drawing.Size(75, 29);
            this.上下文提示Switch.TabIndex = 101;
            this.上下文提示Switch.Text = "自定义Switch1";
            // 
            // 使用模型Label
            // 
            this.使用模型Label.AutoSize = true;
            this.使用模型Label.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.使用模型Label.Location = new System.Drawing.Point(693, 13);
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
            this.使用模型Box.Location = new System.Drawing.Point(777, 9);
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
            this.连接域名Box.Watermark = "例：api.openai.com";
            // 
            // uiLabel6
            // 
            this.uiLabel6.AutoSize = true;
            this.uiLabel6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel6.Location = new System.Drawing.Point(326, 102);
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
            this.语境Box.Location = new System.Drawing.Point(326, 137);
            this.语境Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.语境Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.语境Box.Multiline = true;
            this.语境Box.Name = "语境Box";
            this.语境Box.ShowText = false;
            this.语境Box.Size = new System.Drawing.Size(637, 200);
            this.语境Box.TabIndex = 111;
            this.语境Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.语境Box.Watermark = "";
            // 
            // uiLabel11
            // 
            this.uiLabel11.AutoSize = true;
            this.uiLabel11.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel11.Location = new System.Drawing.Point(29, 52);
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
            this.次数限制Box.Location = new System.Drawing.Point(225, 48);
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
            this.uiLabel12.Location = new System.Drawing.Point(29, 88);
            this.uiLabel12.Name = "uiLabel12";
            this.uiLabel12.Size = new System.Drawing.Size(170, 21);
            this.uiLabel12.TabIndex = 125;
            this.uiLabel12.Text = "调用限制(Token/分钟)";
            this.uiLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Token限制Box
            // 
            this.Token限制Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Token限制Box.DoubleValue = 40000D;
            this.Token限制Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Token限制Box.IntValue = 40000;
            this.Token限制Box.Location = new System.Drawing.Point(225, 84);
            this.Token限制Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Token限制Box.Maximum = 100000000D;
            this.Token限制Box.Minimum = 10000D;
            this.Token限制Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.Token限制Box.Name = "Token限制Box";
            this.Token限制Box.ShowText = false;
            this.Token限制Box.Size = new System.Drawing.Size(75, 29);
            this.Token限制Box.TabIndex = 124;
            this.Token限制Box.Text = "40000";
            this.Token限制Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.Token限制Box.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.Token限制Box.Watermark = "";
            // 
            // uiLabel13
            // 
            this.uiLabel13.AutoSize = true;
            this.uiLabel13.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel13.Location = new System.Drawing.Point(29, 450);
            this.uiLabel13.Name = "uiLabel13";
            this.uiLabel13.Size = new System.Drawing.Size(106, 21);
            this.uiLabel13.TabIndex = 131;
            this.uiLabel13.Text = "异常重试上限";
            this.uiLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 异常重试上限Box
            // 
            this.异常重试上限Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.异常重试上限Box.DoubleValue = 10D;
            this.异常重试上限Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.异常重试上限Box.IntValue = 10;
            this.异常重试上限Box.Location = new System.Drawing.Point(225, 446);
            this.异常重试上限Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.异常重试上限Box.Maximum = 100D;
            this.异常重试上限Box.Minimum = 3D;
            this.异常重试上限Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.异常重试上限Box.Name = "异常重试上限Box";
            this.异常重试上限Box.ShowText = false;
            this.异常重试上限Box.Size = new System.Drawing.Size(75, 29);
            this.异常重试上限Box.TabIndex = 130;
            this.异常重试上限Box.Text = "10";
            this.异常重试上限Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.异常重试上限Box.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.异常重试上限Box.Watermark = "";
            // 
            // uiLabel15
            // 
            this.uiLabel15.AutoSize = true;
            this.uiLabel15.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel15.Location = new System.Drawing.Point(29, 172);
            this.uiLabel15.Name = "uiLabel15";
            this.uiLabel15.Size = new System.Drawing.Size(90, 21);
            this.uiLabel15.TabIndex = 136;
            this.uiLabel15.Text = "上下文深度";
            this.uiLabel15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 上下文深度Box
            // 
            this.上下文深度Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.上下文深度Box.DoubleValue = 1D;
            this.上下文深度Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.上下文深度Box.IntValue = 1;
            this.上下文深度Box.Location = new System.Drawing.Point(225, 168);
            this.上下文深度Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.上下文深度Box.Maximum = 10D;
            this.上下文深度Box.Minimum = 1D;
            this.上下文深度Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.上下文深度Box.Name = "上下文深度Box";
            this.上下文深度Box.ShowText = false;
            this.上下文深度Box.Size = new System.Drawing.Size(75, 29);
            this.上下文深度Box.TabIndex = 135;
            this.上下文深度Box.Text = "1";
            this.上下文深度Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.上下文深度Box.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.上下文深度Box.Watermark = "";
            // 
            // uiLabel14
            // 
            this.uiLabel14.AutoSize = true;
            this.uiLabel14.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel14.Location = new System.Drawing.Point(29, 213);
            this.uiLabel14.Name = "uiLabel14";
            this.uiLabel14.Size = new System.Drawing.Size(106, 21);
            this.uiLabel14.TabIndex = 138;
            this.uiLabel14.Text = "连续对话合并";
            this.uiLabel14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 连续对话合并Switch
            // 
            this.连续对话合并Switch.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.连续对话合并Switch.Location = new System.Drawing.Point(225, 209);
            this.连续对话合并Switch.MinimumSize = new System.Drawing.Size(1, 1);
            this.连续对话合并Switch.Name = "连续对话合并Switch";
            this.连续对话合并Switch.Size = new System.Drawing.Size(75, 29);
            this.连续对话合并Switch.TabIndex = 139;
            this.连续对话合并Switch.Text = "自定义Switch1";
            this.连续对话合并Switch.ActiveChanged += new System.EventHandler(this.连续对话合并Switch_ActiveChanged);
            // 
            // uiLabel16
            // 
            this.uiLabel16.AutoSize = true;
            this.uiLabel16.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel16.Location = new System.Drawing.Point(29, 282);
            this.uiLabel16.Name = "uiLabel16";
            this.uiLabel16.Size = new System.Drawing.Size(90, 21);
            this.uiLabel16.TabIndex = 141;
            this.uiLabel16.Text = "合并分隔符";
            this.uiLabel16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 合并分隔符Box
            // 
            this.合并分隔符Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.合并分隔符Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.合并分隔符Box.Location = new System.Drawing.Point(225, 278);
            this.合并分隔符Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.合并分隔符Box.Maximum = 10D;
            this.合并分隔符Box.Minimum = 0D;
            this.合并分隔符Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.合并分隔符Box.Name = "合并分隔符Box";
            this.合并分隔符Box.ShowText = false;
            this.合并分隔符Box.Size = new System.Drawing.Size(75, 29);
            this.合并分隔符Box.TabIndex = 140;
            this.合并分隔符Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.合并分隔符Box.Watermark = "";
            // 
            // uiLabel7
            // 
            this.uiLabel7.AutoSize = true;
            this.uiLabel7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel7.Location = new System.Drawing.Point(29, 365);
            this.uiLabel7.Name = "uiLabel7";
            this.uiLabel7.Size = new System.Drawing.Size(106, 21);
            this.uiLabel7.TabIndex = 145;
            this.uiLabel7.Text = "漏翻重试次数";
            this.uiLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 漏翻重试次数Box
            // 
            this.漏翻重试次数Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.漏翻重试次数Box.DoubleValue = 1D;
            this.漏翻重试次数Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.漏翻重试次数Box.IntValue = 1;
            this.漏翻重试次数Box.Location = new System.Drawing.Point(225, 361);
            this.漏翻重试次数Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.漏翻重试次数Box.Maximum = 10D;
            this.漏翻重试次数Box.Minimum = 1D;
            this.漏翻重试次数Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.漏翻重试次数Box.Name = "漏翻重试次数Box";
            this.漏翻重试次数Box.ShowText = false;
            this.漏翻重试次数Box.Size = new System.Drawing.Size(75, 29);
            this.漏翻重试次数Box.TabIndex = 144;
            this.漏翻重试次数Box.Text = "1";
            this.漏翻重试次数Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.漏翻重试次数Box.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.漏翻重试次数Box.Watermark = "";
            // 
            // uiLabel8
            // 
            this.uiLabel8.AutoSize = true;
            this.uiLabel8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel8.Location = new System.Drawing.Point(29, 328);
            this.uiLabel8.Name = "uiLabel8";
            this.uiLabel8.Size = new System.Drawing.Size(74, 21);
            this.uiLabel8.TabIndex = 142;
            this.uiLabel8.Text = "漏翻检测";
            this.uiLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 漏翻检测Switch
            // 
            this.漏翻检测Switch.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.漏翻检测Switch.Location = new System.Drawing.Point(225, 325);
            this.漏翻检测Switch.MinimumSize = new System.Drawing.Size(1, 1);
            this.漏翻检测Switch.Name = "漏翻检测Switch";
            this.漏翻检测Switch.Size = new System.Drawing.Size(75, 29);
            this.漏翻检测Switch.TabIndex = 143;
            this.漏翻检测Switch.Text = "自定义Switch1";
            this.漏翻检测Switch.ActiveChanged += new System.EventHandler(this.漏翻检测Switch_ActiveChanged);
            // 
            // uiLabel9
            // 
            this.uiLabel9.AutoSize = true;
            this.uiLabel9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel9.Location = new System.Drawing.Point(326, 351);
            this.uiLabel9.Name = "uiLabel9";
            this.uiLabel9.Size = new System.Drawing.Size(74, 21);
            this.uiLabel9.TabIndex = 146;
            this.uiLabel9.Text = "润色语境";
            this.uiLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 润色语境Box
            // 
            this.润色语境Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.润色语境Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.润色语境Box.Location = new System.Drawing.Point(326, 385);
            this.润色语境Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.润色语境Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.润色语境Box.Multiline = true;
            this.润色语境Box.Name = "润色语境Box";
            this.润色语境Box.ShowText = false;
            this.润色语境Box.Size = new System.Drawing.Size(637, 200);
            this.润色语境Box.TabIndex = 112;
            this.润色语境Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.润色语境Box.Watermark = "";
            // 
            // uiLabel17
            // 
            this.uiLabel17.AutoSize = true;
            this.uiLabel17.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel17.Location = new System.Drawing.Point(806, 351);
            this.uiLabel17.Name = "uiLabel17";
            this.uiLabel17.Size = new System.Drawing.Size(74, 21);
            this.uiLabel17.TabIndex = 147;
            this.uiLabel17.Text = "翻后润色";
            this.uiLabel17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 翻后润色Switch
            // 
            this.翻后润色Switch.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.翻后润色Switch.Location = new System.Drawing.Point(888, 347);
            this.翻后润色Switch.MinimumSize = new System.Drawing.Size(1, 1);
            this.翻后润色Switch.Name = "翻后润色Switch";
            this.翻后润色Switch.Size = new System.Drawing.Size(75, 29);
            this.翻后润色Switch.TabIndex = 148;
            this.翻后润色Switch.Text = "自定义Switch1";
            // 
            // 词汇表设置Btn
            // 
            this.词汇表设置Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.词汇表设置Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.词汇表设置Btn.Location = new System.Drawing.Point(326, 52);
            this.词汇表设置Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.词汇表设置Btn.Name = "词汇表设置Btn";
            this.词汇表设置Btn.Size = new System.Drawing.Size(105, 35);
            this.词汇表设置Btn.TabIndex = 149;
            this.词汇表设置Btn.Text = "词汇表设置";
            this.词汇表设置Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.词汇表设置Btn.Click += new System.EventHandler(this.词汇表设置Btn_Click);
            // 
            // uiLabel18
            // 
            this.uiLabel18.AutoSize = true;
            this.uiLabel18.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel18.Location = new System.Drawing.Point(29, 414);
            this.uiLabel18.Name = "uiLabel18";
            this.uiLabel18.Size = new System.Drawing.Size(74, 21);
            this.uiLabel18.TabIndex = 150;
            this.uiLabel18.Text = "错误跳过";
            this.uiLabel18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 错误跳过Switch
            // 
            this.错误跳过Switch.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.错误跳过Switch.Location = new System.Drawing.Point(225, 410);
            this.错误跳过Switch.MinimumSize = new System.Drawing.Size(1, 1);
            this.错误跳过Switch.Name = "错误跳过Switch";
            this.错误跳过Switch.Size = new System.Drawing.Size(75, 29);
            this.错误跳过Switch.TabIndex = 151;
            this.错误跳过Switch.Text = "自定义Switch1";
            this.错误跳过Switch.ActiveChanged += new System.EventHandler(this.错误跳过Switch_ActiveChanged);
            // 
            // Http设置Btn
            // 
            this.Http设置Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Http设置Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Http设置Btn.Location = new System.Drawing.Point(140, 6);
            this.Http设置Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.Http设置Btn.Name = "Http设置Btn";
            this.Http设置Btn.Size = new System.Drawing.Size(78, 35);
            this.Http设置Btn.TabIndex = 152;
            this.Http设置Btn.Text = "https://";
            this.Http设置Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Http设置Btn.Click += new System.EventHandler(this.Http设置Btn_Click);
            // 
            // uiLine1
            // 
            this.uiLine1.BackColor = System.Drawing.Color.Transparent;
            this.uiLine1.FillColor = System.Drawing.Color.Transparent;
            this.uiLine1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLine1.ForeColor = System.Drawing.Color.Transparent;
            this.uiLine1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.uiLine1.LineColor2 = System.Drawing.Color.Transparent;
            this.uiLine1.Location = new System.Drawing.Point(33, 118);
            this.uiLine1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Size = new System.Drawing.Size(267, 10);
            this.uiLine1.Style = Sunny.UI.UIStyle.Custom;
            this.uiLine1.StyleCustomMode = true;
            this.uiLine1.TabIndex = 153;
            // 
            // uiLine2
            // 
            this.uiLine2.BackColor = System.Drawing.Color.Transparent;
            this.uiLine2.FillColor = System.Drawing.Color.Transparent;
            this.uiLine2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLine2.ForeColor = System.Drawing.Color.Transparent;
            this.uiLine2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.uiLine2.LineColor2 = System.Drawing.Color.Transparent;
            this.uiLine2.Location = new System.Drawing.Point(33, 200);
            this.uiLine2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLine2.Name = "uiLine2";
            this.uiLine2.Size = new System.Drawing.Size(267, 10);
            this.uiLine2.Style = Sunny.UI.UIStyle.Custom;
            this.uiLine2.StyleCustomMode = true;
            this.uiLine2.TabIndex = 154;
            // 
            // uiLine3
            // 
            this.uiLine3.BackColor = System.Drawing.Color.Transparent;
            this.uiLine3.FillColor = System.Drawing.Color.Transparent;
            this.uiLine3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLine3.ForeColor = System.Drawing.Color.Transparent;
            this.uiLine3.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.uiLine3.LineColor2 = System.Drawing.Color.Transparent;
            this.uiLine3.Location = new System.Drawing.Point(33, 312);
            this.uiLine3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLine3.Name = "uiLine3";
            this.uiLine3.Size = new System.Drawing.Size(267, 10);
            this.uiLine3.Style = Sunny.UI.UIStyle.Custom;
            this.uiLine3.StyleCustomMode = true;
            this.uiLine3.TabIndex = 155;
            // 
            // uiLine4
            // 
            this.uiLine4.BackColor = System.Drawing.Color.Transparent;
            this.uiLine4.FillColor = System.Drawing.Color.Transparent;
            this.uiLine4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLine4.ForeColor = System.Drawing.Color.Transparent;
            this.uiLine4.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.uiLine4.LineColor2 = System.Drawing.Color.Transparent;
            this.uiLine4.Location = new System.Drawing.Point(33, 397);
            this.uiLine4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLine4.Name = "uiLine4";
            this.uiLine4.Size = new System.Drawing.Size(267, 10);
            this.uiLine4.Style = Sunny.UI.UIStyle.Custom;
            this.uiLine4.StyleCustomMode = true;
            this.uiLine4.TabIndex = 156;
            // 
            // uiLine5
            // 
            this.uiLine5.BackColor = System.Drawing.Color.Transparent;
            this.uiLine5.Direction = Sunny.UI.UILine.LineDirection.Vertical;
            this.uiLine5.FillColor = System.Drawing.Color.Transparent;
            this.uiLine5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLine5.ForeColor = System.Drawing.Color.Transparent;
            this.uiLine5.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.uiLine5.LineColor2 = System.Drawing.Color.Transparent;
            this.uiLine5.Location = new System.Drawing.Point(307, 48);
            this.uiLine5.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLine5.Name = "uiLine5";
            this.uiLine5.Size = new System.Drawing.Size(12, 537);
            this.uiLine5.Style = Sunny.UI.UIStyle.Custom;
            this.uiLine5.StyleCustomMode = true;
            this.uiLine5.TabIndex = 157;
            // 
            // 连接路由Box
            // 
            this.连接路由Box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.连接路由Box.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.连接路由Box.Location = new System.Drawing.Point(445, 9);
            this.连接路由Box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.连接路由Box.MinimumSize = new System.Drawing.Size(1, 16);
            this.连接路由Box.Name = "连接路由Box";
            this.连接路由Box.ShowText = false;
            this.连接路由Box.Size = new System.Drawing.Size(183, 29);
            this.连接路由Box.TabIndex = 93;
            this.连接路由Box.Text = "/v1/chat/completions";
            this.连接路由Box.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.连接路由Box.Watermark = "例：/v1/chat/completions";
            // 
            // 修改Btn
            // 
            this.修改Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.修改Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.修改Btn.Location = new System.Drawing.Point(635, 9);
            this.修改Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.修改Btn.Name = "修改Btn";
            this.修改Btn.Size = new System.Drawing.Size(52, 29);
            this.修改Btn.TabIndex = 158;
            this.修改Btn.Text = "修改";
            this.修改Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.修改Btn.Click += new System.EventHandler(this.修改Btn_Click);
            // 
            // uiLabel3
            // 
            this.uiLabel3.AutoSize = true;
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel3.Location = new System.Drawing.Point(806, 98);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(74, 21);
            this.uiLabel3.TabIndex = 159;
            this.uiLabel3.Text = "简易模式";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 简易模式Switch
            // 
            this.简易模式Switch.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.简易模式Switch.Location = new System.Drawing.Point(888, 94);
            this.简易模式Switch.MinimumSize = new System.Drawing.Size(1, 1);
            this.简易模式Switch.Name = "简易模式Switch";
            this.简易模式Switch.Size = new System.Drawing.Size(75, 29);
            this.简易模式Switch.TabIndex = 160;
            this.简易模式Switch.Text = "自定义Switch1";
            this.简易模式Switch.ActiveChanged += new System.EventHandler(this.简易模式Switch_ActiveChanged);
            // 
            // uiLabel5
            // 
            this.uiLabel5.AutoSize = true;
            this.uiLabel5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel5.Location = new System.Drawing.Point(29, 248);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(106, 21);
            this.uiLabel5.TabIndex = 161;
            this.uiLabel5.Text = "相邻对话合并";
            this.uiLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 相邻对话合并Switch
            // 
            this.相邻对话合并Switch.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.相邻对话合并Switch.Location = new System.Drawing.Point(225, 244);
            this.相邻对话合并Switch.MinimumSize = new System.Drawing.Size(1, 1);
            this.相邻对话合并Switch.Name = "相邻对话合并Switch";
            this.相邻对话合并Switch.Size = new System.Drawing.Size(75, 29);
            this.相邻对话合并Switch.TabIndex = 162;
            this.相邻对话合并Switch.Text = "自定义Switch1";
            this.相邻对话合并Switch.ActiveChanged += new System.EventHandler(this.相邻对话合并Switch_ActiveChanged);
            // 
            // uiLabel10
            // 
            this.uiLabel10.AutoSize = true;
            this.uiLabel10.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel10.Location = new System.Drawing.Point(437, 59);
            this.uiLabel10.Name = "uiLabel10";
            this.uiLabel10.Size = new System.Drawing.Size(154, 21);
            this.uiLabel10.TabIndex = 163;
            this.uiLabel10.Text = "输出人名优先词汇表";
            this.uiLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 输出人名优先词汇表Switch
            // 
            this.输出人名优先词汇表Switch.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.输出人名优先词汇表Switch.Location = new System.Drawing.Point(600, 55);
            this.输出人名优先词汇表Switch.MinimumSize = new System.Drawing.Size(1, 1);
            this.输出人名优先词汇表Switch.Name = "输出人名优先词汇表Switch";
            this.输出人名优先词汇表Switch.Size = new System.Drawing.Size(75, 29);
            this.输出人名优先词汇表Switch.TabIndex = 164;
            this.输出人名优先词汇表Switch.Text = "自定义Switch1";
            // 
            // uiLabel19
            // 
            this.uiLabel19.AutoSize = true;
            this.uiLabel19.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel19.Location = new System.Drawing.Point(609, 98);
            this.uiLabel19.Name = "uiLabel19";
            this.uiLabel19.Size = new System.Drawing.Size(94, 21);
            this.uiLabel19.TabIndex = 165;
            this.uiLabel19.Text = "Sakura机翻";
            this.uiLabel19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Sakura机翻Switch
            // 
            this.Sakura机翻Switch.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Sakura机翻Switch.Location = new System.Drawing.Point(712, 94);
            this.Sakura机翻Switch.MinimumSize = new System.Drawing.Size(1, 1);
            this.Sakura机翻Switch.Name = "Sakura机翻Switch";
            this.Sakura机翻Switch.Size = new System.Drawing.Size(75, 29);
            this.Sakura机翻Switch.TabIndex = 166;
            this.Sakura机翻Switch.Text = "自定义Switch1";
            this.Sakura机翻Switch.ActiveChanged += new System.EventHandler(this.Sakura机翻Switch_ActiveChanged);
            // 
            // Sakura和简易模式按钮判定Timer
            // 
            this.Sakura和简易模式按钮判定Timer.Interval = 30;
            this.Sakura和简易模式按钮判定Timer.Tick += new System.EventHandler(this.Sakura和简易模式按钮判定Timer_Tick);
            // 
            // GPT设置
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(987, 618);
            this.Controls.Add(this.uiLabel19);
            this.Controls.Add(this.Sakura机翻Switch);
            this.Controls.Add(this.uiLabel10);
            this.Controls.Add(this.输出人名优先词汇表Switch);
            this.Controls.Add(this.uiLabel5);
            this.Controls.Add(this.相邻对话合并Switch);
            this.Controls.Add(this.uiLabel3);
            this.Controls.Add(this.简易模式Switch);
            this.Controls.Add(this.修改Btn);
            this.Controls.Add(this.连接路由Box);
            this.Controls.Add(this.uiLine5);
            this.Controls.Add(this.uiLine4);
            this.Controls.Add(this.uiLine3);
            this.Controls.Add(this.uiLine2);
            this.Controls.Add(this.uiLine1);
            this.Controls.Add(this.Http设置Btn);
            this.Controls.Add(this.uiLabel18);
            this.Controls.Add(this.错误跳过Switch);
            this.Controls.Add(this.词汇表设置Btn);
            this.Controls.Add(this.uiLabel17);
            this.Controls.Add(this.翻后润色Switch);
            this.Controls.Add(this.润色语境Box);
            this.Controls.Add(this.uiLabel9);
            this.Controls.Add(this.uiLabel7);
            this.Controls.Add(this.漏翻重试次数Box);
            this.Controls.Add(this.uiLabel8);
            this.Controls.Add(this.漏翻检测Switch);
            this.Controls.Add(this.uiLabel16);
            this.Controls.Add(this.合并分隔符Box);
            this.Controls.Add(this.uiLabel14);
            this.Controls.Add(this.连续对话合并Switch);
            this.Controls.Add(this.uiLabel15);
            this.Controls.Add(this.上下文深度Box);
            this.Controls.Add(this.uiLabel13);
            this.Controls.Add(this.异常重试上限Box);
            this.Controls.Add(this.uiLabel12);
            this.Controls.Add(this.Token限制Box);
            this.Controls.Add(this.uiLabel11);
            this.Controls.Add(this.次数限制Box);
            this.Controls.Add(this.语境Box);
            this.Controls.Add(this.uiLabel6);
            this.Controls.Add(this.uiLabel4);
            this.Controls.Add(this.请求等待延迟Box);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.错行重试数Box);
            this.Controls.Add(this.uiLabel21);
            this.Controls.Add(this.单次机翻行Box);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.上下文提示Switch);
            this.Controls.Add(this.使用模型Label);
            this.Controls.Add(this.使用模型Box);
            this.Controls.Add(this.连接域名Label);
            this.Controls.Add(this.连接域名Box);
            this.Name = "GPT设置";
            this.Text = "GPT设置";
            this.Page被选中 += new 翻译姬.自定义Page.自定义Page被选中(this.GPT设置_Page被选中);
            this.Load += new System.EventHandler(this.GPT设置_Load);
            this.Shown += new System.EventHandler(this.GPT设置_Shown);
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
    private Sunny.UI.UILabel 使用模型Label;
    private Sunny.UI.UITextBox 使用模型Box;
    private Sunny.UI.UILabel 连接域名Label;
    private Sunny.UI.UITextBox 连接域名Box;
    private Sunny.UI.UILabel uiLabel6;
    private Sunny.UI.UITextBox 语境Box;
    private Sunny.UI.UILabel uiLabel11;
    private Sunny.UI.UITextBox 次数限制Box;
    private Sunny.UI.UILabel uiLabel12;
    private Sunny.UI.UITextBox Token限制Box;
    private Sunny.UI.UILabel uiLabel13;
    private Sunny.UI.UITextBox 异常重试上限Box;
    private Sunny.UI.UILabel uiLabel15;
    private Sunny.UI.UITextBox 上下文深度Box;
    private Sunny.UI.UILabel uiLabel14;
    private 自定义Switch 连续对话合并Switch;
    private Sunny.UI.UILabel uiLabel16;
    private Sunny.UI.UITextBox 合并分隔符Box;
    private Sunny.UI.UILabel uiLabel7;
    private Sunny.UI.UITextBox 漏翻重试次数Box;
    private Sunny.UI.UILabel uiLabel8;
    private 自定义Switch 漏翻检测Switch;
    private Sunny.UI.UILabel uiLabel9;
    private Sunny.UI.UITextBox 润色语境Box;
    private Sunny.UI.UILabel uiLabel17;
    private 自定义Switch 翻后润色Switch;
    private Sunny.UI.UIButton 词汇表设置Btn;
    private Sunny.UI.UILabel uiLabel18;
    private 自定义Switch 错误跳过Switch;
    private Sunny.UI.UIButton Http设置Btn;
    private Sunny.UI.UILine uiLine1;
    private Sunny.UI.UILine uiLine2;
    private Sunny.UI.UILine uiLine3;
    private Sunny.UI.UILine uiLine4;
    private Sunny.UI.UILine uiLine5;
    private Sunny.UI.UITextBox 连接路由Box;
    private Sunny.UI.UIButton 修改Btn;
    private Sunny.UI.UILabel uiLabel3;
    private 自定义Switch 简易模式Switch;
    private Sunny.UI.UILabel uiLabel5;
    private 自定义Switch 相邻对话合并Switch;
    private Sunny.UI.UILabel uiLabel10;
    private 自定义Switch 输出人名优先词汇表Switch;
    private Sunny.UI.UILabel uiLabel19;
    private 自定义Switch Sakura机翻Switch;
    private System.Windows.Forms.Timer Sakura和简易模式按钮判定Timer;
}