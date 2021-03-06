using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xuesky.Common.ClassLibary;
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
            , IRoleService roleService
            , ISysUserService sysUserService)
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
        public IActionResult ChangePass()
        {
            return View();
        }
        /// <summary>
        /// GetMyInfo
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public async Task<JsonResult> GetMyInfo()
        {
            int userId = identityExtentions.getUserId();
            string loginId = identityExtentions.getLoginId();
            var result = await accountService.GetAccountInfo(userId, loginId);
            return new JsonResult(JsonResultWrap.Success("OK", 0, result));
        }
        /// <summary>
        /// UpdateMyInfo
        /// </summary>
        /// <param name="sysUserUpdateInput"></param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        [HttpPost]
        public async Task<JsonResult> UpdateMyInfo(SysUserUpdateInput sysUserUpdateInput)
        {
            int userId = identityExtentions.getUserId();
            sysUserUpdateInput.UserId = userId;
            var result = await accountService.UpdateAccountInfo(sysUserUpdateInput);

            return new JsonResult(result > 0 ? JsonResultWrap.Success("OK") : JsonResultWrap.Fail("更新失败"));
        }
        /// <summary>
        /// Update密码
        /// </summary>
        /// <param name="sysUserUpdateInput"></param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        [HttpPost]
        public async Task<JsonResult> UpdatePass(SysUserChangePasswordInput sysUserChangePasswordInput)
        {
            int userId = identityExtentions.getUserId();
            sysUserChangePasswordInput.UserId = userId;
            if (sysUserChangePasswordInput.UserNewPwd != sysUserChangePasswordInput.UserNewPwdAgain)
            {
                return new JsonResult(JsonResultWrap.Fail("新密码与确认密码不一致"));
            }
            if (!await accountService.CheckAccountPass(sysUserChangePasswordInput))
            {
                return new JsonResult(JsonResultWrap.Fail("旧密码不正确"));
            }
            var result = await accountService.ChangePassword(sysUserChangePasswordInput);

            return new JsonResult(result > 0 ? JsonResultWrap.Success("密码更新成功") : JsonResultWrap.Fail("更新失败"));
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
                var userName = identityExtentions.getUserName();
                return View("Welcome", userName);
            }
            return View("Welcome", "请登录");
        }
    }
}