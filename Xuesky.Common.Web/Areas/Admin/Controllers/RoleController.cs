using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Xuesky.Common.ClassLibary;
using Xuesky.Common.Service;

namespace Xuesky.Common.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 角色信息管理
    /// </summary>
    [Area("admin")]
    public class RoleController : Controller
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddRole()
        {
            return View();
        }
        public IActionResult EditRole(int roleId)
        {
            ViewBag.RoleId = roleId;
            return View();
        }
        public async Task<JsonResult> GetRole(int roleId)
        {
            var role = await roleService.GetRole(roleId);
            return new JsonResult(JsonResultWrap.Success("获取成功", 1, role));
        }
        [HttpPost]
        public async Task<JsonResult> EditRole(SysRoleUpdateInput sysRoleUpdateInput)
        {
            var roleList = await roleService.GetRoleList(s => s.RoleName == sysRoleUpdateInput.RoleName);
            if (roleList.Any() && roleList[0].RoleId != sysRoleUpdateInput.RoleId)
            {
                return new JsonResult(JsonResultWrap.Fail($"修改失败,角色名称:[{sysRoleUpdateInput.RoleName}]已经存在"));
            }
            var result = await roleService.UpdateRole(s => s.RoleId == sysRoleUpdateInput.RoleId,
                sysRoleUpdateInput);
            return new JsonResult(result > 0 ? JsonResultWrap.Success("修改成功", result) : JsonResultWrap.Fail("修改失败"));
        }
        /// <summary>
        /// AddRole
        /// </summary>
        /// <param name="sysRoleAddInput"></param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        [HttpPost]
        public async Task<JsonResult> AddRole(SysRoleAddInput sysRoleAddInput)
        {
            var roleList = await roleService.GetRoleList(s => s.RoleName == sysRoleAddInput.RoleName);
            if (roleList.Any() && roleList.First().RoleId
                != sysRoleAddInput.RoleId)
            {
                return new JsonResult(JsonResultWrap.Fail($"添加失败,角色名称:[{sysRoleAddInput.RoleName}]已经被占用"));
            }
            var result = await roleService.InsertRole(sysRoleAddInput);
            return new JsonResult(result > 0 ? JsonResultWrap.Success("添加成功", result) : JsonResultWrap.Fail("添加失败"));
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int roleId)
        {
            var result = await roleService.DeleteRole(new int[] { roleId });
            return new JsonResult(result > 0 ? JsonResultWrap.Success("删除成功", result) : JsonResultWrap.Fail("删除失败"));
        }
        [HttpPost]
        public async Task<JsonResult> Use(int roleId, bool isUse)
        {
            var result = await roleService.UseOrStopRole(new int[] { roleId }, isUse);
            return new JsonResult(result > 0 ? JsonResultWrap.Success("更新成功", result) : JsonResultWrap.Fail("更新失败"));
        }
        public async Task<JsonResult> GetRoleList(int page, int limit, string key)
        {
            var (total, list) = await roleService.GetRoleListPage(page, limit, key);
            return new JsonResult(JsonResultWrap.Success("OK", total, list));
        }
    }
}