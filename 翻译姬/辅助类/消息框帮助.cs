using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Sunny.UI.TipStyle;

namespace 翻译姬 {
    public static class 消息框帮助 {

        private static Bitmap 轻便消息Icon;

        static 消息框帮助() {
            Graphics g = null;
            Pen pen = null;
            Brush brush = null;
            Bitmap bmp = null;
            int 一 = (int)(1 * 全局字符串.屏幕缩放比);
            int 二 = (int)(2 * 全局字符串.屏幕缩放比);
            int 三 = (int)(3 * 全局字符串.屏幕缩放比);
            int 六 = (int)(6 * 全局字符串.屏幕缩放比);
            int 八 = (int)(8 * 全局字符串.屏幕缩放比);
            int 十 = (int)(10 * 全局字符串.屏幕缩放比);
            int 十二 = (int)(12 * 全局字符串.屏幕缩放比);
            int 十八 = (int)(18 * 全局字符串.屏幕缩放比);
            int 二四 = (int)(24 * 全局字符串.屏幕缩放比);
            try {
                bmp = new Bitmap(二四, 二四);
                g = Graphics.FromImage(bmp);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                brush = new SolidBrush(全局字符串.背景色);
                g.FillEllipse(brush, 三, 三, 十八, 十八);
                
                pen = new Pen(全局字符串.主题色, 二);
                g.DrawLine(pen, new Point(十二, 六), new Point(十二, 八));
                g.DrawLine(pen, new Point(十二, 十), new Point(十二, 十八));
                pen.Width = 一;
                g.DrawEllipse(pen, 三, 三, 十八, 十八);
                轻便消息Icon = bmp;
            } catch {
                bmp?.Dispose();
                throw;
            } finally {
                pen?.Dispose();
                brush?.Dispose();
                g?.Dispose();
            }
        }

        public static void 通知栏消息(string 内容, string 标题 = "翻译姬") {
            new 自定义通知(内容, 标题).Show();
        }

        /// <param name="内容">提示内容</param>
        /// <param name="指定控件">指定后则会在控件附近出现</param>
        /// <param name="消息类型">消息的类型，默认Info</param>
        /// <param name="显示时长"></param>
        /// <param name="是否悬浮"></param>
        public static void 轻便消息(string 内容, Control 指定控件, UINotifierType 消息类型 = UINotifierType.INFO, int 显示时长 = 1000, bool 是否悬浮 = true) {
            TipStyle s = new TipStyle() {
                Icon = 轻便消息Icon,
                BackColor = 全局字符串.背景色,
                BorderColor = 全局字符串.主题色,
                TextColor = 全局字符串.主题色
            };

            指定控件.Invoke(()=> UIMessageTip.Show(指定控件, 内容, s, 显示时长, 是否悬浮));
        }

    }
}
