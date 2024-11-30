using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace 翻译姬;
public class GPTAPI : API接口模板 {

    private static GPT设置数据 GPT设置数据 => 全局数据.GPT设置数据;
    public override int QPS显示单位 => 60000;//QPS以分钟作限制

    private GPT调用 GPT调用;
    GPT数据处理接口 数据处理;
    public GPTAPI(API信息 data) : base(data) {
        if (GPT设置数据.Sakura机翻) {
            数据处理 = new Sakura调用();
        } else if (GPT设置数据.简易模式) {
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
            GPT设置数据.frequency_penalty,
            GPT设置数据.temperature,
            GPT设置数据.top_p,
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
            int 最大token = 全局数据.BPE算法.Token计算(JsonConvert.SerializeObject(请求内容));
机翻开始:
            try {
                返回结果 = GPT调用.调用(请求内容, 最大token);
                if (返回结果.Usage.TotalTokens == 最大token) {
                    throw new Exception("返回token达到最大token限制，疑似退化，重试");
                }
                机翻字符增加(返回结果.Usage.TotalTokens);
                解析结束的请求 = 数据处理.返回值解析(返回结果.Choices[0].Message.Content, 原请求, false);//List<GPT请求>
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
                    异常处理.错误处理($"{ex.Message}:{返回结果?.Choices[0].Message.Content}");
                }
                goto 机翻开始;
            }
            //润色
            if (GPT设置数据.翻后润色) {
                var 待润色请求 = 数据处理.获取待润色数据(原请求, 解析结束的请求);
                var 待润色请求内容 = 数据处理.获取请求内容(true, 待润色请求);
润色开始:
                try {
                    var 润色返回结果 = GPT调用.调用(待润色请求内容, 最大token);
                    if (返回结果.Usage.TotalTokens == 最大token) {
                        throw new Exception("返回token达到最大token限制，疑似退化，重试");
                    }
                    机翻字符增加(润色返回结果.Usage.TotalTokens);
                    解析结束的请求 = 数据处理.返回值解析(润色返回结果.Choices[0].Message.Content, 待润色请求, true);
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
                        异常处理.错误处理($"{ex.Message}:{返回结果?.Choices[0].Message.Content}");
                    }
                    goto 润色开始;
                }
            }
            //漏翻检测并标记漏翻的句子
            if (GPT设置数据.漏翻检测 && 数据处理.漏翻检测(解析结束的请求)) {
                if (漏翻重试次数 == GPT设置数据.漏翻重试次数) {
                    异常处理.错误处理("漏翻重试次数已达上限");
                } else {
                    异常处理.错误处理("GPT检测到漏翻，重试");
                    漏翻重试次数++;
                    goto 机翻开始;
                }
            }
            //写入文本
            数据处理.添加上文内容(原请求, 解析结束的请求);
            数据处理.请求写入文本(解析结束的请求, 待机翻);
        } catch (Exception ex) {
            if (GPT设置数据.错误跳过) {
                数据处理.清空上文内容();
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
        if (GPT调用.已进行token限制) {
            GPT调用.已进行token限制 = false;
            机翻执行次数 = 0;
        } else {
            机翻执行次数++;
        }
    }

    public static IEnumerable<文本[]> 文本分割(文本[] arr) {
        //使用键值对索引提速，linq嵌套会消耗大量性能
        var 下标分组 = (from 文本 in arr
                    where 文本.文本类型 != 文本类型.人名
                    group 文本 by 文本.文本下标 into g
                    select g).ToDictionary(t => t.Key, t => t);
        var 对话结果 = new List<文本[]>();//一组视为一个对话
        文本 人名文本 = null;
        int 上级文本下标 = -2;
        for (int i = 0; i < arr.Length; i++) {
            var 文本 = arr[i];
            if (文本.文本类型 == 文本类型.人名) {//人名不算做对话组
                if (人名文本 != null) {
                    对话结果.Add(new 文本[] { 人名文本 });//连续人名会导致返回的文本数量与有效文本不一致
                }
                人名文本 = 文本;
                if (i == arr.Length - 1) {
                    对话结果.Add(new 文本[] { 人名文本 });//极端情况，连续人名，且处于文本最后
                }
                continue;
            }
            if (GPT设置数据.连续对话合并 && 文本.文本下标 == 上级文本下标) {
                continue;
            }
            if (GPT设置数据.连续对话合并) {
                if (GPT设置数据.相邻对话合并 && 文本.文本下标 == 上级文本下标 + 1) {
                    var 文本arr = 下标分组[文本.文本下标];
                    var 最后对话list = 对话结果.Last().ToList();
                    最后对话list.AddRange(文本arr);
                    对话结果[对话结果.Count - 1] = 最后对话list.ToArray();//填充到最后对话中
                    上级文本下标 = 文本.文本下标;
                    continue;
                }

            }
            if (GPT设置数据.连续对话合并) {
                var 原文组 = 下标分组[文本.文本下标].ToList();
                if (人名文本 != null) {
                    原文组.Insert(0, 人名文本);
                    人名文本 = null;
                }
                对话结果.Add(原文组.ToArray());
            } else {
                if (人名文本 != null) {
                    对话结果.Add(new 文本[] { 人名文本, 文本 });
                    人名文本 = null;
                } else {
                    对话结果.Add(new 文本[] { 文本 });
                }
            }
            上级文本下标 = 文本.文本下标;
        }


        for (int i = 0; i < 对话结果.Count; i += GPT设置数据.单次机翻行) {
            int 拷贝长度 = Math.Min(GPT设置数据.单次机翻行, 对话结果.Count - i);
            var temp = new 文本[拷贝长度][];
            对话结果.CopyTo(i, temp, 0, 拷贝长度);//Copy方案比linq快无数倍
            var res = new List<文本>();
            foreach (文本[] 文本arr in temp) {
                res.AddRange(文本arr);
            }
            yield return res.ToArray();
        }
        
    }

    //废弃的原算法
    /*public static IEnumerable<文本[]> 文本分割2(文本[] arr) {
        //使用键值对索引提速，linq嵌套会消耗大量性能
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
    }*/


}
