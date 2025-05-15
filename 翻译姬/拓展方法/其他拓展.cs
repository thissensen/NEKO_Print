using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace 翻译姬 {
    public static class 其他拓展 {

        public static void 窗体相对屏幕居中(this Form f) {
            //显示位置屏幕居中
            var rect = Screen.GetWorkingArea(f);
            var p = new Point((rect.Width - f.Width) / 2, (rect.Height - f.Height) / 2);
            f.Location = p;
        }

        public static void AddRange(this SqlParameterCollection sqlParameterCollection, Dictionary<string, object> dic) {
            if (dic == null) {
                return;
            }
            foreach (KeyValuePair<string, object> item in dic) {
                sqlParameterCollection.AddWithValue(item.Key, item.Value ?? DBNull.Value);
            }
        }

    }
}
