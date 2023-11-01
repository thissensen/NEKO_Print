namespace 翻译姬 {
    partial class 自定义Page {
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
            this.SuspendLayout();
            // 
            // 自定义Page
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(987, 618);
            this.Name = "自定义Page";
            this.Text = "自定义Page";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.自定义Page_FormClosed);
            this.Load += new System.EventHandler(this.自定义Page_Load);
            this.Shown += new System.EventHandler(this.自定义Page_Shown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}