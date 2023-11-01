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
    public partial class 自定义ComboBox : UIComboBox {

        public 自定义ComboBox() {
            InitializeComponent();
        }

        public override string Text {
            get {
                return base.Text;
            }
            set {
                base.Text = value;
            }
        }

        public void 初始化(IEnumerable<string> res) => DataSource = res;
        public new void OnLoad(EventArgs e) => base.OnLoad(e);

    }
}
