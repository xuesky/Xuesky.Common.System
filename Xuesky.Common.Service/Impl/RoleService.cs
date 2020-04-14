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

        public async Task<int> DeleteRole(int[] roleIds) => await context
                .Orm
                .Update<SysModule>(roleIds)
                .Set(d => d.IsDelete, true)
                .ExecuteAffrowsAsync();

        public async Task<SysRole> GetRole(int roleId) => await context
                        .SysRoles
                        .Select
                        .Where(s => s.RoleId == roleId)
                        .FirstAsync();
        public async Task<List<SysRole>> GetRoleList(Expression<Func<SysRole, bool>> func) => await context
            .SysRoles
            .Select
            .Where(func)
            .ToListAsync();

        public async Task<(long total, List<SysRole> list)> GetRoleListPage(int page, int limit, string key)
        {
            var dataSource = context.SysRoles.Select
    .WhereIf(!string.IsNullOrEmpty(key), s => s.RoleName.Contains(key))
    .Count(out var total);
            if (limit > 0)
            {
                dataSource = dataSource.Page(page, limit);
            }
            var list = await dataSource.ToListAsync();
            return (total, list);
        }

        public async Task<int> InsertRole(SysRole Role)
        {
            context.SysRoles.Add(Role);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateRole(Expression<Func<SysRole, bool>> condition, Expression<Func<SysRole, object>> obj)
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
            return await context.Orm.Update<SysRole>().Set(obj).Where(condition).ExecuteAffrowsAsync();
        }

        public async Task<int> UseOrStopRole(int[] roleIds, bool isUse) => await context
                .Orm
                .Update<SysRole>(roleIds)
                .Set(d => d.IsUse, isUse)
                .ExecuteAffrowsAsync();
    }
}
