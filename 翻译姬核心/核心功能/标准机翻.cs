using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 翻译姬;

public class 标准机翻 {

    private static 全局设置数据 全局设置数据 => 全局数据.全局设置数据;
    private static 数据库连接 数据库 => 全局数据.数据库;

    /// <summary>
    /// 用于存储机翻内容
    /// </summary>
    public static ConcurrentDictionary<string, string> 原文_机翻 = new ConcurrentDictionary<string, string>();

    public static void 机翻(params 文件结构[] 文件组) {
        if (文件组.Length == 0) {
            return;
        }
        var 临时文件 = 文件组.First();
        //数据获取
        Type API类型 = 临时文件.处理数据.API类型;
        string API名称 = API类型.Name.Substring(0, API类型.Name.Length - 3);//去掉API3个字
        DataTable API明细 = 数据库.Select($"select * from API明细 where 是否启用=1 and 类型='{API名称}'");
        if (API明细.Rows.Count == 0) {
            throw new Exception($"【{API名称}】没有可用的账号，请前往添加并开启");
        }
        原文_机翻.Clear();
        //待机翻数据整理
        var 文本栈 = new ConcurrentStack<文本组[]>();
        var 文本组arr = 文件切割(文件组).ToArray();//普通多线程一个数组只有1个
        for (int i = 文本组arr.Length - 1; i >= 0; i--) {
            文本栈.Push(文本组arr.ElementAt(i));
        }
        //开始机翻
        if (File.Exists(全局数据.缓存数据路径)) {
            File.Delete(全局数据.缓存数据路径);
        }
        int 线程数 = API明细.Rows.Count;
        bool 是否机翻完 = 文本组arr.Length == 0;
        int 机翻完成数 = 0;
        线程池 pool = new 线程池(API明细.Rows.Count);
        foreach (DataRow 明细row in API明细.Rows) {
            API信息 data = API信息.Parse(明细row);
            for (int i = 0; i < 全局设置数据.单账号线程数; i++) {//单账号复用线程数
                API接口模板 api = Activator.CreateInstance(API类型, data) as API接口模板;
                var t = new Task(() => {
                    while (!全局数据.是否中止 && !是否机翻完) {
                        文本组[] 文本arr;
                        if (!文本栈.TryPop(out 文本arr)) {
                            Thread.Sleep(100);
                            continue;
                        }
                        try {
                            foreach (文本组 文本 in 文本arr) {
                                if (全局数据.是否中止) {
                                    throw new Exception("机翻被中止");
                                }
                                if (文本.机翻状态) {
                                    continue;
                                }
                                api.文本机翻(文本.文本);//机翻
                                文本.机翻状态 = true;
                                //进度条增加
                                Util.多线程进度条增加(文本.文本.Length);
                            }
                            api.单组执行完();
                            机翻完成数++;
                        } catch (Exception ex) {
                            异常处理.错误处理($"机翻过程错误:{ex.Message}");
                            文本栈.Push(文本arr);
                            break;
                        }
                        //判断机翻完成情况
                        if (机翻完成数 == 文本组arr.Length) {
                            是否机翻完 = true;
                        }
                    }
                });
                pool.添加线程(t);
            }
        }
        pool.执行并等待();
        if (文本栈.Count > 0) {
            File.WriteAllText(全局数据.缓存数据路径, JsonConvert.SerializeObject(全局数据.处理中文件结构, Formatting.Indented));
            throw new Exception("所有线程均已停止机翻，已将进度保存到缓存");
        }
    }

    //根据组上限切割
    public static IEnumerable<文本组[]> 文件切割(params 文件结构[] 文件组) {
        if (!全局设置数据.文本级多线程) {
            if (全局设置数据.启用单组上限) {
                var 全文本组 = new List<文本组>();
                foreach (文件结构 文件 in 文件组) {
                    foreach (文本组 文本 in 文件.文本组.OrderBy(t => t.序号)) {
                        if (文本.机翻状态) {
                            continue;
                        }
                        全文本组.Add(文本);
                    }
                }
                var res = new List<文本组>();
                foreach (var 文本 in 全文本组) {
                    res.Add(文本);
                    if (res.Count == 全局设置数据.API单组上限) {
                        yield return res.ToArray();
                        res.Clear();
                    }
                }
                if (res.Count > 0) {
                    yield return res.ToArray();
                }
            } else {
                foreach (文件结构 文件 in 文件组) {
                    foreach (文本组 文本 in 文件.文本组.OrderBy(t => t.序号)) {
                        if (文本.机翻状态) {
                            continue;
                        }
                        yield return new 文本组[] { 文本 };
                    }
                }
            }
        } else {//文本级多线程按照文本单组上限进行分割
            var res = new List<文本组>();
            foreach (文件结构 文件 in 文件组) {
                foreach (文本组 文本 in 文件.文本组) {
                    if (文本.机翻状态) {
                        continue;
                    }
                    res.Add(文本);
                    if (全局设置数据.启用单组上限 && res.Count == 全局设置数据.API单组上限) {
                        yield return res.ToArray();
                        res.Clear();
                    }
                }
                if (res.Count > 0) {
                    yield return res.ToArray();
                    res.Clear();
                }
            }
            if (res.Count > 0) {
                yield return res.ToArray();
            }
        }
        yield break;
    }

}
