using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬 {
    /// <summary>
    /// 显示在DataGridView单元格的控件
    /// </summary>
    public interface 列控件 {
        public DataGridViewCell Cell { get; set; }
        public object 当前值 { get; }//对应单元格的数据
        public void 单元格值修改(object obj);//单元格修改时传入
        public void 列控件值修改(object obj);//列控件值修改时触发
    }
}
