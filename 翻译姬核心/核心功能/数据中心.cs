using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace 翻译姬;
public delegate void 文件完成机翻();
public class 文件结构 {

    private 全局设置数据 全局设置数据 => 全局数据.全局设置数据;
    private GPT设置数据 GPT设置数据 => 全局数据.GPT设置数据;

    public string 路径 {
        get => _路径;
        set {
            _路径 = value;
            if (相对路径 == null) {
                var s = _路径.Replace(全局设置数据.读取目录, "");
                s = Regex.Replace(s, @"^[\\/]", "");
                相对路径 = s;
            }
        }
    }
    private string _路径;
    public string 相对路径;
    public string 文件名 => new FileInfo(路径).Name;
    public 读取方式 读取方式;//也用作于标明文件格式

    public string[] 原文本行 = new string[0];//普通文本的全文本
    public string Json文本;
    public string Xml文本;

    public object _lock = new object();
    public int 已机翻文本组数 = 0;
    public 文本[] 有效文本;//所有提取类型提取出的有效文本，文本组生成前/续翻前
    public 文本组[] 文本组 = new 文本组[0];//提取后的
    public 文本处理数据 处理数据 = new 文本处理数据();

    [JsonIgnore]
    public 文件完成机翻 文件完成机翻;

    public void 读取后数据处理(读取方式 读取方式, ref string 读取编码) {
        if (读取方式 == 读取方式.文本读取) {
            原文本行 = 文本读写.读取文本行(路径, ref 读取编码);
            有效文本 = 正则读写.正则文本提取(原文本行, 处理数据.正则.Rows[0]);
        } else if (读取方式 == 读取方式.Json读取) {
            Json文本 = 文本读写.读取文本(路径, ref 读取编码);
            有效文本 = 文本读写.Json提取(Json文本, 处理数据.Json指令.Rows[0], 处理数据.正则.Rows[0]);
        } else if (读取方式 == 读取方式.Xml读取) {
            Xml文本 = 文本读写.读取文本(路径, ref 读取编码);
            有效文本 = 文本读写.Xml提取(Xml文本, 处理数据.Xml指令.Rows[0], 处理数据.正则.Rows[0]);
        }
        var rows = 处理数据.替换列表.Select("替换时机='机翻前'");
        if (rows.Length > 0) {
            文本替换.替换(有效文本, 替换类型.原文, rows.CopyToDataTable());
        }
        //有效文本.对话检索();
    }

    public void 机翻前数据处理() {
        //生成文本组
        var res = new List<文本组>();
        int 下标 = 0;
        foreach (文本[] 分割后 in 分割文本(处理数据.API类型, 有效文本)) {
            文本组 文本 = new 文本组();
            文本.序号 = 下标++;
            文本.文本 = 分割后;
            for (int i = 0; i < 分割后.Length; i++) {
                分割后[i].文本组中下标 = i;
            }
            文本.文本组已完成机翻 += () => {//数据和业务未解耦导致的屎山代码位置
                lock (_lock) {
                    已机翻文本组数++;
                    if (已机翻文本组数 == 文本组.Length) {
                        文件完成机翻?.Invoke();
                    }
                }
            };
            res.Add(文本);
        }
        文本组 = res.ToArray();
    }

    public void 机翻后数据处理(机翻方式 机翻方式) {
        var rows = 处理数据.替换列表.Select("替换时机='机翻后'");
        if (机翻方式 != 机翻方式.不机翻) {
            if (rows.Length > 0) {
                文本替换.替换(有效文本, 替换类型.译文, rows.CopyToDataTable());
            }
            有效文本.文本检查(处理数据.API类型.Name == typeof(GPTAPI).Name);
            完成状态检查();
        } else {
            foreach (var 文本 in 有效文本) {
                文本.译文 = 文本.原文;
            }
            foreach (var 文本组 in 文本组) {
                文本组.完成状态 = true;
            }
            if (rows.Length > 0) {
                文本替换.替换(有效文本, 替换类型.译文, rows.CopyToDataTable());
            }
        }
    }

