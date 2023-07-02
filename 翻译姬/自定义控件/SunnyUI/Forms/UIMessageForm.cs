﻿/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2023 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.dll can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UIMessageBox.cs
 * 文件说明: 消息提示窗体
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2021-11-09: V3.0.8 多个按钮显示时增加FocusLine
 * 2022-07-13: V3.2.1 消息弹窗文本增加滚动条
******************************************************************************/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    public sealed partial class UIMessageForm : UIForm
    {
        /// <summary>
        /// 消息提示窗体
        /// </summary>
        public UIMessageForm()
        {
            InitializeComponent();

            btnOK.Text = UILocalize.OK;
            btnCancel.Text = UILocalize.Cancel;
        }

        /// <summary>
        /// 是否OK
        /// </summary>
        public bool IsOK
        {
            get; private set;
        }

        private bool _showCancel = true;

        /// <summary>
        /// 显示取消按钮
        /// </summary>
        public bool ShowCancel
        {
            get => _showCancel;
            set
            {
                _showCancel = value;
                btnCancel.Visible = value;
                OnSizeChanged(null);
            }
        }

        /// <summary>
        /// 重载控件尺寸变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (btnOK == null || btnCancel == null)
            {
                return;
            }

            if (_showCancel)
            {
                //btnOK.RectSides = ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right;
                //btnOK.RectSides = btnCancel.RectSides = ToolStripStatusLabelBorderSides.All;
                btnOK.Width = btnCancel.Width = Width / 2 - 2;
                btnCancel.Left = btnOK.Left + btnOK.Width - 1;
                btnCancel.Width = Width - btnCancel.Left - 2;
            }
            else
            {
                //btnOK.RectSides = ToolStripStatusLabelBorderSides.Top;
                btnOK.Width = Width - 4;
            }

            btnCancel.Left = btnOK.Right - 1;
        }

        /// <summary>
        /// 回车事件
        /// </summary>
        protected override void DoEnter()
        {
            base.DoEnter();
            btnOK_Click(null, null);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            IsOK = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            IsOK = false;
            Close();
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);

            if (btnOK != null)
            {
                btnOK.FillColor = BackColor;
                btnOK.RectColor = Color.FromArgb(36, uiColor.ButtonRectColor);
                btnOK.ForeColor = uiColor.LabelForeColor;
            }

            if (btnCancel != null)
            {
                btnCancel.FillColor = BackColor;
                btnCancel.RectColor = Color.FromArgb(36, uiColor.ButtonRectColor);
                btnCancel.ForeColor = uiColor.LabelForeColor;
            }

            if (lbMsg != null)
            {
                lbMsg.ForeColor = uiColor.LabelForeColor;
                lbMsg.BackColor = uiColor.PlainColor;
                lbMsg.FillColor = uiColor.PlainColor;
                lbMsg.SelectionColor = RectColor;
                lbMsg.ScrollBarColor = uiColor.RectColor;
            }
        }

        private void btnOK_MouseEnter(object sender, EventArgs e)
        {
            //((UIButton)sender).RadiusSides = UICornerRadiusSides.All;
        }

        private void btnOK_MouseLeave(object sender, EventArgs e)
        {
            //((UIButton)sender).RadiusSides = UICornerRadiusSides.None;
        }

        /// <summary>
        /// 显示消息提示窗体
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="title">标题</param>
        /// <param name="showCancel">显示取消按钮</param>
        /// <param name="style">主题风格</param>
        public void ShowMessage(string message, string title, bool showCancel, UIStyle style = UIStyle.Blue)
        {
            Style = style;
            Text = title;
            lbMsg.Text = message;
            ShowCancel = showCancel;
            //btnOK.ShowFocusLine = btnCancel.ShowFocusLine = showCancel;
            btnOK.ShowFocusColor = btnCancel.ShowFocusColor = showCancel;
        }
    }
}