namespace 翻译姬 {
    partial class 自定义提示窗 {
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
            this.窗体总Panel = new Sunny.UI.UIPanel();
            this.中间区Panel = new Sunny.UI.UIPanel();
            this.lbMsg = new Sunny.UI.UIRichTextBox();
            this.按钮区Panel = new Sunny.UI.UIPanel();
            this.取消Btn = new Sunny.UI.UIButton();
            this.确认Btn = new Sunny.UI.UIButton();
            this.标题栏Panel = new Sunny.UI.UIPanel();
            this.窗体总Panel.SuspendLayout();
            this.中间区Panel.SuspendLayout();
            this.按钮区Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // 窗体总Panel
            // 
            this.窗体总Panel.Controls.Add(this.中间区Panel);
            this.窗体总Panel.Controls.Add(this.按钮区Panel);
            this.窗体总Panel.Controls.Add(this.标题栏Panel);
            this.窗体总Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.窗体总Panel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.窗体总Panel.Location = new System.Drawing.Point(0, 0);
            this.窗体总Panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.窗体总Panel.MinimumSize = new System.Drawing.Size(1, 1);
            this.窗体总Panel.Name = "窗体总Panel";
            this.窗体总Panel.Radius = 25;
            this.窗体总Panel.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.窗体总Panel.Size = new System.Drawing.Size(516, 314);
            this.窗体总Panel.Style = Sunny.UI.UIStyle.Custom;
            this.窗体总Panel.TabIndex = 0;
            this.窗体总Panel.Text = null;
            this.窗体总Panel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 中间区Panel
            // 
            this.中间区Panel.Controls.Add(this.lbMsg);
            this.中间区Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.中间区Panel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.中间区Panel.Location = new System.Drawing.Point(0, 53);
            this.中间区Panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.中间区Panel.MinimumSize = new System.Drawing.Size(1, 1);
            this.中间区Panel.Name = "中间区Panel";
            this.中间区Panel.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.中间区Panel.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.中间区Panel.Size = new System.Drawing.Size(516, 170);
            this.中间区Panel.Style = Sunny.UI.UIStyle.Custom;
            this.中间区Panel.TabIndex = 2;
            this.中间区Panel.Text = null;
            this.中间区Panel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbMsg
            // 
            this.lbMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.lbMsg.FillColor = System.Drawing.Color.White;
            this.lbMsg.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.lbMsg.Location = new System.Drawing.Point(13, 10);
            this.lbMsg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbMsg.MinimumSize = new System.Drawing.Size(1, 1);
            this.lbMsg.Name = "lbMsg";
            this.lbMsg.Padding = new System.Windows.Forms.Padding(2);
            this.lbMsg.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.lbMsg.ReadOnly = true;
            this.lbMsg.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.lbMsg.ShowText = false;
            this.lbMsg.Size = new System.Drawing.Size(490, 150);
            this.lbMsg.Style = Sunny.UI.UIStyle.Custom;
            this.lbMsg.TabIndex = 8;
            this.lbMsg.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 按钮区Panel
            // 
            this.按钮区Panel.Controls.Add(this.取消Btn);
            this.按钮区Panel.Controls.Add(this.确认Btn);
            this.按钮区Panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.按钮区Panel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.按钮区Panel.Location = new System.Drawing.Point(0, 223);
            this.按钮区Panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.按钮区Panel.MinimumSize = new System.Drawing.Size(1, 1);
            this.按钮区Panel.Name = "按钮区Panel";
            this.按钮区Panel.Radius = 25;
            this.按钮区Panel.RadiusSides = ((Sunny.UI.UICornerRadiusSides)((Sunny.UI.UICornerRadiusSides.RightBottom | Sunny.UI.UICornerRadiusSides.LeftBottom)));
            this.按钮区Panel.Size = new System.Drawing.Size(516, 91);
            this.按钮区Panel.Style = Sunny.UI.UIStyle.Custom;
            this.按钮区Panel.TabIndex = 1;
            this.按钮区Panel.Text = null;
            this.按钮区Panel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 取消Btn
            // 
            this.取消Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.取消Btn.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.取消Btn.Location = new System.Drawing.Point(289, 16);
            this.取消Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.取消Btn.Name = "取消Btn";
            this.取消Btn.Radius = 15;
            this.取消Btn.Size = new System.Drawing.Size(165, 62);
            this.取消Btn.Style = Sunny.UI.UIStyle.Custom;
            this.取消Btn.TabIndex = 1;
            this.取消Btn.Text = "取消";
            this.取消Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.取消Btn.Click += new System.EventHandler(this.取消Btn_Click);
            // 
            // 确认Btn
            // 
            this.确认Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.确认Btn.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.确认Btn.Location = new System.Drawing.Point(64, 16);
            this.确认Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.确认Btn.Name = "确认Btn";
            this.确认Btn.Radius = 15;
            this.确认Btn.Size = new System.Drawing.Size(165, 62);
            this.确认Btn.Style = Sunny.UI.UIStyle.Custom;
            this.确认Btn.TabIndex = 0;
            this.确认Btn.Text = "确认";
            this.确认Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.确认Btn.Click += new System.EventHandler(this.确认Btn_Click);
            // 
            // 标题栏Panel
            // 
            this.标题栏Panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.标题栏Panel.Font = new System.Drawing.Font("微软雅黑", 24F);
            this.标题栏Panel.Location = new System.Drawing.Point(0, 0);
            this.标题栏Panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.标题栏Panel.MinimumSize = new System.Drawing.Size(1, 1);
            this.标题栏Panel.Name = "标题栏Panel";
            this.标题栏Panel.Radius = 25;
            this.标题栏Panel.RadiusSides = ((Sunny.UI.UICornerRadiusSides)((Sunny.UI.UICornerRadiusSides.LeftTop | Sunny.UI.UICornerRadiusSides.RightTop)));
            this.标题栏Panel.Size = new System.Drawing.Size(516, 53);
            this.标题栏Panel.Style = Sunny.UI.UIStyle.Custom;
            this.标题栏Panel.TabIndex = 0;
            this.标题栏Panel.Text = "提示内容";
            this.标题栏Panel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.标题栏Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.移动窗口_MouseDown);
            // 
            // 自定义提示窗
            // 
            this.AllowShowTitle = false;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.ClientSize = new System.Drawing.Size(516, 314);
            this.Controls.Add(this.窗体总Panel);
            this.Movable = false;
            this.Name = "自定义提示窗";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.ShowInTaskbar = false;
            this.ShowRadius = false;
            this.ShowRect = false;
            this.ShowTitle = false;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "自定义提示窗";
            this.Load += new System.EventHandler(this.自定义提示窗_Load);
            this.Shown += new System.EventHandler(this.自定义提示窗_Shown);
            this.窗体总Panel.ResumeLayout(false);
            this.中间区Panel.ResumeLayout(false);
            this.按钮区Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel 窗体总Panel;
        private Sunny.UI.UIPanel 按钮区Panel;
        private Sunny.UI.UIPanel 标题栏Panel;
        private Sunny.UI.UIButton 取消Btn;
        private Sunny.UI.UIButton 确认Btn;
        private Sunny.UI.UIPanel 中间区Panel;
        private Sunny.UI.UIRichTextBox lbMsg;
    }
}