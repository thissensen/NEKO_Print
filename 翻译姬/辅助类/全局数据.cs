using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬 {
    /// <summary>
    /// 只负责数据存储
    /// </summary>
    public static class 全局数据 {

        public static Dictionary<string, JObject> 窗体名_序列化数据 { get; set; }

        public static DataTable API主表 { get; set; }

        /// <summary>
        /// 总数据库
        /// </summary>
        public static 数据库连接 数据库 {
            get {
                return _数据库;
            } set {
                //数据库校验
                _数据库 = value;
            } 
        }
        private static 数据库连接 _数据库;

        /// <summary>
        /// 全局设置的数据
        /// </summary>
        public static 全局设置数据 全局设置数据 { get; set; } = new 全局设置数据();

        /// <summary>
        /// GPT相关的设置数据
        /// </summary>
        public static GPT设置数据 GPT设置数据 { get; set; } = new GPT设置数据();

        /// <summary>
        /// 软件主题
        /// </summary>
        public static 全局主题设置 全局主题设置 = new 全局主题设置();

        public static void 数据初始化() {
            //序列化预加载
            DataTable dt = 数据库.Select($"select 窗体名,序列化值 from 窗体序列化");
            窗体名_序列化数据 = (from row in dt.AsEnumerable()
                         select new {
                             k = row["窗体名"].ToString(),
                             v = JObject.Parse(row["序列化值"].ToString())
                         }).ToDictionary(t1 => t1.k, t2 => t2.v);
            //设置数据加载
            if (窗体名_序列化数据.ContainsKey("全局设置数据")) {
                全局设置数据 = JsonConvert.DeserializeObject<全局设置数据>(窗体名_序列化数据["全局设置数据"].ToString());
            }
            if (窗体名_序列化数据.ContainsKey("GPT设置数据")) {
                GPT设置数据 = JsonConvert.DeserializeObject<GPT设置数据>(窗体名_序列化数据["GPT设置数据"].ToString());
            }
            if (窗体名_序列化数据.ContainsKey("全局主题设置")) {
                全局主题设置 = JsonConvert.DeserializeObject<全局主题设置>(窗体名_序列化数据["全局主题设置"].ToString());
            }
            DataTable 主表 = new DataTable("API主表");
            主表.Columns.Add("类型");
            主表.Columns.Add("注册地址");
            主表.Columns.Add("日语");
            主表.Columns.Add("英语");
            主表.Columns.Add("韩语");
            主表.Columns.Add("简中");
            主表.Columns.Add("繁中");
            主表.Rows.Add("阿里云", "https://mt.console.aliyun.com/products", "ja", "en", "ko", "zh", "zh-tw");
            主表.Rows.Add("百度", "http://api.fanyi.baidu.com/api/trans/product/index", "jp", "en", "kor", "zh", "cht");
            主表.Rows.Add("腾讯云", "https://console.cloud.tencent.com/cam/capi", "ja", "en", "ko", "zh", "zh-TW");

            API主表 = 主表;
        }

        public static void 保存设置数据() {
            var dic = new Dictionary<string, object>();
            dic.Add("全局设置数据", JsonConvert.SerializeObject(全局设置数据));
            dic.Add("GPT设置数据", JsonConvert.SerializeObject(GPT设置数据));
            dic.Add("全局主题设置", JsonConvert.SerializeObject(全局主题设置));
            var sqls = new List<string>();
            DataRow 全局row = 数据库.Select($"select 窗体名 from 窗体序列化 where 窗体名='全局设置数据'")?.AsEnumerable()?.ElementAtOrDefault(0);
            if (全局row == null) {
                sqls.Add($"insert into 窗体序列化(窗体名,序列化值) values('全局设置数据',@全局设置数据)");
            } else {
                sqls.Add($"update 窗体序列化 set 序列化值=@全局设置数据 where 窗体名='全局设置数据'");
            }
            DataRow GPTrow = 数据库.Select($"select 窗体名 from 窗体序列化 where 窗体名='GPT设置数据'")?.AsEnumerable()?.ElementAtOrDefault(0);
            if (GPTrow == null) {
                sqls.Add($"insert into 窗体序列化(窗体名,序列化值) values('GPT设置数据',@GPT设置数据)");
            } else {
                sqls.Add($"update 窗体序列化 set 序列化值=@GPT设置数据 where 窗体名='GPT设置数据'");
            }
            DataRow 主题row = 数据库.Select($"select 窗体名 from 窗体序列化 where 窗体名='全局主题设置'")?.AsEnumerable()?.ElementAtOrDefault(0);
            if (主题row == null) {
                sqls.Add($"insert into 窗体序列化(窗体名,序列化值) values('全局主题设置',@全局主题设置)");
            } else {
                sqls.Add($"update 窗体序列化 set 序列化值=@全局主题设置 where 窗体名='全局主题设置'");
            }
            数据库.Work(sqls.ToArray(), dic);
        }

    }

    public class 全局设置数据 : INotifyPropertyChanged {


        public string 使用机翻 {
            get => _使用机翻;
            set {
                if (_使用机翻 != value) {
                    _使用机翻 = value;
                    通知更改(() => 使用机翻);
                }
            }
        }
        private string _使用机翻;

        public string 使用正则 {
            get => _使用正则;
            set {
                if (_使用正则 != value) {
                    _使用正则 = value;
                    通知更改(() => 使用正则);
                }
            }
        }
        private string _使用正则;

        public string 指令集名称 {
            get => _指令集名称;
            set {
                if (_指令集名称 != value) {
                    _指令集名称 = value;
                    通知更改(() => 指令集名称);
                }
            }
        }
        private string _指令集名称;

        public string Xml指令集名称 {
            get => _Xml指令集名称;
            set {
                if (_Xml指令集名称 != value) {
                    _Xml指令集名称 = value;
                    通知更改(() => Xml指令集名称);
                }
            }
        }
        private string _Xml指令集名称;

        public string 读取目录 {
            get => _读取目录;
            set {
                if (_读取目录 != value) {
                    _读取目录 = value;
                    通知更改(() => 读取目录);
                }
            }
        }
        private string _读取目录;

        public string 读取编码 {
            get => _读取编码;
            set {
                if (_读取编码 != value) {
                    _读取编码 = value;
                    通知更改(() => 读取编码);
                }
            }
        }
        private string _读取编码;

        public string 读取后缀 {
            get => _读取后缀;
            set {
                if (_读取后缀 != value) {
                    _读取后缀 = value;
                    通知更改(() => 读取后缀);
                }
            }
        }
        private string _读取后缀;

        public string 源语言 {
            get => _源语言;
            set {
                if (_源语言 != value) {
                    _源语言 = value;
                    通知更改(() => 源语言);
                }
            }
        }
        private string _源语言;

        public string 目标语言 {
            get => _目标语言;
            set {
                if (_目标语言 != value) {
                    _目标语言 = value;
                    通知更改(() => 目标语言);
                }
            }
        }
        private string _目标语言;

        public bool 机翻空值使用原文 {
            get => _机翻空值使用原文;
            set {
                if (_机翻空值使用原文 != value) {
                    _机翻空值使用原文 = value;
                    通知更改(() => 机翻空值使用原文);
                }
            }
        }
        private bool _机翻空值使用原文;

        public bool 无视返回空值 {
            get => _无视返回空值;
            set {
                if (_无视返回空值 != value) {
                    _无视返回空值 = value;
                    通知更改(() => 无视返回空值);
                }
            }
        }
        private bool _无视返回空值;

        public bool 重复内容跳过 {
            get => _重复内容跳过;
            set {
                if (_重复内容跳过 != value) {
                    _重复内容跳过 = value;
                    通知更改(() => 重复内容跳过);
                }
            }
        }
        private bool _重复内容跳过;

        public bool 无视返回原文 {
            get => _无视返回原文;
            set {
                if (_无视返回原文 != value) {
                    _无视返回原文 = value;
                    通知更改(() => 无视返回原文);
                }
            }
        }
        private bool _无视返回原文;

        public string 机翻目标地址 {
            get => _机翻目标地址;
            set {
                if (_机翻目标地址 != value) {
                    _机翻目标地址 = value;
                    通知更改(() => 机翻目标地址);
                }
            }
        }
        private string _机翻目标地址;

        public int 机翻目标端口 {
            get => _机翻目标端口;
            set {
                if (_机翻目标端口 != value) {
                    _机翻目标端口 = value;
                    通知更改(() => 机翻目标端口);
                }
            }
        }
        private int _机翻目标端口;

        public string 机翻连接类型 {
            get => _机翻连接类型;
            set {
                if (_机翻连接类型 != value) {
                    _机翻连接类型 = value;
                    通知更改(() => 机翻连接类型);
                }
            }
        }
        private string _机翻连接类型;

        public string 写出目录 {
            get => _写出目录;
            set {
                if (_写出目录 != value) {
                    _写出目录 = value;
                    通知更改(() => 写出目录);
                }
            }
        }
        private string _写出目录;

        public string 写出编码 {
            get => _写出编码;
            set {
                if (_写出编码 != value) {
                    _写出编码 = value;
                    通知更改(() => 写出编码);
                }
            }
        }
        private string _写出编码;

        public string 写出格式 {
            get => _写出格式;
            set {
                if (_写出格式 != value) {
                    _写出格式 = value;
                    通知更改(() => 写出格式);
                }
            }
        }
        private string _写出格式;

        public bool 写出后删除源文件 {
            get => _写出后删除源文件;
            set {
                if (_写出后删除源文件 != value) {
                    _写出后删除源文件 = value;
                    通知更改(() => 写出后删除源文件);
                }
            }
        }
        private bool _写出后删除源文件;

        public event PropertyChangedEventHandler PropertyChanged;
        public void 通知更改<T>(Expression<Func<T>> property) {
            if (PropertyChanged == null)
                return;
            var memberExpression = property.Body as MemberExpression;
            if (memberExpression == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(memberExpression.Member.Name));
        }
    }

    public class GPT设置数据 : INotifyPropertyChanged {

        public string 连接域名 {
            get => _连接域名;
            set {
                if (_连接域名 != value) {
                    _连接域名 = value;
                    通知更改(() => 连接域名);
                }
            }
        }
        private string _连接域名;

        public string 使用模型 {
            get => _使用模型;
            set {
                if (_使用模型 != value) {
                    _使用模型 = value;
                    通知更改(() => 使用模型);
                }
            }
        }
        private string _使用模型;

        public int 次数限制 {
            get => _次数限制;
            set {
                if (_次数限制 != value) {
                    _次数限制 = value;
                    通知更改(() => 次数限制);
                }
            }
        }
        private int _次数限制;

        public int Token限制 {
            get => _Token限制;
            set {
                if (_Token限制 != value) {
                    _Token限制 = value;
                    通知更改(() => Token限制);
                }
            }
        }
        private int _Token限制;

        public int 请求等待延迟 {
            get => _请求等待延迟;
            set {
                if (_请求等待延迟 != value) {
                    _请求等待延迟 = value;
                    通知更改(() => 请求等待延迟);
                }
            }
        }
        private int _请求等待延迟 = 3;

        public int 单次机翻行 {
            get => _单次机翻行;
            set {
                if (_单次机翻行 != value) {
                    _单次机翻行 = value;
                    通知更改(() => 单次机翻行);
                }
            }
        }
        private int _单次机翻行 = 8;

        public int 错行重试数 {
            get => _错行重试数;
            set {
                if (_错行重试数 != value) {
                    _错行重试数 = value;
                    通知更改(() => 错行重试数);
                }
            }
        }
        private int _错行重试数 = 1;

        public bool 使用多线程 {
            get => _使用多线程;
            set {
                if (_使用多线程 != value) {
                    _使用多线程 = value;
                    通知更改(() => 使用多线程);
                }
            }
        }
        private bool _使用多线程;

        public bool 上下文提示 {
            get => _上下文提示;
            set {
                if (_上下文提示 != value) {
                    _上下文提示 = value;
                    通知更改(() => 上下文提示);
                }
            }
        }
        private bool _上下文提示;

        public bool 发送预设 {
            get => _发送预设;
            set {
                if (_发送预设 != value) {
                    _发送预设 = value;
                    通知更改(() => 发送预设);
                }
            }
        }
        private bool _发送预设;

        public string 模型词表 {
            get => _模型词表;
            set {
                if (_模型词表 != value) {
                    _模型词表 = value;
                    通知更改(() => 模型词表);
                }
            }
        }
        private string _模型词表;

        public string 语境 {
            get => _语境;
            set {
                if (_语境 != value) {
                    _语境 = value;
                    通知更改(() => 语境);
                }
            }
        }
        private string _语境;

        public string 预设原文 {
            get => _预设原文;
            set {
                if (_预设原文 != value) {
                    _预设原文 = value;
                    通知更改(() => 预设原文);
                }
            }
        }
        private string _预设原文;

        public string 预设返回 {
            get => _预设返回;
            set {
                if (_预设返回 != value) {
                    _预设返回 = value;
                    通知更改(() => 预设返回);
                }
            }
        }
        private string _预设返回;

        public void 必要数据验证() {
            if (string.IsNullOrWhiteSpace(连接域名)) {
                throw new Exception("请前往GPT设置填写连接域名");
            }
            if (string.IsNullOrWhiteSpace(使用模型)) {
                throw new Exception("请前往GPT设置填写使用模型");
            }
            if (string.IsNullOrWhiteSpace(语境)) {
                throw new Exception("请前往GPT设置填写语境，使GPT返回Json格式");
            }
            if (发送预设) {
                if (string.IsNullOrWhiteSpace(预设原文)) {
                    throw new Exception("请前往GPT设置填写预设原文");
                }
                if (string.IsNullOrWhiteSpace(预设返回)) {
                    throw new Exception("请前往GPT设置填写预设返回");
                }
                try {
                    var obj = JObject.Parse(预设原文);
                } catch {
                    throw new Exception("预设原文非Json格式");
                }
                try {
                    var obj = JObject.Parse(预设返回);
                } catch {
                    throw new Exception("预设返回非Json格式");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void 通知更改<T>(Expression<Func<T>> property) {
            if (PropertyChanged == null)
                return;

            var memberExpression = property.Body as MemberExpression;
            if (memberExpression == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(memberExpression.Member.Name));
        }
    }

    public class 全局主题设置 {
        public Color 主题色 { get; set; } = Color.FromArgb(255, 121, 188);
        public Color 深级主题色 { get; set; } = Color.FromArgb(255, 180, 188);
        public Color 次级主题色 { get; set; } = Color.FromArgb(50, 255, 121, 188);
        public Color 不可用时颜色 { get; set; } = Color.FromArgb(220, 220, 220);//灰色
        public Color 背景色 { get; set; } = Color.White;
    }
}
