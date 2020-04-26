using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Xuesky.Common.ClassLibary;
using Xuesky.Common.Service;

namespace Xuesky.Common.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 用户信息管理
    /// </summary>
    [Area("admin")]
    public class UserController : Controller
    {
        private readonly ISysUserService sysUserService;
        private readonly ITeacherService teacherService;
        private readonly IRoleService roleService;

        public UserController(ISysUserService sysUserService, ITeacherService teacherService, IRoleService roleService)
        {
            this.sysUserService = sysUserService;
            this.teacherService = teacherService;
            this.roleService = roleService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddUser()
        {
            ViewBag.Roles = await roleService.GetRoleList(s => true);
            ViewBag.Teachers = await teacherService.GetTeacherList(s => true);
            return View();
        }
        public async Task<IActionResult> EditUser(int userId)
        {
            ViewBag.Roles = await roleService.GetRoleList(s => true);
            ViewBag.Teachers = await teacherService.GetTeacherList(s => true);
            ViewBag.UserId = userId;
            return View();
        }
        public async Task<JsonResult> GetUser(int userId)
        {
            var user = await sysUserService.GetUser(userId);
            return new JsonResult(JsonResultWrap.Success("获取成功", 1, user));
        }
        [HttpPost]
        public async Task<JsonResult> EditUser(SysUserUpdateInput sysUserUpdateInput)
        {
            var userList = await sysUserService.GetUserList(s => s.UserLogin == sysUserUpdateInput.UserLogin);
            if (userList.Any() && userList[0].UserId != sysUserUpdateInput.UserId)
            {
                return new JsonResult(JsonResultWrap.Fail($"修改失败,登录ID:[{sysUserUpdateInput.UserLogin}]已经存在"));
            }
            var result = await sysUserService.UpdateSysuer(s => s.UserId == sysUserUpdateInput.UserId, sysUserUpdateInput);
            return new JsonResult(result > 0 ? JsonResultWrap.Success("修改成功", result) : JsonResultWrap.Fail("修改失败"));
        }
        /// <summary>
        /// AddUser
        /// </summary>
        /// <param name="sysUserAddInput"></param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Reflection.TargetInvocationException"></exception>
        [HttpPost]
        public async Task<JsonResult> AddUser(SysUserAddInput sysUserAddInput)
        {
            var userList = await sysUserService.GetUserList(s => s.UserLogin == sysUserAddInput.UserLogin);
            if (userList.Any())
            {
                return new JsonResult(JsonResultWrap.Fail($"添加失败,登录ID:[{sysUserAddInput.UserLogin}]已经存在"));
            }

            sysUserAddInput.OrgId = 1;
            sysUserAddInput.UserPwd = Md5Crypto.Md5Hash(sysUserAddInput.UserPwd);
            var result = await sysUserService.InsertSysUser(sysUserAddInput);
            return new JsonResult(result > 0 ? JsonResultWrap.Success("添加成功", result) : JsonResultWrap.Fail("添加失败"));
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int userId)
        {
            var result = await sysUserService.DeleteSysUser(new int[] { userId });
            return new JsonResult(result > 0 ? JsonResultWrap.Success("删除成功", result) : JsonResultWrap.Fail("删除失败"));
        }
        [HttpPost]
        public async Task<JsonResult> Use(int userId, bool isUse)
        {
            var result = await sysUserService.UseOrStopUser(new int[] { userId }, isUse);
            return new JsonResult(result > 0 ? JsonResultWrap.Success("更新成功", result) : JsonResultWrap.Fail("更新失败"));
        }
        public async Task<JsonResult> GetUserList(int page, int limit, string key)
        {
            var (total, list) = await sysUserService.GetUserListPage(page, limit, key);
            return new JsonResult(JsonResultWrap.Success("OK", total, list));
        }
    }
}