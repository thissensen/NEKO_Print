using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬 {
    public partial class 自定义Form : UIForm, 自定义窗体 {

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public 数据库连接 数据库 {
            get {
                if (_数据库 == null) {
                    _数据库 = 全局数据.数据库;
                }
                return _数据库;
            }
            set {
                _数据库 = value;
            }
        }
        private 数据库连接 _数据库;

        public Dictionary<string, Control> Name_Control { get; set; } = new Dictionary<string, Control>();

        public bool 是否序列化 { get; set; } = true;

        public bool IsLoad { get; set; }

        public 自定义Form() {
            InitializeComponent();
        }

        private void 自定义Form_Load(object sender, EventArgs e) {
            if (DesignMode) {
                return;
            }
            获取所有自定义控件(this);
            this.主题设置();
            this.DPI自适应(Name_Control);
            IsLoad = true;
        }

        private void 自定义Form_Shown(object sender, EventArgs e) {
            if (DesignMode) {
                return;
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

        private void 自定义Form_FormClosed(object sender, FormClosedEventArgs e) {
            控件序列化 序列化 = new 控件序列化(this);
            序列化.序列化();
        }

    }
}
