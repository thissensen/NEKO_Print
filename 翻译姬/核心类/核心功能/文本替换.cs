using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace 翻译姬;
public class 文本替换 {

    public static string[] 替换(string[] 替换文本, DataTable 替换列表, int 单线程处理数 = 100) {
        //计算替换数据
        List<替换数据> 替换数据 = (from row in 替换列表.AsEnumerable()
                select new 替换数据() {
                    匹配行为 = (匹配行为)Enum.Parse(typeof(匹配行为), row["匹配行为"].ToString()),
                    替换列表 = JsonConvert.DeserializeObject<Dictionary<string, string>>(row["替换列表"].ToString())
                }).ToList();
        if (替换文本.Length == 0 || 替换数据.Count == 0) {
            return 替换文本;
        }
        if (单线程处理数 < 1) {
            throw new Exception("单线程处理数不符合要求");
        }
        //替换
        线程池 pool = new 线程池(Environment.ProcessorCount);
        int num = 0;
        var 已完成队列 = new BlockingCollection<文本队列>();
        foreach (string[] arr in 工具类.分割数组(替换文本, 单线程处理数)) {
            文本队列 队列 = new 文本队列();
            队列.序号 = num++;
            队列.文本 = arr;
            Task t = new Task(() => {
                for (int i = 0; i < arr.Length; i++) {
                    foreach (替换数据 data in 替换数据) {
                        if (data.匹配行为 == 匹配行为.正则) {
                            foreach (var kv in data.替换列表) {
                                arr[i] = Regex.Replace(arr[i], kv.Key, kv.Value);
                            }
                        } else if (data.匹配行为 == 匹配行为.包含) {
                            foreach (var kv in data.替换列表) {
                                arr[i] = arr[i].Replace(kv.Key, kv.Value);
                            }
                        } else if (data.匹配行为 == 匹配行为.全字) {
                            foreach (var kv in data.替换列表) {
                                if (arr[i] == kv.Key) {
                                    arr[i] = kv.Value;
                                }
                            }
                        }
                    }
                }
                已完成队列.Add(队列);
            });
            pool.添加线程(t);
        }
        pool.执行并等待();
        //合并
        return 已完成队列.OrderBy(t => t.序号).SelectMany(t => t.文本).ToArray();
    }

}
public struct 文本队列 {
    public int 序号;
    public string[] 文本;
}
public struct 替换数据 {
    public 匹配行为 匹配行为;
    public Dictionary<string, string> 替换列表;
}
public enum 匹配行为 {
    正则,
    包含,
    全字
}
