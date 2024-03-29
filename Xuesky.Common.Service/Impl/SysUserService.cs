using Omu.ValueInjecter;
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
        public async Task<SysUserOutput> GetUser(int userId) =>
                        await context
                        .SysUsers
                        .Select
                        .Where(s => s.UserId == userId)
                        .FirstAsync<SysUserOutput>();

        public async Task<List<SysUserOutput>> GetUserList(Expression<Func<SysUser, bool>> func)
        {
            return await context
             .SysUsers
             .Select
             .Where(func)
             .ToListAsync<SysUserOutput>();
        }

        public async Task<(long total, List<SysUserOutput> list)> GetUserListPage(int page, int limit, string key)
        {
            var dataSource = context
                .SysUsers
                .Select
                .WhereIf(!string.IsNullOrEmpty(key), s => s.UserName.Contains(key) || s.UserLogin.Contains(key))
                .Count(out var total);
            if (limit > 0)
            {
                dataSource = dataSource.Page(page, limit);
            }
            var list = await dataSource.ToListAsync<SysUserOutput>();
            return (total, list);
        }
        public async Task<int> InsertSysUser(SysUserAddInput sysUserAddInput)
        {
            var sysUser = Mapper.Map<SysUserAddInput, SysUser>(sysUserAddInput);
            sysUser.UserAddtime = sysUser.UserLasttime = DateTime.Now;
            var userId = await context.Orm.Insert(sysUser).ExecuteIdentityAsync();
            var userRole = new SysUserRole { UserId = (int)userId, RoleId = sysUserAddInput.RoleId };
            context.SysUserRoles.Add(userRole);
            return await context.SaveChangesAsync();
        }

        public async Task<int> BatchInserSysUser(IEnumerable<SysUserAddInput> sysUserAddInputs)
        {
            var sysUsers = Mapper.Map<IEnumerable<SysUserAddInput>, IEnumerable<SysUser>>(sysUserAddInputs);
            var result = await context.Orm.Insert(sysUsers).ExecuteAffrowsAsync();
            await context.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteSysUser(int[] userIds)
        {
            var result = await context
                 .Orm
                 .Update<SysUser>(userIds)
                 .Set(d => d.IsDelete, true)
                 .ExecuteAffrowsAsync();
            await context.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateSysuer(Expression<Func<SysUser, bool>> condition, object obj)
        {
            var userInfo = context
                 .SysUsers
                 .Select
                 .Where(condition)
                 .First();
            if (userInfo == null)
            {
                await Task.CompletedTask;
                return 0;
            }
            var result = await context.Orm.Update<SysUser>().SetDto(obj).Where(condition).ExecuteAffrowsAsync();
            await context.SaveChangesAsync();
            return result;
        }

        public async Task<int> UseOrStopUser(int[] userIds, bool isUse)
        {
            var result = await context
                  .Orm
                  .Update<SysUser>(userIds)
                  .Set(d => d.IsUse, isUse)
                  .ExecuteAffrowsAsync();
            await context.SaveChangesAsync();
            return result;
        }


    }
}
