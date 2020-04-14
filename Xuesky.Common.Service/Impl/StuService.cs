using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xuesky.Common.DataAccess;

namespace Xuesky.Common.Service
{
    public class StuService:IStuService
    {
        private readonly SystemDbContext context;

        public StuService(SystemDbContext context)
        {
            this.context = context;
        }

        public async Task<int> DeleteStu(int[] stuIds) => await context
                .Orm
                .Update<StuInfo>(stuIds)
                .Set(d => d.IsDelete, true)
                .ExecuteAffrowsAsync();

        public async Task<StuInfo> GetStu(int StuId) => await context
                        .StuInfos
                        .Select
                        .Where(s => s.StuId == StuId)
                        .FirstAsync();
        public async Task<List<StuInfo>> GetStuList(Expression<Func<StuInfo, bool>> func) => await context
            .StuInfos
            .Select
            .Where(func)
            .ToListAsync();

        public async Task<(long total, List<StuInfo> list)> GetStuListPage(int page, int limit, string key)
        {
            var dataSource = context.StuInfos.Select
            .WhereIf(!string.IsNullOrEmpty(key), s => s.StuName.Contains(key) || s.StuNo.Contains(key))
            .Count(out var total);
            if (limit > 0)
            {
                dataSource = dataSource.Page(page, limit);
            }
            var list = await dataSource.ToListAsync();
            return (total, list);
        }

        public async Task<int> InsertStu(StuInfo Role)
        {
            context.StuInfos.Add(Role);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateStu(Expression<Func<StuInfo, bool>> condition, Expression<Func<StuInfo, object>> obj)
        {
            var roleInfo = context
                .StuInfos
                .Select
                .Where(condition)
                .First();
            if (roleInfo == null)
            {
                await Task.CompletedTask;
                return 0;
            }
            return await context.Orm.Update<StuInfo>().Set(obj).Where(condition).ExecuteAffrowsAsync();
        }

        public async Task<int> UseOrStopStu(int[] stuIds, bool isUse) => await context
                .Orm
                .Update<StuInfo>(stuIds)
                .Set(d => d.IsUse, isUse)
                .ExecuteAffrowsAsync();
    }
}