using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml;

namespace 翻译姬;
public class Xml指令 : Json指令 {

    protected override string 关联表 => "Xml指令";

    protected override Type 指令集数值更改窗 => typeof(Xml指令集数值更改);

    public Xml指令() {
        Text = "Xml指令";
        选择JsonBtn.Text = "选择Xml";
    }

    protected override void 选择JsonBtn_Click(object sender, EventArgs e) {
        string[] paths = 工具类.选择文件("请选择Xml文件", "Xml文件", "*.txt;*.xml");
        if (paths.Length == 0) {
            return;
        }
        try {
            string text = File.ReadAllText(paths[0], Encoding.GetEncoding(Util.文本编码识别(paths[0])));
            XmlDocument xml = new XmlDocument();
            try {
                xml.LoadXml(text);
            } catch { throw new Exception("文本非Xml格式"); }
            指令表格.Tag = text;
            DataTable dt = 指令表格.DataTable.Clone();
            var stack = new Stack<XmlNode>();
            stack.Push(xml.DocumentElement);
            while (stack.Count > 0) {
                var node = stack.Pop();
                if (node.HasChildNodes) {
                    for (int i = node.ChildNodes.Count - 1; i >= 0; i--) {
                        stack.Push(node.ChildNodes[i]);
                    }
                } else {
                    DataRow row = dt.NewRow();
                    row["指令"] = node.获取XPath();
                    row["内容"] = node.InnerText;
                    dt.Rows.Add(row);
                }
            }
            指令表格.DataTable = dt;
        } catch (Exception ex) {
            消息框帮助.轻便消息(ex.Message, 查询表格, UINotifierType.WARNING);
        }
    }

    protected override void 指令表格_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
        if (e.RowIndex < 0 || e.ColumnIndex != 0) {
            return;
        }
        string 指令 = 指令表格.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        if (!指令集Lines.Contains(指令)) {
            指令集Lines = 指令集Lines.AddRange(指令);
        }
    }

    protected override void 提取Btn_Click(object sender, EventArgs e) {
        try {
            if (指令表格.DataTable.Rows.Count == 0) {
                throw new Exception("请先选择Xml文件");
            }
            if (指令集Box.Text.IsNullOrEmpty()) {
                throw new Exception("指令集未填写");
            }
            DataRow 指令row = 查询表格.DataTable.Clone().NewRow();
            指令row["指令集"] = string.Join("|", 指令集Lines);
            文本[] res = 文本读写.Xml提取(指令表格.Tag.ToString(), 指令row);
            提取结果Box.Text = string.Join(Environment.NewLine, res.获取原文组());

        } catch (Exception ex) {
            消息框帮助.轻便消息(ex.Message, 查询表格, UINotifierType.WARNING);
        }
    }


}
