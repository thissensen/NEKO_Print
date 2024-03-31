namespace 翻译姬;

partial class 自定义颜色选择器 {
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
            this.btnCancel = new Sunny.UI.UISymbolButton();
            this.btnOK = new Sunny.UI.UISymbolButton();
            this.lblB = new Sunny.UI.UILabel();
            this.lblG = new Sunny.UI.UILabel();
            this.lblR = new Sunny.UI.UILabel();
            this.edtB = new Sunny.UI.UITextBox();
            this.edtG = new Sunny.UI.UITextBox();
            this.edtR = new Sunny.UI.UITextBox();
            this.RBar = new Sunny.UI.UITrackBar();
            this.GBar = new Sunny.UI.UITrackBar();
            this.BBar = new Sunny.UI.UITrackBar();
            this.颜色框 = new Sunny.UI.UIPanel();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnCancel.Location = new System.Drawing.Point(165, 149);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.btnCancel.Size = new System.Drawing.Size(95, 26);
            this.btnCancel.Symbol = 61453;
            this.btnCancel.TabIndex = 28;
            this.btnCancel.Text = "取消";
            this.btnCancel.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnOK.Location = new System.Drawing.Point(31, 149);
            this.btnOK.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.btnOK.Size = new System.Drawing.Size(95, 26);
            this.btnOK.TabIndex = 27;
            this.btnOK.Text = "确定";
            this.btnOK.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblB
            // 
            this.lblB.AutoSize = true;
            this.lblB.BackColor = System.Drawing.Color.Transparent;
            this.lblB.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblB.Location = new System.Drawing.Point(12, 86);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(18, 20);
            this.lblB.TabIndex = 24;
            this.lblB.Text = "B";
            this.lblB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblG
            // 
            this.lblG.AutoSize = true;
            this.lblG.BackColor = System.Drawing.Color.Transparent;
            this.lblG.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblG.Location = new System.Drawing.Point(12, 51);
            this.lblG.Name = "lblG";
            this.lblG.Size = new System.Drawing.Size(19, 20);
            this.lblG.TabIndex = 23;
            this.lblG.Text = "G";
            this.lblG.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblR
            // 
            this.lblR.AutoSize = true;
            this.lblR.BackColor = System.Drawing.Color.Transparent;
            this.lblR.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblR.Location = new System.Drawing.Point(12, 16);
            this.lblR.Name = "lblR";
            this.lblR.Size = new System.Drawing.Size(18, 20);
            this.lblR.TabIndex = 22;
            this.lblR.Text = "R";
            this.lblR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // edtB
            // 
            this.edtB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.edtB.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.edtB.Location = new System.Drawing.Point(31, 83);
            this.edtB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edtB.Maximum = 255D;
            this.edtB.Minimum = 0D;
            this.edtB.MinimumSize = new System.Drawing.Size(1, 16);
            this.edtB.Name = "edtB";
            this.edtB.Padding = new System.Windows.Forms.Padding(5);
            this.edtB.ShowText = false;
            this.edtB.Size = new System.Drawing.Size(41, 26);
            this.edtB.TabIndex = 20;
            this.edtB.Text = "0";
            this.edtB.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.edtB.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.edtB.Watermark = "";
            this.edtB.TextChanged += new System.EventHandler(this.颜色变更_Changed);
            // 
            // edtG
            // 
            this.edtG.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.edtG.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.edtG.Location = new System.Drawing.Point(31, 48);
            this.edtG.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edtG.Maximum = 255D;
            this.edtG.Minimum = 0D;
            this.edtG.MinimumSize = new System.Drawing.Size(1, 16);
            this.edtG.Name = "edtG";
            this.edtG.Padding = new System.Windows.Forms.Padding(5);
            this.edtG.ShowText = false;
            this.edtG.Size = new System.Drawing.Size(41, 26);
            this.edtG.TabIndex = 19;
            this.edtG.Text = "0";
            this.edtG.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.edtG.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.edtG.Watermark = "";
            this.edtG.TextChanged += new System.EventHandler(this.颜色变更_Changed);
            // 
            // edtR
            // 
            this.edtR.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.edtR.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.edtR.Location = new System.Drawing.Point(31, 13);
            this.edtR.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edtR.Maximum = 255D;
            this.edtR.Minimum = 0D;
            this.edtR.MinimumSize = new System.Drawing.Size(1, 16);
            this.edtR.Name = "edtR";
            this.edtR.Padding = new System.Windows.Forms.Padding(5);
            this.edtR.ShowText = false;
            this.edtR.Size = new System.Drawing.Size(41, 26);
            this.edtR.TabIndex = 18;
            this.edtR.Text = "0";
            this.edtR.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.edtR.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.edtR.Watermark = "";
            this.edtR.TextChanged += new System.EventHandler(this.颜色变更_Changed);
            // 
            // RBar
            // 
            this.RBar.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RBar.Location = new System.Drawing.Point(79, 12);
            this.RBar.Maximum = 255;
            this.RBar.MinimumSize = new System.Drawing.Size(1, 1);
            this.RBar.Name = "RBar";
            this.RBar.Size = new System.Drawing.Size(202, 29);
            this.RBar.TabIndex = 30;
            this.RBar.Text = "uiTrackBar2";
            // 
            // GBar
            // 
            this.GBar.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.GBar.Location = new System.Drawing.Point(79, 47);
            this.GBar.Maximum = 255;
            this.GBar.MinimumSize = new System.Drawing.Size(1, 1);
            this.GBar.Name = "GBar";
            this.GBar.Size = new System.Drawing.Size(202, 29);
            this.GBar.TabIndex = 31;
            this.GBar.Text = "uiTrackBar3";
            // 
            // BBar
            // 
            this.BBar.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BBar.Location = new System.Drawing.Point(79, 82);
            this.BBar.Maximum = 255;
            this.BBar.MinimumSize = new System.Drawing.Size(1, 1);
            this.BBar.Name = "BBar";
            this.BBar.Size = new System.Drawing.Size(202, 29);
            this.BBar.TabIndex = 32;
            this.BBar.Text = "uiTrackBar4";
            // 
            // 颜色框
            // 
            this.颜色框.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.颜色框.Location = new System.Drawing.Point(16, 119);
            this.颜色框.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.颜色框.MinimumSize = new System.Drawing.Size(1, 1);
            this.颜色框.Name = "颜色框";
            this.颜色框.Size = new System.Drawing.Size(264, 22);
            this.颜色框.TabIndex = 33;
            this.颜色框.Text = null;
            this.颜色框.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 自定义颜色选择器
            // 
            this.AllowShowTitle = false;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(293, 189);
            this.Controls.Add(this.颜色框);
            this.Controls.Add(this.BBar);
            this.Controls.Add(this.GBar);
            this.Controls.Add(this.RBar);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblB);
            this.Controls.Add(this.lblG);
            this.Controls.Add(this.lblR);
            this.Controls.Add(this.edtB);
            this.Controls.Add(this.edtG);
            this.Controls.Add(this.edtR);
            this.Name = "自定义颜色选择器";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.ShowInTaskbar = false;
            this.ShowRadius = false;
            this.ShowTitle = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "自定义颜色选择器";
            this.Shown += new System.EventHandler(this.自定义颜色选择器_Shown);
            this.Leave += new System.EventHandler(this.自定义颜色选择器_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Sunny.UI.UISymbolButton btnCancel;
    private Sunny.UI.UISymbolButton btnOK;
    private Sunny.UI.UILabel lblB;
    private Sunny.UI.UILabel lblG;
    private Sunny.UI.UILabel lblR;
    private Sunny.UI.UITextBox edtB;
    private Sunny.UI.UITextBox edtG;
    private Sunny.UI.UITextBox edtR;
    private Sunny.UI.UITrackBar RBar;
    private Sunny.UI.UITrackBar GBar;
    private Sunny.UI.UITrackBar BBar;
    private Sunny.UI.UIPanel 颜色框;
}