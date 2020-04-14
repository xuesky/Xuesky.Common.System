using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public async Task<int> InsertSysModule(SysModule sysModule)
        {
            context.SysModules.Add(sysModule);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteSysModule(int[] moduleIds) => await context
                .Orm
                .Update<SysModule>(moduleIds)
                .Set(d => d.IsDelete, true)
                .ExecuteAffrowsAsync();
        public async Task<SysModule> GetSysModule(int moduleId) => await context
                .SysModules
                .Select
                .Where(s => s.ModuleId == moduleId)
                .FirstAsync();
        public async Task<List<SysModule>> GetSysModuleList(Expression<Func<SysModule, bool>> func) => await context
            .SysModules
            .Select
            .Where(func)
            .ToListAsync();

        public async Task<(long total, List<SysModule> list)> GetSysModuleListPage(int page, int limit, string key)
        {
            var dataSource = context.SysModules.Select
                .WhereIf(!string.IsNullOrEmpty(key), s => s.ModuleName.Contains(key) || s.ModuleCode.Contains(key))
                .Count(out var total);
            if (limit > 0)
            {
                dataSource = dataSource.Page(page, limit);
            }
            var list = await dataSource.ToListAsync();
            return (total, list);
        }

        public async Task<int> UseOrStopSysModule(int[] moduleIds, bool isUse) => await context
                .Orm
                .Update<SysModule>(moduleIds)
                .Set(d => d.IsUse, isUse)
                .ExecuteAffrowsAsync();

        public async Task<int> UpdateSysModule(Expression<Func<SysModule, bool>> condition, Expression<Func<SysModule, object>> obj)
        {
            var moduleInfo = context
                .SysModules
                .Select
                .Where(condition)
                .First();
            if (moduleInfo == null)
            {
                await Task.CompletedTask;
                return 0;
            }
            return await context.Orm.Update<SysModule>().Set(obj).Where(condition).ExecuteAffrowsAsync();

        }
    }
}
