using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace 翻译姬 {
    public static class 其他拓展 {

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
