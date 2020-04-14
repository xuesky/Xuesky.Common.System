using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xuesky.Common.ClassLibary.Wrap;
using Xuesky.Common.Service;

namespace Xuesky.Common.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAccountService accountService;

        public LoginController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Login(string loginId, string password)
        {
            var result = await accountService.Login(loginId, password);
            if (result != null)
            {
                var userRole = result.sys_user_roles.FirstOrDefault(s => s.UserId == result.UserId);
                int roleId = userRole != null ? userRole.RoleId : 2;//2:普通用户
                var claims = new List<Claim>{
                new Claim(ClaimTypes.PrimarySid,result.UserId.ToString()),
                new Claim(ClaimTypes.Sid,result.UserLogin),
                new Claim(ClaimTypes.Name,result.UserName),
                new Claim(ClaimTypes.Role,roleId.ToString())
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "User");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
                {
                    //IssuedUtc 凭证颁发的日期,ExpiresUtc 凭证的有效截止日期,AllowRefresh 快到期时是否刷新凭证,IsPersistent 是否将凭证保存到cookie中
                    IsPersistent = true,
                    AllowRefresh = true
                });
            }
            return new JsonResult(result != null ? JsonResultWrap.Success("OK") : JsonResultWrap.Fail("登录失败"));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index");
        }
    }
}