using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xuesky.Common.DataAccess;

namespace Xuesky.Common.Service
{
    public interface IStuService
    {
        #region Student
        /// <summary>
        /// 获取学生实体数据
        /// </summary>
        /// <param name="StuId"></param>
        /// <returns></returns>
        Task<StuInfoOutput> GetStu(int StuId);
        /// <summary>
        /// 获取学生列表
        /// </summary>
        /// <returns></returns>
        Task<List<StuInfoOutput>> GetStuList(Expression<Func<StuInfo, bool>> func);
        /// <summary>
        /// 获取学生列表分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<(long total, List<StuInfoClassInfoOutput> list)> GetStuListPage(int page, int limit, string key);

        /// <summary>
        /// 添加学生数据
        /// </summary>
        /// <param name="Stu"></param>
        /// <returns></returns>
        Task<int> InsertStu(StuInfoAddInput Stu);

        /// <summary>
        /// 更新学生数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="obj">更新对象</param>
        /// <returns></returns>
        Task<int> UpdateStu(Expression<Func<StuInfo, bool>> condition, object obj);

        /// <summary>
        /// 删除学生数据
        /// </summary>
        /// <param name="StuIds"></param>
        /// <returns></returns>
        Task<int> DeleteStu(int[] stuIds);

        /// <summary>
        /// 停用/启用学生数据
        /// </summary>
        /// <param name="StuIds"></param>
        /// <returns></returns>
        Task<int> UseOrStopStu(int[] stuIds, bool isUse);
        #endregion
    }
}