    public void 写出() {
        if (读取方式 == 读取方式.文本读取) {
            string[] 写入文本 = 正则读写.正则文本写入(原文本行, 有效文本, 处理数据.正则.Rows[0]);
            文本读写.写出文本行(this, 写入文本);
        } else if (读取方式 == 读取方式.Json读取) {
            string json = 文本读写.Json写入(Json文本, 有效文本, 处理数据.Json指令.Rows[0], 处理数据.正则.Rows[0]);
            文本读写.写出文本(this, json);
        } else if (读取方式 == 读取方式.Xml读取) {
            string xml = 文本读写.Xml写入(Xml文本, 有效文本, 处理数据.Xml指令.Rows[0], 处理数据.正则.Rows[0]);
            文本读写.写出文本(this, xml);
        }
    }

    /// <summary>
    /// 机翻数据检查
    /// </summary>
    public void 完成状态检查() {
        foreach (var 文本组 in 文本组) {
            var f = true;
            foreach (var 文本 in 文本组.文本) {
                if (文本.异常状态 != 文本异常状态.无) {
                    f = false;
                    break;
                }
            }
            文本组.完成状态 = f;
        }
    }
    public IEnumerable<文本[]> 分割文本(Type API类型, 文本[] 原文本) {
        if (API类型.Name == typeof(百度API).Name) {
            return Util.分割数组(原文本, 总字符上限: 2000, 百度模式: true);

        } else if (API类型.Name == typeof(阿里云API).Name) {
            return Util.分割数组(原文本, 50, -1, 1000);

        } else if (API类型.Name == typeof(腾讯云API).Name) {
            return Util.分割数组(原文本, 总字符上限: 5999);

        } else if (API类型.Name == typeof(火山API).Name) {
            return Util.分割数组(原文本, 16, 5000);

        } else if (API类型.Name == typeof(GPTAPI).Name) {
            if (GPT设置数据.单次机翻行 < 1) {
                throw new Exception($"请填写正确的GPT单次机翻行数");
            }
            return GPTAPI.文本分割(原文本);

        }
        throw new Exception($"API类型错误：{API类型}");
    }
    public static Type 获取API类型(string API名称 = null) {
        API名称 = API名称 ?? 全局数据.全局设置数据.使用机翻;
        return API名称 switch {
            "百度" => typeof(百度API),
            "阿里云" => typeof(阿里云API),
            "腾讯云" => typeof(腾讯云API),
            "GPT" => typeof(GPTAPI),
            "火山" => typeof(火山API),
            _ => throw new Exception($"不存在API名称【{全局数据.全局设置数据.使用机翻}】")
        };
    }

}
public delegate void 文本组已完成机翻();
//多个文本对象分为一组
public class 文本组 {
    public int 序号;
    public bool 完成状态;
    public bool 机翻状态 {
        get => _机翻状态;
        set {
            _机翻状态 = value;
            if (_机翻状态) {
                文本组已完成机翻?.Invoke();
            }
        }
    }
    private bool _机翻状态;
    public 文本[] 文本;
    public event 文本组已完成机翻 文本组已完成机翻;

    public void 强制完成() {//强制改为完成状态，译文填充原文
        if (完成状态) {
            return;
        }
        if (!机翻状态) {
            foreach (var 文本 in 文本) {
                文本.译文 = 文本.原文;
                文本.异常状态 = 文本异常状态.返回原文;
            }
        }
    }

}
public class 文本 {
    public int 文本组中下标;//该文本在所在文本组中的下标
    public int 文本下标;//该文本在整个文件中的下标
    public 文本类型 文本类型 = 文本类型.独白;
    public string 人名;
    public string 原文;
    public string 译文 = "";
    public 文本异常状态 异常状态 = 文本异常状态.无;

