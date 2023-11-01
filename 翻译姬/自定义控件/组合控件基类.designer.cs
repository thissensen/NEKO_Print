namespace 翻译姬 {
    public partial class 组合控件基类<T> {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        public void InitializeComponent() {
            this.LabelPanel = new Sunny.UI.UIPanel();
            this.Label = new Sunny.UI.UILabel();
            this.LabelPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelPanel
            // 
            this.LabelPanel.Controls.Add(this.Label);
            this.LabelPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelPanel.FillColor = System.Drawing.Color.Transparent;
            this.LabelPanel.FillColor2 = System.Drawing.Color.Transparent;
            this.LabelPanel.FillDisableColor = System.Drawing.Color.Transparent;
            this.LabelPanel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelPanel.Location = new System.Drawing.Point(0, 0);
            this.LabelPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LabelPanel.MinimumSize = new System.Drawing.Size(1, 1);
            this.LabelPanel.Name = "LabelPanel";
            this.LabelPanel.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.LabelPanel.Size = new System.Drawing.Size(100, 29);
            this.LabelPanel.Style = Sunny.UI.UIStyle.Custom;
            this.LabelPanel.TabIndex = 0;
            this.LabelPanel.Text = null;
            this.LabelPanel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label.Location = new System.Drawing.Point(0, 0);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(73, 21);
            this.Label.Style = Sunny.UI.UIStyle.Custom;
            this.Label.TabIndex = 0;
            this.Label.Text = "uiLabel1";
            this.Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 组合控件基类
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.LabelPanel);
            this.MinimumSize = new System.Drawing.Size(0, 29);
            this.Name = "组合控件基类";
            this.Size = new System.Drawing.Size(300, 29);
            this.Load += new System.EventHandler(this.数据库控件基类_Load);
            this.LabelPanel.ResumeLayout(false);
            this.LabelPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public Sunny.UI.UIPanel LabelPanel;
        public Sunny.UI.UILabel Label;
    }
}
