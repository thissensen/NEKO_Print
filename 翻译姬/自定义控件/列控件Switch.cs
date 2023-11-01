using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬 {
    public class 列控件Switch : 自定义Switch, 列控件 {

        public object 当前值 => Active;

        public DataGridViewCell Cell { get; set; }

        public 列控件Switch() {
            BackColor = 全局字符串.背景色;
            ActiveColor = 全局字符串.主题色;
            InActiveColor = 全局字符串.次级主题色;
            ForeColor = 全局字符串.主题色;
            if (Active) {
                ButtonColor = 全局字符串.背景色;
            } else {
                ButtonColor = 全局字符串.主题色;
            }

            ActiveChanged += 列控件Switch_ActiveChanged;
        }

        public void 列控件值修改(object obj) {
            Cell.Value = obj;
        }

        public void 单元格值修改(object obj) {
            if (obj == DBNull.Value) {
                Active = false;
            } else {
                Active = (bool)obj;
            }
        }

        private void 列控件Switch_ActiveChanged(object sender, EventArgs e) {
            列控件值修改(Active);
        }
    }
}
