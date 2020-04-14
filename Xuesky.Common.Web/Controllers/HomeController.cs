using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Xuesky.Common.ClassLibary.Wrap;
using Xuesky.Common.Service;
using Xuesky.Common.Web.Extenstions;

namespace Xuesky.Common.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IdentityExtentions identityExtentions;
        private readonly IAccountService accountService;
        private readonly IRoleService roleService;

        public HomeController(IdentityExtentions identityExtentions
            , IAccountService accountService
            , IRoleService roleService)
        {
            this.identityExtentions = identityExtentions;
            this.accountService = accountService;
            this.roleService = roleService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> MyInfo()
        {
            ViewBag.Roles = await roleService.GetRoleList(s => true);
            return View();
        }
        /// <summary>
        /// GetMyInfo
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public async Task<JsonResult> GetMyInfo()
        {
            int userId = int.Parse(identityExtentions.getClaimValue(ClaimTypes.PrimarySid));
            string loginId = identityExtentions.getClaimValue(ClaimTypes.Sid);
            var result = await accountService.GetAccountInfo(userId, loginId);
            return new JsonResult(JsonResultWrap.Success("OK", 0, result));
        }
        /// <summary>
        /// UpdateMyInfo
        /// </summary>
        /// <param name="myinfo"></param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        [HttpPost]
        public async Task<JsonResult> UpdateMyInfo(VM_UserInfo myinfo)
        {
            int userId = int.Parse(identityExtentions.getClaimValue(ClaimTypes.PrimarySid));
            myinfo.UserId = userId;
            var result = await accountService.UpdateAccountInfo(myinfo);

            return new JsonResult(result > 0 ? JsonResultWrap.Success("OK") : JsonResultWrap.Fail("更新失败"));
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