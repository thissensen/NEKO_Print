using Sunny.UI;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 翻译姬;
/*
 * 普通错误：仅输出一下，尝试重复机翻
 * API异常：当前API账号无法继续机翻，不影响整体机翻
 * 
 */
public class 标准机翻 {

    private static 数据库连接 数据库 => 全局数据.数据库;

    /// <summary>
    /// 用于存储机翻内容
    /// </summary>
    public static ConcurrentDictionary<string, string> 原文_机翻 = new ConcurrentDictionary<string, string>();

    public void 机翻(文件结构 文件) {
        if (文件.文本组.Length == 0) {
            return;
        }
        //数据获取
        Type API类型 = 文件.处理数据.API类型;
        string API名称 = API类型.Name.Substring(0, API类型.Name.Length - 3);//去掉API3个字
        DataTable API明细 = 数据库.Select($"select * from API明细 where 是否启用=1 and 类型='{API名称}'");
        if (API明细.Rows.Count == 0) {
            throw new Exception($"【{API名称}】没有可用的账号，请前往添加并开启");
        }
        原文_机翻.Clear();
        //文本组转为文本栈
        var 文本栈 = new ConcurrentStack<文本组>();
        var 排序后文本组 = 文件.文本组.OrderBy(t => t.序号);
        for (int i = 文件.文本组.Length - 1; i >= 0; i--) {
            文本栈.Push(排序后文本组.ElementAt(i));
        }
        //机翻
        int 线程数 = API明细.Rows.Count;
        bool 是否机翻完 = false;
        int 机翻完成数 = 0;
        if (API名称 == "GPT" && !全局数据.GPT设置数据.使用多线程) {
            线程数 = 1;
        }
        线程池 pool = new 线程池(线程数);
        foreach (DataRow 明细row in API明细.Rows) {
            API信息 data = API信息.Parse(明细row);
            API接口模板 api = Activator.CreateInstance(API类型, data) as API接口模板;
            var t = new Task(() => {
                while (!是否机翻完) {
                    文本组 文本;
                    if (!文本栈.TryPop(out 文本)) {
                        Thread.Sleep(100);
                        continue;
                    }
                    //取到了
                    try {
                        文本.译文 = api.文本机翻(文本.原文);//机翻
                        //进度条增加
                        工具类.多线程进度条增加(文本.原文.Length);
                        机翻完成数++;
                    } catch (Exception ex) {
                        错误处理.普通错误处理($"机翻过程错误:{ex.Message}");
                        文本栈.Push(文本);
                        break;
                    }
                    //判断机翻完成情况
                    if (机翻完成数 == 排序后文本组.Count()) {
                        是否机翻完 = true;
                    }
                }
            });
            pool.添加线程(t);
        }
        pool.执行并等待();
        if (文本栈.Count > 0) {
            throw new Exception("所有账号均出现异常，无法继续机翻");
        }
    }

}
