using System.Collections.Generic;
using System.Threading.Tasks;
using Xuesky.Common.DataAccess;
namespace Xuesky.Common.Service
{
    public class SystemService : ISystemService
    {
        private readonly SystemDbContext context;

        public SystemService(SystemDbContext context)
        {
            this.context = context;
        }

        public async Task<int> AddSysModules(SysModule sysModule)
        {
            context.SysModules.Add(sysModule);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteSysModule(int[] moduleId)
        {
            var result = await context.Orm.Update<SysModule>(moduleId).Set(d => d.IsDelete, true).ExecuteAffrowsAsync();
            return result;
        }

        public async Task<SysModule> GetSysModule(int moduleId)
        {
            var moduleInfo = await context.SysModules.Select.Where(s => s.ModuleId == moduleId).FirstAsync();
            return moduleInfo;
        }

        public async Task<(long total, List<SysModule> list)> GetSysModules(int page, int limit, string key)
        {
            var list = await context.SysModules.Select
                .WhereIf(!string.IsNullOrEmpty(key), s => s.ModuleName.Contains(key) || s.ModuleCode.Contains(key))
                .Count(out var total)
                .Page(page, limit)
                .ToListAsync();
            return (total, list);
        }

        public async Task<int> UseSysModule(int[] moduleId, bool isUse)
        {
            var result = await context.Orm.Update<SysModule>(moduleId).Set(d => d.IsUse, isUse).ExecuteAffrowsAsync();
            return result;
        }

        public async Task<int> UpdateSysModules(SysModule sysModule)
        {
            var moduleInfo = context.SysModules.Select.Where(s => s.ModuleId == sysModule.ModuleId).First();
            if (moduleInfo == null)
            {
                await Task.CompletedTask;
                return 0;
            }
            context.SysModules.Update(moduleInfo);
            return await context.SaveChangesAsync();
        }
    }
}
