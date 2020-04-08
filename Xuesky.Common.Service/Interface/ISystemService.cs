using System.Collections.Generic;
using System.Threading.Tasks;
using Xuesky.Common.DataAccess;

namespace Xuesky.Common.Service
{
    public interface ISystemService
    {
        #region Module
        /// <summary>
        /// 获取模块数据
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        Task<SysModule> GetSysModule(int moduleId);
        /// <summary>
        /// 获取模块数据列表
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="limit">每页显示条数</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        Task<(long total, List<SysModule> list)> GetSysModules(int page, int limit, string key);

        /// <summary>
        /// 添加模块数据
        /// </summary>
        /// <param name="sysModule"></param>
        /// <returns></returns>
        Task<int> AddSysModules(SysModule sysModule);

        /// <summary>
        /// 更新模块数据
        /// </summary>
        /// <param name="sysModule"></param>
        /// <returns></returns>
        Task<int> UpdateSysModules(SysModule sysModule);

        /// <summary>
        /// 删除模块数据
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        Task<int> DeleteSysModule(int[] moduleId);

        /// <summary>
        /// 停用/启用模块数据
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        Task<int> UseSysModule(int[] moduleId, bool isUse);
        #endregion
    }
}
