using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 翻译姬.Properties;
using static Sunny.UI.UIAvatar;

namespace 翻译姬 {

    /// <summary>
    /// 所有文件夹都要以\结尾
    /// </summary>
    public static class 全局字符串 {

        static 全局字符串() {
            //安装键盘钩子
            try {
                全局键盘监听.KeyDownEvent += (_, e) => 键盘按下按钮组.Add(e.KeyCode);
                全局键盘监听.KeyUpEvent += (_, e) => 键盘按下按钮组.Remove(e.KeyCode);
                全局键盘监听.Start();
            } catch (Exception ex) {
                MessageBoxEx.Show("键盘钩子加载失败");
            }
            //设置字体
            byte[] 字体 = Resources.MaoKenZhuYuanTi_MaokenZhuyuanTi_2;
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddMemoryFont(字体.ToIntPtr(), 字体.Length);
            主字体 = pfc.Families[0];
            //主字体 = new FontFamily("微软雅黑");
#if DEBUG
            //主题色 = Color.Black;
            //深级主题色 = Color.Black;
            //次级主题色 = Color.Gray;
            //不可用时颜色 = Color.Gray;
#endif
        }

        public static 键盘Hook 全局键盘监听 = new 键盘Hook();
        public static Keys 键盘单个按下按钮 => 键盘按下按钮组.Count == 1 ? 键盘按下按钮组.Single() : Keys.None;
        public static List<Keys> 键盘按下按钮组 = new List<Keys>();

        public static FontFamily 主字体 { get; set; }
        public static Color 主题色 {
            get => 全局数据.全局主题设置.主题色;
            set => 全局数据.全局主题设置.主题色 = value;
        }
        public static Color 深级主题色 {
            get => 全局数据.全局主题设置.深级主题色;
            set => 全局数据.全局主题设置.深级主题色 = value;
        }
        public static Color 次级主题色 {
            get => 全局数据.全局主题设置.次级主题色;
            set => 全局数据.全局主题设置.次级主题色 = value;
        }
        public static Color 不可用时颜色 {
            get => 全局数据.全局主题设置.不可用时颜色;
            set => 全局数据.全局主题设置.不可用时颜色 = value;
        }
        public static Color 背景色 {
            get => 全局数据.全局主题设置.背景色; 
            set => 全局数据.全局主题设置.背景色 = value;
        }
        public static decimal 屏幕缩放比 { get; set; }

        /// <summary>
        /// 当前exe所在路径
        /// </summary>
        public static readonly string 项目路径 = Application.StartupPath + "\\";
        public static readonly string 桌面路径 = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\";
        /// <summary>
        /// 软件的exe名称
        /// </summary>
        public static string 软件名称 { get; set; } = Path.GetFileName(Application.ExecutablePath).Substring(0, Path.GetFileName(Application.ExecutablePath).Length - 4);
        /// <summary>
        /// 软件的默认存储目录，AppData目录
        /// </summary>
        public static readonly string 软件存储目录 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $@"\{软件名称}\";
        /// <summary>
        /// 同时打开多个
        /// </summary>
        public static bool 允许重复启动 { get; set; }
        /// <summary>
        /// 计算机名
        /// </summary>
        public static readonly string 计算机名 = Environment.MachineName;
        /// <summary>
        /// 计算机的用户名
        /// </summary>
        public static readonly string 用户名 = Environment.UserName;
        /// <summary>
        /// CPU处理器核心数
        /// </summary>
        public static readonly int CPU核心数 = Environment.ProcessorCount;
    }


}
