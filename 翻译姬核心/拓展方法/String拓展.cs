using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 翻译姬 {
    public static class String拓展 {
        /// <summary>
        /// Json字符串转对象
        /// ·where T : struct 限制类型参数T必须继承自System.ValueType。
        /// ·where T : class 限制类型参数T必须是引用类型，也就是不能继承自System.ValueType。
        /// ·where T : new () 限制类型参数T必须有一个缺省的构造函数（无参构造）
        /// ·where T : NameOfClass 限制类型参数T必须继承自某个类或实现某个接口。
        /// 可组合使用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(this string json, Formatting format = Formatting.Indented) where T : class, new() {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Formatting = format;//缩进
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        /// <summary>
        /// Contains拓展，可无视大小写之类
        /// </summary>
        public static bool Contains(this string str, string value, StringComparison stringComparison) {
            return str.IndexOf(value, stringComparison) >= 0;
        }

        /// <summary>
        /// 必须绝对路径，给文件创建文件夹，防止生成出错
        /// </summary>
        /// <param name="path">必须绝对路径</param>
        /// <returns></returns>
        public static string 创建父目录(this string path) {
            DirectoryInfo dir = new FileInfo(path).Directory;
            dir.Create();
            return path;
        }

        public static string 补全路径斜杠(this string path) {
            string 斜杠 = path.Contains("\\") ? "\\" : "/";
            if (!path.EndsWith(斜杠)) {
                return path + 斜杠;
            }
            return path;
        }

        /// <summary>
        /// 给文件创建，防止出错
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string 创建文件(this string path) {
            FileInfo info = new FileInfo(path);
            if (!info.Exists) {
                info.Create();
            }
            return path;
        }

        /// <summary>
        /// 给文件夹创建目录，防止出错
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string 创建目录(this string path) {
            DirectoryInfo info = new DirectoryInfo(path);
            if (!info.Exists) {
                info.Create();
            }
            return path;
        }

        /// <summary>
        /// 如果已存在该文件，则返回不重复的名字
        /// </summary>
        /// <param name="path"></param>
        /// <param name="已存在个数"></param>
        /// <returns></returns>
        public static string 获取不重复路径(this string path, int 已存在个数 = 0) {
            FileInfo info = new FileInfo(path);
            if (!info.Exists) {
                return path;
            }
            已存在个数++;//已存在该文件
            if (已存在个数 == 1) {//初次后面加
                path = info.FullName.Substring(0, info.FullName.Length - info.Extension.Length) + $" ({已存在个数}){info.Extension}";
            } else {//比如即存在原来的又存在(1)，把1改成2
                path = info.FullName.Substring(0, info.FullName.Length - info.Extension.Length - (已存在个数 - 1).ToString().Length - 3) + $" ({已存在个数}){info.Extension}";
            }
            if (!File.Exists(path)) {
                return path;
            }
            return 获取不重复路径(path, 已存在个数);
        }

        public static string[] ToLines(this string text) {
            return text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static int 总字符数(this string[] arr) {
            int num = 0;
            foreach (var item in arr) {
                num += item.Length;
            }
            return num;
        }

    }
}
