using Microsoft.AspNetCore.Mvc;
using Omu.ValueInjecter;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xuesky.Common.ClassLibary.Security;
using Xuesky.Common.ClassLibary.Wrap;
using Xuesky.Common.DataAccess;
using Xuesky.Common.Service;

namespace Xuesky.Common.Web.Areas.Admin.Controllers
{
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
            var role = await sysUserService.GetUser(userId);
            return new JsonResult(JsonResultWrap.Success("获取成功", 1, role));
        }
        [HttpPost]
        public async Task<JsonResult> EditUser(VM_UserInfo vmUser)
        {
            var userList = await sysUserService.GetUserList(s => s.UserLogin == vmUser.UserLogin);
            if (userList.Any() && userList[0].UserId != vmUser.UserId)
            {
                return new JsonResult(JsonResultWrap.Fail($"修改失败,登录ID:[{vmUser.UserLogin}]已经存在"));
            }
            var result = await sysUserService.UpdateSysuer(s => s.UserId == vmUser.UserId,
                o => new { vmUser.UserLogin, vmUser.UserName, vmUser.UserGender, vmUser.UserTel, vmUser.IsUse, vmUser.Remark });
            return new JsonResult(result > 0 ? JsonResultWrap.Success("修改成功", result) : JsonResultWrap.Fail("修改失败"));
        }
        /// <summary>
        /// AddRole
        /// </summary>
        /// <param name="vmUser"></param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        [HttpPost]
        public async Task<JsonResult> AddUser(VM_UserInfo vmUser)
        {
            var userList = await sysUserService.GetUserList(s => s.UserLogin == vmUser.UserLogin);
            if (userList.Any() && userList.First().UserId
                != vmUser.UserId)
            {
                return new JsonResult(JsonResultWrap.Fail($"添加失败,角色名称:[{vmUser.UserLogin}]已经被占用"));
            }

            var sysUser = Mapper.Map<VM_UserInfo, SysUser>(vmUser);
            sysUser.UserAddtime = sysUser.UserLasttime = DateTime.Now;
            sysUser.OrgId = 1;
            sysUser.UserPwd = Md5Crypto.Md5Hash(sysUser.UserPwd);
            var result = await sysUserService.InsertSysUser(sysUser);
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