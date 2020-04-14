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

        public async Task<int> DeleteTeacher(int[] teacherIds) => await context
                .Orm
                .Update<SysModule>(teacherIds)
                .Set(d => d.IsDelete, true)
                .ExecuteAffrowsAsync();

        public async Task<TeacherInfo> GetTeacher(int teacherId) => await context
                        .TeacherInfos
                        .Select
                        .Where(s => s.TeacherId == teacherId)
                        .FirstAsync();
        public async Task<List<TeacherInfo>> GetTeacherList(Expression<Func<TeacherInfo, bool>> func) => await context
            .TeacherInfos
            .Select
            .Where(func)
            .ToListAsync();

        public async Task<(long total, List<TeacherInfo> list)> GetTeacherListPage(int page, int limit, string key)
        {
            var dataSource = context.TeacherInfos.Select
            .WhereIf(!string.IsNullOrEmpty(key), s => s.TeacherName.Contains(key) || s.TeacherNo.Contains(key))
            .Count(out var total);
            if (limit > 0)
            {
                dataSource = dataSource.Page(page, limit);
            }
            var list = await dataSource.ToListAsync();
            return (total, list);
        }

        public async Task<int> InsertTeacher(TeacherInfo Role)
        {
            context.TeacherInfos.Add(Role);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateTeacher(Expression<Func<TeacherInfo, bool>> condition, Expression<Func<TeacherInfo, object>> obj)
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
            return await context.Orm.Update<TeacherInfo>().Set(obj).Where(condition).ExecuteAffrowsAsync();
        }

        public async Task<int> UseOrStopTeacher(int[] teacherIds, bool isUse) => await context
                .Orm
                .Update<TeacherInfo>(teacherIds)
                .Set(d => d.IsUse, isUse)
                .ExecuteAffrowsAsync();
    }
}
