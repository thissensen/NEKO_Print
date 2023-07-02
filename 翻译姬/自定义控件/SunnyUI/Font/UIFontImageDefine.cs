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
 * 文件名称: UFontImageDefine.cs
 * 文件说明: 字体图片定义类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2021-07-18: V3.0.5 将字体图标最大尺寸从64调整到128
******************************************************************************/

using System.Drawing;

namespace Sunny.UI
{
    public interface ISymbol
    {
        int Symbol { get; set; }

        int SymbolSize { get; set; }

        Point SymbolOffset { get; set; }
    }

    public enum UISymbolType
    {
        FontAwesomeV4 = 0,
        FontAwesomeV5Brands = 1,
        FontAwesomeV5Regular = 2,
        FontAwesomeV5Solid = 3
    }
}