namespace 翻译姬 {
    partial class 自定义通知 {
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
            this.定时器 = new System.Windows.Forms.Timer(this.components);
            this.全区域Panel = new Sunny.UI.UIPanel();
            this.标题栏Panel = new Sunny.UI.UIPanel();
            this.文字区Label = new Sunny.UI.UISymbolLabel();
            this.全区域Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // 定时器
            // 
            this.定时器.Interval = 2000;
            this.定时器.Tick += new System.EventHandler(this.定时器_Tick);
            // 
            // 全区域Panel
            // 
            this.全区域Panel.Controls.Add(this.标题栏Panel);
            this.全区域Panel.Controls.Add(this.文字区Label);
            this.全区域Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.全区域Panel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.全区域Panel.Location = new System.Drawing.Point(0, 0);
            this.全区域Panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.全区域Panel.MinimumSize = new System.Drawing.Size(1, 1);
            this.全区域Panel.Name = "全区域Panel";
            this.全区域Panel.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.全区域Panel.Size = new System.Drawing.Size(455, 170);
            this.全区域Panel.Style = Sunny.UI.UIStyle.Custom;
            this.全区域Panel.TabIndex = 0;
            this.全区域Panel.Text = null;
            this.全区域Panel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 标题栏Panel
            // 
            this.标题栏Panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.标题栏Panel.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.标题栏Panel.Location = new System.Drawing.Point(0, 0);
            this.标题栏Panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.标题栏Panel.MinimumSize = new System.Drawing.Size(1, 1);
            this.标题栏Panel.Name = "标题栏Panel";
            this.标题栏Panel.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.标题栏Panel.Size = new System.Drawing.Size(455, 44);
            this.标题栏Panel.Style = Sunny.UI.UIStyle.Custom;
            this.标题栏Panel.TabIndex = 1;
            this.标题栏Panel.Text = "翻译姬";
            this.标题栏Panel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 文字区Label
            // 
            this.文字区Label.Font = new System.Drawing.Font("微软雅黑", 24F);
            this.文字区Label.Location = new System.Drawing.Point(31, 68);
            this.文字区Label.MinimumSize = new System.Drawing.Size(1, 1);
            this.文字区Label.Name = "文字区Label";
            this.文字区Label.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.文字区Label.Size = new System.Drawing.Size(309, 75);
            this.文字区Label.Style = Sunny.UI.UIStyle.Custom;
            this.文字区Label.Symbol = 61737;
            this.文字区Label.SymbolSize = 56;
            this.文字区Label.TabIndex = 0;
            this.文字区Label.Text = "任务已完成";
            this.文字区Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 自定义通知
            // 
            this.AllowShowTitle = false;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.ClientSize = new System.Drawing.Size(455, 170);
            this.Controls.Add(this.全区域Panel);
            this.Name = "自定义通知";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.ShowInTaskbar = false;
            this.ShowRadius = false;
            this.ShowRect = false;
            this.ShowTitle = false;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "自定义通知";
            this.Shown += new System.EventHandler(this.自定义通知_Shown);
            this.全区域Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer 定时器;
        private Sunny.UI.UIPanel 全区域Panel;
        private Sunny.UI.UIPanel 标题栏Panel;
        private Sunny.UI.UISymbolLabel 文字区Label;
    }
}