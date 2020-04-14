using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xuesky.Common.DataAccess;

namespace Xuesky.Common.Service
{
    public class SysUserService : ISysUserService
    {
        private readonly SystemDbContext context;

        public SysUserService(SystemDbContext context)
        {
            this.context = context;
        }
        public async Task<SysUser> GetUser(int userId) => await context
                        .SysUsers
                        .Select
                        .Where(s => s.UserId == userId)
                        .FirstAsync();

        public async Task<List<SysUser>> GetUserList(Expression<Func<SysUser, bool>> func) => await context
            .SysUsers
            .Select
            .Where(func)
            .ToListAsync();

        public async Task<(long total, List<SysUser> list)> GetUserListPage(int page, int limit, string key)
        {
            var dataSource = context.SysUsers.Select
            .WhereIf(!string.IsNullOrEmpty(key), s => s.UserName.Contains(key) || s.UserLogin.Contains(key))
            .Count(out var total);
            if (limit > 0)
            {
                dataSource = dataSource.Page(page, limit);
            }
            var list = await dataSource.ToListAsync();
            return (total, list);
        }
        public async Task<int> InsertSysUser(SysUser sysUser)
        {
            await context.SysUsers.AddAsync(sysUser);
            return await context.SaveChangesAsync();
        }

        public async Task<int> BatchInserSysUser(IEnumerable<SysUser> sysUsers)
        {
            await context.Orm.Insert(sysUsers).ExecuteSqlBulkCopyAsync();
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteSysUser(int[] userIds) => await context
                .Orm
                .Update<SysUser>(userIds)
                .Set(d => d.IsDelete, true)
                .ExecuteAffrowsAsync();

        public async Task<int> UpdateSysuer(Expression<Func<SysUser, bool>> condition, Expression<Func<SysUser, object>> obj)
        {
            var userInfo = context
                 .SysRoles
                 .Select
                 .Where(condition)
                 .First();
            if (userInfo == null)
            {
                await Task.CompletedTask;
                return 0;
            }
            return await context.Orm.Update<SysUser>().Set(obj).Where(condition).ExecuteAffrowsAsync();
        }

        public async Task<int> UseOrStopUser(int[] userIds, bool isUse) => await context
                .Orm
                .Update<SysUser>(userIds)
                .Set(d => d.IsUse, isUse)
                .ExecuteAffrowsAsync();


    }
}
