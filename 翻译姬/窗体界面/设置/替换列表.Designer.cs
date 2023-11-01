namespace 翻译姬 {
    partial class 替换列表 {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.表格增删改 = new 翻译姬.表格增删改();
            this.查询表格 = new 翻译姬.自定义DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.是否启用 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.替换时机 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.匹配行为 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.替换列表Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.查询表格)).BeginInit();
            this.SuspendLayout();
            // 
            // 表格增删改
            // 
            this.表格增删改.Dock = System.Windows.Forms.DockStyle.Top;
            this.表格增删改.Location = new System.Drawing.Point(0, 0);
            this.表格增删改.Name = "表格增删改";
            this.表格增删改.Size = new System.Drawing.Size(987, 41);
            this.表格增删改.SQL = null;
            this.表格增删改.TabIndex = 0;
            this.表格增删改.主键 = "ID";
            this.表格增删改.关联表名 = "替换列表";
            this.表格增删改.表格 = this.查询表格;
            this.表格增删改.表格行移动中 = false;
            this.表格增删改.新添前行验证 += new 翻译姬.表格增删改.表格增删改_新添前行验证(this.表格增删改_新添前行验证);
            this.表格增删改.新添行后执行 += new 翻译姬.表格增删改.表格增删改_新添行后执行(this.表格增删改_新添行后执行);
            this.表格增删改.保存前验证 += new 翻译姬.表格增删改.表格增删改_保存前验证(this.表格增删改_保存前验证);
            // 
            // 查询表格
            // 
            this.查询表格.AllowUserToAddRows = false;
            this.查询表格.AllowUserToDeleteRows = false;
            this.查询表格.AllowUserToResizeColumns = false;
            this.查询表格.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.查询表格.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.查询表格.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.查询表格.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.查询表格.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.查询表格.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.查询表格.ColumnHeadersHeight = 32;
            this.查询表格.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.查询表格.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.是否启用,
            this.名称,
            this.替换时机,
            this.匹配行为,
            this.替换列表Col});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.查询表格.DefaultCellStyle = dataGridViewCellStyle3;
            this.查询表格.Dock = System.Windows.Forms.DockStyle.Fill;
            this.查询表格.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.查询表格.EnableHeadersVisualStyles = false;
            this.查询表格.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.查询表格.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(173)))), ((int)(((byte)(255)))));
            this.查询表格.Location = new System.Drawing.Point(0, 41);
            this.查询表格.Name = "查询表格";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.查询表格.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.查询表格.RowHeadersWidth = 4;
            this.查询表格.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.查询表格.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.查询表格.RowTemplate.Height = 29;
            this.查询表格.Size = new System.Drawing.Size(987, 577);
            this.查询表格.TabIndex = 1;
            this.查询表格.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.查询表格_CellMouseClick);
            this.查询表格.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.查询表格_DataError);
            this.查询表格.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.查询表格_EditingControlShowing);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // 是否启用
            // 
            this.是否启用.DataPropertyName = "是否启用";
            this.是否启用.HeaderText = "是否启用";
            this.是否启用.Name = "是否启用";
            this.是否启用.Width = 80;
            // 
            // 名称
            // 
            this.名称.DataPropertyName = "名称";
            this.名称.HeaderText = "名称";
            this.名称.Name = "名称";
            this.名称.Width = 300;
            // 
            // 替换时机
            // 
            this.替换时机.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.替换时机.DataPropertyName = "替换时机";
            this.替换时机.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.替换时机.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.替换时机.HeaderText = "替换时机";
            this.替换时机.Name = "替换时机";
            this.替换时机.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.替换时机.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.替换时机.Width = 98;
            // 
            // 匹配行为
            // 
            this.匹配行为.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.匹配行为.DataPropertyName = "匹配行为";
            this.匹配行为.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.匹配行为.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.匹配行为.HeaderText = "匹配行为";
            this.匹配行为.Name = "匹配行为";
            this.匹配行为.Width = 79;
            // 
            // 替换列表Col
            // 
            this.替换列表Col.DataPropertyName = "替换列表";
            this.替换列表Col.HeaderText = "替换列表";
            this.替换列表Col.Name = "替换列表Col";
            this.替换列表Col.ReadOnly = true;
            this.替换列表Col.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.替换列表Col.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.替换列表Col.Width = 442;
            // 
            // 替换列表
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(987, 618);
            this.Controls.Add(this.查询表格);
            this.Controls.Add(this.表格增删改);
            this.Name = "替换列表";
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "替换列表";
            this.Page被选中 += new 翻译姬.自定义Page.自定义Page被选中(this.替换列表_Page被选中);
            this.Load += new System.EventHandler(this.替换列表_Load);
            this.Shown += new System.EventHandler(this.替换列表_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.查询表格)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private 表格增删改 表格增删改;
        private 自定义DataGridView 查询表格;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn 是否启用;
        private System.Windows.Forms.DataGridViewTextBoxColumn 名称;
        private System.Windows.Forms.DataGridViewComboBoxColumn 替换时机;
        private System.Windows.Forms.DataGridViewComboBoxColumn 匹配行为;
        private System.Windows.Forms.DataGridViewTextBoxColumn 替换列表Col;
    }
}