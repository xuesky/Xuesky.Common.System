using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Xuesky.Common.ClassLibary
{
    /// <summary>
    ///<see cref="MethodInfo"/>扩展
    /// </summary>
    public static class MethodInfoExtensions
    {
        /// <summary>
        /// 判断<see cref="MethodInfo"/>是否包含特性<see cref="T"/>
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        /// <exception cref="TypeLoadException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static bool HasAttribute<T>(this MethodInfo method)
        {
            return method.GetCustomAttributes(typeof(T), false).Any(s => s is T);
        }

        /// <summary>
        /// 判断<see cref="MethodInfo"/>是否为异步
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static bool IsAsync(this MethodInfo method)
        {
            return method.ReturnType == typeof(Task)
                || (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>));

        }
    }
}
