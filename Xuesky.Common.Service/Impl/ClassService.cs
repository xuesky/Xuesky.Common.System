using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xuesky.Common.DataAccess;

namespace Xuesky.Common.Service
{
    public class ClassService : IClassService
    {
        private readonly SystemDbContext context;

        public ClassService(SystemDbContext context)
        {
            this.context = context;
        }

        public async Task<int> DeleteClass(int[] classIds) => await context
                .Orm
                .Update<ClassInfo>(classIds)
                .Set(d => d.IsDelete, true)
                .ExecuteAffrowsAsync();

        public async Task<ClassInfoOutput> GetClass(int classId) => await context
                        .ClassInfos
                        .Select
                        .Where(s => s.ClassId == classId)
                        .FirstAsync<ClassInfoOutput>();
        public async Task<List<ClassInfoOutput>> GetClassList(Expression<Func<ClassInfo, bool>> func) => await context
            .ClassInfos
            .Select
            .Where(func)
            .ToListAsync<ClassInfoOutput>();

        public async Task<(long total, List<ClassInfoOutput> list)> GetClassListPage(int page, int limit, string key)
        {
            var dataSource = context.ClassInfos.Select
            .WhereIf(!string.IsNullOrEmpty(key), s => s.ClassName.Contains(key) || s.ClassNo.Contains(key))
            .Count(out var total);
            if (limit > 0)
            {
                dataSource = dataSource.Page(page, limit);
            }
            var list = await dataSource.ToListAsync<ClassInfoOutput>();
            return (total, list);
        }

        public async Task<int> InsertClass(ClassInfoAddInput classInfoAddInput)
        {
            var classInfo = Mapper.MapDefault<ClassInfo>(classInfoAddInput);
            context.ClassInfos.Add(classInfo);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateClass(Expression<Func<ClassInfo, bool>> condition, object obj)
        {
            var roleInfo = context
                .ClassInfos
                .Select
                .Where(condition)
                .First();
            if (roleInfo == null)
            {
                await Task.CompletedTask;
                return 0;
            }
            return await context.Orm.Update<ClassInfo>().SetDto(obj).Where(condition).ExecuteAffrowsAsync();
        }

        public async Task<int> UseOrStopClass(int[] classIds, bool isUse) => await context
                .Orm
                .Update<ClassInfo>(classIds)
                .Set(d => d.IsUse, isUse)
                .ExecuteAffrowsAsync();
    }
}
