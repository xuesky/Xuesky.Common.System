using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

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
        /// 获取Claim值
        /// </summary>
        /// <param name="claimType"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public string getClaimValue(string claimType)
        {
            return httpContextAccessor.HttpContext.User.Claims.First(s => s.Type == claimType).Value;
        }
    }
}
