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
 * 文件名称: UIDatePicker.cs
 * 文件说明: 日期选择框
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-08-07: V2.2.7 可编辑输入，日期范围控制以防止出错
 * 2021-04-15: V3.0.3 增加ShowToday显示今日属性
 * 2021-08-14: V3.0.6 增加可选择年、年月、年月日
 * 2022-11-08: V3.2.8 增加MaxDate,MinDate
******************************************************************************/

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    [DefaultProperty("Value")]
    [DefaultEvent("ValueChanged")]
    public partial class UIDatePicker : UIDropControl, IToolTip
    {
        public delegate void OnDateTimeChanged(object sender, DateTime value);

        public UIDatePicker()
        {
            InitializeComponent();
            Value = DateTime.Now;
            MaxLength = 10;
            EditorLostFocus += UIDatePicker_LostFocus;
            TextChanged += UIDatePicker_TextChanged;

            CreateInstance();
        }

        private DateTime max = DateTime.MaxValue;
        private DateTime min = DateTime.MinValue;

        static internal DateTime EffectiveMaxDate(DateTime maxDate)
        {
            DateTime maxSupportedDate = DateTimePicker.MaximumDateTime;
            if (maxDate > maxSupportedDate)
            {
                return maxSupportedDate;
            }
            return maxDate;
        }

        static internal DateTime EffectiveMinDate(DateTime minDate)
        {
            DateTime minSupportedDate = DateTimePicker.MinimumDateTime;
            if (minDate < minSupportedDate)
            {
                return minSupportedDate;
            }
            return minDate;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly DateTime MinDateTime = new DateTime(1753, 1, 1);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly DateTime MaxDateTime = new DateTime(9998, 12, 31);

        [DefaultValue(typeof(DateTime), "9998/12/31")]
        [Description("最大日期"), Category("SunnyUI")]
        public DateTime MaxDate
        {
            get
            {
                return EffectiveMaxDate(max);
            }
            set
            {
                if (value != max)
                {
                    if (value < EffectiveMinDate(min))
                    {
                        value = EffectiveMinDate(min);
                    }

                    // If trying to set the maximum greater than MaxDateTime, throw.
                    if (value > MaximumDateTime)
                    {
                        value = MaximumDateTime;
                    }

                    max = value;

                    //If Value (which was once valid) is suddenly greater than the max (since we just set it)
                    //then adjust this...
                    if (Value > max)
                    {
                        Value = max;
                    }
                }
            }
        }

        [DefaultValue(typeof(DateTime), "1753/1/1")]
        [Description("最小日期"), Category("SunnyUI")]
        public DateTime MinDate
        {
            get
            {
                return EffectiveMinDate(min);
            }
            set
            {
                if (value != min)
                {
                    if (value > EffectiveMaxDate(max))
                    {
                        value = EffectiveMaxDate(max);
                    }

                    // If trying to set the minimum less than MinimumDateTime, throw.
                    if (value < MinimumDateTime)
                    {
                        value = MinimumDateTime;
                    }

                    min = value;

                    //If Value (which was once valid) is suddenly less than the min (since we just set it)
                    //then adjust this...
                    if (Value < min)
                    {
                        Value = min;
                    }
                }
            }
        }

        internal static DateTime MaximumDateTime
        {
            get
            {
                DateTime maxSupportedDateTime = CultureInfo.CurrentCulture.Calendar.MaxSupportedDateTime;
                if (maxSupportedDateTime.Year > MaxDateTime.Year)
                {
                    return MaxDateTime;
                }
                return maxSupportedDateTime;
            }
        }

        internal static DateTime MinimumDateTime
        {
            get
            {
                DateTime minSupportedDateTime = CultureInfo.CurrentCulture.Calendar.MinSupportedDateTime;
                if (minSupportedDateTime.Year < 1753)
                {
                    return new DateTime(1753, 1, 1);
                }
                return minSupportedDateTime;
            }
        }

        [DefaultValue(false)]
        [Description("日期输入时，是否可空显示"), Category("SunnyUI")]
        public bool CanEmpty { get; set; }

        [DefaultValue(false)]
        [Description("日期输入时，显示今日按钮"), Category("SunnyUI")]
        public bool ShowToday { get; set; }

        private UIDateType showType = UIDateType.YearMonthDay;

        [DefaultValue(UIDateType.YearMonthDay)]
        [Description("日期显示类型"), Category("SunnyUI")]
        public UIDateType ShowType
        {
            get => showType;
            set
            {
                showType = value;
                switch (value)
                {
                    case UIDateType.YearMonthDay:
                        DateFormat = "yyyy-MM-dd";
                        break;
                    case UIDateType.YearMonth:
                        DateFormat = "yyyy-MM";
                        break;
                    case UIDateType.Year:
                        DateFormat = "yyyy";
                        break;
                }
            }
        }

        /// <summary>
        /// 需要额外设置ToolTip的控件
        /// </summary>
        /// <returns>控件</returns>
        public Control ExToolTipControl()
        {
            return edit;
        }

        public int Year => Value.Year;
        public int Month => Value.Month;
        public int Day => Value.Day;

        private void UIDatePicker_TextChanged(object sender, EventArgs e)
        {
            if (Text.Length == MaxLength)
            {
                try
                {
                    DateTime dt = Text.ToDateTime(DateFormat);
                    if (Value != dt) Value = dt;
                }
                catch
                {
                    Value = DateTime.Now.Date;
                }
            }
        }

        private void UIDatePicker_LostFocus(object sender, EventArgs e)
        {
            if (Text.IsNullOrEmpty())
            {
                if (CanEmpty) return;
            }

            try
            {
                DateTime dt = Text.ToDateTime(DateFormat);
                if (Value != dt) Value = dt;
            }
            catch
            {
                Value = DateTime.Now.Date;
            }
        }

        public event OnDateTimeChanged ValueChanged;

        /// <summary>
        /// 值改变事件
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="value">值</param>
        protected override void ItemForm_ValueChanged(object sender, object value)
        {
            Value = (DateTime)value;
            Invalidate();
        }

        private readonly UIDateItem item = new UIDateItem();

        /// <summary>
        /// 创建对象
        /// </summary>
        protected override void CreateInstance()
        {
            ItemForm = new UIDropDown(item);
        }

        [Description("选中日期"), Category("SunnyUI")]
        public DateTime Value
        {
            get => item.Date;
            set
            {
                if (value < new DateTime(1900, 1, 1))
                    value = new DateTime(1900, 1, 1);
                Text = value.ToString(dateFormat);

                if (item.Date != value)
                {
                    item.Date = value;
                }

                ValueChanged?.Invoke(this, Value);
            }
        }

        private void UIDatetimePicker_ButtonClick(object sender, EventArgs e)
        {
            item.ShowType = ShowType;
            item.Date = Value;
            item.ShowToday = ShowToday;
            item.PrimaryColor = RectColor;
            item.Translate();
            item.SetDPIScale();
            item.SetStyleColor(UIStyles.ActiveStyleColor);
            item.max = MaxDate;
            item.min = MinDate;
            ItemForm.Show(this);
        }

        private string dateFormat = "yyyy-MM-dd";

        [Description("日期格式化掩码"), Category("SunnyUI")]
        [DefaultValue("yyyy-MM-dd")]
        public string DateFormat
        {
            get => dateFormat;
            set
            {
                dateFormat = value;
                Text = Value.ToString(dateFormat);
                MaxLength = dateFormat.Length;
            }
        }
    }
}