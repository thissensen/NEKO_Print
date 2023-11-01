using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 翻译姬 {
    /// <summary>
    /// 一旦触发，中止机翻操作
    /// </summary>
    public class Exception_严重错误 : Exception {
        public Exception_严重错误(string Message) : base(Message) { }
    }
    /// <summary>
    /// 单线程触发，不影响整体机翻
    /// </summary>
    public class Exception_API异常 : Exception {
        public Exception_API异常(string Message) : base(Message) { }
    }
}
