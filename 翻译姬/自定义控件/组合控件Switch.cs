using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬 {
    [ToolboxItem(true)]
    public partial class 组合控件Switch : 组合控件基类<自定义Switch> {
        public 组合控件Switch() {
            InitializeComponent();
        }

        [Category("控件绑定"), Description("控件状态")]
        public bool Active {
            get {
                return Switch.Active;
            }
            set {
                Switch.Active = value;
            }
        }

    }
}
