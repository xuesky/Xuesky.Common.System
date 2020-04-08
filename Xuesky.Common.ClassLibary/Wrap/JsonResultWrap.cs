using System;

namespace Xuesky.Common.ClassLibary.Wrap
{
    /// <summary>
    /// 返回结果的结构包装定义。
    /// </summary>
    public class JsonResultWrap
    {
        /// <summary>
        /// 获取或设置是否调用成功。
        /// </summary>
        public bool Succeed { get; set; }

        /// <summary>
        /// 获取或设置调用的信息。
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 总数据量
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// 获取或设置客户端接收的数据。
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 返回一个提醒的响应信息。
        /// </summary>
        /// <param name="message">提供给客户端显示的信息。</param>
        /// <param name="total">总记录数</param>
        /// <param name="data">返回给客户端的数据。</param>
        /// <returns></returns>
        public static JsonResultWrap Info(string message, long total = 0, object data = null)
        {
            return new JsonResultWrap { Succeed = true, Message = message, Total = total, Data = data };
        }

        /// <summary>
        /// 返回一个提醒的响应信息。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="message">提供给客户端显示的信息。</param>
        /// <param name="total">总记录数</param>
        /// <param name="data">返回给客户端的数据。</param>
        /// <returns></returns>
        public static JsonResultWrap<T> Info<T>(string message, long total = 0, T data = default)
        {
            return new JsonResultWrap<T> { Succeed = true, Message = message, Total = total, Data = data };
        }

        /// <summary>
        /// 返回一个成功的响应信息。
        /// </summary>
        /// <param name="message">提供给客户端显示的调用成功的信息。</param>
        /// <param name="total">总记录数</param>
        /// <param name="data">返回给客户端的数据。</param>
        /// <returns></returns>
        public static JsonResultWrap Success(string message, long total = 0, object data = null)
        {
            return new JsonResultWrap { Succeed = true, Message = message, Total = total, Data = data };
        }

        /// <summary>
        /// 返回一个成功的响应信息。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="message">提供给客户端显示的调用成功的信息。</param>
        /// <param name="total">总记录数</param>
        /// <param name="data">返回给客户端的数据。</param>
        /// <returns></returns>
        public static JsonResultWrap<T> Success<T>(string message, long total = 0, T data = default)
        {
            return new JsonResultWrap<T> { Succeed = true, Message = message, Total = total, Data = data };
        }

        /// <summary>
        /// 返回一个成功的响应信息。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="message">提供给客户端显示的调用成功的信息。</param>
        /// <param name="total">总记录数</param>
        /// <param name="data">返回给客户端的数据。</param>
        /// <returns></returns>
        public static JsonResultWrap<T> Success<T>(string message, long total = 0, object data = null)
        {
            T value = default;
            if (data != null)
            {
                value = (T)data;
            }
            return new JsonResultWrap<T> { Succeed = true, Message = message, Total = total, Data = value };
        }

        /// <summary>
        /// 返回一个失败的响应信息。
        /// </summary>
        /// <param name="message">提供给客户端显示的调用失败的信息。</param>
        /// <returns></returns>
        public static JsonResultWrap Fail(string message)
        {
            return new JsonResultWrap { Succeed = false, Message = message };
        }

        /// <summary>
        /// 返回一个失败的响应信息。
        /// </summary>
        /// <param name="message">提供给客户端显示的调用失败的信息。</param>
        /// <returns></returns>
        public static JsonResultWrap<T> Fail<T>(string message)
        {
            return new JsonResultWrap<T> { Succeed = false, Message = message };
        }

        /// <summary>
        /// 返回一个失败的响应信息。
        /// </summary>
        /// <param name="exception">引发调用失败的异常信息。</param>
        /// <returns></returns>
        public static JsonResultWrap Fail(Exception exception)
        {
            return new JsonResultWrap { Succeed = false, Message = exception.Message };
        }

        /// <summary>
        /// 返回一个失败的响应信息。
        /// </summary>
        /// <param name="exception">引发调用失败的异常信息。</param>
        /// <returns></returns>
        public static JsonResultWrap<T> Fail<T>(Exception exception)
        {
            return new JsonResultWrap<T> { Succeed = false, Message = exception.Message };
        }

        public override string ToString()
        {
            if (Succeed)
            {
                return string.Format($"成功,数据：{Data}");
            }
            else
            {
                return string.Format("失败,{0}", Message);
            }
        }
    }
    /// <summary>
    /// 返回结果的结构定义，采用泛型。
    /// </summary>
    /// <typeparam name="T">数据类型。</typeparam>
    public class JsonResultWrap<T>
    {
        /// <summary>
        /// 获取或设置是否调用成功。
        /// </summary>
        public bool Succeed { get; set; }

        /// <summary>
        /// 获取或设置调用的信息。
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 总记录数量
        /// </summary>
        public long Total { get; set; }
        /// <summary>
        /// 获取或设置客户端接收的数据。
        /// </summary>
        public T Data { get; set; }

        public static implicit operator JsonResultWrap<T>(T data)
        {
            return new JsonResultWrap<T> { Succeed = true, Data = data };
        }
    }
}
