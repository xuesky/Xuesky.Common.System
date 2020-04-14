using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xuesky.Common.Service;

namespace Xuesky.Common.Web.ViewComponents
{
    public class ModuleNavViewComponent : ViewComponent
    {
        private readonly ISystemService systemService;

        public ModuleNavViewComponent(ISystemService systemService)
        {
            this.systemService = systemService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var moduleData = await systemService.GetSysModuleList(s => true);
            return View(moduleData);
        }
    }
}
