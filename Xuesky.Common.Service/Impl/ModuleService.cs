using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xuesky.Common.DataAccess;

namespace Xuesky.Common.Service
{
    public class ModuleService : IModuleService
    {
        private readonly SystemDbContext context;

        public ModuleService(SystemDbContext context)
        {
            this.context = context;
        }

        public async Task<int> InsertSysModule(SysModuleAddInput sysModuleAddInput)
        {
            var sysModule = Mapper.MapDefault<SysModule>(sysModuleAddInput);
            context.SysModules.Add(sysModule);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteSysModule(int[] moduleIds)
        {
            var result = await context
                 .Orm
                 .Update<SysModule>(moduleIds)
                 .Set(d => d.IsDelete, true)
                 .ExecuteAffrowsAsync();
            await context.SaveChangesAsync();
            return result;
        }
        public async Task<SysModuleOutput> GetSysModule(int moduleId) => await context
                .SysModules
                .Select
                .Where(s => s.ModuleId == moduleId)
                .FirstAsync<SysModuleOutput>();
        public async Task<List<SysModuleOutput>> GetSysModuleList(Expression<Func<SysModule, bool>> func) => await context
            .SysModules
            .Select
            .Where(func)
            .ToListAsync<SysModuleOutput>();
        /// <summary>
        /// GetSysModuleByRoleList
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public async Task<List<SysModuleOutput>> GetSysModuleByRoleList(int roleId, Expression<Func<SysModule, bool>> func)
        {
            var modules = await context
             .SysModules
             .Select
             .Where(func)
             .OrderByDescending(s => s.Order)
             .ToListAsync<SysModuleOutput>();

            var roleModules = await context.SysRoleModules.Select.Where(s => s.RoleId == roleId).ToListAsync();
            modules.ForEach(s =>
            {
                s.IsProcess = roleModules.Any(a => a.ModuleId == s.ModuleId);
            });
            return modules;
        }
        public async Task<(long total, List<SysModuleOutput> list)> GetSysModuleListPage(int page, int limit, string key)
        {
            var dataSource = context.SysModules.Select
                .WhereIf(!string.IsNullOrEmpty(key), s => s.ModuleName.Contains(key) || s.ModuleCode.Contains(key))
                .Count(out var total)
                ;
            if (limit > 0)
            {
                dataSource = dataSource.Page(page, limit);
            }
            var list = await dataSource.ToListAsync<SysModuleOutput>();
            return (total, list);
        }

        public async Task<int> UseOrStopSysModule(int[] moduleIds, bool isUse)
        {
            var result = await context
                .Orm
                .Update<SysModule>(moduleIds)
                .Set(d => d.IsUse, isUse)
                .ExecuteAffrowsAsync();
            await context.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateSysModule(Expression<Func<SysModule, bool>> condition, object obj)
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
            var result = await context.Orm.Update<SysModule>().SetDto(obj).Where(condition).ExecuteAffrowsAsync();
            await context.SaveChangesAsync();
            return result;

        }
    }
}
