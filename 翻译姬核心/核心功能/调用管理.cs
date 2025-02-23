using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace 翻译姬;
public class 调用管理 {

    private static 数据库连接 数据库 => 全局数据.数据库;
    private static 全局设置数据 全局设置数据 => 全局数据.全局设置数据;

    //文本读取 -> 正则提取? -> 对话检索 -> 机翻前替换
    public static IEnumerable<文件结构> 文本读取(读取方式 读取方式) {
        DataTable 替换列表 = 数据库.Select("select * from 替换列表 where 是否启用=1");
        DataTable 正则 = 数据库.Select($"select * from 正则 where 正则名称='{全局设置数据.使用正则}'");
        Type API类型 = 文件结构.获取API类型();
        string 读取编码 = 全局设置数据.读取编码;
        if (读取方式 == 读取方式.文本读取) {
            if (正则.Rows.Count == 0) {
                throw new Exception($"不存在正则【{全局设置数据.使用正则}】");
            }
            foreach (string 路径 in 文本读写.获取文件列表()) {
                文件结构 结构 = new 文件结构();
                结构.路径 = 路径;
                结构.读取方式 = 读取方式;
                结构.处理数据.正则 = 正则;
                结构.处理数据.API类型 = API类型;
                结构.处理数据.替换列表 = 替换列表;
                结构.读取后数据处理(读取方式, ref 读取编码);
                yield return 结构;
            }

        } else if (读取方式 == 读取方式.Json读取) {
            DataTable Json指令 = 数据库.Select($"select * from Json指令 where 名称='{全局设置数据.指令集名称}'");
            if (Json指令.Rows.Count == 0) {
                throw new Exception($"不存在Json指令【{全局设置数据.指令集名称}】");
            }
            foreach (string 路径 in 文本读写.获取文件列表()) {
                文件结构 结构 = new 文件结构();
                结构.路径 = 路径;
                结构.读取方式 = 读取方式;
                结构.处理数据.正则 = 正则;
                结构.处理数据.API类型 = API类型;
                结构.处理数据.替换列表 = 替换列表;
                结构.处理数据.Json指令 = Json指令;
                结构.读取后数据处理(读取方式, ref 读取编码);
                yield return 结构;
            }

        } else if (读取方式 == 读取方式.Xml读取) {
            DataTable Xml指令 = 数据库.Select($"select * from Xml指令 where 名称='{全局设置数据.Xml指令集名称}'");
            if (Xml指令.Rows.Count == 0) {
                throw new Exception($"不存在Json指令【{全局设置数据.指令集名称}】");
            }
            foreach (string 路径 in 文本读写.获取文件列表()) {
                文件结构 结构 = new 文件结构();
                结构.路径 = 路径;
                结构.读取方式 = 读取方式;
                结构.处理数据.正则 = 正则;
                结构.处理数据.API类型 = API类型;
                结构.处理数据.替换列表 = 替换列表;
                结构.处理数据.Xml指令 = Xml指令;
                结构.读取后数据处理(读取方式, ref 读取编码);
                yield return 结构;
            }

        }
    }

    public static void 文本机翻(机翻方式 机翻方式, bool 机翻前数据处理 = true, params 文件结构[] 文件组) {
        if (机翻前数据处理) {
            foreach (var 文件 in 文件组) {
                文件.机翻前数据处理();
            }
        }
        if (机翻方式 == 机翻方式.标准机翻) {//预显示不进行机翻
            标准机翻.机翻(文件组);
            //数据中转.文本显示AppendLine($"机翻完成 ---> {文件.文件名}");
        }
        /*foreach (var 文件 in 文件组) {
            文件.机翻后数据处理(机翻方式);
        }*/
    }

}
public enum 读取方式 {
    文本读取,
    Json读取,
    Xml读取
}
public enum 机翻方式 {
    标准机翻,
    不机翻
}
public enum 写出方式 {
    文本写出,
    Json写出,
    Xml写出
}

