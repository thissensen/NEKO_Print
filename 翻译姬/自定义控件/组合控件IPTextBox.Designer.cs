namespace 翻译姬 {
    partial class 组合控件IPTextBox {
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
        private void InitializeComponent() {
            this.IPTextBox = new Sunny.UI.UIIPTextBox();
            this.LabelPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // IPTextBox
            // 
            this.IPTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IPTextBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.IPTextBox.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.IPTextBox.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IPTextBox.Location = new System.Drawing.Point(100, 0);
            this.IPTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.IPTextBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Padding = new System.Windows.Forms.Padding(1);
            this.IPTextBox.ShowText = false;
            this.IPTextBox.Size = new System.Drawing.Size(200, 29);
            this.IPTextBox.Style = Sunny.UI.UIStyle.Custom;
            this.IPTextBox.TabIndex = 1;
            this.IPTextBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 组合控件IPTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.IPTextBox);
            this.Name = "组合控件IPTextBox";
            this.主控件Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Controls.SetChildIndex(this.LabelPanel, 0);
            this.Controls.SetChildIndex(this.IPTextBox, 0);
            this.LabelPanel.ResumeLayout(false);
            this.LabelPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIIPTextBox IPTextBox;
    }
}
