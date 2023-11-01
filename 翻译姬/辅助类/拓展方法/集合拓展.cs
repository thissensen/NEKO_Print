using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 翻译姬 {
    public static class 集合拓展 {
        public static T[] AddRange<T>(this T[] arr, params T[] val) {
            T[] res = new T[arr.Length + val.Length];
            Array.Copy(arr, res, arr.Length);
            Array.Copy(val, 0, res, arr.Length, val.Length);
            return res;
        }
    }
}