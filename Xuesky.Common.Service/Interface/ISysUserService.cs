using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        /// 获取用户实体数据
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<SysUser> GetUser(int userId);
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        Task<List<SysUser>> GetUserList(Expression<Func<SysUser, bool>> func);
        /// <summary>
        /// 获取用户分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<(long total, List<SysUser> list)> GetUserListPage(int page, int limit, string key);
        /// <summary>
        /// 添加系统用户
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        Task<int> InsertSysUser(SysUser sysUser);
        /// <summary>
        /// 指添加系统用户
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        Task<int> BatchInserSysUser(IEnumerable<SysUser> sysUsers);

        /// <summary>
        /// 删除系统用户
        /// </summary>
        /// <param name="sysUsers"></param>
        /// <returns></returns>
        Task<int> DeleteSysUser(int[] ids);
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        Task<int> UpdateSysuer(Expression<Func<SysUser, bool>> condition, Expression<Func<SysUser, object>> obj);

        /// <summary>
        /// 停用/启用角色数据
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        Task<int> UseOrStopUser(int[] userIds, bool isUse);
        #endregion
    }
}
