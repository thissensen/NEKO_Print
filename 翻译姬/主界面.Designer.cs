namespace 翻译姬 {
    partial class 主界面 {
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
            this.选择区Panel = new Sunny.UI.UIPanel();
            this.窗体列表 = new Sunny.UI.UINavMenu();
            this.LOGOPanel = new Sunny.UI.UIPanel();
            this.标题栏Panel = new Sunny.UI.UIPanel();
            this.最小化Btn = new Sunny.UI.UISymbolButton();
            this.进度条 = new Sunny.UI.UIProcessBar();
            this.关闭Btn = new Sunny.UI.UISymbolButton();
            this.右边半块Panel = new Sunny.UI.UIPanel();
            this.窗体显示区TabControl = new Sunny.UI.UITabControl();
            this.StyleManager = new Sunny.UI.UIStyleManager(this.components);
            this.通知图标 = new System.Windows.Forms.NotifyIcon(this.components);
            this.选择区Panel.SuspendLayout();
            this.标题栏Panel.SuspendLayout();
            this.右边半块Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // 选择区Panel
            // 
            this.选择区Panel.Controls.Add(this.窗体列表);
            this.选择区Panel.Controls.Add(this.LOGOPanel);
            this.选择区Panel.Dock = System.Windows.Forms.DockStyle.Left;
            this.选择区Panel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.选择区Panel.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.选择区Panel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.选择区Panel.Location = new System.Drawing.Point(0, 0);
            this.选择区Panel.Margin = new System.Windows.Forms.Padding(0);
            this.选择区Panel.MinimumSize = new System.Drawing.Size(1, 1);
            this.选择区Panel.Name = "选择区Panel";
            this.选择区Panel.Padding = new System.Windows.Forms.Padding(1);
            this.选择区Panel.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.选择区Panel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.选择区Panel.Size = new System.Drawing.Size(292, 720);
            this.选择区Panel.Style = Sunny.UI.UIStyle.Custom;
            this.选择区Panel.TabIndex = 3;
            this.选择区Panel.Text = null;
            this.选择区Panel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 窗体列表
            // 
            this.窗体列表.BackColor = System.Drawing.Color.White;
            this.窗体列表.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.窗体列表.Dock = System.Windows.Forms.DockStyle.Fill;
            this.窗体列表.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.窗体列表.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.窗体列表.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.窗体列表.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.窗体列表.FullRowSelect = true;
            this.窗体列表.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.窗体列表.Indent = 19;
            this.窗体列表.ItemHeight = 70;
            this.窗体列表.LineColor = System.Drawing.Color.Red;
            this.窗体列表.Location = new System.Drawing.Point(1, 101);
            this.窗体列表.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.窗体列表.Name = "窗体列表";
            this.窗体列表.ScrollBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.窗体列表.ScrollBarHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.窗体列表.ScrollBarPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.窗体列表.ScrollFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.窗体列表.SecondBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.窗体列表.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.窗体列表.SelectedColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.窗体列表.ShowLines = false;
            this.窗体列表.ShowOneNode = true;
            this.窗体列表.ShowSecondBackColor = true;
            this.窗体列表.ShowTips = true;
            this.窗体列表.Size = new System.Drawing.Size(290, 618);
            this.窗体列表.Style = Sunny.UI.UIStyle.Custom;
            this.窗体列表.StyleCustomMode = true;
            this.窗体列表.TabIndex = 4;
            this.窗体列表.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.窗体列表.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.窗体列表_AfterExpand);
            this.窗体列表.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.窗体列表_NodeMouseClick);
            // 
            // LOGOPanel
            // 
            this.LOGOPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.LOGOPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.LOGOPanel.Font = new System.Drawing.Font("微软雅黑", 48F);
            this.LOGOPanel.Location = new System.Drawing.Point(1, 1);
            this.LOGOPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LOGOPanel.MinimumSize = new System.Drawing.Size(1, 1);
            this.LOGOPanel.Name = "LOGOPanel";
            this.LOGOPanel.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.LOGOPanel.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            this.LOGOPanel.Size = new System.Drawing.Size(290, 100);
            this.LOGOPanel.Style = Sunny.UI.UIStyle.Custom;
            this.LOGOPanel.TabIndex = 2;
            this.LOGOPanel.Text = "翻译姬";
            this.LOGOPanel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.LOGOPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.移动窗口_MouseDown);
            // 
            // 标题栏Panel
            // 
            this.标题栏Panel.Controls.Add(this.最小化Btn);
            this.标题栏Panel.Controls.Add(this.进度条);
            this.标题栏Panel.Controls.Add(this.关闭Btn);
            this.标题栏Panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.标题栏Panel.Font = new System.Drawing.Font("华文隶书", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.标题栏Panel.Location = new System.Drawing.Point(0, 1);
            this.标题栏Panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.标题栏Panel.MinimumSize = new System.Drawing.Size(1, 1);
            this.标题栏Panel.Name = "标题栏Panel";
            this.标题栏Panel.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.标题栏Panel.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            this.标题栏Panel.Size = new System.Drawing.Size(987, 100);
            this.标题栏Panel.Style = Sunny.UI.UIStyle.Custom;
            this.标题栏Panel.TabIndex = 0;
            this.标题栏Panel.Text = null;
            this.标题栏Panel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.标题栏Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.移动窗口_MouseDown);
            // 
            // 最小化Btn
            // 
            this.最小化Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.最小化Btn.Dock = System.Windows.Forms.DockStyle.Right;
            this.最小化Btn.FillColor = System.Drawing.Color.Transparent;
            this.最小化Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.最小化Btn.ForeColor = System.Drawing.Color.Black;
            this.最小化Btn.ForeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.最小化Btn.ForePressColor = System.Drawing.Color.Red;
            this.最小化Btn.ForeSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.最小化Btn.Location = new System.Drawing.Point(787, 0);
            this.最小化Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.最小化Btn.Name = "最小化Btn";
            this.最小化Btn.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.最小化Btn.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            this.最小化Btn.Size = new System.Drawing.Size(100, 100);
            this.最小化Btn.Style = Sunny.UI.UIStyle.Custom;
            this.最小化Btn.StyleCustomMode = true;
            this.最小化Btn.Symbol = 61544;
            this.最小化Btn.SymbolColor = System.Drawing.Color.Black;
            this.最小化Btn.SymbolHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.最小化Btn.SymbolPressColor = System.Drawing.Color.Red;
            this.最小化Btn.SymbolSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.最小化Btn.SymbolSize = 54;
            this.最小化Btn.TabIndex = 2;
            this.最小化Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.最小化Btn.Click += new System.EventHandler(this.最小化Btn_Click);
            // 
            // 进度条
            // 
            this.进度条.DecimalPlaces = 2;
            this.进度条.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.进度条.Location = new System.Drawing.Point(49, 31);
            this.进度条.MinimumSize = new System.Drawing.Size(70, 3);
            this.进度条.Name = "进度条";
            this.进度条.Radius = 15;
            this.进度条.Size = new System.Drawing.Size(713, 41);
            this.进度条.Style = Sunny.UI.UIStyle.Custom;
            this.进度条.TabIndex = 1;
            this.进度条.Text = "uiProcessBar1";
            this.进度条.MouseDown += new System.Windows.Forms.MouseEventHandler(this.移动窗口_MouseDown);
            // 
            // 关闭Btn
            // 
            this.关闭Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.关闭Btn.Dock = System.Windows.Forms.DockStyle.Right;
            this.关闭Btn.FillColor = System.Drawing.Color.Transparent;
            this.关闭Btn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.关闭Btn.ForeColor = System.Drawing.Color.Black;
            this.关闭Btn.ForeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.关闭Btn.ForePressColor = System.Drawing.Color.Red;
            this.关闭Btn.ForeSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.关闭Btn.Location = new System.Drawing.Point(887, 0);
            this.关闭Btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.关闭Btn.Name = "关闭Btn";
            this.关闭Btn.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.关闭Btn.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            this.关闭Btn.Size = new System.Drawing.Size(100, 100);
            this.关闭Btn.Style = Sunny.UI.UIStyle.Custom;
            this.关闭Btn.StyleCustomMode = true;
            this.关闭Btn.Symbol = 61453;
            this.关闭Btn.SymbolColor = System.Drawing.Color.Black;
            this.关闭Btn.SymbolHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.关闭Btn.SymbolPressColor = System.Drawing.Color.Red;
            this.关闭Btn.SymbolSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.关闭Btn.SymbolSize = 54;
            this.关闭Btn.TabIndex = 0;
            this.关闭Btn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.关闭Btn.Click += new System.EventHandler(this.关闭Btn_Click);
            // 
            // 右边半块Panel
            // 
            this.右边半块Panel.Controls.Add(this.窗体显示区TabControl);
            this.右边半块Panel.Controls.Add(this.标题栏Panel);
            this.右边半块Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.右边半块Panel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.右边半块Panel.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.右边半块Panel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.右边半块Panel.Location = new System.Drawing.Point(292, 0);
            this.右边半块Panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.右边半块Panel.MinimumSize = new System.Drawing.Size(1, 1);
            this.右边半块Panel.Name = "右边半块Panel";
            this.右边半块Panel.Padding = new System.Windows.Forms.Padding(0, 1, 1, 1);
            this.右边半块Panel.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.右边半块Panel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.右边半块Panel.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.右边半块Panel.Size = new System.Drawing.Size(988, 720);
            this.右边半块Panel.Style = Sunny.UI.UIStyle.Custom;
            this.右边半块Panel.TabIndex = 4;
            this.右边半块Panel.Text = null;
            this.右边半块Panel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 窗体显示区TabControl
            // 
            this.窗体显示区TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.窗体显示区TabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.窗体显示区TabControl.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.窗体显示区TabControl.Frame = this;
            this.窗体显示区TabControl.ItemSize = new System.Drawing.Size(0, 1);
            this.窗体显示区TabControl.Location = new System.Drawing.Point(0, 101);
            this.窗体显示区TabControl.MainPage = "";
            this.窗体显示区TabControl.Name = "窗体显示区TabControl";
            this.窗体显示区TabControl.SelectedIndex = 0;
            this.窗体显示区TabControl.Size = new System.Drawing.Size(987, 618);
            this.窗体显示区TabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.窗体显示区TabControl.Style = Sunny.UI.UIStyle.Custom;
            this.窗体显示区TabControl.TabIndex = 1;
            this.窗体显示区TabControl.TabVisible = false;
            this.窗体显示区TabControl.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // 通知图标
            // 
            this.通知图标.BalloonTipText = "提示内容";
            this.通知图标.BalloonTipTitle = "翻译姬提示";
            this.通知图标.Visible = true;
            // 
            // 主界面
            // 
            this.AllowShowTitle = false;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.ControlBoxFillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.Controls.Add(this.右边半块Panel);
            this.Controls.Add(this.选择区Panel);
            this.MainTabControl = this.窗体显示区TabControl;
            this.Name = "主界面";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.ShowRadius = false;
            this.ShowTitle = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "主界面";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.Load += new System.EventHandler(this.主界面_Load);
            this.Shown += new System.EventHandler(this.主界面_Shown);
            this.选择区Panel.ResumeLayout(false);
            this.标题栏Panel.ResumeLayout(false);
            this.右边半块Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIPanel 选择区Panel;
        private Sunny.UI.UIPanel LOGOPanel;
        private Sunny.UI.UIPanel 标题栏Panel;
        private Sunny.UI.UIPanel 右边半块Panel;
        private Sunny.UI.UISymbolButton 关闭Btn;
        private Sunny.UI.UITabControl 窗体显示区TabControl;
        private Sunny.UI.UISymbolButton 最小化Btn;
        private Sunny.UI.UIProcessBar 进度条;
        private Sunny.UI.UINavMenu 窗体列表;
        private Sunny.UI.UIStyleManager StyleManager;
        private System.Windows.Forms.NotifyIcon 通知图标;
    }
}