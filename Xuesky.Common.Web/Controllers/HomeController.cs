using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xuesky.Common.Web.Extenstions;

namespace Xuesky.Common.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IdentityExtentions identityExtentions;

        public HomeController(IdentityExtentions identityExtentions)
        {
            this.identityExtentions = identityExtentions;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Welcome
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public IActionResult Welcome()
        {
            var user = HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                var userName = identityExtentions.getClaimValue(ClaimTypes.Name);
                return View("Welcome", userName);
            }
            return View("Welcome", "请登录");
        }
    }
}