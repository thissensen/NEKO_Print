using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬 {
    public interface 自定义窗体 {

        public bool 是否序列化 { get; set; }
        public Dictionary<string, Control> Name_Control { get; set; }

    }
}
