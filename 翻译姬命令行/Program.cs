// 开发中，可作为Linux和Docker版。
// 由于不会web界面，目前只能做成api/命令行的形式，如果有大佬愿意帮忙写web界面请联系我！
using Newtonsoft.Json;
using System.Data;
using System.IO;
using System.Text;
using 翻译姬;

namespace 翻译姬命令行;
public class Program {

    private static string 配置文件路径 = "配置文件.json";

    public static void Main(string[] args) {
        全局数据.软件存储目录 = AppDomain.CurrentDomain.BaseDirectory;//更换为当前软件目录
        try {
            创建内存数据库();
            配置文件读取();
            var sb = new StringBuilder();
        } catch (Exception ex) {
            Console.WriteLine($"[异常]:{ex.Message}");
        }
    }


    private static void 配置文件读取() {
        //常规读取
        if (!File.Exists(配置文件路径)) {
            throw new Exception($"不存在配置文件[{配置文件路径}]");
        }
        var a = JsonConvert.DeserializeObject<配置文件操作>(File.ReadAllText(配置文件路径));

        //数据库读取
        string 读取编码 = "UTF-8";
        string sql = null;
        //GPT词汇表读取
        var dt = Util.词汇表读取("词汇表.csv");
        全局数据.GPT设置数据.GPT词汇表 = dt;
        //正则读取
        sql = "select * from 正则";
        var 正则 = 全局数据.数据库.Select(sql);
        配置文件操作.常规读取(正则, 文本读写.读取文本行("正则配置.txt", ref 读取编码), "正则名称");
        全局数据.数据库.Update(sql, 正则);
        //API明细读取
        sql = "select * from API明细";
        var API明细 = 全局数据.数据库.Select(sql);
        配置文件操作.常规读取(API明细, 文本读写.读取文本行("API明细配置.txt", ref 读取编码));
        全局数据.数据库.Update(sql, API明细);
        //Json指令读取
        sql = "select * from Json指令";
        var Json指令 = 全局数据.数据库.Select(sql);
        配置文件操作.常规读取(Json指令, 文本读写.读取文本行("Json指令配置.txt", ref 读取编码));
        全局数据.数据库.Update(sql, Json指令);
        //Xml指令读取
        sql = "select * from Xml指令";
        var Xml指令 = 全局数据.数据库.Select(sql);
        配置文件操作.常规读取(Xml指令, 文本读写.读取文本行("Xml指令配置.txt", ref 读取编码));
        全局数据.数据库.Update(sql, Xml指令);
        //替换列表读取
        sql = "select * from Xml指令";
        var 替换列表 = 全局数据.数据库.Select(sql);
        string[] 替换列表文件组 = Directory.GetFiles("/替换列表");
        foreach (string file in 替换列表文件组) {
            配置文件操作.常规读取(替换列表, 文本读写.读取文本行(file, ref 读取编码));
        }
        foreach (DataRow row in 替换列表.Rows) {

        }
        全局数据.数据库.Update(sql, 替换列表);
    }

    private static void 创建内存数据库() {
        全局数据.数据库 = new SQLite数据库();
        全局数据.数据库.Execute("""
            PRAGMA foreign_keys = off;
            BEGIN TRANSACTION;

            -- 表：API明细
            CREATE TABLE IF NOT EXISTS [API明细] (
            	[ID]	integer PRIMARY KEY AUTOINCREMENT NOT NULL,
            	[类型]	nvarchar(50) COLLATE NOCASE,
            	[是否启用]	bit,
            	[KEY]	nvarchar(100) COLLATE NOCASE,
            	[秘钥]	nvarchar(100) COLLATE NOCASE,
            	[QPS]	integer,
            	[已用额度]	integer,
            	[可用额度]	integer

            );

            -- 表：Json指令
            CREATE TABLE IF NOT EXISTS [Json指令] (
            	[名称]	nvarchar(50) NOT NULL COLLATE NOCASE,
            	[指令集]	nvarchar COLLATE NOCASE,
            	[备注]	nvarchar(500) COLLATE NOCASE,
                PRIMARY KEY ([名称])

            );

            -- 表：Xml指令
            CREATE TABLE IF NOT EXISTS [Xml指令] (
            	[名称]	nvarchar(50) NOT NULL COLLATE NOCASE,
            	[指令集]	nvarchar COLLATE NOCASE,
            	[备注]	nvarchar(500) COLLATE NOCASE,
                PRIMARY KEY ([名称])

            );

            -- 表：替换列表
            CREATE TABLE IF NOT EXISTS [替换列表] (
            	[ID]	integer PRIMARY KEY AUTOINCREMENT NOT NULL,
            	[名称]	nvarchar(50) COLLATE NOCASE,
            	[是否启用]	bit,
            	[替换时机]	nvarchar(50) COLLATE NOCASE,
            	[匹配行为]	nvarchar(50) COLLATE NOCASE,
            	[替换列表]	nvarchar COLLATE NOCASE

            );

            -- 表：正则
            CREATE TABLE IF NOT EXISTS [正则] (
            	[Id]	integer PRIMARY KEY AUTOINCREMENT NOT NULL,
            	[正则名称]	nvarchar(50) COLLATE NOCASE,
            	[行过滤正则]	nvarchar(200) COLLATE NOCASE,
            	[文本过滤正则]	nvarchar(200) COLLATE NOCASE,
            	[提取前行过滤正则]	nvarchar(200) COLLATE NOCASE,
            	[提取型正则]	nvarchar(200) COLLATE NOCASE

            );

            COMMIT TRANSACTION;
            PRAGMA foreign_keys = on;
            
            """);
    }

}
