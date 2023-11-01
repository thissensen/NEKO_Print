using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace 翻译姬 {
    /// <summary>
    /// 用于Assembly的解析，由于相对耗时，所以做个类比较好
    /// </summary>
    public class 程序集解析 {

        public List<Type> types { get; set; }

        public 程序集解析(Assembly assembly) {
            types = assembly.GetTypes().ToList();
        }

        public void 添加程序集(Assembly assembly) {
            types.AddRange(assembly.GetTypes());
        }

        /// <summary>
        /// 返回包含指定特性的类
        /// </summary>
        /// <typeparam name="T">需要查找的特性</typeparam>
        /// <param name="是否查找继承链">是否查询继承下来的特性</param>
        /// <returns>返回特性以及type</returns>
        public IEnumerable<Type_Attribute<T>> 查找指定特性类<T>(bool 是否查找继承链 = true) where T : Attribute {
            foreach (var type in types) {
                var attribute = type.GetCustomAttributes<T>(是否查找继承链);
                if (attribute.Count() != 0) {
                    yield return new Type_Attribute<T>() {
                        Type = type,
                        Attribute = attribute.ToArray()
                    };
                }
            }
            yield break;
        }

        /// <summary>
        /// 返回包含指定特性的方法
        /// </summary>
        /// <typeparam name="T">需要查找的特性</typeparam>
        /// <param name="方法约束">查找方法时的约束</param>
        /// <param name="是否查找继承链">是否查询继承下来的特性</param>
        /// <returns>返回特性以及type</returns>
        public IEnumerable<Type_Attribute<T>> 查找指定特性方法<T>(bool 是否查找继承链 = true) where T : Attribute {
            foreach (var type in types) {
                var methods = type.GetMethods();//获取方法集
                foreach (var method in methods) {
                    var attribute = method.GetCustomAttributes<T>(是否查找继承链);
                    if (attribute.Count() != 0) {
                        yield return new Type_Attribute<T>() {
                            Type = type,
                            Attribute = attribute.ToArray()
                        };
                    }
                }
            }
        }

    }

    public static class 程序集拓展方法 {
        /// <summary>
        /// 方法是否为某委托，不支持Func,Action
        /// </summary>
        /// <typeparam name="T">delegate委托</typeparam>
        /// <param name="method">方法</param>
        /// <returns>是否匹配</returns>
        public static bool 比较委托<T>(this MethodInfo method) where T : class {
            Type delegateType = typeof(T);
            MethodInfo delegateSignature = delegateType.GetMethod("Invoke");
            bool parametersEqual = delegateSignature
                .GetParameters()
                .Select(x => x.ParameterType)
                .SequenceEqual(method.GetParameters()
                    .Select(x => x.ParameterType));
            return delegateSignature.ReturnType == method.ReturnType &&
                   parametersEqual;
        }
    }

    public struct Type_Attribute<T> where T : Attribute {
        public Type Type;
        public T[] Attribute;
    }

}
