using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 翻译姬.Properties;

namespace 翻译姬 {
    public partial class 自定义Page : UIPage, 自定义窗体 {

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public 数据库连接 数据库 => 全局数据.数据库;
        /// <summary>
        /// 是否Shown
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsShown { get; set; } = false;
        /// <summary>
        /// 是否Load
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLoad { get; set; } = false;

        public Dictionary<string, Control> Name_Control { get; set; } = new Dictionary<string, Control>();

        public bool 是否序列化 { get; set; } = true;
        public delegate void 自定义Page被选中();
        public event 自定义Page被选中 Page被选中;

        public 自定义Page() {
            InitializeComponent();
        }

        public void OnPage被选中() {
            Page被选中?.Invoke();
        }

        private void 自定义Page_Load(object sender, EventArgs e) {
            if (DesignMode) {
                return;
            }
            获取所有自定义控件(this);
            IsLoad = true;
            this.主题设置();
            this.DPI自适应(Name_Control);
        }

        private void 自定义Page_Shown(object sender, EventArgs e) {
            if (DesignMode) {
                return;
            }
            IsShown = true;
            foreach (var kv in Name_Control) {
                if (kv.Value is 自定义DataGridView view) {
                    view.Shown();
                }
            }
            控件序列化 序列化 = new 控件序列化(this);
            序列化.反序列化();
        }

        private void 获取所有自定义控件(Control control) {
            foreach (Control c in control.Controls) {
                if (c.Name.IsNullOrEmpty()) {
                    continue;
                }
                Name_Control.Add(c.Name, c);
                if (c.Controls != null) {
                    获取所有自定义控件(c);
                }
            }
        }

        private void 自定义Page_FormClosed(object sender, FormClosedEventArgs e) {
            控件序列化 序列化 = new 控件序列化(this);
            序列化.序列化();
        }
    }
}
