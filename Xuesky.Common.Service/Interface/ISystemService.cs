using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xuesky.Common.DataAccess;

namespace Xuesky.Common.Service
{
    public interface ISystemService
    {
        #region Module
        /// <summary>
        /// 获取模块实体
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        Task<SysModule> GetSysModule(int moduleId);
        /// <summary>
        /// 获取模块数据
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        Task<List<SysModule>> GetSysModuleList(Expression<Func<SysModule, bool>> func);
        /// <summary>
        /// 获取模块数据列表
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="limit">每页显示条数</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        Task<(long total, List<SysModule> list)> GetSysModuleListPage(int page, int limit, string key);

        /// <summary>
        /// 添加模块数据
        /// </summary>
        /// <param name="sysModule"></param>
        /// <returns></returns>
        Task<int> InsertSysModule(SysModule sysModule);

        /// <summary>
        /// 更新模块数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="obj">更新对象</param>
        /// <returns></returns>
        Task<int> UpdateSysModule(Expression<Func<SysModule, bool>> condition, Expression<Func<SysModule, object>> obj);

        /// <summary>
        /// 删除模块数据
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <returns></returns>
        Task<int> DeleteSysModule(int[] moduleIds);

        /// <summary>
        /// 停用/启用模块数据
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <returns></returns>
        Task<int> UseOrStopSysModule(int[] moduleIds, bool isUse);
        #endregion
    }
}
