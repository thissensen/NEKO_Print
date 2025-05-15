using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 翻译姬 {
    public static class Image拓展 {

        public static byte[] ToBytes(this Image img) {
            using (MemoryStream ms = new MemoryStream()) {
                img.Save(ms, img.RawFormat);
                return ms.ToArray();
            }
        }
    }
}
