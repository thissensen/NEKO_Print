using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬 {
    public interface 组合控件基类接口 {

        public bool 是否序列化 { get; set; }
        public UIPanel LabelPanel控件 { get;}
        public Control 主控件 { get; }
        public Size Size { get; set; }

    }
}
