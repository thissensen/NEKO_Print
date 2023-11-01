namespace 翻译姬 {
    partial class 组合控件Switch {
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
            this.Switch = new 翻译姬.自定义Switch();
            this.LabelPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Switch
            // 
            this.Switch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Switch.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Switch.Location = new System.Drawing.Point(100, 0);
            this.Switch.MinimumSize = new System.Drawing.Size(1, 1);
            this.Switch.Name = "Switch";
            this.Switch.Size = new System.Drawing.Size(79, 29);
            this.Switch.TabIndex = 1;
            this.Switch.Text = "uiSwitch1";
            // 
            // 组合控件Switch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.Switch);
            this.Name = "组合控件Switch";
            this.Size = new System.Drawing.Size(179, 29);
            this.主控件Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.主控件Text = "uiSwitch1";
            this.Controls.SetChildIndex(this.LabelPanel, 0);
            this.Controls.SetChildIndex(this.Switch, 0);
            this.LabelPanel.ResumeLayout(false);
            this.LabelPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public 自定义Switch Switch;
    }
}
