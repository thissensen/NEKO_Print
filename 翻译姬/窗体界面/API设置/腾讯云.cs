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
    public partial class 腾讯云 : 通用API窗体 {

        public override Dictionary<string, int> QPS数据 => new Dictionary<string, int> {
            ["通用版"] = 5
        };

        public 腾讯云() {
            InitializeComponent();
            QPS.ReadOnly = true;
            KEY.HeaderText = "SecretId";
            秘钥.HeaderText = "SecretKey";
        }
        protected override void QPS列数据变化(DataGridViewRow row, int 值) {
            if (row == null) {
                return;
            }
            row.Cells[QPS.DataPropertyName].Value = 5;
            row.Cells[可用额度.DataPropertyName].Value = 5000000;
        }

    }
}
