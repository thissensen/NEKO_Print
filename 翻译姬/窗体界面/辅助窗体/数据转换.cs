using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬;
public partial class 数据转换 : 自定义Form {

    private static readonly string name_message = "name-message";
    private Dictionary<string, string> 导出预览 = new Dictionary<string, string>() {
        [name_message] = """
        [
            {
                "name":"xx",//不是对话就没有
                "meassage":"xxxxxxxxx"
            },
            {
                "message":"xxxxxxxxx"
            }
            ……
        ]
        """
    };

    public 数据转换() {
        InitializeComponent();
    }

    private void 数据导出Form_Load(object sender, EventArgs e) {
        this.窗体相对屏幕居中();
        Name_Control.Remove(类型预览Box.Name);
        转换类型Box.DataSource = 导出预览.Keys.ToList();
        if (文本翻译.处理中文件结构 == null) {
            全部导出Btn.Enabled = false;
            全部导入Btn.Enabled = false;
        }
    }

    private void 转换类型Box_TextChanged(object sender, EventArgs e) {
        if (转换类型Box.Text == "") {
            return;
        }
        类型预览Box.Text = 导出预览[转换类型Box.Text];
    }

    private void 返回Btn_Click(object sender, EventArgs e) {
        Close();
    }

    private void 全部导出Btn_Click(object sender, EventArgs e) {
        try {
            string file = 工具类.选择文件夹("导出文件夹", 全局字符串.桌面路径).ElementAt(0);
            if (file == "") {
                return;
            }
            foreach (var 文件 in 文本翻译.处理中文件结构) {
                if (转换类型Box.Text == name_message) {
                    name_message导出(file, 文件);
                }
            }
            消息框帮助.轻便消息("导出成功", this);
        } catch (Exception ex) {
            MessageBoxEx.Show(ex.Message);
        }
    }

    private void 全部导入Btn_Click(object sender, EventArgs e) {
        try {
            string file = 工具类.选择文件夹("导入文件夹", 全局字符串.桌面路径).ElementAt(0);
            if (file == "") {
                return;
            }
            var files = 文本读写.获取文件列表(file);
            foreach (var f in files) {
                if (转换类型Box.Text == name_message) {
                    name_message导入(file, f);
                }
            }
            MessageBoxEx.Show("导入成功，请前往[数据处理]页面进行检查并保存");
        } catch (Exception ex) {
            MessageBoxEx.Show(ex.Message);
        }
    }

    private void name_message导出(string 输出目录, 文件结构 文件) {
        if (!Regex.IsMatch(输出目录, @"[\\/]$")) {
            输出目录 += "\\";
        }
        var res = new List<Name_Message>();
        var gpt请求 = 工具类.文本转请求(文件.有效文本);
        foreach (var g in gpt请求) {
            var n = new Name_Message();
            n.name = g.name;
            n.message = g.src;
            res.Add(n);
        }
        var file = 输出目录 + 文件.相对路径;
        file.创建父目录();
        File.WriteAllText(file, JsonConvert.SerializeObject(res, Formatting.Indented));
    }

    private void name_message导入(string 输入目录, string 输入路径) {
        var 相对路径 = 输入路径.Replace(输入目录, "");
        相对路径 = Regex.Replace(相对路径, @"^[\\/]", "");
        var 文件 = 文本翻译.处理中文件结构.SingleOrDefault(t => t.相对路径 == 相对路径);
        if (文件 == null) {
            throw new Exception("找不到对应的文件用于覆盖，请点击预显示后再次尝试");
        }
        List<Name_Message> res = null;
        try {
            res = JsonConvert.DeserializeObject<List<Name_Message>>(File.ReadAllText(输入路径));
        } catch {
            throw new Exception("Json格式转换错误，请勿改变Json格式以及编码");
        }
        if (res.Count != 文件.有效文本.Length) {
            throw new Exception($"导入数量不一致,导入数量:{res.Count},原数量:{文件.有效文本.Length}");
        }
        for (int i = 0; i < res.Count; i++) {
            var n = res[i];
            var 文本 = 文件.有效文本[i];
            文本.译文 = n.message;
            if (n.name != null && 文本.人名 != null) {
                文本.人名 = n.name;
            }
        }
    }
}
public class Name_Message {
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string name { get; set; }
    public string message { get; set; }
}