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
        /// 获取老师基本信息视图数据
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        Task<TeacherInfoOutput> GetTeacher(int teacherId);
        /// <summary>
        /// 获取老师基本信息视图数据列表
        /// </summary>
        /// <returns></returns>
        Task<List<TeacherInfoOutput>> GetTeacherList(Expression<Func<TeacherInfo, bool>> func);
        /// <summary>
        /// 获取老师基本信息视图数据分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<(long total, List<TeacherInfoOutput> list)> GetTeacherListPage(int page, int limit, string key);

        /// <summary>
        /// 添加老师数据
        /// </summary>
        /// <param name="Teacher"></param>
        /// <returns></returns>
        Task<int> InsertTeacher(TeacherInfoAddInput Teacher);

        /// <summary>
        /// 更新老师数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="obj">更新对象</param>
        /// <returns></returns>
        Task<int> UpdateTeacher(Expression<Func<TeacherInfo, bool>> condition, object obj);

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
