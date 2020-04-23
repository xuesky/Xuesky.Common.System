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
        Task<SysUserOutput> GetUser(int userId);
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="func">查询条件</param>
        /// <returns></returns>
        Task<List<SysUserOutput>> GetUserList(Expression<Func<SysUser, bool>> func);
        /// <summary>
        /// 获取用户分页视图数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<(long total, List<SysUserOutput> list)> GetUserListPage(int page, int limit, string key);
        /// <summary>
        /// 添加系统用户
        /// </summary>
        /// <param name="sysUserAddInput">输入视图对象</param>
        /// <returns></returns>
        Task<int> InsertSysUser(SysUserAddInput sysUserAddInput);
        /// <summary>
        /// 指添加系统用户
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        Task<int> BatchInserSysUser(IEnumerable<SysUserAddInput> sysUsers);

        /// <summary>
        /// 删除系统用户
        /// </summary>
        /// <param name="userIds">用户主键数组</param>
        /// <returns></returns>
        Task<int> DeleteSysUser(int[] userIds);
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="obj">更新对象</param>
        /// <returns></returns>
        Task<int> UpdateSysuer(Expression<Func<SysUser, bool>> condition, object obj);

        /// <summary>
        /// 停用/启用角色数据
        /// </summary>
        /// <param name="userIds">用户主键数组</param>
        /// <param name="isUse">是否启用</param>
        /// <returns></returns>
        Task<int> UseOrStopUser(int[] userIds, bool isUse);
        #endregion
    }
}
