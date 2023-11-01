using Newtonsoft.Json;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 翻译姬;
public class 文件结构 {

    private 全局设置数据 全局设置数据 => 全局数据.全局设置数据;
    private GPT设置数据 GPT设置数据 => 全局数据.GPT设置数据;

    public string 路径;
    public string 文件名 => new FileInfo(路径).Name;
    public string Json文本;
    public string Xml文本;
    public string[] 原文本行 = new string[0];//文本行，普通文本的全文本或Json提取的文本
    public string[] 新文本行 = new string[0];//处理完的文本行
    public 文本组[] 文本组 = new 文本组[0];//提取后的
    public 文本处理数据 处理数据 = new 文本处理数据();

    /// <summary>
    /// 机翻流程：正则提取 -> 机翻前替换 -> API分割
    /// </summary>
    public void 生成机翻前文本组() {
        string[] 提取后 = 正则读写.正则文本提取(原文本行, 处理数据.正则row);
        string[] 替换后 = 文本替换.替换(提取后, 处理数据.替换列表.Select("替换时机='机翻前'").CopyToDataTable());
        var res = new List<文本组>();
        int 下标 = 0;
        foreach (string[] 分割后 in 分割文本(处理数据.API类型, 替换后)) {
            文本组 文本 = new 文本组();
            文本.序号 = 下标++;
            文本.原文 = 分割后;
            res.Add(文本);
        }
        文本组 = res.ToArray();
    }
    /// <summary>
    /// 机翻流程：分割后合并 -> 机翻后替换 -> 正则封入
    /// </summary>
    public void 生成机翻后文本组() {
        string[] 合并译文 = 文本组.获取译文组();
        string[] 替换后文本行 = 文本替换.替换(合并译文, 处理数据.替换列表.Select("替换时机='机翻后'").CopyToDataTable());
        新文本行 = 正则读写.正则文本写入(原文本行, 替换后文本行, 处理数据.正则row);
    }
    /// <summary>
    /// 机翻数据检查
    /// </summary>
    public void 处理机翻后数据() {
        foreach (var 文本 in 文本组) {
            if (文本.译文 == null) {
                continue;
            }
            if (文本.译文.Length != 文本.原文.Length) {
                文本.异常状态.Add((文本异常状态.错行, -1));
                continue;
            }
            for (int i = 0; i < 文本.原文.Length; i++) {
                if (!全局设置数据.无视返回原文 && 文本.译文[i] == 文本.原文[i]) {
                    文本.异常状态.Add((文本异常状态.返回原文, i));
                }
                if (!全局设置数据.无视返回空值 && string.IsNullOrWhiteSpace(文本.译文[i])) {
                    文本.异常状态.Add((文本异常状态.空值, i));
                }
            }
            if (文本.异常状态.Count == 0) {
                文本.完成状态 = true;
            }
        }
    }
    private IEnumerable<string[]> 分割文本(Type API类型, string[] 原文本) {
        if (API类型.Name == typeof(百度API).Name) {
            return 工具类.分割数组(原文本, 总字符上限: 2000 - 原文本.Length + 1);

        } else if (API类型.Name == typeof(阿里云API).Name) {
            return 工具类.分割数组(原文本, 50, -1, 1000);

        } else if (API类型.Name == typeof(腾讯云API).Name) {
            return 工具类.分割数组(原文本, 总字符上限: 5999);

        } else if (API类型.Name == typeof(GPTAPI).Name) {
            if (GPT设置数据.单次机翻行 < 1) {
                throw new Exception($"请填写正确的GPT单次机翻行数");
            }
            return 工具类.分割数组(原文本, GPT设置数据.单次机翻行);

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
            _ => throw new Exception($"不存在API名称【{全局数据.全局设置数据.使用机翻}】")
        };
    }
    
}
public class 文本组 {
    public int 序号;
    public bool 完成状态;
    public List<(文本异常状态, int)> 异常状态 = new List<(文本异常状态, int)>();
    public string[] 原文;
    public string[] 译文;//可能无法与原文对齐

    public string[] 获取强制对齐译文() {//强制改为完成状态，译文填充原文
        if (完成状态) {
            return 译文;
        }
        if (译文 == null) {
            return 原文;
        }
        if (原文.Length == 译文.Length) {
            return 译文;
        }
        if (译文?.Length > 原文.Length) {
            return 译文.Take(原文.Length).ToArray();
        } else {
            return 译文.AddRange(原文.Skip(译文.Length).ToArray());
        }
    }

}
public enum 文本异常状态 {
    错行,
    空值,
    返回原文
}
//对文件结构处理的2类数据
public class 文本处理数据 {
    public DataTable 替换列表;
    public DataRow 正则row;
    public Type API类型;
    public DataRow Json指令row;
    public DataRow Xml指令row;
}
public static class 文本组拓展 {
    
    public static string[] 获取原文组(this 文本组[] arr) {
        var res = new List<string>();
        foreach (var item in arr.OrderBy(t => t.序号)) {
            res.AddRange(item.原文);
        }
        return res.ToArray();
    }
    public static string[] 获取译文组(this 文本组[] arr) {
        var res = new List<string>();
        foreach (var item in arr.OrderBy(t => t.序号)) {
            res.AddRange(item.获取强制对齐译文());
        }
        return res.ToArray();
    }
}
public class KeyValue<T1, T2> {
    public T1 Key;
    public T2 Value;
}
