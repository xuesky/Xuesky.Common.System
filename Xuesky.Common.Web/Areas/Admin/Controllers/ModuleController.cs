using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Xuesky.Common.ClassLibary;
using Xuesky.Common.Service;

namespace Xuesky.Common.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 模块信息管理
    /// </summary>
    [Area("admin")]
    public class ModuleController : Controller
    {
        private readonly IModuleService systemService;

        public ModuleController(IModuleService systemService)
        {
            this.systemService = systemService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddModule()
        {
            ViewBag.Modules = await systemService.GetSysModuleList(s => s.ModuleCode.Length == 4);
            return View();
        }
        public async Task<IActionResult> EditModule(int moduleId)
        {
            var list = await systemService.GetSysModuleList(s => s.ModuleCode.Length == 4);
            ViewBag.Modules = list;
            ViewBag.ModuleId = moduleId;
            return View();
        }
        public async Task<JsonResult> GetModule(int moduleId)
        {
            var module = await systemService.GetSysModule(moduleId);
            return new JsonResult(JsonResultWrap.Success("获取成功", 1, module));
        }
        /// <summary>
        /// 修改模块数据
        /// </summary>
        /// <param name="sysModuleUpdateInput"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> EditModule(SysModuleUpdateInput sysModuleUpdateInput)
        {
            var moduleList = await systemService.GetSysModuleList(s => s.ModuleCode == sysModuleUpdateInput.ModuleCode);
            if (moduleList.Any() && moduleList[0].ModuleId != sysModuleUpdateInput.ModuleId)
            {
                return new JsonResult(JsonResultWrap.Fail($"修改失败,编码:[{sysModuleUpdateInput.ModuleCode}]已经被占用"));
            }
            sysModuleUpdateInput.ModuleUrl = sysModuleUpdateInput.ModuleUrl.ToLower();
            var result = await systemService.UpdateSysModule(s => s.ModuleId == sysModuleUpdateInput.ModuleId,
                sysModuleUpdateInput);
            return new JsonResult(result > 0 ? JsonResultWrap.Success("修改成功", result) : JsonResultWrap.Fail("修改失败"));
        }
        [HttpPost]
        public async Task<JsonResult> AddModule(SysModuleAddInput sysModuleAddInput)
        {
            var moduleList = await systemService.GetSysModuleList(s => s.ModuleCode == sysModuleAddInput.ModuleCode);
            if (moduleList.Any())
            {
                return new JsonResult(JsonResultWrap.Fail($"添加失败,编码:[{sysModuleAddInput.ModuleCode}]已经被占用"));
            }
            sysModuleAddInput.ModuleUrl = sysModuleAddInput.ModuleUrl.ToLower();
            var result = await systemService.InsertSysModule(sysModuleAddInput);
            return new JsonResult(result > 0 ? JsonResultWrap.Success("添加成功", result) : JsonResultWrap.Fail("添加失败"));
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int moduleId)
        {
            var result = await systemService.DeleteSysModule(new int[] { moduleId });
            return new JsonResult(result > 0 ? JsonResultWrap.Success("删除成功", result) : JsonResultWrap.Fail("删除失败"));
        }
        [HttpPost]
        public async Task<JsonResult> Use(int moduleId, bool isUse)
        {
            var result = await systemService.UseOrStopSysModule(new int[] { moduleId }, isUse);
            return new JsonResult(result > 0 ? JsonResultWrap.Success("更新成功", result) : JsonResultWrap.Fail("更新失败"));
        }
        public async Task<JsonResult> GetModuleList(int page, int limit, string key)
        {
            (var total, var list) = await systemService.GetSysModuleListPage(page, limit, key);
            return new JsonResult(JsonResultWrap.Success("OK", total, list));
        }
    }
}