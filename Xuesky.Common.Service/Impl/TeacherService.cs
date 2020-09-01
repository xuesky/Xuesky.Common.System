using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xuesky.Common.DataAccess;

namespace Xuesky.Common.Service
{
    public class TeacherService : ITeacherService
    {
        private readonly SystemDbContext context;

        public TeacherService(SystemDbContext context)
        {
            this.context = context;
        }

        public async Task<int> DeleteTeacher(int[] teacherIds)
        {
            var result = await context
                .Orm
                .Update<SysModule>(teacherIds)
                .Set(d => d.IsDelete, true)
                .ExecuteAffrowsAsync();
            await context.SaveChangesAsync();
            return result;
        }

        public async Task<TeacherInfoOutput> GetTeacher(int teacherId) =>
                        await context
                        .TeacherInfos
                        .Select
                        .Where(s => s.TeacherId == teacherId)
                        .FirstAsync<TeacherInfoOutput>();

        public async Task<List<TeacherInfoOutput>> GetTeacherList(Expression<Func<TeacherInfo, bool>> func) =>
            await context
            .TeacherInfos
            .Select
            .Where(func)
            .ToListAsync<TeacherInfoOutput>();

        public async Task<(long total, List<TeacherInfoOutput> list)> GetTeacherListPage(int page, int limit, string key)
        {
            var dataSource = context.TeacherInfos.Select
            .WhereIf(!string.IsNullOrEmpty(key), s => s.TeacherName.Contains(key) || s.TeacherNo.Contains(key))
            .Count(out var total);
            if (limit > 0)
            {
                dataSource = dataSource.Page(page, limit);
            }
            var list = await dataSource.ToListAsync<TeacherInfoOutput>();
            return (total, list);
        }

        public async Task<int> InsertTeacher(TeacherInfoAddInput teacherInfoAddInput)
        {
            var teacher = Mapper.MapDefault<TeacherInfo>(teacherInfoAddInput);
            context.TeacherInfos.Add(teacher);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateTeacher(Expression<Func<TeacherInfo, bool>> condition, object obj)
        {
            var roleInfo = context
                .TeacherInfos
                .Select
                .Where(condition)
                .First();
            if (roleInfo == null)
            {
                await Task.CompletedTask;
                return 0;
            }
            var result = await context.Orm.Update<TeacherInfo>().SetDto(obj).Where(condition).ExecuteAffrowsAsync();
            await context.SaveChangesAsync();
            return result;
        }

        public async Task<int> UseOrStopTeacher(int[] teacherIds, bool isUse)
        {
            var result = await context
                .Orm
                .Update<TeacherInfo>(teacherIds)
                .Set(d => d.IsUse, isUse)
                .ExecuteAffrowsAsync();
            await context.SaveChangesAsync();
            return result;
        }
    }
}
