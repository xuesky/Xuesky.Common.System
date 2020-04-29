using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xuesky.Common.ClassLibary.Cache;
using Xuesky.Common.Service;
using Xuesky.Common.Web.ConfigModels;
using Xuesky.Common.Web.Extenstions;

namespace Xuesky.Common.Web.ViewComponents
{
    public class ModuleNavViewComponent : ViewComponent
    {
        private readonly IModuleService systemService;
        private readonly IdentityExtentions identityExtentions;
        private readonly ICache cache;
        private readonly IConfiguration configuration;

        public ModuleNavViewComponent(IModuleService systemService, IdentityExtentions identityExtentions, ICache cache, IConfiguration configuration)
        {
            this.systemService = systemService;
            this.identityExtentions = identityExtentions;
            this.cache = cache;
            this.configuration = configuration;
        }
        /// <summary>
        /// InvokeAsync
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userRoleConfig = configuration.Get<UserRoleConfig>();
            int roleId = identityExtentions.getRoleId();
            Expression<System.Func<DataAccess.SysModule, bool>> func = null;
            if (roleId == userRoleConfig.SuperRole)
                func = s => true;
            else
                func = s => s.sys_role_modules.AsSelect().Where(r => r.RoleId == roleId).ToList(v => v.ModuleId)
                .Contains(s.ModuleId);
            var cacheKey = string.Format(CacheKey.UserPermissions, roleId);
            var exists = await cache.ExistsAsync(cacheKey);
            if (exists)
            {
                var cacheValue = await cache.GetAsync<List<SysModuleOutput>>(cacheKey);
                return View(cacheValue);
            }
            var moduleData = await systemService.GetSysModuleList(func);
            await cache.SetAsync(cacheKey, moduleData);
            return View(moduleData);
        }
    }
}
