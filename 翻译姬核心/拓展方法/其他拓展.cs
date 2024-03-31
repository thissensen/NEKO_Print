using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace 翻译姬 {
    public static class 其他拓展 {

        public static T[] AddRange<T>(this T[] arr, params T[] val) {
            T[] res = new T[arr.Length + val.Length];
            Array.Copy(arr, res, arr.Length);
            Array.Copy(val, 0, res, arr.Length, val.Length);
            return res;
        }
        public static DataTable CopyToDataTable(this DataRow[] rows) {
            if (rows == null || rows.Length == 0) {
                throw new Exception("DataRow不能为空");
            }
            DataTable dt = new DataTable();
            foreach (DataColumn col in rows[0].Table.Columns) {
                dt.Columns.Add(col.ColumnName, col.DataType);
            }
            foreach (var row in rows) {
                var dr = dt.NewRow();
                dr.ItemArray = row.ItemArray;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public static IEnumerable<DataRow> AsEnumerable(this DataTable dt) {
            foreach (DataRow row in dt.Rows) {
                yield return row;
            }
        }
        /// <summary>
        /// 获取kv，name-值
        /// </summary>
        public static Dictionary<string, string> 获取提取结果(this GroupCollection groups) {
            var dic = new Dictionary<string, string>();
            for (int i = 1; i < groups.Count; i++) {
                dynamic 提取结果 = groups[i];
                string 提取text = 提取结果.Value;
                string 提取name = 提取结果.Name;
                if (提取name == "name") {
                    dic.Add(提取name, 提取text);
                }
            }
            for (int i = 1; i < groups.Count; i++) {
                dynamic 提取结果 = groups[i];
                string 提取text = 提取结果.Value;
                string 提取name = 提取结果.Name;
                if (提取name != "name") {
                    dic.Add(提取name, 提取text);
                }
            }
            return dic;
        }

        public static string 获取XPath(this XmlNode node) {
            var sc = new Stack<string>();
            XmlNode p = node.ParentNode;
            while (p != null) {
                sc.Push(p.Name);
                p = p.ParentNode;
            }
            sc.Pop();
            StringBuilder sb = new StringBuilder();
            while (sc.Count > 0) {
                sb.Append($"/{sc.Pop()}");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 对xml节点进行换行，格式化对齐操作
        /// </summary>
        /// <param name="srcXml"></param>
        /// <returns></returns>
        public static string 格式化Xml(this string srcXml) {
            return IndentedFormat(IndentedFormat(srcXml).Replace("><", ">\r\n<"));
        }

        /// <summary>
        /// 对XML字符串进行换行缩进，格式化
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private static string IndentedFormat(string xml) {
            string indentedText = string.Empty;
            try {
                XmlTextReader reader = new XmlTextReader(new StringReader(xml));
                reader.WhitespaceHandling = WhitespaceHandling.None;

                StringWriter indentedXmlWriter = new StringWriter();
                XmlTextWriter writer = CreateXmlTextWriter(indentedXmlWriter);
                writer.WriteNode(reader, false);
                writer.Flush();

                indentedText = indentedXmlWriter.ToString();
            } catch (Exception) {
                indentedText = xml;
            }
            return indentedText;
        }

        /// <summary>
        /// 写入四个缩进字符【空格】
        /// </summary>
        /// <param name="textWriter"></param>
        /// <returns></returns>
        private static XmlTextWriter CreateXmlTextWriter(TextWriter textWriter) {
            XmlTextWriter writer = new XmlTextWriter(textWriter);
            //将Tab转化为4个空格
            bool convertTabsToSpaces = true;
            if (convertTabsToSpaces) {
                writer.Indentation = 4;
                writer.IndentChar = ' ';
            } else {
                writer.Indentation = 1;
                writer.IndentChar = '\t';
            }
            writer.Formatting = System.Xml.Formatting.Indented;
            return writer;
        }

    }
}
