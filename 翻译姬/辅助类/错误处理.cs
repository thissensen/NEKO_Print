using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 翻译姬 {
    public static class 错误处理 {

        private static object _普通lock = new object();

        public static void 普通错误处理(string text) {
            lock (_普通lock) {
                数据中转.文本显示AppendLine($"{text} {DateTime.Now:HH:mm:ss}");
            }
        }


    }
}
