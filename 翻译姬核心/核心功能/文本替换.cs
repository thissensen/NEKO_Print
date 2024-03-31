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

    public static void 替换(文本[] 替换文本, 替换类型 替换类型, DataTable 替换列表, int 单线程处理数 = 100) {
        //计算替换数据
        List<替换数据> 替换数据 = (from row in 替换列表.AsEnumerable()
                select new 替换数据() {
                    匹配行为 = (匹配行为)Enum.Parse(typeof(匹配行为), row["匹配行为"].ToString()),
                    替换列表 = JsonConvert.DeserializeObject<Dictionary<string, string>>(row["替换列表"].ToString())
                }).ToList();
        if (替换文本.Length == 0 || 替换数据.Count == 0) {
            return;
        }
        if (单线程处理数 < 1) {
            throw new Exception("单线程处理数不符合要求");
        }
        //替换
        线程池 pool = new 线程池(Environment.ProcessorCount);
        int num = 0;
        foreach (文本[] arr in Util.分割数组(替换文本, 单线程处理数)) {
            文本队列 队列 = new 文本队列();
            队列.序号 = num++;
            队列.文本 = arr;
            Task t = new Task(() => {
                for (int i = 0; i < arr.Length; i++) {
                    foreach (替换数据 data in 替换数据) {
                        if (data.匹配行为 == 匹配行为.正则) {
                            foreach (var kv in data.替换列表) {
                                if (替换类型 == 替换类型.原文) {
                                    arr[i].原文 = Regex.Replace(arr[i].原文, kv.Key, kv.Value);
                                } else {
                                    arr[i].译文 = Regex.Replace(arr[i].译文, kv.Key, kv.Value);
                                }
                            }
                        } else if (data.匹配行为 == 匹配行为.包含) {
                            foreach (var kv in data.替换列表) {
                                if (替换类型 == 替换类型.原文) {
                                    arr[i].原文 = arr[i].原文.Replace(kv.Key, kv.Value);
                                } else {
                                    arr[i].译文 = arr[i].译文.Replace(kv.Key, kv.Value);
                                }
                            }
                        } else if (data.匹配行为 == 匹配行为.全字) {
                            foreach (var kv in data.替换列表) {
                                if (替换类型 == 替换类型.原文) {
                                    if (arr[i].原文 == kv.Key) {
                                        arr[i].原文 = kv.Value;
                                    }
                                } else {
                                    if (arr[i].译文 == kv.Key) {
                                        arr[i].译文 = kv.Value;
                                    }
                                }
                            }
                        }
                    }
                }
            });
            pool.添加线程(t);
        }
        pool.执行并等待();
    }

}
public struct 文本队列 {
    public int 序号;
    public 文本[] 文本;
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
public enum 替换类型 {
    原文,
    译文
}
