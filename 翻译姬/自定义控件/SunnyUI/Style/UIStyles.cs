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
 * 文件名称: UIStyles.cs
 * 文件说明: 主题样式管理类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2021-07-12: V3.0.5 增加紫色主题
 * 2021-07-18: V3.0.5 增加多彩主题，以颜色深色，文字白色为主
 * 2021-09-24: V3.0.7 修改默认字体的GdiCharSet
 * 2021-10-16: V3.0.8 增加系统DPI缩放自适应
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 主题样式管理类
    /// </summary>
    public static class UIStyles
    {
        public static bool DPIScale { get; set; }

        public static bool ZoomScale { get; set; }

        public static float FontSize { get; set; } = 12;

        public static List<UIStyle> PopularStyles()
        {
            List<UIStyle> styles = new List<UIStyle>();
            foreach (UIStyle style in Enum.GetValues(typeof(UIStyle)))
            {
                if (style.Value() >= UIStyle.Blue.Value() && style.Value() < UIStyle.Colorful.Value())
                {
                    styles.Add(style);
                }
            }

            return styles;
        }

        /// <summary>
        /// 自定义
        /// </summary>
        private static readonly UIBaseStyle Custom = new UICustomStyle();

        /// <summary>
        /// 蓝
        /// </summary>
        public static readonly UIBaseStyle Blue = new UIBlueStyle();

        /// <summary>
        /// 橙
        /// </summary>
        public static readonly UIBaseStyle Orange = new UIOrangeStyle();

        /// <summary>
        /// 灰
        /// </summary>
        public static readonly UIBaseStyle Gray = new UIGrayStyle();

        /// <summary>
        /// 绿
        /// </summary>
        public static readonly UIBaseStyle Green = new UIGreenStyle();

        /// <summary>
        /// 红
        /// </summary>
        public static readonly UIBaseStyle Red = new UIRedStyle();

        /// <summary>
        /// 深蓝
        /// </summary>
        public static readonly UIBaseStyle DarkBlue = new UIDarkBlueStyle();

        /// <summary>
        /// 黑
        /// </summary>
        public static readonly UIBaseStyle Black = new UIBlackStyle();

        /// <summary>
        /// 紫
        /// </summary>
        public static readonly UIBaseStyle Purple = new UIPurpleStyle();

        /// <summary>
        /// 多彩
        /// </summary>
        private static readonly UIColorfulStyle Colorful = new UIColorfulStyle();

        public static void InitColorful(Color styleColor, Color foreColor)
        {
            Colorful.Init(styleColor, foreColor);
            Style = UIStyle.Colorful;
            SetStyle(Style);
        }

        private static readonly ConcurrentDictionary<UIStyle, UIBaseStyle> Styles = new ConcurrentDictionary<UIStyle, UIBaseStyle>();
        private static readonly ConcurrentDictionary<Guid, UIForm> Forms = new ConcurrentDictionary<Guid, UIForm>();
        private static readonly ConcurrentDictionary<Guid, UIPage> Pages = new ConcurrentDictionary<Guid, UIPage>();

        /// <summary>
        /// 菜单颜色集合
        /// </summary>
        public static readonly ConcurrentDictionary<UIMenuStyle, UIMenuColor> MenuColors = new ConcurrentDictionary<UIMenuStyle, UIMenuColor>();

        static UIStyles()
        {
            AddStyle(Custom);
            AddStyle(Blue);
            AddStyle(Orange);
            AddStyle(Gray);
            AddStyle(Green);
            AddStyle(Red);
            AddStyle(DarkBlue);

            AddStyle(new UIBaseStyle().Init(UIColor.LayuiGreen, UIStyle.LayuiGreen, Color.White, UIFontColor.Primary));
            AddStyle(new UIBaseStyle().Init(UIColor.LayuiRed, UIStyle.LayuiRed, Color.White, UIFontColor.Primary));
            AddStyle(new UIBaseStyle().Init(UIColor.LayuiOrange, UIStyle.LayuiOrange, Color.White, UIFontColor.Primary));

            AddStyle(Black);
            AddStyle(Purple);

            AddStyle(Colorful);

            MenuColors.TryAdd(UIMenuStyle.Custom, new UIMenuCustomColor());
            MenuColors.TryAdd(UIMenuStyle.Black, new UIMenuBlackColor());
            MenuColors.TryAdd(UIMenuStyle.White, new UIMenuWhiteColor());
        }

        /// <summary>
        /// 主题样式整数值
        /// </summary>
        /// <param name="style">主题样式</param>
        /// <returns>整数值</returns>
        public static int Value(this UIStyle style)
        {
            return (int)style;
        }

        /// <summary>
        /// 注册窗体
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <param name="form">窗体</param>
        public static bool Register(Guid guid, UIForm form)
        {
            if (!Forms.ContainsKey(guid))
            {
                Forms.Upsert(guid, form);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 注册页面
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <param name="page">页面</param>
        public static bool Register(Guid guid, UIPage page)
        {
            if (!Pages.ContainsKey(guid))
            {
                Pages.Upsert(guid, page);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 注册窗体
        /// </summary>
        /// <param name="form">窗体</param>
        public static bool Register(this UIForm form)
        {
            if (!Forms.ContainsKey(form.Guid))
            {
                Forms.Upsert(form.Guid, form);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 注册页面
        /// </summary>
        /// <param name="page">页面</param>
        public static bool Register(this UIPage page)
        {
            if (!Pages.ContainsKey(page.Guid))
            {
                Pages.Upsert(page.Guid, page);
                return true;
            }

            return false;
        }

        public static List<T> GetPages<T>() where T : UIPage
        {
            List<T> result = new List<T>();
            foreach (var page in Pages)
            {
                if (page is T pg)
                    result.Add(pg);
            }

            return result;
        }

        /// <summary>
        /// 反注册窗体
        /// </summary>
        /// <param name="form">窗体</param>
        public static void UnRegister(this UIForm form)
        {
            Forms.TryRemove(form.Guid, out _);
        }

        /// <summary>
        /// 反注册页面
        /// </summary>
        /// <param name="page">页面</param>
        public static void UnRegister(this UIPage page)
        {
            Pages.TryRemove(page.Guid, out _);
        }

        /// <summary>
        /// 反注册窗体、页面
        /// </summary>
        /// <param name="guid">GUID</param>
        public static void UnRegister(Guid guid)
        {
            if (Forms.ContainsKey(guid))
                Forms.TryRemove(guid, out _);

            if (Pages.ContainsKey(guid))
                Pages.TryRemove(guid, out _);

        }

        /// <summary>
        /// 获取主题样式
        /// </summary>
        /// <param name="style">主题样式名称</param>
        /// <returns>主题样式</returns>
        public static UIBaseStyle GetStyleColor(UIStyle style)
        {
            if (Styles.ContainsKey(style))
            {
                return Styles[style];
            }

            Style = UIStyle.Blue;
            return Styles[Style];
        }

        public static UIBaseStyle ActiveStyleColor => GetStyleColor(Style);

        private static void AddStyle(UIBaseStyle uiColor)
        {
            if (Styles.ContainsKey(uiColor.Name))
            {
                MessageBox.Show(uiColor.Name + " is already exist.");
            }

            Styles.TryAdd(uiColor.Name, uiColor);
        }

        /// <summary>
        /// 主题样式
        /// </summary>
        public static UIStyle Style { get; private set; } = UIStyle.Blue;

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="style">主题样式</param>
        public static void SetStyle(UIStyle style)
        {
            Style = style;

            foreach (var form in Forms.Values)
            {
                form.Style = style;
            }

            foreach (var page in Pages.Values)
            {
                page.Style = style;
            }
        }

        public static void SetDPIScale()
        {
            if (!DPIScale) return;

            foreach (var form in Forms.Values)
            {
                if (!UIDPIScale.DPIScaleIsOne())
                    form.SetDPIScale();
            }

            foreach (var page in Pages.Values)
            {
                if (!UIDPIScale.DPIScaleIsOne())
                    page.SetDPIScale();
            }
        }

        public static void Translate()
        {
            foreach (var form in Forms.Values)
            {
                form.Translate();
            }
        }
    }
}
