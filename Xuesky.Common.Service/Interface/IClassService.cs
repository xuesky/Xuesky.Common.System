using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xuesky.Common.DataAccess;

namespace Xuesky.Common.Service
{
    public interface IClassService
    {
        #region Class
        /// <summary>
        /// 获取班级实体数据
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        Task<ClassInfoOutput> GetClass(int classId);
        /// <summary>
        /// 获取班级列表
        /// </summary>
        /// <returns></returns>
        Task<List<ClassInfoOutput>> GetClassList(Expression<Func<ClassInfo, bool>> func);
        /// <summary>
        /// 获取班级列表分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<(long total, List<ClassInfoOutput> list)> GetClassListPage(int page, int limit, string key);

        /// <summary>
        /// 添加班级数据
        /// </summary>
        /// <param name="classInfo"></param>
        /// <returns></returns>
        Task<int> InsertClass(ClassInfoAddInput classInfo);

        /// <summary>
        /// 更新班级数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="obj">更新对象</param>
        /// <returns></returns>
        Task<int> UpdateClass(Expression<Func<ClassInfo, bool>> condition, object obj);

        /// <summary>
        /// 删除班级数据
        /// </summary>
        /// <param name="classIds"></param>
        /// <returns></returns>
        Task<int> DeleteClass(int[] classIds);

        /// <summary>
        /// 停用/启用班级数据
        /// </summary>
        /// <param name="classIds"></param>
        /// <returns></returns>
        Task<int> UseOrStopClass(int[] classIds, bool isUse);
        #endregion
    }
}
