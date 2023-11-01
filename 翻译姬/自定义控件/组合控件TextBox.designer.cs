using Sunny.UI;

namespace 翻译姬 {
    partial class 组合控件TextBox {
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
            this.TextBox = new Sunny.UI.UITextBox();
            this.LabelPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox
            // 
            this.TextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TextBox.Location = new System.Drawing.Point(100, 0);
            this.TextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TextBox.MinimumSize = new System.Drawing.Size(1, 16);
            this.TextBox.Name = "TextBox";
            this.TextBox.ShowText = false;
            this.TextBox.Size = new System.Drawing.Size(200, 29);
            this.TextBox.TabIndex = 1;
            this.TextBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.TextBox.Watermark = "";
            // 
            // 组合控件TextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.TextBox);
            this.Name = "组合控件TextBox";
            this.主控件Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.主控件Text = "";
            this.Controls.SetChildIndex(this.LabelPanel, 0);
            this.Controls.SetChildIndex(this.TextBox, 0);
            this.LabelPanel.ResumeLayout(false);
            this.LabelPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public UITextBox TextBox;
    }
}
