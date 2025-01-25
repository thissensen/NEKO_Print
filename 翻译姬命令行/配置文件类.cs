using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using 翻译姬;

namespace 翻译姬命令行;
public class 配置文件操作 {

    public 全局设置数据? 全局设置数据 { get; set; }
    public GPT设置数据? GPT设置数据 { get; set; }

    public static void 常规读取(DataTable dt, string[] 文本行, string 名称列名 = "名称") {
        var 已读取行 = new List<string>();
        var 名称正则 = new Regex(@"^\[(.*?)\].*$");
        string 当前名称 = null;
        DataRow row = null;
        var 已存文本行 = new List<string>();
        foreach (string line in 文本行) {
            if (名称正则.IsMatch(line)) {
                if (row != null) {
                    提取(row, 已存文本行.ToArray());//填充到row中
                    已存文本行.Clear();
                    dt.Rows.Add(row);
                }
                当前名称 = 名称正则.Match(line).Groups[1].Value;
                row = dt.NewRow();
                row[名称列名] = 当前名称;
            } else if (row != null) {
                已存文本行.Add(line);
            }
        }
        if (row != null) {
            提取(row, 已存文本行.ToArray());
            dt.Rows.Add(row);
        }
    }    
    //API明细、Json指令、Xml指令、替换列表
    public static string 常规写出(DataRow row) {
        var sb = new StringBuilder();
        foreach (DataColumn col in row.Table.Columns) {
            if (col.ColumnName == "id") {
                continue;
            }
            if (col.ColumnName == "名称" || col.ColumnName == "正则名称") {
                sb.AppendLine($"[{row[col.ColumnName]}]");
            } else {
                string[] arr = row[col.ColumnName].ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length <= 1) {
                    sb.Append(col.ColumnName).Append('=').AppendLine(string.Join(Environment.NewLine, arr));
                } else {
                    sb.Append(col.ColumnName).AppendLine("=").AppendLine(string.Join(Environment.NewLine, arr));
                }
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// 将以下格式提取到row对应列中
    /// [名称]
    /// 列名1=xxx
    /// 列名2=
    /// xx
    /// xx
    /// </summary>
    /// <param name="row"></param>
    /// <param name="文本行">传入适配一个DataRow的行组</param>
    /// <param name="合并函数"></param>
    private static void 提取(DataRow row, string[] 文本行, Func<List<string>, string> 合并函数 = null) {
        if (合并函数 == null) {
            合并函数 = arr => {
                return string.Join("|", arr);
            };
        }
        string 当前填充列名 = null;
        var 已提取 = new List<string>();
        foreach (string line in 文本行) {
            if (line == "") {//遇到空行直接结束
                if (已提取.Count != 0) {//结束前填充数据
                    row[当前填充列名] = 合并函数(已提取);
                    已提取.Clear();
                }
                当前填充列名 = null;
            } else {
                foreach (DataColumn col in row.Table.Columns) {
                    if (line .StartsWith(col.ColumnName + "=")) {
                        if (已提取.Count != 0) {//遇到换列填充数据
                            row[当前填充列名] = 合并函数(已提取);
                            已提取.Clear();
                        }
                        if (line.Length != col.ColumnName.Length + 1) {
                            //提取等号后边的数据
                            已提取.Add(Regex.Match(line, @"^[^=]+=(.*)$").Groups[1].Value);
                        }
                        当前填充列名 = col.ColumnName;
                        goto 循环结束;
                    }
                }
            }
            if (当前填充列名 != null) {
                已提取.Add(line);
            }
循环结束:
            continue;

        }
        if (已提取.Count != 0) {
            row[当前填充列名] = 合并函数(已提取);
        }
    }

}
