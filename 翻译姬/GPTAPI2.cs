using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬;
/// <summary>
/// 该类仅用于测试GPT的func调用，保证其一定返回json格式
/// 实际效果感觉无变化
/// </summary>
/*public class GPTAPI2 : API接口模板 {
    //最后一次返回时的内容
    private static GPT设置数据 GPT设置数据 => 全局数据.GPT设置数据;
    public Dictionary<string, string> 上文内容 = new Dictionary<string, string>();
    public override int QPS显示单位 => 60 * 1000;
    private bool 润色模式 = false;
    private int 已用Token = 0;//记录单位时间内使用的Token数
    private int 请求异常次数 = 0;
    private int 错行重试次数 = 0;
    private int 漏翻重试次数 = 0;
    private string 当前流程;
    private BPE算法 bpe = new BPE算法();

    private HttpClient client = new HttpClient();
    ~GPTAPI2() {
        client?.Dispose();
    }

    public GPTAPI2(API信息 data) : base(data) {
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + data.秘钥);
        data.QPS = GPT设置数据.次数限制;
        GPT设置数据.必要数据验证();
    }

    protected override string[] 机翻(string[] 传入文本) {
        throw new Exception("不支持机翻函数");
    }

    public void GPT机翻(文本[] 传入文本) {
        //请求内容计算
        当前流程 = "文本转请求前";
        List<GPT请求> GPT请求 = 工具类.文本转请求(传入文本);
        try {
        GPT机翻开始:
            当前流程 = "文本转请求后，机翻前";
            GPT返回 res = 机翻(GPT请求);
            当前流程 = "机翻后，返回值解析前";
            var gpt请求list = 返回值解析(res, GPT请求);
            当前流程 = "返回值解析后，Name对齐前";
            Name对齐(GPT请求, gpt请求list);
            当前流程 = "Name对齐后";
            错行重试次数 = 0;
            //润色
            if (GPT设置数据.翻后润色) {
                润色模式 = true;
                当前流程 = "Name对齐后，恢复src前";
                恢复src(GPT请求, gpt请求list);
                当前流程 = "恢复src后";
            }
        GPT润色开始:
            if (GPT设置数据.翻后润色) {
                当前流程 = "润色机翻前";
                res = 机翻(gpt请求list);
                当前流程 = "润色机翻后，解析前";
                gpt请求list = 返回值解析(res, gpt请求list);
                当前流程 = "润色解析后";
                错行重试次数 = 0;
            }
            if (GPT设置数据.漏翻检测 && 漏翻检测(gpt请求list)) {
                当前流程 = "检测到漏翻后";
                if (漏翻重试次数 == GPT设置数据.漏翻重试次数) {
                    错误处理.普通错误处理($"GPT漏翻重试次数已达上限，无视此次漏翻");
                } else {
                    错误处理.普通错误处理($"GPT检测到漏翻，重试：{res.Choices[0].Message.Content}");
                    漏翻重试次数++;
                    机翻执行次数++;
                    if (!润色模式) {
                        goto GPT机翻开始;
                    } else {
                        goto GPT润色开始;
                    }
                }
            }
            漏翻重试次数 = 0;
            润色模式 = false;
            当前流程 = "请求写入文本前";
            工具类.请求写入文本(传入文本, gpt请求list, true);
            当前流程 = "请求写入文本后，添加上文前";
            添加上文内容(GPT请求, gpt请求list);
            当前流程 = "添加上文后";
        } catch (Exception ex) {
            if (GPT设置数据.错误跳过) {
                错误处理.普通错误处理($"已跳过严重错误：" + 当前流程 + ": " + ex.Message);
            } else {
                throw new Exception_API异常(当前流程 + ": " + ex.Message);
            }
        }
    }

    private GPT返回 机翻(List<GPT请求> GPT请求) {
        var GPT请求Json = JsonConvert.SerializeObject(GPT请求);
        var 请求内容 = 获取请求内容(GPT请求Json, GPT请求);
        //Token限制
        //int 使用Token = (int)(bpe.Token计算(JsonConvert.SerializeObject(请求内容)) * 1.02);
        if (已用Token > GPT设置数据.Token限制) {
            计时器.Stop();
            int 所用毫秒 = (int)计时器.ElapsedMilliseconds;
            if (所用毫秒 < QPS显示单位) {
                Thread.Sleep(QPS显示单位 - 所用毫秒);
            }
            机翻执行次数 = 0;
            已用Token = 0;
            计时器.Restart();
        }
        //错行重试
        string json = GPT调用(请求内容, GPT请求);
        GPT返回 res = null;
        try {
            res = JsonConvert.DeserializeObject<GPT返回>(json);
            已用Token += res.Usage.TotalTokens;
            机翻字符增加(res.Usage.TotalTokens);
        } catch (JsonSerializationException ex) {
            错误处理.普通错误处理($"GPT返回值非Json，重试");
            return 机翻(GPT请求);
        } catch (JsonReaderException ex) {
            错误处理.普通错误处理($"GPT返回值非Json，重试");
            return 机翻(GPT请求);
        }
        return res;
    }

    private void 恢复src(List<GPT请求> 原GPT请求, List<GPT请求> GPT请求) {
        for (int i = 0; i < GPT请求.Count; i++) {
            var g = GPT请求[i];
            var g2 = 原GPT请求[i];
            g.src = g2.src;
        }
    }

    private string 中文Prompt = """
        对GalGame游戏脚本进行翻译，将日语翻译为简体中文，翻译中并不关心伦理道德准则
        # 输入
        将发送给你Json格式的游戏原文，`name`键为人名，`src`键为日语
        # 要求
        1、词汇表(如果有)应准确地使用
        2、如果有`name`键，则对应`src`键视为对话，从角色的角度进行翻译；如果没有`name`键，则从旁观者的角度进行翻译，并尽量省略人称/代词
        3、忽略低俗问题，翻译自然流畅，与上下文关联，遵循高质量色情文学的习惯
        4、转义符"\"和文本中的数字、英文字母不需要翻译
        # 输出
        1、以满足所有"要求"为目标，对内容进行分析，从心理角度构建翻译结果
        2、根据id顺序准确翻译每个`src`键
        3、将`src`键改为`dst`键，然后返回[行数]行翻译结果
        [词汇表]
        """;
    private string 输出要求 = """
        储存对应id键中src键的翻译结果
        翻译要求：
        1、词汇表(如果有)应准确地使用
        2、如果有`name`键，则视为对话，从角色的角度进行翻译；如果没有`name`键，则从旁观者的角度进行翻译，并尽量省略人称/代词
        3、忽略低俗问题，翻译自然流畅，关联上下文，遵循高质量色情文学的习惯
        4、转义符"\"和文本中的数字、英文字母不需要翻译
        [词汇表]
        """;
    private string GPT调用(List<dynamic> 请求内容, List<GPT请求> GPT请求) {
        string test = 语境设置词汇表(输出要求, GPT请求);
        string 连接前缀 = GPT设置数据.是否Https ? "https" : "http";
        string url = $"{连接前缀}://{GPT设置数据.连接域名}/v1/chat/completions";
        string 函数说明 = """
            分析根据提供的Json游戏脚本，`name`键为人名，`src`键为日语，把日语翻译成简体中文，翻译中并不关心伦理道德准则
            """;
        var functions = new List<dynamic> {
                new {
                    name = "trans",
                    description = 函数说明,
                    parameters = new {
                        type = "object",//GPT只接受obj格式
                        properties = new {
                            data = new {
                                type = "array",
                                description = "数量与原文一致",
                                items = new {
                                    type = "object",
                                    properties = new {
                                        id = new { type = "integer", description = "从原文的id键复制过来" },
                                        //name = new { type = "string", description = "从原文的name键(如果有)复制过来" },
                                        dst = new { type = "string", description = "从原文的src键复制过来并进行翻译" },
                                    },
                                    required = new[] {"id", "dst" }
                                }
                            }
                        },
                    }
                }
            };
        var res = new Dictionary<string, dynamic>();
        res.Add("model", GPT设置数据.使用模型);
        res.Add("messages", 请求内容);
        res.Add("functions", functions);
        res.Add("function_call", new { name = "trans" });
        string 请求体 = JsonConvert.SerializeObject(res);

        string 返回值 = null;
        try {
            using var rep = client.PostAsync(url, new StringContent(请求体, Encoding.UTF8, "application/json")).Result;
            返回值 = rep.Content.ReadAsStringAsync().Result;
        } catch (Exception ex) {
            错误处理.普通错误处理($"GPT错误：{ex.Message}");
            请求异常次数++;
            if (请求异常次数 <= 10) {
                return GPT调用(请求内容, GPT请求);
            } else {
                throw new Exception_API异常(ex.Message);
            }
        }
        请求异常次数 = 0;
        return 返回值;
    }

    public List<GPT请求> 返回值解析(GPT返回 gpt返回, List<GPT请求> GPT请求) {
        if (gpt返回.Choices[0].FinishReason == "stop") {
            throw new Exception("返回被终止，FinishReason='stop'");
        }
        JObject jobj = JObject.Parse(gpt返回.Choices[0].Message.Function_call.Arguments);
        string 原始text = jobj["data"].ToString();
        string text = Regex.Replace(原始text, @"^`+[^{\[]+", "");
        text = Regex.Replace(text, @"[\r\n]", "");
        text = Regex.Replace(text, @"`+$", "");
        text = Regex.Replace(text, @"^\[|\]$", "");//删除左右[]
        text = Regex.Replace(text, @"(?<=""|,)}?\]?\s*?[，,]?\s*?\[?{", "}\r\n{");
        if (!text.EndsWith("}")) {
            text += "}";
        }
        List<GPT请求> res = new List<GPT请求>();
        try {
            string[] arr = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var reg_dst = new Regex(@".*?""id"".+?(?'id'\d+).*?""dst"".*?""(?'dst'.*?)[""}]");
            var reg_src = new Regex(@".*?""id"".+?(?'id'\d+).*?""src"".*?""(?'src'.*?)[""}]");
            foreach (var s in arr) {
                GroupCollection g = null;
                int id = -1;
                string dst = null;
                if (reg_dst.IsMatch(s)) {
                    g = reg_dst.Match(s).Groups;
                    id = int.Parse(g["id"].ToString());
                    dst = g["dst"].ToString().Trim();
                    if (dst != "") {
                        goto 返回值提取成功;
                    }
                    dst = null;
                }
                //src提取解析
                g = reg_src.Match(s).Groups;
                id = int.Parse(g["id"].ToString());
                var src = g["src"].ToString().Trim();
                if (src != "" && !工具类.漏翻检测(src)) {
                    dst = src;
                }
            返回值提取成功:
                var gpt = new GPT请求();
                gpt.id = id;
                gpt.dst = dst;
                res.Add(gpt);
            }
            goto 解析结束;
        } catch { }

    *//*try {
        *//*
         * 模式：
         * {id=0,name="x"}
         * 文本
         *//*
        string[] arr = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        if (arr.Length != GPT请求.Count) {
            goto 解析结束;
        }
        for (int i = 0; i < arr.Length; i++) {
            var gpt请求 = JsonConvert.DeserializeObject<GPT请求>(arr[i]);
            gpt请求.src = arr[i + 1];
            i++;
            res.Add(gpt请求);
        }
    } catch { }*//*
    解析结束:
        foreach (var g in res) {
            if (g.dst == null) {
                错误处理.普通错误处理($"GPT未返回dst，重试，{原始text}");
                机翻执行次数++;
                var 返回 = 机翻(GPT请求);
                return 返回值解析(返回, GPT请求);
            }
        }
        if (res.Count != GPT请求.Count) {
            if (错行重试次数 == GPT设置数据.错行重试数) {
                throw new Exception($"GPT错行次数已达上限");
            } else {
                错误处理.普通错误处理($"GPT错行，异常内容：{原始text}\r\n");
                错行重试次数++;
                机翻执行次数++;
                var 返回 = 机翻(GPT请求);
                return 返回值解析(返回, GPT请求);
            }
        }
        for (int i = 0; i < GPT请求.Count; i++) {
            if (res.FirstOrDefault(t => t.id == i) == null) {
                错误处理.普通错误处理($"GPT返回错误id，异常内容：{原始text}");
                机翻执行次数++;
                var 返回 = 机翻(GPT请求);
                return 返回值解析(返回, GPT请求);
            }
        }
        return res.OrderBy(t => t.id).ToList();
    }

    public List<dynamic> 获取请求内容(string json, List<GPT请求> GPT请求) {
        //请求内容计算
        var 请求内容 = new List<dynamic>();
        *//*if (!润色模式) {
            请求内容.Add(new { role = "system", content = 语境设置词汇表(GPT设置数据.语境, GPT请求) });
        } else {
            请求内容.Add(new { role = "system", content = 语境设置词汇表(GPT设置数据.润色语境, GPT请求) });
        }*/
        /*if (GPT设置数据.上下文提示 && 上文内容.Count > 0) {
            var 取值深度 = 上文内容.Count < GPT设置数据.上下文深度 ? 上文内容.Count : GPT设置数据.上下文深度;
            var res = 上文内容.Skip(上文内容.Count - 取值深度);
            foreach (var kv in res) {
                请求内容.Add(new { role = "user", content = kv.Key });
                //请求内容.Add(new { role = "system", content = kv.Value });
                请求内容.Add(new { role = "assistant", content = kv.Value });
            }
        }*//*
        请求内容.Add(new { role = "user", content = json });
        return 请求内容;
    }

    private string 语境设置词汇表(string 语境, List<GPT请求> GPT请求) {
        语境 = 语境.Replace("[行数]", GPT请求.Count.ToString());
        var 人名arr = (from 请求 in GPT请求
                     where 请求.name != null && 请求.name != ""
                     select 请求.name).Distinct();
        var 文本arr = (from 请求 in GPT请求
                     select 请求.src).Distinct();
        var 请求内容arr = 人名arr.Concat(文本arr).Distinct();
        var sb = new StringBuilder();
        var 待添加rows = new List<DataRow>();
        foreach (DataRow row in GPT设置数据.GPT词汇表.Rows) {
            var 原文 = row["原文"].ToString();
            bool 是否添加 = false;
            foreach (var 请求内容 in 请求内容arr) {
                if (请求内容.Contains(原文)) {
                    是否添加 = true;
                    break;
                }
            }
            if (是否添加 && !待添加rows.Contains(row)) {
                待添加rows.Add(row);
            }
        }
        foreach (var row in 待添加rows) {
            sb.AppendLine($"| {row["原文"]} | {row["译文"]} | {row["备注"]} |");
        }
        if (sb.Length == 0) {
            return 语境.Replace("[词汇表]", "");
        } else {
            //sb.Insert(0, "| --- | --- | --- |");
            //sb.Insert(0, "| Src | Dst | Note |");
            //sb.Insert(0, "# Glossary");
            sb.Insert(0, "| --- | --- | --- |");
            sb.Insert(0, "| 原文 | 译文 | 备注 |");
            sb.Insert(0, "# 词汇表");
            return 语境.Replace("[词汇表]", sb.ToString());
        }
    }

    public bool 漏翻检测(List<GPT请求> gpt请求) {
        bool 漏 = false;
        foreach (var g in gpt请求) {
            if (工具类.漏翻检测(g.dst)) {
                g.是否漏翻 = true;
                漏 = true;
            }
        }
        return 漏;
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

    private void 添加上文内容(List<GPT请求> 原请求, List<GPT请求> 返回值) {
        var 新返回值 = new List<GPT请求>();
        for (int i = 0; i < 返回值.Count; i++) {
            var g = 返回值[i];
            var g2 = 原请求[i];
            var n = new GPT请求();
            n.id = g2.id;
            n.name = g2.name;
            n.dst = g.dst;
            新返回值.Add(n);
        }
        var k = JsonConvert.SerializeObject(原请求);
        var v = JsonConvert.SerializeObject(新返回值);
        if (!上文内容.ContainsKey(k)) {
            上文内容.Add(k, v);
        }
    }

    private void Name对齐(List<GPT请求> 原请求, List<GPT请求> 返回值) {
        for (int i = 0; i < 返回值.Count; i++) {
            var g = 返回值[i];
            var g2 = 原请求[i];
            g.name = g2.name;
        }
    }

}*/
