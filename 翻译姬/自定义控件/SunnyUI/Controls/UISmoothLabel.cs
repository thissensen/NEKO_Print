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
 * 文件名称: UISmoothLabel.cs
 * 文件说明: 平滑文字带边框的标签
 * 当前版本: V3.1
 * 创建日期: 2022-01-22
 *
 * 2022-01-22: V3.1.0 增加文件说明
 * 2022-03-19: V3.1.1 重构主题配色
******************************************************************************/


using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    public sealed class UISmoothLabel : Label, IStyleInterface, IZoomScale
    {
        public UISmoothLabel()
        {
            base.Font = UIFontColor.Font(36);
            Version = UIGlobal.Version;

            foreColor = UIStyles.Blue.SmoothLabelForeColor;
            rectColor = UIStyles.Blue.SmoothLabelRectColor;

            drawPath = new GraphicsPath();
            drawPen = new Pen(new SolidBrush(rectColor), rectSize);
            forecolorBrush = new SolidBrush(ForeColor);
            Size = new Size(300, 60);
        }

        /// <summary>
        /// 禁止控件跟随窗体缩放
        /// </summary>
        [DefaultValue(false), Category("SunnyUI"), Description("禁止控件跟随窗体缩放")]
        public bool ZoomScaleDisabled { get; set; }

        /// <summary>
        /// 控件缩放前在其容器里的位置
        /// </summary>
        [Browsable(false), DefaultValue(typeof(Rectangle), "0, 0, 0, 0")]
        public Rectangle ZoomScaleRect { get; set; }

        /// <summary>
        /// 设置控件缩放比例
        /// </summary>
        /// <param name="scale">缩放比例</param>
        public void SetZoomScale(float scale)
        {

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                forecolorBrush?.Dispose();
                drawPath?.Dispose();
                drawPen?.Dispose();
            }

            base.Dispose(disposing);
        }

        private PointF point;
        private SizeF drawSize;
        private Pen drawPen;
        private GraphicsPath drawPath;
        private SolidBrush forecolorBrush;

        [Browsable(false), DefaultValue(false)]
        public bool IsScaled { get; set; }

        public void SetDPIScale()
        {
            if (!IsScaled)
            {
                this.SetDPIScaleFont();
                IsScaled = true;
            }
        }

        private Color foreColor;

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString { get; set; }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("字体颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public override Color ForeColor
        {
            get => foreColor;
            set
            {
                foreColor = value;
                forecolorBrush.Color = foreColor;
                SetStyleCustom();
            }
        }

        private void SetStyleCustom(bool needRefresh = true)
        {
            _style = UIStyle.Custom;
            if (needRefresh) Invalidate();
        }

        public string Version { get; }

        public void SetStyle(UIStyle style)
        {
            if (!style.IsCustom())
            {
                SetStyleColor(style.Colors());
                Invalidate();
            }

            _style = style;
        }

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode { get; set; }

        public void SetStyleColor(UIBaseStyle uiColor)
        {
            foreColor = uiColor.SmoothLabelForeColor;
            rectColor = uiColor.SmoothLabelRectColor;

            forecolorBrush.Color = foreColor;
            if (rectSize != 0)
                drawPen.Color = rectColor;
        }

        private UIStyle _style = UIStyle.Blue;

        /// <summary>
        /// 主题样式
        /// </summary>
        [DefaultValue(UIStyle.Blue), Description("主题样式"), Category("SunnyUI")]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
        }

        /// <summary>
        /// 重载字体变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            Invalidate();
        }

        protected override void OnTextAlignChanged(EventArgs e)
        {
            base.OnTextAlignChanged(e);
            Invalidate();
        }

        private Color rectColor;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color RectColor
        {
            get
            {
                return rectColor;
            }
            set
            {
                if (rectColor != value)
                {
                    rectColor = value;
                    if (rectSize != 0) drawPen.Color = rectColor;
                    RectColorChanged?.Invoke(this, null);
                    SetStyleCustom();
                }
            }
        }

        public event EventHandler RectColorChanged;

        private int rectSize = 2;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框宽度"), Category("SunnyUI")]
        [DefaultValue(2)]
        public int RectSize
        {
            get => rectSize;
            set
            {
                value = Math.Max(0, value);
                if (rectSize != value)
                {
                    rectSize = value;

                    if (value == 0)
                    {
                        drawPen.Color = Color.Transparent;
                    }
                    else
                    {
                        drawPen.Color = RectColor;
                        drawPen.Width = value;
                    }

                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Text.IsNullOrEmpty()) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

            drawSize = e.Graphics.MeasureString(Text, Font, new PointF(), StringFormat.GenericTypographic);

            if (AutoSize)
            {
                point.X = Padding.Left;
                point.Y = Padding.Top;
            }
            else
            {
                if (TextAlign == ContentAlignment.TopLeft ||
                    TextAlign == ContentAlignment.MiddleLeft ||
                    TextAlign == ContentAlignment.BottomLeft)
                {
                    point.X = Padding.Left;
                }
                else if (TextAlign == ContentAlignment.TopCenter ||
                         TextAlign == ContentAlignment.MiddleCenter ||
                         TextAlign == ContentAlignment.BottomCenter)
                {
                    point.X = (Width - drawSize.Width) / 2;
                }
                else
                {
                    point.X = Width - (Padding.Right + drawSize.Width);
                }

                if (TextAlign == ContentAlignment.TopLeft ||
                    TextAlign == ContentAlignment.TopCenter ||
                    TextAlign == ContentAlignment.TopRight)
                {
                    point.Y = Padding.Top;
                }
                else if (TextAlign == ContentAlignment.MiddleLeft ||
                         TextAlign == ContentAlignment.MiddleCenter ||
                         TextAlign == ContentAlignment.MiddleRight)
                {
                    point.Y = (Height - drawSize.Height) / 2;
                }
                else
                {
                    point.Y = Height - (Padding.Bottom + drawSize.Height);
                }
            }

            float fontSize = e.Graphics.DpiY * Font.SizeInPoints / 72;

            drawPath.Reset();
            drawPath.AddString(Text, Font.FontFamily, (int)Font.Style, fontSize,
                point, StringFormat.GenericTypographic);

            e.Graphics.FillPath(forecolorBrush, drawPath);
            e.Graphics.DrawPath(drawPen, drawPath);
        }
    }
}
