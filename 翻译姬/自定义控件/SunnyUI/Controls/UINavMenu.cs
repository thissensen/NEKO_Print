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
* 文件名称: UINavMenu.cs
* 文件说明: 导航菜单
* 当前版本: V3.1
* 创建日期: 2020-01-01
*
* 2020-01-01: V2.2.0 增加文件说明
* 2020-07-01: V2.2.6 解决引发事件所有结点重绘导致闪烁；解决滚轮失效问题。
* 2020-03-12: V3.0.2 增加设置二级菜单底色
* 2021-06-14: V3.0.4 增加右侧图标
* 2021-08-07: V3.0.5 显示/隐藏子节点提示箭头
* 2021-08-27: V3.0.6 增加自定义TipsText显示的颜色 
* 2021-12-13: V3.0.9 选中项可设置背景色渐变
* 2022-01-02: V3.0.9 滚动条可设置颜色
* 2022-03-19: V3.1.1 重构主题配色
* 2022-03-24: V3.1.1 修复TipsText显示位置
* 2022-04-14: V3.1.3 重构扩展函数
* 2022-06-23: V3.2.0 绘制节点字体图标增加偏移SymbolOffset
* 2022-08-19: V3.2.3 修复选中节点右侧图标前景色
* 2022-11-03: V3.2.6 增加了可设置垂直滚动条宽度的属性
* 2022-11-03: V3.2.6 重写了节点右侧图标的绘制
* 2023-02-02: V3.3.1 修复了鼠标离开事件
* 2023-02-10: V3.3.2 有子节点时，鼠标左键点击父级点展开/收缩，右键选中
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("MenuItemClick")]
    [DefaultProperty("Nodes")]
    public sealed class UINavMenu : TreeView, IStyleInterface, IZoomScale
    {
        public delegate void OnMenuItemClick(TreeNode node, NavMenuItem item, int pageIndex);

        public event OnMenuItemClick MenuItemClick;

        private readonly UIScrollBar Bar = new UIScrollBar();

        public UINavMenu()
        {
            SetStyle(ControlStyles.DoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            BorderStyle = BorderStyle.None;
            //HideSelection = false;
            DrawMode = TreeViewDrawMode.OwnerDrawAll;
            FullRowSelect = true;
            ShowLines = false;
            //ShowPlusMinus = false;
            //ShowRootLines = false;

            DoubleBuffered = true;
            Font = UIFontColor.Font();
            //CheckBoxes = false;
            ItemHeight = 50;
            BackColor = Color.FromArgb(56, 56, 56);

            Bar.ValueChanged += Bar_ValueChanged;
            Bar.Dock = DockStyle.Right;
            Bar.Visible = false;
            Bar.Style = UIStyle.Custom;
            Bar.StyleCustomMode = true;
            Bar.FillColor = fillColor;

            Bar.ForeColor = Color.Silver;
            Bar.HoverColor = Color.Silver;
            Bar.PressColor = Color.Silver;
            Bar.ZoomScaleDisabled = true;

            Controls.Add(Bar);
            Version = UIGlobal.Version;
            SetScrollInfo();

            selectedForeColor = UIStyles.Blue.NavMenuMenuSelectedColor;
            selectedHighColor = UIStyles.Blue.NavMenuMenuSelectedColor;
        }

        private int scrollBarWidth = 0;

        [DefaultValue(0), Category("SunnyUI"), Description("垂直滚动条宽度，最小为原生滚动条宽度")]
        public int ScrollBarWidth
        {
            get => scrollBarWidth;
            set
            {
                scrollBarWidth = value;
                SetScrollInfo();
            }
        }

        private int scrollBarHandleWidth = 6;

        [DefaultValue(6), Category("SunnyUI"), Description("垂直滚动条滑块宽度，最小为原生滚动条宽度")]
        public int ScrollBarHandleWidth
        {
            get => scrollBarHandleWidth;
            set
            {
                scrollBarHandleWidth = value;
                if (Bar != null) Bar.FillWidth = value;
            }
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

        [Description("滚动条填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "56, 56, 56")]
        public Color ScrollFillColor
        {
            get => Bar.FillColor;
            set
            {
                menuStyle = UIMenuStyle.Custom;
                Bar.FillColor = value;
                Invalidate();
            }
        }

        [Description("滚动条填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "Silver")]
        public Color ScrollBarColor
        {
            get => Bar.ForeColor;
            set
            {
                menuStyle = UIMenuStyle.Custom;
                Bar.ForeColor = value;
                Invalidate();
            }
        }

        [Description("滚动条移上填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "Silver")]
        public Color ScrollBarHoverColor
        {
            get => Bar.HoverColor;
            set
            {
                menuStyle = UIMenuStyle.Custom;
                Bar.HoverColor = value;
                Invalidate();
            }
        }

        [Description("滚动条按下填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "Silver")]
        public Color ScrollBarPressColor
        {
            get => Bar.PressColor;
            set
            {
                menuStyle = UIMenuStyle.Custom;
                Bar.PressColor = value;
                Invalidate();
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            tmpFont?.Dispose();
        }

        [Browsable(false)]
        public bool IsScaled { get; private set; }

        public void SetDPIScale()
        {
            if (!IsScaled)
            {
                this.SetDPIScaleFont();
                IsScaled = true;
            }
        }

        [DefaultValue(false)]
        [Description("只显示一个打开的节点"), Category("SunnyUI")]
        public bool ShowOneNode { get; set; }

        private bool showItemsArrow = true;

        [DefaultValue(true)]
        [Description("显示子节点提示箭头"), Category("SunnyUI")]
        public bool ShowItemsArrow
        {
            get => showItemsArrow;
            set
            {
                showItemsArrow = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString { get; set; }

        public void ClearAll()
        {
            Nodes.Clear();
            MenuHelper.Clear();
        }

        private Color backColor = Color.FromArgb(56, 56, 56);

        [DefaultValue(typeof(Color), "56, 56, 56")]
        [Description("背景颜色"), Category("SunnyUI")]
        public override Color BackColor
        {
            get => backColor;
            set
            {
                if (backColor != value)
                {
                    backColor = value;
                    menuStyle = UIMenuStyle.Custom;
                    Invalidate();
                }
            }
        }

        private Color fillColor = Color.FromArgb(56, 56, 56);

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "56, 56, 56")]
        public Color FillColor
        {
            get => fillColor;
            set
            {
                if (fillColor != value)
                {
                    fillColor = value;
                    menuStyle = UIMenuStyle.Custom;
                    Invalidate();
                }
            }
        }

        private Color foreColor = Color.Silver;

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("字体颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "Silver")]
        public override Color ForeColor
        {
            get => foreColor;
            set
            {
                if (foreColor != value)
                {
                    foreColor = value;
                    menuStyle = UIMenuStyle.Custom;
                    Invalidate();
                }
            }
        }

        private void Bar_ValueChanged(object sender, EventArgs e)
        {
            ScrollBarInfo.SetScrollValue(Handle, Bar.Value);
        }

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode { get; set; }

        [DefaultValue(null)]
        [Description("关联的TabControl"), Category("SunnyUI")]
        public UITabControl TabControl { get; set; }

        private Color selectedColor = Color.FromArgb(36, 36, 36);

        private bool showTips;

        [Description("是否显示角标"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool ShowTips
        {
            get => showTips;
            set
            {
                if (showTips != value)
                {
                    showTips = value;
                    Invalidate();
                }
            }
        }

        private Font tipsFont = UIFontColor.SubFont();

        [Description("角标文字字体"), Category("SunnyUI")]
        [DefaultValue(typeof(Font), "微软雅黑, 9pt")]
        public Font TipsFont
        {
            get => tipsFont;
            set
            {
                if (!tipsFont.Equals(value))
                {
                    tipsFont = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "36, 36, 36")]
        [Description("选中节点颜色"), Category("SunnyUI")]
        public Color SelectedColor
        {
            get => selectedColor;
            set
            {
                if (selectedColor != value)
                {
                    selectedColor = value;
                    menuStyle = UIMenuStyle.Custom;
                    Invalidate();
                }
            }
        }

        private bool fillColorGradient;

        [Description("填充颜色渐变"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool SelectedColorGradient
        {
            get => fillColorGradient;
            set
            {
                if (fillColorGradient != value)
                {
                    fillColorGradient = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 设置填充颜色
        /// </summary>
        /// <param name="value">颜色</param>
        private void SetFillColor2(Color value)
        {
            if (selectedColor2 != value)
            {
                selectedColor2 = value;
                menuStyle = UIMenuStyle.Custom;
                Invalidate();
            }
        }

        /// <summary>
        /// 填充颜色
        /// </summary>
        private Color selectedColor2 = Color.FromArgb(36, 36, 36);

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "36, 36, 36")]
        public Color SelectedColor2
        {
            get => selectedColor2;
            set => SetFillColor2(value);
        }

        private Color selectedHighColor = UIColor.Blue;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("选中节点高亮颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color SelectedHighColor

        {
            get => selectedHighColor;
            set
            {
                selectedHighColor = value;
                SetStyleCustom();
            }
        }

        private Color hoverColor = Color.FromArgb(76, 76, 76);

        [DefaultValue(typeof(Color), "76, 76, 76")]
        [Description("鼠标移上颜色"), Category("SunnyUI")]
        public Color HoverColor
        {
            get => hoverColor;
            set
            {
                if (hoverColor != value)
                {
                    hoverColor = value;
                    menuStyle = UIMenuStyle.Custom;
                }
            }
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

        public void SetStyle(UIStyle style)
        {
            if (!style.IsCustom())
            {
                SetStyleColor(style.Colors());
                Invalidate();
            }

            _style = style;
        }

        public void SetStyleColor(UIBaseStyle uiColor)
        {
            selectedForeColor = uiColor.NavMenuMenuSelectedColor;
            selectedHighColor = uiColor.NavMenuMenuSelectedColor;
        }

        private UIMenuStyle menuStyle = UIMenuStyle.Black;

        [DefaultValue(UIMenuStyle.Black)]
        [Description("导航菜单主题风格"), Category("SunnyUI")]
        public UIMenuStyle MenuStyle
        {
            get => menuStyle;
            set
            {
                if (value != UIMenuStyle.Custom)
                {
                    SetMenuStyle(UIStyles.MenuColors[value]);
                }

                menuStyle = value;
            }
        }

        private void SetMenuStyle(UIMenuColor uiColor)
        {
            BackColor = uiColor.BackColor;
            fillColor = uiColor.BackColor;
            selectedColor = uiColor.SelectedColor;
            selectedColor2 = uiColor.SelectedColor2;
            foreColor = uiColor.UnSelectedForeColor;
            hoverColor = uiColor.HoverColor;
            secondBackColor = uiColor.SecondBackColor;

            if (Bar != null)
            {
                Bar.FillColor = uiColor.BackColor;
                Bar.ForeColor = uiColor.UnSelectedForeColor;
                Bar.HoverColor = uiColor.UnSelectedForeColor;
                Bar.PressColor = uiColor.UnSelectedForeColor;
            }

            Invalidate();
        }

        private void SetStyleCustom(bool needRefresh = true)
        {
            _style = UIStyle.Custom;
            if (needRefresh) Invalidate();
        }

        private Color selectedForeColor = UIColor.Blue;

        [DefaultValue(typeof(Color), "80, 160, 255")]
        [Description("选中节点字体颜色"), Category("SunnyUI")]
        public Color SelectedForeColor
        {
            get => selectedForeColor;
            set
            {
                if (selectedForeColor != value)
                {
                    selectedForeColor = value;
                    SetStyleCustom();
                }
            }
        }

        private bool ScrollBarVisible;

        private TreeNode CurrentNode;

        /// <summary>
        /// 重载鼠标移动事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            TreeNode node = GetNodeAt(e.Location);
            if (node == null || CurrentNode == node)
            {
                return;
            }

            Graphics g = CreateGraphics();
            if (CurrentNode != null && CurrentNode != SelectedNode)
            {
                ClearCurrentNode(g);
            }

            if (node != SelectedNode)
            {
                CurrentNode = node;
                OnDrawNode(new DrawTreeNodeEventArgs(g, CurrentNode, new Rectangle(0, CurrentNode.Bounds.Y, Width, CurrentNode.Bounds.Height), TreeNodeStates.Hot));
            }

            g.Dispose();
        }

        private void ClearCurrentNode(Graphics g)
        {
            if (CurrentNode != null && CurrentNode != SelectedNode)
            {
                OnDrawNode(new DrawTreeNodeEventArgs(g, CurrentNode, new Rectangle(0, CurrentNode.Bounds.Y, Width, CurrentNode.Bounds.Height), TreeNodeStates.Default));
                CurrentNode = null;
            }
        }

        /// <summary>
        /// 重载鼠标离开事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            Graphics g = CreateGraphics();
            ClearCurrentNode(g);
            g.Dispose();
            base.OnMouseLeave(e);
        }

        private bool checkBoxes;

        [Browsable(false)]
        public new bool CheckBoxes
        {
            get => checkBoxes;
            set => checkBoxes = false;
        }

        private bool showSecondBackColor;

        [DefaultValue(false)]
        [Description("显示二级节点背景颜色"), Category("SunnyUI")]
        public bool ShowSecondBackColor
        {
            get => showSecondBackColor;
            set
            {
                if (ShowSecondBackColor != value)
                {
                    showSecondBackColor = value;
                    Invalidate();
                }
            }
        }

        private Color secondBackColor = Color.FromArgb(66, 66, 66);

        [DefaultValue(typeof(Color), "66, 66, 66")]
        [Description("二级节点背景颜色"), Category("SunnyUI")]
        public Color SecondBackColor
        {
            get => secondBackColor;
            set
            {
                if (secondBackColor != value)
                {
                    secondBackColor = value;
                    Invalidate();
                }
            }
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            if (BorderStyle != BorderStyle.None)
            {
                BorderStyle = BorderStyle.None;
            }

            SetScrollInfo();
            CheckBoxes = false;

            if (e.Node == null || (e.Node.Bounds.Width <= 0 && e.Node.Bounds.Height <= 0 && e.Node.Bounds.X <= 0 && e.Node.Bounds.Y <= 0))
            {
                e.DrawDefault = true;
            }
            else
            {
                int drawLeft = e.Node.Level * 16 + 16 + 4;
                int imageLeft = drawLeft;
                bool haveImage = false;

                if (MenuHelper.GetSymbol(e.Node) > 0)
                {
                    haveImage = true;
                    drawLeft += MenuHelper.GetSymbolSize(e.Node) + 6;
                }
                else
                {
                    if (ImageList != null && ImageList.Images.Count > 0 && e.Node.ImageIndex >= 0 && e.Node.ImageIndex < ImageList.Images.Count)
                    {
                        haveImage = true;
                        drawLeft += ImageList.ImageSize.Width + 6;
                    }
                }

                SizeF sf = e.Graphics.MeasureString(e.Node.Text, Font);
                if (e.Node == SelectedNode)
                {
                    if (SelectedColorGradient)
                    {
                        LinearGradientBrush br = new LinearGradientBrush(new Point(0, 0), new Point(0, e.Node.Bounds.Height), SelectedColor, SelectedColor2);
                        br.GammaCorrection = true;
                        e.Graphics.FillRectangle(br, new Rectangle(new Point(0, e.Node.Bounds.Y), new Size(Width, e.Node.Bounds.Height)));
                        br.Dispose();
                    }
                    else
                    {
                        e.Graphics.FillRectangle(SelectedColor, new Rectangle(new Point(0, e.Node.Bounds.Y), new Size(Width, e.Node.Bounds.Height)));
                    }

                    e.Graphics.DrawString(e.Node.Text, Font, SelectedForeColor, drawLeft, e.Bounds.Y + (ItemHeight - sf.Height) / 2.0f);
                    e.Graphics.FillRectangle(SelectedHighColor, new Rectangle(0, e.Bounds.Y, 4, e.Bounds.Height));
                }
                else if (e.Node == CurrentNode && (e.State & TreeNodeStates.Hot) != 0)
                {
                    e.Graphics.FillRectangle(HoverColor, new Rectangle(new Point(0, e.Node.Bounds.Y), new Size(Width, e.Node.Bounds.Height)));
                    e.Graphics.DrawString(e.Node.Text, Font, ForeColor, drawLeft, e.Bounds.Y + (ItemHeight - sf.Height) / 2.0f);
                }
                else
                {
                    Color color = fillColor;
                    if (showSecondBackColor && e.Node.Level > 0)
                    {
                        color = SecondBackColor;
                    }

                    e.Graphics.FillRectangle(color, new Rectangle(new Point(0, e.Node.Bounds.Y), new Size(Width, e.Node.Bounds.Height)));
                    e.Graphics.DrawString(e.Node.Text, Font, ForeColor, drawLeft, e.Bounds.Y + (ItemHeight - sf.Height) / 2.0f);
                }

                //画右侧图标
                Color rightSymbolColor = ForeColor;
                if (e.Node == SelectedNode) rightSymbolColor = SelectedForeColor;
                if (TreeNodeSymbols.ContainsKey(e.Node) && TreeNodeSymbols[e.Node].Count > 0)
                {
                    int size = e.Node.Nodes.Count > 0 ? 24 : 0;
                    int left = Width - size - 6;
                    if (Bar.Visible) left -= Bar.Width;

                    int firstLeft = left - TreeNodeSymbols[e.Node].Count * 30;
                    for (int i = 0; i < TreeNodeSymbols[e.Node].Count; i++)
                    {
                        e.Graphics.DrawFontImage(TreeNodeSymbols[e.Node][i], 24, rightSymbolColor, new Rectangle(firstLeft + i * 30, e.Bounds.Top, 30, e.Bounds.Height));
                    }
                }

                //画图片
                if (haveImage)
                {
                    if (MenuHelper.GetSymbol(e.Node) > 0)
                    {
                        SizeF fiSize = e.Graphics.GetFontImageSize(MenuHelper.GetSymbol(e.Node), MenuHelper.GetSymbolSize(e.Node));
                        Color color = e.Node == SelectedNode ? SelectedForeColor : ForeColor;
                        Point offset = MenuHelper.GetSymbolOffset(e.Node);
                        e.Graphics.DrawFontImage(MenuHelper.GetSymbol(e.Node), MenuHelper.GetSymbolSize(e.Node), color, imageLeft + (MenuHelper.GetSymbolSize(e.Node) - fiSize.Width) / 2.0f + offset.X, e.Bounds.Y + (e.Bounds.Height - fiSize.Height) / 2 + offset.Y);
                    }
                    else
                    {
                        if (e.Node == SelectedNode && e.Node.SelectedImageIndex >= 0 && e.Node.SelectedImageIndex < ImageList.Images.Count)
                            e.Graphics.DrawImage(ImageList.Images[e.Node.SelectedImageIndex], imageLeft, e.Bounds.Y + (e.Bounds.Height - ImageList.ImageSize.Height) / 2);
                        else
                            e.Graphics.DrawImage(ImageList.Images[e.Node.ImageIndex], imageLeft, e.Bounds.Y + (e.Bounds.Height - ImageList.ImageSize.Height) / 2);
                    }
                }

                //显示右侧下拉箭头
                if (ShowItemsArrow && e.Node.Nodes.Count > 0)
                {
                    int size = 24;
                    int left = Width - size - 6;
                    if (Bar.Visible) left -= Bar.Width;
                    e.Graphics.DrawFontImage(e.Node.IsExpanded ? 61702 : 61703, 24, ForeColor, left, e.Bounds.Y + (ItemHeight - 24) / 2);
                }

                //显示Tips圆圈
                if (ShowTips && MenuHelper.GetTipsText(e.Node).IsValid())
                {
                    SizeF tipsSize = e.Graphics.MeasureString(MenuHelper.GetTipsText(e.Node), TempFont);
                    float sfMax = Math.Max(tipsSize.Width, tipsSize.Height) + 1;
                    float tipsLeft = Width - sfMax - 16;
                    if (e.Node.Nodes.Count > 0) tipsLeft -= 24;
                    if (Bar.Visible) tipsLeft -= Bar.Width;
                    if (TreeNodeSymbols.ContainsKey(e.Node)) tipsLeft -= TreeNodeSymbols[e.Node].Count * 30;

                    float tipsTop = e.Bounds.Y + (ItemHeight - sfMax) / 2;

                    if (MenuHelper[e.Node] != null)
                    {
                        using StringFormat alignment = GDI.SetAlignment();
                        if (MenuHelper[e.Node].TipsCustom)
                        {
                            e.Graphics.FillEllipse(MenuHelper[e.Node].TipsBackColor, tipsLeft, tipsTop, sfMax, sfMax);
                            e.Graphics.DrawString(MenuHelper.GetTipsText(e.Node), TempFont, MenuHelper[e.Node].TipsForeColor, new RectangleF(tipsLeft, tipsTop, sfMax, sfMax), alignment);
                        }
                        else
                        {
                            e.Graphics.FillEllipse(TipsColor, tipsLeft, tipsTop, sfMax, sfMax);
                            e.Graphics.DrawString(MenuHelper.GetTipsText(e.Node), TempFont, TipsForeColor, new RectangleF(tipsLeft, tipsTop, sfMax, sfMax), alignment);
                        }
                    }
                }
            }

            base.OnDrawNode(e);
        }

        Font tmpFont;

        private Font TempFont
        {
            get
            {
                if (tmpFont == null || !tmpFont.Size.EqualsFloat(TipsFont.DPIScaleFontSize()))
                {
                    tmpFont?.Dispose();
                    tmpFont = TipsFont.DPIScaleFont();
                }

                return tmpFont;
            }
        }

        private Color tipsColor = Color.Red;

        [DefaultValue(typeof(Color), "Red"), Category("SunnyUI"), Description("节点提示圆点背景颜色")]
        public Color TipsColor
        {
            get => tipsColor;
            set
            {
                tipsColor = value;
                Invalidate();
            }
        }

        private Color tipsForeColor = Color.White;

        [DefaultValue(typeof(Color), "White"), Category("SunnyUI"), Description("节点提示圆点文字颜色")]
        public Color TipsForeColor
        {
            get => tipsForeColor;
            set
            {
                tipsForeColor = value;
                Invalidate();
            }
        }

        [Description("展开节点后选中第一个子节点"), Category("SunnyUI"), DefaultValue(true)]
        public bool ExpandSelectFirst { get; set; } = true;

        public string Version { get; }

        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);
            if (e.Node == null) return;

            int size = e.Node.Nodes.Count > 0 ? 24 : 0;
            int left = Width - size - 6;
            if (Bar.Visible) left -= Bar.Width;

            int firstLeft = 0;
            if (TreeNodeSymbols.ContainsKey(e.Node))
                firstLeft = left - TreeNodeSymbols[e.Node].Count * 30;

            if (TreeNodeSymbols.ContainsKey(e.Node) && TreeNodeSymbols[e.Node].Count > 0 && e.X >= firstLeft && e.X < firstLeft + TreeNodeSymbols[e.Node].Count * 30)
            {
                int idx = (e.X - firstLeft) / 30;
                if (idx >= 0 && idx < TreeNodeSymbols[e.Node].Count)
                {
                    NodeRightSymbolClick?.Invoke(this, e.Node, idx, TreeNodeSymbols[e.Node][idx]);
                }
            }
            else
            {
                if (e.Node.Nodes.Count > 0)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (e.Node.IsExpanded)
                        {
                            e.Node.Collapse();
                        }
                        else
                        {
                            e.Node.Expand();
                        }

                        if (SelectedNode != null && SelectedNode == e.Node && e.Node.IsExpanded && ExpandSelectFirst && e.Node.Nodes.Count > 0)
                        {
                            SelectedNode = e.Node.Nodes[0];
                        }
                        else
                        {
                            SelectedNode = e.Node;
                        }
                    }

                    if (e.Button == MouseButtons.Right)
                    {
                        SelectedNode = e.Node;
                    }
                }
                else
                {
                    SelectedNode = e.Node;
                }

                ShowSelectedNode();
            }
        }

        public void SelectFirst()
        {
            if (Nodes.Count > 0)
            {
                if (Nodes[0].Nodes.Count > 0 && ExpandSelectFirst)
                {
                    Nodes[0].Expand();
                    SelectedNode = Nodes[0].Nodes[0];
                }
                else
                {
                    SelectedNode = Nodes[0];
                }

                Nodes[0].EnsureVisible();
            }

            ShowSelectedNode();
        }

        private void ShowSelectedNode()
        {
            NavMenuItem item = MenuHelper[SelectedNode];
            if (item != null)
            {
                if (item.PageGuid != Guid.Empty)
                {
                    TabControl?.SelectPage(item.PageGuid);
                }
                else
                {
                    if (item.PageIndex >= 0)
                    {
                        TabControl?.SelectPage(item.PageIndex);
                    }
                }
            }

            MenuItemClick?.Invoke(SelectedNode, MenuHelper[SelectedNode], GetPageIndex(SelectedNode));
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (e.Delta > 10)
            {
                ScrollBarInfo.ScrollUp(Handle);
            }
            else if (e.Delta < -10)
            {
                ScrollBarInfo.ScrollDown(Handle);
            }

            SetScrollInfo();
        }

        public void SetScrollInfo()
        {
            if (Nodes.Count == 0)
            {
                Bar.Visible = false;
                return;
            }

            int barWidth = Math.Max(ScrollBarInfo.VerticalScrollBarWidth(), ScrollBarWidth);
            var si = ScrollBarInfo.GetInfo(Handle);
            Bar.Maximum = si.ScrollMax;
            Bar.Visible = si.ScrollMax > 0 && si.nMax > 0 && si.nPage > 0;
            Bar.Value = si.nPos;
            Bar.Width = barWidth;
            Bar.BringToFront();

            if (ScrollBarVisible != Bar.Visible)
            {
                ScrollBarVisible = Bar.Visible;
                Invalidate();
            }
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);

            if (ShowOneNode)
            {
                TreeNode node = e.Node.PrevNode;
                while (node != null)
                {
                    if (node.IsExpanded)
                    {
                        node.Collapse();
                    }
                    node = node.PrevNode;
                }

                node = e.Node.NextNode;
                while (node != null)
                {
                    if (node.IsExpanded)
                    {
                        node.Collapse();
                    }
                    node = node.NextNode;
                }
            }

            if (e.Node != null && ExpandSelectFirst && e.Node.Nodes.Count > 0)
            {
                e.Node.Expand();
                SelectedNode = e.Node.Nodes[0];
            }
            else
            {
                SelectedNode = e.Node;
            }
        }

        protected override void OnAfterExpand(TreeViewEventArgs e)
        {
            base.OnAfterExpand(e);
            SetScrollInfo();
        }

        protected override void OnAfterCollapse(TreeViewEventArgs e)
        {
            base.OnAfterCollapse(e);
            SetScrollInfo();
        }

        protected override void WndProc(ref Message m)
        {
            if (IsDisposed || Disposing) return;
            if (m.Msg == Win32.User.WM_ERASEBKGND)
            {
                m.Result = IntPtr.Zero;
                return;
            }

            base.WndProc(ref m);
            Win32.User.ShowScrollBar(Handle, 3, false);
        }

        #region 扩展函数
        private readonly NavMenuHelper MenuHelper = new NavMenuHelper();

        public void SelectPage(int pageIndex)
        {
            var node = MenuHelper.GetTreeNode(pageIndex);
            if (node != null)
            {
                SelectedNode = node;
                ShowSelectedNode();
            }
        }

        public void SelectPage(Guid pageGuid)
        {
            var node = MenuHelper.GetTreeNode(pageGuid);
            if (node != null)
            {
                SelectedNode = node;
                ShowSelectedNode();
            }
        }

        public int GetPageIndex(TreeNode node)
        {
            return MenuHelper.GetPageIndex(node);
        }

        public TreeNode GetTreeNode(int pageIndex)
        {
            return MenuHelper.GetTreeNode(pageIndex);
        }

        private void SetNodeItem(TreeNode node, NavMenuItem item)
        {
            MenuHelper.Add(node, item);
        }

        public UINavMenu SetNodePageIndex(TreeNode node, int pageIndex)
        {
            MenuHelper.SetPageIndex(node, pageIndex);
            return this;
        }

        public UINavMenu SetNodeSymbol(TreeNode node, int symbol, int symbolSize = 24)
        {
            MenuHelper.SetSymbol(node, symbol, symbolSize);
            return this;
        }

        public UINavMenu SetNodeSymbol(TreeNode node, int symbol, Point symbolOffset, int symbolSize = 24)
        {
            MenuHelper.SetSymbol(node, symbol, symbolOffset, symbolSize);
            return this;
        }

        public UINavMenu SetNodeImageIndex(TreeNode node, int imageIndex)
        {
            node.ImageIndex = imageIndex;
            return this;
        }

        public void SetNodeTipsText(TreeNode node, string nodeTipsText)
        {
            MenuHelper.SetTipsText(node, nodeTipsText);
        }

        public void SetNodeTipsText(TreeNode node, string nodeTipsText, Color nodeTipsBackColor, Color nodeTipsForeColor)
        {
            MenuHelper.SetTipsText(node, nodeTipsText, nodeTipsBackColor, nodeTipsForeColor);
        }

        public TreeNode CreateNode(string text, int pageIndex)
        {
            return CreateNode(new NavMenuItem(text, pageIndex));
        }

        public TreeNode CreateNode(string text, Guid pageGuid)
        {
            return CreateNode(new NavMenuItem(text, pageGuid));
        }

        public TreeNode CreateNode(UIPage page)
        {
            return CreateNode(new NavMenuItem(page));
        }

        public TreeNode CreateNode(string text, int symbol, int symbolSize, int pageIndex)
        {
            var node = CreateNode(text, pageIndex);
            SetNodeSymbol(node, symbol, symbolSize);
            return node;
        }

        public TreeNode CreateNode(string text, int symbol, Point symbolOffset, int symbolSize, int pageIndex)
        {
            var node = CreateNode(text, pageIndex);
            SetNodeSymbol(node, symbol, symbolOffset, symbolSize);
            return node;
        }

        private TreeNode CreateNode(NavMenuItem item)
        {
            TreeNode node = new TreeNode(item.Text);
            Nodes.Add(node);
            SetNodeItem(node, item);
            return node;
        }

        public TreeNode CreateChildNode(TreeNode parent, string text, int pageIndex)
        {
            return CreateChildNode(parent, new NavMenuItem(text, pageIndex));
        }

        public TreeNode CreateChildNode(TreeNode parent, string text, Guid pageGuid)
        {
            return CreateChildNode(parent, new NavMenuItem(text, pageGuid));
        }

        public TreeNode CreateChildNode(TreeNode parent, UIPage page)
        {
            var childNode = CreateChildNode(parent, new NavMenuItem(page));
            if (page.Symbol > 0)
            {
                MenuHelper.SetSymbol(childNode, page.Symbol, page.SymbolOffset, page.SymbolSize);
            }

            return childNode;
        }

        public TreeNode CreateChildNode(TreeNode parent, string text, int symbol, int symbolSize, int pageIndex)
        {
            var node = CreateChildNode(parent, text, pageIndex);
            SetNodeSymbol(node, symbol, symbolSize);
            return node;
        }

        public TreeNode CreateChildNode(TreeNode parent, string text, int symbol, Point symbolOffset, int symbolSize, int pageIndex)
        {
            var node = CreateChildNode(parent, text, pageIndex);
            SetNodeSymbol(node, symbol, symbolOffset, symbolSize);
            return node;
        }

        private TreeNode CreateChildNode(TreeNode parent, NavMenuItem item)
        {
            TreeNode childNode = new TreeNode(item.Text);
            parent.Nodes.Add(childNode);
            SetNodeItem(childNode, item);
            return childNode;
        }

        private readonly Dictionary<TreeNode, List<int>> TreeNodeSymbols = new Dictionary<TreeNode, List<int>>();

        public void AddNodeRightSymbol(TreeNode node, int symbol)
        {
            if (!TreeNodeSymbols.ContainsKey(node))
                TreeNodeSymbols.Add(node, new List<int>());

            TreeNodeSymbols[node].Add(symbol);
            Invalidate();
        }

        public void RemoveNodeRightSymbol(TreeNode node, int symbol)
        {
            if (TreeNodeSymbols.ContainsKey(node))
            {
                int idx = TreeNodeSymbols[node].IndexOf(symbol);
                if (idx >= 0)
                {
                    TreeNodeSymbols[node].RemoveAt(idx);
                    Invalidate();
                }
            }
        }

        public void ClearNodeRightSymbol(TreeNode node)
        {
            if (TreeNodeSymbols.ContainsKey(node))
            {
                TreeNodeSymbols[node].Clear();
                Invalidate();
            }
        }

        #endregion 扩展函数

        public delegate void OnNodeRightSymbolClick(object sender, TreeNode node, int index, int symbol);

        public event OnNodeRightSymbolClick NodeRightSymbolClick;
    }
}