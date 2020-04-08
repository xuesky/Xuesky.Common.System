using System.Collections.Generic;
using System.Threading.Tasks;
using Xuesky.Common.DataAccess;

namespace Xuesky.Common.Service
{
    /// <summary>
    /// 系统用户报务
    /// </summary>
    public interface ISysUserService
    {

        #region SysUser
        /// <summary>
        /// 添加系统用户
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        Task<int> InsertSysUser(SysUser sysUser);
        /// <summary>
        /// 添加系统用户
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        Task<int> InserSysUser(IEnumerable<SysUser> sysUsers);

        /// <summary>
        /// 删除系统用户
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        Task<int> DelSysUser(SysUser sysUser);

        /// <summary>
        /// 删除系统用户
        /// </summary>
        /// <param name="sysUsers"></param>
        /// <returns></returns>
        Task<int> DelSysUser(IEnumerable<SysUser> sysUsers);

        /// <summary>
        /// 删除系统用户
        /// </summary>
        /// <param name="sysUsers"></param>
        /// <returns></returns>
        Task<int> DelSysUser(int[] ids);
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        Task<int> UpdateSysuer(SysUser sysUser);

        #endregion
    }
}
