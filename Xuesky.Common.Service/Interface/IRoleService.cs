using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xuesky.Common.DataAccess;

namespace Xuesky.Common.Service
{
    public interface IRoleService
    {
        #region Rose
        /// <summary>
        /// 获取角色实体数据
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<SysRole> GetRole(int roleId);
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        Task<List<SysRole>> GetRoleList(Expression<Func<SysRole, bool>> func);
        /// <summary>
        /// 获取角色列表分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<(long total, List<SysRole> list)> GetRoleListPage(int page, int limit, string key);

        /// <summary>
        /// 添加角色数据
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        Task<int> InsertRole(SysRole Role);

        /// <summary>
        /// 更新角色数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="obj">更新对象</param>
        /// <returns></returns>
        Task<int> UpdateRole(Expression<Func<SysRole, bool>> condition, Expression<Func<SysRole, object>> obj);

        /// <summary>
        /// 删除角色数据
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<int> DeleteRole(int[] roleIds);

        /// <summary>
        /// 停用/启用角色数据
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<int> UseOrStopRole(int[] roleIds, bool isUse);
        #endregion
    }
}
