using System.Collections.Generic;
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

        public async Task<int> InsertSysUser(SysUser sysUser)
        {
            await context.SysUsers.AddAsync(sysUser);
            return await context.SaveChangesAsync();
        }

        public async Task<int> InserSysUser(IEnumerable<SysUser> sysUsers)
        {
            await context.Orm.Insert(sysUsers).ExecuteSqlBulkCopyAsync();
            //await context.AddRangeAsync(sysUserList);
            return await context.SaveChangesAsync();
        }

        public Task<int> DelSysUser(SysUser sysUser)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DelSysUser(IEnumerable<SysUser> sysUsers)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DelSysUser(int[] ids)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> UpdateSysuer(SysUser sysUser)
        {
            throw new System.NotImplementedException();
        }
    }
}
