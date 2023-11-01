using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace 翻译姬;
public partial class GPT设置 : 自定义Page {

    private GPT设置数据 GPT设置数据 => 全局数据.GPT设置数据;
    private Dictionary<string, string> 预设Json = new Dictionary<string, string> {
        ["日语"] = """
        {
        "0":"a=\"　　ぞ…ゾンビ系…。",
        "1":"敏捷性が上昇する。　　　　　　　\r\n効果：パッシブ",
        "2":"【ベーカリー】営業時間8：00～18：00",
        "3":"&f.Item[f.Select_Item][1]+'　個'",
        "4":"ちょろ……ちょろろ……\nじょぼぼぼ……♡",
        "5": "さて！",
        "6": "さっそくオジサンをいじめちゃおっかな！",
        "7": "若くて♫⚡綺麗で♫⚡エロくて"
        }
        """,
        ["英语"] = """
        {
        "0":"a=\"　　It's so scary….",
        "1":"Agility increases.　　　　　　　\r\nEffect: Passive",
        "2":"【Bakery】Business hours 8:00-18:00",
        "3":"&f.Item[f.Select_Item][1]",
        "4":"Gurgle…Gurgle…\nDadadada…♡",
        "5": "Well then!",
        "6": "Let's bully the uncle right away!",
        "7": "Young ♫⚡beautiful ♫⚡sexy."
        }
        """,
        ["韩语"] = """
        {
        "0":"a=\"　　정말 무서워요….",
        "1":"민첩성이 상승한다.　　　　　　　\r\n효과：패시브",
        "2":"【빵집】영업 시간 8:00~18:00",
        "3":"&f.Item[f.Select_Item][1]",
        "4":"둥글둥글…둥글둥글…\n둥글둥글…♡",
        "5": "그래서!",
        "6": "지금 바로 아저씨를 괴롭히자!",
        "7": "젊고♫⚡아름답고♫⚡섹시하고"
        }
        """,
        ["繁中"] = """
        {
        "0":"a=\"　　好可怕啊……。",
        "1":"提高敏捷性。　　　　　　　\r\n效果：被動",
        "2":"【麵包店】營業時間8：00～18：00",
        "3":"&f.Item[f.Select_Item][1]+'　個'",
        "4":"咕嚕……咕嚕嚕……\n哒哒哒……♡",
        "5": "那麼！",
        "6": "現在就來欺負一下大叔吧！",
        "7": "年輕♫⚡漂亮♫⚡色情"
        }
        """,
        ["简中"] = """
        {   
        "0":"a=\"　　好可怕啊……。",
        "1":"提高敏捷性。　　　　　　　\r\n效果：被动",
        "2":"【面包店】营业时间8：00～18：00",
        "3":"&f.Item[f.Select_Item][1]+'　个'",
        "4":"咕噜……咕噜噜……\n哒哒哒……♡",
        "5": "那么！",
        "6": "现在就来欺负一下大叔吧！",
        "7": "年轻♫⚡漂亮♫⚡色情"
        }
        """,
        ["自定义"] = ""
    };

    public GPT设置() {
        InitializeComponent();

        预设原文选择Box.Tag = 预设原文Box;
        预设返回选择Box.Tag = 预设返回Box;
        
    }

