using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sunny.UI.FormEx;
using System.Windows.Forms;

namespace 翻译姬 {
    public class MessageBoxEx {

        public static bool Show(string 内容, string 标题 = "提示", 提示窗按钮 显示按钮 = 提示窗按钮.确认, string 确认按钮文本 = "确认", string 取消按钮文本 = "取消") {
            /*自定义提示窗 f = new 自定义提示窗(内容, 标题, 显示按钮);
            //显示遮罩
            Point pt = SystemEx.GetCursorPos();
            Rectangle screen = Screen.GetBounds(pt);
            FMask mask = new FMask();
            mask.Bounds = screen;
            mask.Show();

            f.ShowInTaskbar = false;
            f.TopMost = true;
            f.ShowDialog();
            mask.Dispose();
            return f.是否确认;*/
            自定义提示窗 f = new 自定义提示窗(内容, 标题, 显示按钮);
            f.确认Btn.Text = 确认按钮文本;
            f.取消Btn.Text = 取消按钮文本;
            f.ShowDialog();
            return f.是否确认;
        }

    }
    public enum 提示窗按钮 {
        确认,
        确认取消
    }
}
