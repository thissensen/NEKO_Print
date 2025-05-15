using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 翻译姬 {
    public static class Bytes拓展 {

        /// <summary>
        /// 该方法的Image不能Save，会GDI+错误
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static Image ToImage(this byte[] buf) {
            if (buf == null || buf.Length == 0) {
                return null;
            }
            Image img;
            using (MemoryStream ms = new MemoryStream(buf)) {
                img = Image.FromStream(ms);
            }
            return img;
        }

        public static IntPtr ToIntPtr(this byte[] buf) {
            return System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(buf, 0);

        }
       
    }
}
