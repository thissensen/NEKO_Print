namespace 翻译姬;

partial class GPT词汇表窗体 {
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.清空Btn = new Sunny.UI.UIButton();
            this.导出Btn = new Sunny.UI.UIButton();
            this.导入Btn = new Sunny.UI.UIButton();
            this.词汇表 = new 翻译姬.自定义DataGridView();
            this.返回Btn = new Sunny.UI.UIButton();
            this.原文 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.译文 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.备注 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.词汇表)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.返回Btn);
            this.panel1.Controls.Add(this.清空Btn);
            this.panel1.Controls.Add(this.导出Btn);
            this.panel1.Controls.Add(this.导入Btn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(391, 43);
            this.panel1.TabIndex = 0;
            // 
            // 清空Btn
            // 
            this.清空Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.清空Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.清空Btn.Location = new System.Drawing.Point(205, 4);
            this.清空Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.清空Btn.Name = "清空Btn";
            this.清空Btn.Size = new System.Drawing.Size(73, 33);
            this.清空Btn.TabIndex = 2;
            this.清空Btn.Text = "清空";
            this.清空Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.清空Btn.Click += new System.EventHandler(this.清空Btn_Click);
            // 
            // 导出Btn
            // 
            this.导出Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.导出Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.导出Btn.Location = new System.Drawing.Point(110, 4);
            this.导出Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.导出Btn.Name = "导出Btn";
            this.导出Btn.Size = new System.Drawing.Size(73, 33);
            this.导出Btn.TabIndex = 1;
            this.导出Btn.Text = "导出";
            this.导出Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.导出Btn.Click += new System.EventHandler(this.导出Btn_Click);
            // 
            // 导入Btn
            // 
            this.导入Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.导入Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.导入Btn.Location = new System.Drawing.Point(15, 4);
            this.导入Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.导入Btn.Name = "导入Btn";
            this.导入Btn.Size = new System.Drawing.Size(73, 33);
            this.导入Btn.TabIndex = 0;
            this.导入Btn.Text = "导入";
            this.导入Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.导入Btn.Click += new System.EventHandler(this.导入Btn_Click);
            // 
            // 词汇表
            // 
            this.词汇表.AllowUserToAddRows = false;
            this.词汇表.AllowUserToDeleteRows = false;
            this.词汇表.AllowUserToResizeRows = false;
            this.词汇表.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.词汇表.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.词汇表.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.词汇表.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.原文,
            this.译文,
            this.备注});
            this.词汇表.Dock = System.Windows.Forms.DockStyle.Fill;
            this.词汇表.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.词汇表.EnableHeadersVisualStyles = false;
            this.词汇表.Location = new System.Drawing.Point(1, 79);
            this.词汇表.Name = "词汇表";
            this.词汇表.ReadOnly = true;
            this.词汇表.RowHeadersVisible = false;
            this.词汇表.RowHeadersWidth = 4;
            this.词汇表.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.词汇表.RowTemplate.Height = 29;
            this.词汇表.Size = new System.Drawing.Size(391, 370);
            this.词汇表.TabIndex = 1;
            // 
            // 返回Btn
            // 
            this.返回Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.返回Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.返回Btn.Location = new System.Drawing.Point(300, 4);
            this.返回Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.返回Btn.Name = "返回Btn";
            this.返回Btn.Size = new System.Drawing.Size(73, 33);
            this.返回Btn.TabIndex = 3;
            this.返回Btn.Text = "返回";
            this.返回Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.返回Btn.Click += new System.EventHandler(this.返回Btn_Click);
            // 
            // 原文
            // 
            this.原文.DataPropertyName = "原文";
            this.原文.HeaderText = "原文";
            this.原文.Name = "原文";
            this.原文.Width = 120;
            // 
            // 译文
            // 
            this.译文.DataPropertyName = "译文";
            this.译文.HeaderText = "译文";
            this.译文.Name = "译文";
            // 
            // 备注
            // 
            this.备注.DataPropertyName = "备注";
            this.备注.HeaderText = "备注";
            this.备注.Name = "备注";
            this.备注.Width = 170;
            // 
            // GPT词汇表窗体
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(393, 450);
            this.ControlBox = false;
            this.Controls.Add(this.词汇表);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GPT词汇表窗体";
            this.Padding = new System.Windows.Forms.Padding(1, 36, 1, 1);
            this.ShowInTaskbar = false;
            this.Text = "GPT词汇表窗体";
            this.Load += new System.EventHandler(this.GPT词汇表窗体_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.词汇表)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private 自定义DataGridView 词汇表;
    private Sunny.UI.UIButton 导入Btn;
    private Sunny.UI.UIButton 导出Btn;
    private Sunny.UI.UIButton 清空Btn;
    private Sunny.UI.UIButton 返回Btn;
    private System.Windows.Forms.DataGridViewTextBoxColumn 原文;
    private System.Windows.Forms.DataGridViewTextBoxColumn 译文;
    private System.Windows.Forms.DataGridViewTextBoxColumn 备注;
}