    public 文本(int 文本下标, string 原文) {
        this.文本下标 = 文本下标;
        this.原文 = 原文;
    }

}
public enum 文本异常状态 {
    无,
    空值,
    返回原文,
    存在漏翻,
    多换行符
}
public enum 文本类型 {
    人名,//纯人名
    对话,
    独白
}
//对文件结构处理的2类数据
public class 文本处理数据 {
    public Type API类型;
    public DataTable 替换列表 {
        get => _替换列表;
        set {
            if (value.Columns.Count == 0) {//反序列化必须有row才会生成列
                value.Columns.Add("ID", typeof(int));
                value.Columns.Add("名称");
                value.Columns.Add("是否启用", typeof(bool));
                value.Columns.Add("替换时机");
                value.Columns.Add("匹配行为");
                value.Columns.Add("替换列表");
            }
            _替换列表 = value;
        }
    }
    private DataTable _替换列表;
    public DataTable 正则;
    public DataTable Json指令;
    public DataTable Xml指令;
}
public static class 文本拓展方法 {

    public static string[] 获取原文组(this 文本[] arr) {
        return arr.Select(t => t.原文).ToArray();
    }

    public static string[] 获取译文组(this 文本[] arr) {
        return arr.Select(t => t.译文).ToArray();
    }

    public static void 设置译文(this 文本[] arr, string[] 译文) {
        if (arr.Length != 译文.Length) {
            throw new Exception("机翻后译文行数与原文不匹配");
        }
        int index = 0;
        foreach (var t in arr) {
            t.译文 = 译文[index++];
        }
    }

    public static void 文本检查(this 文本[] 文本, bool 是否GPT = false) {
        foreach (var t in 文本) {
            if (t.文本类型 == 文本类型.人名) {
                if (t.译文.IsNullOrEmpty()) {
                    t.译文 = t.原文;
                }
                if (是否GPT && 全局数据.GPT设置数据.输出人名优先词汇表) {
                    //GPT从GPT词汇表尝试读取
                    var 词汇表row = 全局数据.GPT设置数据.GPT词汇表.AsEnumerable().Where(r => r["原文"].ToString() == t.原文).LastOrDefault();
                    if (词汇表row != null) {
                        t.译文 = 词汇表row["译文"].ToString();
                    }
                }
                continue;
            }
            if (!全局数据.全局设置数据.无视返回空值 && t.译文.IsNullOrEmpty()) {
                t.异常状态 = 文本异常状态.空值;
            } else if (!全局数据.全局设置数据.无视返回原文 && t.译文 == t.原文) {
                t.异常状态 = 文本异常状态.返回原文;
            } else if (t.异常状态 != 文本异常状态.存在漏翻 && t.异常状态 != 文本异常状态.多换行符) {
                //这2个作为GPT专属，不在这里检测
                t.异常状态 = 文本异常状态.无;
            }
        }
    }

    /// <summary>
    /// 没机翻的使用原文替代
    /// </summary>
    /// <param name="文本组"></param>
    public static void 强制生成机翻(this 文本组[] 文本组) {
        foreach (var 文本 in 文本组) {
            if (文本.机翻状态) {
                continue;
            }
            foreach (var t in 文本.文本) {
                t.译文 = t.原文;
            }
        }
    }

    public static void 对话检索(this 文本[] 文本arr) {
        //确认是对话还是独白
        文本 最后人名 = null;
        for (int i = 0; i < 文本arr.Length; i++) {
            var 文本 = 文本arr[i];
            if (文本.文本类型 == 文本类型.人名) {
                最后人名 = 文本;
                continue;
            }
            if (最后人名 == null) {
                continue;
            }
            if (文本.文本下标 == 最后人名.文本下标 || 文本.文本下标 - 1 == 最后人名.文本下标) {
                文本arr.设置同行为对话(最后人名, 文本.文本下标);
            }
            最后人名 = null;
        }
    }

    private static void 设置同行为对话(this 文本[] 全文本, 文本 人名文本, int 文本下标) {
        foreach (var t in 全文本) {
            if (t.文本下标 == 文本下标 && t.文本类型 == 文本类型.独白) {
                t.人名 = 人名文本.人名;
                t.文本类型 = 文本类型.对话;
            }
        }
    }

}
