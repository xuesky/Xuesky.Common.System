using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xuesky.Common.DataAccess;

namespace Xuesky.Common.Service
{
    public class StuService : IStuService
    {
        private readonly SystemDbContext context;

        public StuService(SystemDbContext context)
        {
            this.context = context;
        }

        public async Task<int> DeleteStu(int[] stuIds)
        {
            var result = await context
                .Orm
                .Update<StuInfo>(stuIds)
                .Set(d => d.IsDelete, true)
                .ExecuteAffrowsAsync();
            await context.SaveChangesAsync();
            return result;
        }

        public async Task<StuInfoOutput> GetStu(int StuId) => await context
                        .StuInfos
                        .Select
                        .Where(s => s.StuId == StuId)
                        .FirstAsync<StuInfoOutput>();
        public async Task<List<StuInfoOutput>> GetStuList(Expression<Func<StuInfo, bool>> func) =>
            await context
            .StuInfos
            .Select
            .Where(func)
            .ToListAsync<StuInfoOutput>();

        public async Task<(long total, List<StuInfoClassInfoOutput> list)> GetStuListPage(int page, int limit, string key)
        {
            var dataSource = context.StuInfos.Select
            .WhereIf(!string.IsNullOrEmpty(key), s => s.StuName.Contains(key) || s.StuNo.Contains(key))
            .Include(s => s.class_info)
            .Count(out var total);
            if (limit > 0)
            {
                dataSource = dataSource.Page(page, limit);
            }
            var list = await dataSource.ToListAsync<StuInfoClassInfoOutput>();
            return (total, list);
        }

        public async Task<int> InsertStu(StuInfoAddInput stuInfoAddInput)
        {
            var stuInfo = Mapper.MapDefault<StuInfo>(stuInfoAddInput);
            context.StuInfos.Add(stuInfo);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateStu(Expression<Func<StuInfo, bool>> condition, object obj)
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
            var result = await context.Orm.Update<StuInfo>().SetDto(obj).Where(condition).ExecuteAffrowsAsync();
            await context.SaveChangesAsync();
            return result;
        }

        public async Task<int> UseOrStopStu(int[] stuIds, bool isUse)
        {
            var result = await context
                 .Orm
                 .Update<StuInfo>(stuIds)
                 .Set(d => d.IsUse, isUse)
                 .ExecuteAffrowsAsync();
            await context.SaveChangesAsync();
            return result;
        }
    }
}