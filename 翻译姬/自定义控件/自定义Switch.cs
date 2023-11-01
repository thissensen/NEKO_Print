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
    public partial class 自定义Switch : UISwitch {
        public 自定义Switch() {
            InitializeComponent();
            ActiveChanged += 自定义Switch_ActiveChanged;
            EnabledChanged += 自定义Switch_EnabledChanged;
        }

        private void 自定义Switch_EnabledChanged(object sender, EventArgs e) {
            if (Enabled) {
                if (Active) {
                    ButtonColor = 全局字符串.背景色;
                } else {
                    ButtonColor = 全局字符串.主题色;
                }
            } else {
                ButtonColor = 全局字符串.背景色;
            }
        }

        private void 自定义Switch_ActiveChanged(object sender, EventArgs e) {
            if (Active) {
                ButtonColor = 全局字符串.背景色;
            } else {
                ButtonColor = 全局字符串.主题色;
            }
        }
    }
}