    private void GPT设置_Load(object sender, EventArgs e) {
        连接域名Box.DataBindings.Add("Text", GPT设置数据, "连接域名", false, DataSourceUpdateMode.OnPropertyChanged);
        使用模型Box.DataBindings.Add("Text", GPT设置数据, "使用模型", false, DataSourceUpdateMode.OnPropertyChanged);
        次数限制Box.DataBindings.Add("Text", GPT设置数据, "次数限制", false, DataSourceUpdateMode.OnPropertyChanged);
        Token限制Box.DataBindings.Add("Text", GPT设置数据, "Token限制", false, DataSourceUpdateMode.OnPropertyChanged);
        请求等待延迟Box.DataBindings.Add("Text", GPT设置数据, "请求等待延迟", false, DataSourceUpdateMode.OnPropertyChanged);
        单次机翻行Box.DataBindings.Add("Text", GPT设置数据, "单次机翻行", false, DataSourceUpdateMode.OnPropertyChanged);
        错行重试数Box.DataBindings.Add("Text", GPT设置数据, "错行重试数", false, DataSourceUpdateMode.OnPropertyChanged);
        
        使用多线程Switch.DataBindings.Add("Active", GPT设置数据, "使用多线程", false, DataSourceUpdateMode.OnPropertyChanged);
        上下文提示Switch.DataBindings.Add("Active", GPT设置数据, "上下文提示", false, DataSourceUpdateMode.OnPropertyChanged);
        发送预设Switch.DataBindings.Add("Active", GPT设置数据, "发送预设", false, DataSourceUpdateMode.OnPropertyChanged);
        
        //模型词表Box.DataBindings.Add("Text", GPT设置数据, "模型词表", false, DataSourceUpdateMode.OnPropertyChanged);
        语境Box.DataBindings.Add("Text", GPT设置数据, "语境", false, DataSourceUpdateMode.OnPropertyChanged);
        预设原文Box.DataBindings.Add("Text", GPT设置数据, "预设原文", false, DataSourceUpdateMode.OnPropertyChanged);
        预设返回Box.DataBindings.Add("Text", GPT设置数据, "预设返回", false, DataSourceUpdateMode.OnPropertyChanged);
        模型词表Box.TextChanged += (_, _) => GPT设置数据.模型词表 = 模型词表Box.Text;

        预设原文选择Box.DataSource = 预设Json.Keys.ToList();
        预设返回选择Box.DataSource = 预设Json.Keys.ToList();
        GPT设置_Page被选中();
    }

    private void GPT设置_Page被选中() {
        //刷新Text，同步到全局设置
        foreach (var kv in this.Name_Control) {
            if (kv.Value is UITextBox box) {
                var temp = box.Text;
                box.Text = "";
                box.Text = temp;
            }
        }
        模型词表Box.Text = GPT设置数据.模型词表;
    }

    private void 模型词表Btn_Click(object sender, EventArgs e) {
        string[] paths = 工具类.选择文件("请选择Tiktoken文件", "Tiktoken文件", "*.tiktoken");
        if (paths.Length == 0) {
            return;
        }
        try {
            模型词表Box.Text = paths[0];
        } catch (Exception ex) {
            消息框帮助.轻便消息(ex.Message, this, UINotifierType.WARNING);
        }
    }

    private void 下载Btn_Click(object sender, EventArgs e) {
        Process.Start(@"https://openaipublic.blob.core.windows.net/encodings/cl100k_base.tiktoken");
    }

    private void 预设选择下拉框_TextChanged(object sender, EventArgs e) {
        UIComboBox box = sender as UIComboBox;
        if (box.Text == "") {
            return;
        }
        UITextBox textBox = box.Tag as UITextBox;
        if (box.Text != "自定义") {
            textBox.Text = 预设Json[box.Text];
            textBox.Enabled = false;
        } else {
            textBox.Enabled = true;
        }
    }

    private void 使用多线程Switch_ActiveChanged(object sender, EventArgs e) {
        if (使用多线程Switch.Active && 上下文提示Switch.Active) {
            使用多线程Switch.Active = false;
            MessageBoxEx.Show("多线程与上下文冲突，无法同时使用");
        }
    }

    private void 上下文提示Switch_ActiveChanged(object sender, EventArgs e) {
        if (上下文提示Switch.Active) {
            if (使用多线程Switch.Active) {
                上下文提示Switch.Active = false;
                MessageBoxEx.Show("上下文与多线程冲突，无法同时使用");
            } else if (发送预设Switch.Active) {
                上下文提示Switch.Active = false;
                MessageBoxEx.Show("上下文与预设内容冲突，无法同时使用");
            }
        }
    }

    private void 发送预设Switch_ActiveChanged(object sender, EventArgs e) {
        if (发送预设Switch.Active && 上下文提示Switch.Active) {
            发送预设Switch.Active = false;
            MessageBoxEx.Show("预设内容与上下文冲突，无法同时使用");
        }
    }
}
