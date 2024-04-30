﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 翻译姬;
public class GPTAPI : API接口模板 {

    private static GPT设置数据 GPT设置数据 => 全局数据.GPT设置数据;
    public override int QPS显示单位 => 60000;//QPS以分钟作限制

    private GPT调用 GPT调用;
    GPT数据处理接口 数据处理;
    public GPTAPI(API信息 data) : base(data) {
        if (GPT设置数据.简易模式) {
            数据处理 = new 序列号调用();
        } else {
            数据处理 = new Src_Dst调用();
        }
        数据处理.GPT设置数据 = GPT设置数据;//为了将来多类型GPT做准备，故手动赋值

        data.QPS = GPT设置数据.次数限制;
        GPT设置数据.必要数据验证();
        GPT调用 = new GPT调用(
            计时器, 
            GPT设置数据.Token限制, 
            GPT设置数据.是否Https,
            GPT设置数据.连接域名,
            GPT设置数据.连接路由,
            GPT设置数据.使用模型, 
            GPT设置数据.请求等待延迟, 
            data.秘钥
        );
    }

    protected override string[] 机翻(string[] 传入文本) {
        throw new NotImplementedException();
    }

    public override void 单组执行完() {
        数据处理.清空上文内容();
    }

    private int 漏翻重试次数 = 0;
    private int 异常重试次数 = 0;
    public void GPT机翻(文本[] 待机翻) {
        try {
            //生成请求
            var 原请求 = 数据处理.文本转请求(待机翻);//List<GPT请求>
            var 请求内容 = 数据处理.获取请求内容(false, 原请求);//List<dynamic>，机翻用的
            //机翻
            GPT返回 返回结果 = null;
            dynamic 解析结束的请求;
机翻开始:
            try {
                返回结果 = GPT调用.调用(请求内容);
                机翻执行次数++;//用于QPS的限制
                机翻字符增加(返回结果.Usage.TotalTokens);
                解析结束的请求 = 数据处理.返回值解析(返回结果.Choices[0].Message.Content, 原请求);//List<GPT请求>
            } catch (Exception_API异常) {
                throw;
            } catch (Exception ex) {
                if (异常重试次数 == GPT设置数据.异常重试上限) {
                    throw new Exception("异常重试次数已达上限");
                }
                异常重试次数++;
                if (返回结果 == null) {
                    异常处理.错误处理(ex.Message);
                } else {
                    异常处理.错误处理($"{ex.Message}{返回结果?.Choices[0].Message.Content}");
                }
                goto 机翻开始;
            }
            //润色
            if (GPT设置数据.翻后润色) {
                var 待润色请求 = 数据处理.获取待润色数据(原请求, 解析结束的请求);
                var 待润色请求内容 = 数据处理.获取请求内容(true, 待润色请求);
润色开始:
                try {
                    var 润色返回结果 = GPT调用.调用(待润色请求内容);
                    机翻执行次数++;
                    机翻字符增加(润色返回结果.Usage.TotalTokens);
                    解析结束的请求 = 数据处理.返回值解析(润色返回结果.Choices[0].Message.Content, 待润色请求);
                } catch (Exception_API异常) {
                    throw;
                } catch (Exception ex) {
                    if (异常重试次数 == GPT设置数据.异常重试上限) {
                        throw new Exception("异常重试次数已达上限");
                    }
                    异常重试次数++;
                    异常处理.错误处理(ex.Message);
                    goto 润色开始;
                }
            }
            //漏翻检测并标记漏翻的句子
            if (GPT设置数据.漏翻检测 && 数据处理.漏翻检测(解析结束的请求)) {
                if (漏翻重试次数 == GPT设置数据.漏翻重试次数) {
                    throw new Exception("漏翻重试次数已达上限");
                }
                异常处理.错误处理("GPT检测到漏翻，重试");
                漏翻重试次数++;
                goto 机翻开始;
            }
            //写入文本
            数据处理.添加上文内容(原请求, 解析结束的请求);
            数据处理.请求写入文本(解析结束的请求, 待机翻);
        } catch (Exception ex) {
            if (GPT设置数据.错误跳过) {
                //跳过严重异常，继续机翻
                异常处理.错误处理($"已跳过严重错误:{ex.Message}");
                return;
            }
            //不跳过严重异常，当前线程停止机翻
            throw new Exception_API异常(ex.Message);
        } finally {
            漏翻重试次数 = 0;
            异常重试次数 = 0;
        }
    }

    protected override void 机翻字符增加(int 字符数) {
        base.机翻字符增加(字符数);
        GPT调用.已用token += 字符数;
    }

    public static IEnumerable<文本[]> 文本分割(文本[] arr) {
        //使用键值对索引提速，linq嵌套会消耗大类性能
        var 下标分组 = (from 文本 in arr
                    where 文本.文本类型 != 文本类型.人名
                    group 文本 by 文本.文本下标 into g
                    select g).ToDictionary(t => t.Key, t => t);
        var res = new List<文本>();
        int 已存对话组 = 0, 最后合并id = -1;
        for (int i = 0; i < arr.Length; i++) {
            var 文本 = arr[i];
            if (文本.文本类型 == 文本类型.人名) {//人名不算做对话组
                res.Add(文本);
                continue;
            }
            if (GPT设置数据.连续对话合并) {
                if (文本.文本下标 == 最后合并id) {
                    continue;
                }
                var 文本arr = 下标分组[文本.文本下标];
                res.AddRange(文本arr);
                最后合并id = 文本.文本下标;
                已存对话组++;
            } else {
                已存对话组++;
            }
            if (已存对话组 == GPT设置数据.单次机翻行) {
                yield return res.ToArray();
                res.Clear();
                已存对话组 = 0;
            }
        }
        if (res.Count > 0) {
            yield return res.ToArray();
        }
    }

}
