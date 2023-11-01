using Newtonsoft.Json;
using Sunny.UI;
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

    public static IEnumerable<文件结构> 文本读取(读取方式 读取方式) {
        //流程：(文本读取 or Json读取)
        DataTable 替换列表 = 数据库.Select("select * from 替换列表 where 是否启用=1");
        DataRow 正则row = 数据库.Select($"select * from 正则 where 正则名称='{全局设置数据.使用正则}'")?.AsEnumerable()?.ElementAtOrDefault(0);
        Type API类型 = 文件结构.获取API类型();
        if (读取方式 == 读取方式.本地读取) {
            if (正则row == null) {
                throw new Exception($"不存在正则【{全局设置数据.使用正则}】");
            }
            foreach (string 路径 in 文本读写.获取文件列表()) {
                文件结构 结构 = new 文件结构();
                结构.路径 = 路径;
                结构.原文本行 = 文本读写.读取文本行(路径);
                结构.处理数据.替换列表 = 替换列表;
                结构.处理数据.正则row = 正则row;
                结构.处理数据.API类型 = API类型;
                yield return 结构;
            }

        } else if (读取方式 == 读取方式.Json读取) {
            DataRow Json指令Row = 数据库.Select($"select * from Json指令 where 名称='{全局设置数据.指令集名称}'")?.AsEnumerable()?.ElementAtOrDefault(0);
            if (Json指令Row == null) {
                throw new Exception($"不存在Json指令【{全局设置数据.指令集名称}】");
            }
            foreach (string 路径 in 文本读写.获取文件列表()) {
                string Json文本 = 文本读写.读取文本(路径);
                文件结构 结构 = new 文件结构();
                结构.路径 = 路径;
                结构.原文本行 = 文本读写.Json提取(Json文本, Json指令Row);
                结构.处理数据.替换列表 = 替换列表;
                结构.处理数据.正则row = 正则row;
                结构.处理数据.API类型 = API类型;
                结构.Json文本 = Json文本;
                结构.处理数据.Json指令row = Json指令Row;
                yield return 结构;
            }

        } else if (读取方式 == 读取方式.Xml读取) {
            DataRow Xml指令Row = 数据库.Select($"select * from Xml指令 where 名称='{全局设置数据.Xml指令集名称}'")?.AsEnumerable()?.ElementAtOrDefault(0);
            if (Xml指令Row == null) {
                throw new Exception($"不存在Json指令【{全局设置数据.指令集名称}】");
            }
            foreach (string 路径 in 文本读写.获取文件列表()) {
                string Xml文本 = 文本读写.读取文本(路径);
                文件结构 结构 = new 文件结构();
                结构.路径 = 路径;
                结构.原文本行 = 文本读写.Xml提取(Xml文本, Xml指令Row);
                结构.处理数据.替换列表 = 替换列表;
                结构.处理数据.正则row = 正则row;
                结构.处理数据.API类型 = API类型;
                结构.Xml文本 = Xml文本;
                结构.处理数据.Xml指令row = Xml指令Row;
                yield return 结构;
            }

        }
    }

    public static void 文本机翻(机翻方式 机翻方式, 文件结构 文件) {
        文件.生成机翻前文本组();//流程：正则提取 -> 机翻前替换 -> API分割
        //流程：机翻
        if (机翻方式 == 机翻方式.标准机翻) {
            new 标准机翻().机翻(文件);
            数据中转.文本显示Append($"机翻完成 ---> {文件.文件名}");

        } else if (机翻方式 == 机翻方式.不机翻) {
            

        } else if (机翻方式 == 机翻方式.TCP机翻) {
            TCP工具 tcp = new TCP工具(全局设置数据.机翻目标端口, 获取连接类型());
            tcp.连接成功 += sc => 数据中转.文本显示Append($"{sc.RemoteEndPoint}连接成功");
            tcp.连接断开 += sc => {
                数据中转.文本显示Append($"{sc.RemoteEndPoint}连接断开，尝试重连");
                tcp.开始连接(全局设置数据.机翻目标地址);
            };
            tcp.数据接收 += Tcp_数据接收;
            tcp.开始连接(全局设置数据.机翻目标地址);
            tcp.发送数据(JsonConvert.SerializeObject(文件.文本组.获取原文组()));
            文件.文本组[0].译文 = 获取TCP机翻后文本();
            数据中转.文本显示Append($"机翻完成 ---> {new FileInfo(文件.路径).Name}");
        }
        //进行机翻结果的检验
        文件.处理机翻后数据();
    }

    public static void 文本写出(写出方式 写出方式, 文件结构 文件) {
        文件.生成机翻后文本组();//流程：分割后合并 -> 机翻后替换 -> 正则封入
        //流程：(Json封入 or 文本写出)
        if (写出方式 == 写出方式.本地写出) {
            文本读写.写出文本行(文件.路径, 文件.新文本行);
        } else if (写出方式 == 写出方式.Json写出) {
            string 写入后Json文本 = 文本读写.Json写入(文件.Json文本, 文件.新文本行, 文件.处理数据.Json指令row);
            文本读写.写出文本(文件.路径, 写入后Json文本);
        } else if (写出方式 == 写出方式.Xml写出) {
            string 写入后Xml文本 = 文本读写.Xml写入(文件.Xml文本, 文件.新文本行, 文件.处理数据.Json指令row);
            文本读写.写出文本(文件.路径, 写入后Xml文本);
        }
    }

    private static string[] 当前机翻中原文;
    private static string[] TCP机翻后文本;
    private static void Tcp_数据接收(Socket socket, string text) {
        try {
            string[] arr = JsonConvert.DeserializeObject<string[]>(text);
            if (当前机翻中原文.Length != arr.Length) {
                throw new Exception("返回的数量与发送的数量不符");
            }
            TCP机翻后文本 = arr;
        } catch (JsonSerializationException) {
            错误处理.普通错误处理("Json格式有误，请返回正确的格式");
        } catch (Exception ex) {
            错误处理.普通错误处理(ex.Message);
        }
    }
    private static string[] 获取TCP机翻后文本() {
        while (TCP机翻后文本 == null) {
        }
        string[] res = TCP机翻后文本;
        TCP机翻后文本 = null;
        return res;
    }

    private static 连接类型 获取连接类型() => 全局设置数据.机翻连接类型 switch {
        "IPv4" => 连接类型.IPV4,
        "IPv6" => 连接类型.IPV6,
        _ => throw new Exception($"意外的读取连接类型：{全局设置数据.机翻连接类型}")
    };

}
public enum 读取方式 {
    本地读取,
    Json读取,
    Xml读取
}
public enum 机翻方式 {
    标准机翻,
    TCP机翻,
    不机翻
}
public enum 写出方式 {
    本地写出,
    Json写出,
    Xml写出,
    不写出
}

