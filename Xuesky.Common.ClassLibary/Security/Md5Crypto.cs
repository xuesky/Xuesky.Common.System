using System;
using System.Security.Cryptography;
using System.Text;

namespace Xuesky.Common.ClassLibary
{
    /// <summary>
    /// MD5加密操作
    /// </summary>
    public class Md5Crypto
    {
        //MD5加盐格式
        private static readonly string SALT_FORMAT = "$_n{0}_jX3mS)";
        /// <summary>
        /// 32位Md5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="System.Reflection.TargetInvocationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static string Md5Hash(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;
            input = string.Format(SALT_FORMAT, input);
            using (MD5 mD5 = MD5.Create())
            {
                var bytes = mD5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var result = BitConverter.ToString(bytes);
                return result.Replace("-", "");
            }
        }
        /// <summary>
        /// 64位MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="System.Reflection.TargetInvocationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static string Md5Hash64(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;
            input = string.Format(SALT_FORMAT, input);
            using (var md5 = MD5.Create())
            {
                var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                return bytes.ToBase64();
            }
        }
    }
}
