using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 翻译姬 {
    public delegate void 翻译姬核心使用字符增加(int num);
    /// <summary>
    /// API调用主模块
    /// </summary>
    public abstract class API接口模板 {

        public static bool 是否更新字符数 { get; set; } = true;
        
        public API信息 data { get; set; }

        public Stopwatch 计时器 = new Stopwatch();
        /// <summary>
        /// 默认的QPS以秒做限制，单位毫秒
        /// </summary>
        public virtual int QPS显示单位 { get; set; } = 1000;
        public int 机翻执行次数 {
            get => _机翻执行次数;
            set {
                _机翻执行次数 = value;
                if (_机翻执行次数 == data.QPS) {
                    等待定时();
                    _机翻执行次数 = 0;
                }
            }
        }
        private int _机翻执行次数 = 0;

        public API接口模板(API信息 data) {
            this.data = data;
        }

        public void 文本机翻(文本[] 待机翻) {
            计时器.Start();
            if (data.类型 == "GPT") {
                (this as GPTAPI).GPT机翻(待机翻);//GPT没有重复内容跳过功能
            } else {
                //重复内容跳过
                string[] 机翻完;
                if (全局数据.全局设置数据.重复内容跳过) {
                    var 待机翻替换后 = 待机翻文本替换(标准机翻.原文_机翻, 待机翻);//返回k-v，int-string，已替换，未替换
                    Dictionary<int, string> 已替换 = 待机翻替换后.Item1;
                    Dictionary<int, string> 未替换 = 待机翻替换后.Item2;
                    if (未替换.Count == 0) {//全替换完成
                        机翻完 = 已替换.Select(t => t.Value).ToArray();
                    } else {
                        //机翻未替换
                        string[] 未替换待机翻 = 未替换.Select(t => t.Value).ToArray();
                        string[] 替换后机翻完 = 机翻(未替换待机翻);
                        //合并还原
                        if (未替换待机翻.Length != 替换后机翻完.Length) {
                            throw new Exception($"文本行出现错行，例如GPT等易错行API请勿开启【重复内容跳过】功能");
                        }
                        for (int i = 0; i < 未替换.Count; i++) {
                            var kv = 未替换.ElementAt(i);
                            未替换[kv.Key] = 替换后机翻完[i];//替换回去
                        }
                        机翻完 = 待机翻文本合并还原(已替换, 未替换);
                    }
                    if (机翻完.Length == 待机翻.Length) {//没错行，保存为
                        for (int i = 0; i < 待机翻.Length; i++) {
                            if (标准机翻.原文_机翻.ContainsKey(待机翻[i].原文)) {
                                continue;
                            }
                            bool flag = false;
                            do {
                                flag = 标准机翻.原文_机翻.TryAdd(待机翻[i].原文, 机翻完[i]);
                            } while (!flag);
                        }
                    }
                } else {
                    机翻完 = 机翻(待机翻.获取原文组());//机翻
                }
                待机翻.设置译文(机翻完);
            }
            //为空使用原文
            if (全局数据.全局设置数据.机翻空值使用原文) {
                空值替换为原文(待机翻);
            }
            机翻执行次数++;//QPS控制
        }

        /// <summary>
        /// 3、传入原文返回译文
        /// </summary>
        /// <param name="传入文本"></param>
        /// <returns></returns>
        protected abstract string[] 机翻(string[] 传入文本);
        public virtual void 单组执行完() {//一组的文本都执行完了

        }
        /// <summary>
        /// 记录使用的字符数
        /// </summary>
        /// <param name="字符数"></param>
        public static event 翻译姬核心使用字符增加 翻译姬核心使用字符增加;
        protected virtual void 机翻字符增加(int 字符数) {
            //进行额度判断
            if (data.可用额度 != -1 && data.可用额度 > 0) {
                if (data.已用额度 + 字符数 > data.可用额度) {
                    Util.多线程数据库Exec($"update API明细 set 是否启用=0 where ID={data.ID}");
                    throw new Exception_API异常($"{data.类型}字符已达上限");
                }
            }
            data.已用额度 += 字符数;
            翻译姬核心使用字符增加?.Invoke(字符数);
            //数据库更新
            if (是否更新字符数) {
                Util.多线程数据库Exec($"update API明细 set 已用额度=已用额度+{字符数} where ID={data.ID}");
            }
        }

        private (Dictionary<int, string>, Dictionary<int, string>) 待机翻文本替换(ConcurrentDictionary<string, string> dic, 文本[] 待机翻) {
            Dictionary<int, string> 已替换 = new Dictionary<int, string>();
            Dictionary<int, string> 未替换 = new Dictionary<int, string>();
            for (int i = 0; i < 待机翻.Length; i++) {
                string text = 待机翻[i].原文;
                if (dic.ContainsKey(text)) {
                    已替换.Add(i, dic[text]);
                } else {
                    未替换.Add(i, text);
                }
            }
            return (已替换, 未替换);
        }

        private string[] 待机翻文本合并还原(Dictionary<int, string> 已替换, Dictionary<int, string> 未替换) {
            foreach (var kv in 未替换) {
                已替换.Add(kv.Key, kv.Value);
            }
            //排序返回
            return (from kv in 已替换 orderby kv.Key select kv.Value).ToArray();
        }

        private void 空值替换为原文(文本[] 文本) {
            foreach (var t in 文本) {
                if (t.译文.IsNullOrEmpty()) {
                    t.译文 = t.原文;
                }
            }
        }

        protected void 等待定时() {
            计时器.Stop();
            int 所用毫秒 = (int)计时器.ElapsedMilliseconds;
            if (所用毫秒 < QPS显示单位) {
                Thread.Sleep(QPS显示单位 - 所用毫秒);
            }
            计时器.Restart();
        }

    }

    public class API信息 {

        private API信息() { }

        public int ID { get; set; }
        public string 类型 { get; set; }
        public string KEY { get; set; }
        public string 秘钥 { get; set; }
        public int QPS { get; set; }
        public string 源语言 { get; set; }
        public string 目标语言 { get; set; }
        public int 已用额度 { get; set; }
        public int 可用额度 { get; set; }

        public static API信息 Parse(DataRow 明细row) {
            string KEY = 明细row["KEY"].ToString().Trim();
            string 秘钥 = 明细row["秘钥"].ToString().Trim();
            int.TryParse(明细row["已用额度"].ToString(), out int 已用额度);
            int.TryParse(明细row["可用额度"].ToString(), out int 可用额度);
            API信息 data = new API信息();
            int.TryParse(明细row["ID"].ToString(), out int ID);
            data.ID = ID;
            data.类型 = 明细row["类型"].ToString();
            data.KEY = KEY;
            data.秘钥 = 秘钥;
            data.QPS = int.Parse(明细row["QPS"].ToString());
            data.已用额度 = 已用额度;
            data.可用额度 = 可用额度;
            DataRow 主表row = 全局数据.API主表.Select($"类型='{明细row["类型"]}'").SingleOrDefault();
            if (主表row != null) {
                data.源语言 = 主表row[全局数据.全局设置数据.源语言].ToString();
                data.目标语言 = 主表row[全局数据.全局设置数据.目标语言].ToString();
            }
            return data;
        }

    }

}
