using CSScriptLib;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xuesky.Common.DataAccess;

namespace Xuesky.Common.Service
{
    public class RoleService : IRoleService
    {
        private readonly SystemDbContext context;

        public RoleService(SystemDbContext context)
        {
            this.context = context;
        }
        #region Role
        public async Task<int> DeleteRole(int[] roleIds)
        {
            var result = await context
                 .Orm
                 .Update<SysModule>(roleIds)
                 .Set(d => d.IsDelete, true)
                 .ExecuteAffrowsAsync();
            await context.SaveChangesAsync();
            return result;
        }

        public async Task<SysRoleOutput> GetRole(int roleId) => await context
                        .SysRoles
                        .Select
                        .Where(s => s.RoleId == roleId)
                        .FirstAsync<SysRoleOutput>();
        public async Task<List<SysRoleOutput>> GetRoleList(Expression<Func<SysRole, bool>> func) => await context
            .SysRoles
            .Select
            .Where(func)
            .ToListAsync<SysRoleOutput>();

        public async Task<(long total, List<SysRoleOutput> list)> GetRoleListPage(int page, int limit, string key)
        {
            var dataSource = context.SysRoles.Select
            .WhereIf(!string.IsNullOrEmpty(key), s => s.RoleName.Contains(key))
            .Count(out var total);
            if (limit > 0)
            {
                dataSource = dataSource.Page(page, limit);
            }
            var list = await dataSource.ToListAsync<SysRoleOutput>();
            return (total, list);
        }

        public async Task<int> InsertRole(SysRoleAddInput sysRoleAddInput)
        {
            var role = Mapper.MapDefault<SysRole>(sysRoleAddInput);
            context.SysRoles.Add(role);
            return await context.SaveChangesAsync();
        }



        public async Task<int> UpdateRole(Expression<Func<SysRole, bool>> condition, object obj)
        {
            var roleInfo = context
                .SysRoles
                .Select
                .Where(condition)
                .First();
            if (roleInfo == null)
            {
                await Task.CompletedTask;
                return 0;
            }
            var result = await context.Orm.Update<SysRole>().SetDto(obj).Where(condition).ExecuteAffrowsAsync();
            await context.SaveChangesAsync();
            return result;
        }

        public async Task<int> UseOrStopRole(int[] roleIds, bool isUse)
        {
            var result = await context
                 .Orm
                 .Update<SysRole>(roleIds)
                 .Set(d => d.IsUse, isUse)
                 .ExecuteAffrowsAsync();
            await context.SaveChangesAsync();
            return result;
        }
        #endregion
        public async Task<int> RoleModuleAuthorize(int roleId, int[] modules)
        {
            await context.SysRoleModules.RemoveAsync(s => s.RoleId == roleId);
            List<SysRoleModule> list = new List<SysRoleModule>();
            modules.ForEach(s =>
            {
                list.Add(new SysRoleModule { RoleId = roleId, ModuleId = s, IsDelete = false });
            });
            await context.SysRoleModules.AddRangeAsync(list);

            return await context.SaveChangesAsync();
        }
    }
}
