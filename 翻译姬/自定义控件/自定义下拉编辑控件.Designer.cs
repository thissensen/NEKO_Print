namespace 翻译姬;

partial class 自定义下拉编辑控件 {
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
            this.ComboBox = new 翻译姬.自定义ComboBox();
            this.SuspendLayout();
            // 
            // ComboBox
            // 
            this.ComboBox.DataSource = null;
            this.ComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComboBox.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.ComboBox.FillColor = System.Drawing.Color.White;
            this.ComboBox.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ComboBox.Location = new System.Drawing.Point(0, 0);
            this.ComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ComboBox.MinimumSize = new System.Drawing.Size(63, 0);
            this.ComboBox.Name = "ComboBox";
            this.ComboBox.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.ComboBox.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.ComboBox.Size = new System.Drawing.Size(150, 33);
            this.ComboBox.TabIndex = 0;
            this.ComboBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComboBox.Watermark = "";
            // 
            // 自定义下拉编辑控件
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ComboBox);
            this.Name = "自定义下拉编辑控件";
            this.Size = new System.Drawing.Size(150, 33);
            this.Load += new System.EventHandler(this.自定义下拉编辑控件_Load);
            this.ResumeLayout(false);

    }

    #endregion

    public 自定义ComboBox ComboBox;
}
