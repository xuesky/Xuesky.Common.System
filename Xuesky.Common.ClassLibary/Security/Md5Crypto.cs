using System;
using System.Security.Cryptography;
using System.Text;

namespace Xuesky.Common.ClassLibary.Security
{
    /// <summary>
    /// MD5加密操作
    /// </summary>
    public class Md5Crypto
    {
        //MD5加盐格式
        private static readonly string SALT_FORMAT = "$_n{0}_jX3mS)";
        /// <summary>
        /// Md5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="System.Reflection.TargetInvocationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static string Md5Hash(string input)
        {
            input = string.Format(SALT_FORMAT, input);
            using (MD5 mD5 = MD5.Create())
            {
                var bytes = mD5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var result = BitConverter.ToString(bytes);
                return result.Replace("-", "");
            }
        }
    }
}
