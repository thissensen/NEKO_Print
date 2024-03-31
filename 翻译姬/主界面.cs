using Newtonsoft.Json;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 翻译姬.Properties;

namespace 翻译姬 {
    //要做的事：退出时Close()界面
    public partial class 主界面 : 自定义Form {

        #region 拖动panel移动窗口
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "PostMessage")]
        private extern static void PostMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        //Panel控件,鼠标点击时移动窗口位置,MouseDown事件
        private void 移动窗口_MouseDown(object sender, MouseEventArgs e) { ReleaseCapture(); PostMessage(this.Handle, 0x112, 0xf012, 0); }
        #endregion

        public static Dictionary<string, 自定义Page> 界面组 = new Dictionary<string, 自定义Page>() {
                ["文本翻译"] = new 文本翻译(),
                ["百度"] = new 百度(),
                ["腾讯云"] = new 腾讯云(),
                ["阿里云"] = new 阿里云(),
                ["火山"] = new 火山(),
                ["GPT"] = new GPT(),
                ["全局设置"] = new 全局设置(),
                ["GPT设置"] = new GPT设置(),
                ["正则设置"] = new 正则设置(),
                ["替换列表"] = new 替换列表(),
                ["Json指令"] = new Json指令(),
                ["Xml指令"] = new Xml指令(),
                ["关于翻译姬"] = new 关于翻译姬(),
            };

        public 主界面() {
            InitializeComponent();
            Activated += 主界面_Activated;
            Icon = Resources.sensen;
            数据中转.主窗体 = this;
            数据中转.进度条 = 进度条;
            //Dpi设置
            全局字符串.屏幕缩放比 = 获取屏幕缩放比();
            //主题设置
            窗体列表.BackColor = 全局字符串.背景色;
            //主题设置
            关闭Btn.RectColor = Color.Red;
            关闭Btn.RectHoverColor = Color.Red;
            关闭Btn.RectPressColor = Color.Red;
            //窗体设置
            int 图标大小 = (int)(32 * 全局字符串.屏幕缩放比);
            //提前初始化
            int pageIndex = 0;
            foreach (var kv in 界面组) {
                kv.Value.PageIndex = ++pageIndex;
            }
            界面组["全局设置"].Show();
            界面组["GPT设置"].Show();
            界面组["文本翻译"].Show();
            MainTabControl.AddPage(界面组["文本翻译"]);
            MainTabControl.AddPage(界面组["关于翻译姬"]);
            TreeNode 文本翻译Node = 窗体列表.CreateNode("文本翻译", 61867, 图标大小, 界面组["文本翻译"].PageIndex);
            TreeNode API设置node = 窗体列表.CreateNode("API设置", 361498, 图标大小, -1);
            窗体列表.CreateChildNode(API设置node, AddPage(界面组["百度"]));
            窗体列表.CreateChildNode(API设置node, AddPage(界面组["腾讯云"]));
            窗体列表.CreateChildNode(API设置node, AddPage(界面组["阿里云"]));
            窗体列表.CreateChildNode(API设置node, AddPage(界面组["火山"]));
            窗体列表.CreateChildNode(API设置node, AddPage(界面组["GPT"]));
            TreeNode 设置node = 窗体列表.CreateNode("设置", 57399, 图标大小, -1);
            窗体列表.CreateChildNode(设置node, AddPage(界面组["全局设置"]));
            窗体列表.CreateChildNode(设置node, AddPage(界面组["GPT设置"]));
            窗体列表.CreateChildNode(设置node, AddPage(界面组["正则设置"]));
            窗体列表.CreateChildNode(设置node, AddPage(界面组["替换列表"]));
            窗体列表.CreateChildNode(设置node, AddPage(界面组["Json指令"]));
            窗体列表.CreateChildNode(设置node, AddPage(界面组["Xml指令"]));
            窗体列表.CreateNode("关于", 61530, 图标大小, 界面组["关于翻译姬"].PageIndex);

            窗体列表.SelectedNode = 文本翻译Node;
            界面组["文本翻译"].OnPage被选中();

        }

        private void 主界面_Activated(object sender, EventArgs e) {
            if (数据中转.数据处理 != null) {
                数据中转.数据处理.TopMost = true;
                数据中转.数据处理.TopMost = false;
            }
        }

        protected override void OnResize(EventArgs e) {
            if (WindowState == FormWindowState.Normal && IsActive) {
                //正常化时
                //动一动，防止渲染失败
                Focus();
                /*var p = Location;
                Location = new Point(p.X - 1, p.Y - 1);
                Location = p;*/
            } else if (WindowState == FormWindowState.Minimized) {
                //最小化时
                
            }
        }

        /// <summary>
        ///  获取电脑缩放比例
        /// </summary>
        /// <returns></returns>
        public decimal 获取屏幕缩放比() => CreateGraphics().DpiX switch {
            96 => 1,
            120 => 1.25m,
            144 => 1.5m,
            192 => 2m,
            _ => 1
        };

        private void 主界面_Load(object sender, EventArgs e) {
            //额外主题设置
            最小化Btn.RectColor = 全局字符串.主题色;
            最小化Btn.SymbolHoverColor = 全局字符串.深级主题色;
            最小化Btn.SymbolPressColor = 全局字符串.深级主题色;
            关闭Btn.RectColor = 全局字符串.主题色;
            关闭Btn.SymbolHoverColor = Color.Red;
            关闭Btn.SymbolPressColor = Color.Red;
            //额外DPI设置
            窗体列表.ItemHeight = (int)(窗体列表.ItemHeight * 全局字符串.屏幕缩放比);
            //允许托盘图标
            //通知图标.ShowBalloonTip(2000);
        }

        private void 主界面_Shown(object sender, EventArgs e) {
            //显示位置居中
            int H = Screen.PrimaryScreen.WorkingArea.Height;
            int W = Screen.PrimaryScreen.WorkingArea.Width;
            Point 初始坐标 = new Point();
            初始坐标.X = (W - Width) / 2;
            初始坐标.Y = (H - Height) / 2;
            Location = 初始坐标;
        }

        private void 最小化Btn_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
        }

        private void 关闭Btn_Click(object sender, EventArgs e) {
            if (文本翻译.机翻中) {
                if (MessageBoxEx.Show("机翻中，是否强制退出？", "提示", 提示窗按钮.确认取消)) {
                    File.WriteAllText(全局数据.缓存数据路径, JsonConvert.SerializeObject(文本翻译.处理中文件结构, Formatting.Indented));
                    Close();
                }
            } else {
                Close();
            }
        }

        //下拉框点击事件
        private void 窗体列表_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            NavMenuItem nav = e.Node.Tag as NavMenuItem;
            int pageIndex = nav.PageIndex;
            if ((窗体列表.SelectedNode.Tag as NavMenuItem).PageIndex == pageIndex) {
                return;
            }
            if (pageIndex != -1) {
                自定义Page page = GetPage(pageIndex) as 自定义Page;
                if (page == null) {
                    return;
                }
                if (page.IsLoad) {
                    page.OnPage被选中();
                }
                SelectPage(pageIndex);
            }
        }

        //下拉框展开事件
        private void 窗体列表_AfterExpand(object sender, TreeViewEventArgs e) {
            NavMenuItem nav = e.Node.Tag as NavMenuItem;
            if (nav.PageIndex == -1) {//主，显示子
                nav = e.Node.Nodes[0].Tag as NavMenuItem;
                int pageIndex = nav.PageIndex;
                自定义Page page = GetPage(pageIndex) as 自定义Page;
                if (page.IsLoad) {
                    page.OnPage被选中();
                }
                
                SelectPage(pageIndex);
            }
        }
    }
}
