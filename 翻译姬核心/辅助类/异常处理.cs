using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 翻译姬;
public delegate void 翻译姬核心异常处理(string text);
public static class 异常处理 {

    private static object _lock = new object();
    public static event 翻译姬核心异常处理 翻译姬核心异常处理;

    public static void 错误处理(string text) {
        lock (_lock) {
            翻译姬核心异常处理?.Invoke(text);
        }
    }


}
