using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xuesky.Common.DataAccess;

namespace Xuesky.Common.Service
{
    public interface ITeacherService
    {
        #region Teacher
        /// <summary>
        /// 获取老师实体数据
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        Task<TeacherInfo> GetTeacher(int teacherId);
        /// <summary>
        /// 获取老师列表
        /// </summary>
        /// <returns></returns>
        Task<List<TeacherInfo>> GetTeacherList(Expression<Func<TeacherInfo, bool>> func);
        /// <summary>
        /// 获取老师列表分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<(long total, List<TeacherInfo> list)> GetTeacherListPage(int page, int limit, string key);

        /// <summary>
        /// 添加老师数据
        /// </summary>
        /// <param name="Teacher"></param>
        /// <returns></returns>
        Task<int> InsertTeacher(TeacherInfo Teacher);

        /// <summary>
        /// 更新老师数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="obj">更新对象</param>
        /// <returns></returns>
        Task<int> UpdateTeacher(Expression<Func<TeacherInfo, bool>> condition, Expression<Func<TeacherInfo, object>> obj);

        /// <summary>
        /// 删除老师数据
        /// </summary>
        /// <param name="teacherIds"></param>
        /// <returns></returns>
        Task<int> DeleteTeacher(int[] teacherIds);

        /// <summary>
        /// 停用/启用老师数据
        /// </summary>
        /// <param name="teacherIds"></param>
        /// <returns></returns>
        Task<int> UseOrStopTeacher(int[] teacherIds, bool isUse);
        #endregion
    }
}
