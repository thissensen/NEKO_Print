using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;

namespace 翻译姬 {
    /// <summary>
    /// 只负责数据存储
    /// </summary>
    public static class 全局数据 {

        public static string 缓存数据路径 { 
            get {
                if (_缓存数据路径 == null) {
                    throw new System.Exception("缓存数据路径不能为空");
                }
                return _缓存数据路径;
            }
            set => _缓存数据路径 = value;
        }
        private static string _缓存数据路径;

        /// <summary>
        /// 中止机翻信号
        /// </summary>
        public static bool 是否中止 { get; set; }
        public static 文件结构[] 处理中文件结构 { get; set; }

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
            主表.Rows.Add("火山", "https://console.volcengine.com/home", "ja", "en", "ko", "zh", "zh-Hant");

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

    public class 全局主题设置 {
        public Color 主题色 { get; set; } = Color.FromArgb(255, 121, 188);
        public Color 深级主题色 { get; set; } = Color.FromArgb(255, 180, 188);
        public Color 次级主题色 { get; set; } = Color.FromArgb(50, 255, 121, 188);
        public Color 不可用时颜色 { get; set; } = Color.FromArgb(220, 220, 220);//灰色
        public Color 背景色 { get; set; } = Color.White;
    }
}
