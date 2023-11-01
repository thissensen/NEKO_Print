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
    public partial class 百度 : 通用API窗体 {

        public 百度() {
            InitializeComponent();
            QPS数据.Add("标准版", 1);
            QPS数据.Add("高级版", 10);
            QPS数据.Add("尊享版", 100);
            KEY.HeaderText = "APP ID";
            秘钥.HeaderText = "秘钥";
        }

        protected override void QPS列数据变化(DataGridViewRow row, int 值) {
            //百度对应额度设置
            if (值 == 1) {
                row.Cells[可用额度.DataPropertyName].Value = 50000;
            } else if (值 == 10) {
                row.Cells[可用额度.DataPropertyName].Value = 1000000;
            } else if (值 == 100) {
                row.Cells[可用额度.DataPropertyName].Value = 2000000;
            } else {//默认值
                row.Cells[QPS.DataPropertyName].Value = 1;
                row.Cells[可用额度.DataPropertyName].Value = 50000;
            }
        }

    }
}
