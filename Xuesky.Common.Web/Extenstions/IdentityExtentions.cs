using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Xuesky.Common.Web.Extenstions
{
    public class IdentityExtentions
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public IdentityExtentions(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 获取用户PrimaryKey
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public int getUserId()
        {
            return int.Parse(getClaimValue(ClaimTypes.PrimarySid));
        }
        /// <summary>
        /// 获取用户UserName
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public string getUserName()
        {
            return getClaimValue(ClaimTypes.Name);
        }
        /// <summary>
        /// 获取用户LoginId
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public string getLoginId()
        {
            return getClaimValue(ClaimTypes.Sid);
        }
        /// <summary>
        /// 获取用户RoleId
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public int getRoleId()
        {
            return int.Parse(getClaimValue(ClaimTypes.Role));
        }
        /// <summary>
        /// 获取Claim值
        /// </summary>
        /// <param name="claimType"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private string getClaimValue(string claimType)
        {
            return httpContextAccessor.HttpContext.User.Claims.First(s => s.Type == claimType).Value;
        }


    }
}
