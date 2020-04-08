using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xuesky.Common.ClassLibary.Wrap;
using Xuesky.Common.Service;

namespace Xuesky.Common.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ModuleController : Controller
    {
        private readonly ISystemService systemService;

        public ModuleController(ISystemService systemService)
        {
            this.systemService = systemService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int moduleId)
        {
            var result = await systemService.DeleteSysModule(new int[] { moduleId });
            return new JsonResult(JsonResultWrap.Success("OK", result));
        }
        [HttpPost]
        public async Task<JsonResult> Use(int moduleId, bool isUse)
        {
            var result = await systemService.UseSysModule(new int[] { moduleId }, isUse);
            return new JsonResult(JsonResultWrap.Success("OK", result));
        }
        public async Task<JsonResult> GetModuleList(int page, int limit, string key)
        {
            (var total, var list) = await systemService.GetSysModules(page, limit, key);
            return new JsonResult(JsonResultWrap.Success("OK", total, list));
        }
    }
}