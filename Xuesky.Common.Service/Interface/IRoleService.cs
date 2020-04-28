using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xuesky.Common.DataAccess;

namespace Xuesky.Common.Service
{
    public interface IRoleService
    {
        #region Role
        /// <summary>
        /// 获取角色视图数据
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<SysRoleOutput> GetRole(int roleId);
        /// <summary>
        /// 获取角色视图列表
        /// </summary>
        /// <returns></returns>
        Task<List<SysRoleOutput>> GetRoleList(Expression<Func<SysRole, bool>> func);
        /// <summary>
        /// 获取角色视图分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<(long total, List<SysRoleOutput> list)> GetRoleListPage(int page, int limit, string key);

        /// <summary>
        /// 添加角色数据
        /// </summary>
        /// <param name="sysRoleAddInputole"></param>
        /// <returns></returns>
        Task<int> InsertRole(SysRoleAddInput sysRoleAddInputole);

        /// <summary>
        /// 更新角色数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="obj">更新对象</param>
        /// <returns></returns>
        Task<int> UpdateRole(Expression<Func<SysRole, bool>> condition, object obj);

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

        #region RoleModule
        /// <summary>
        /// 用户模块受权
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        Task<int> RoleModuleAuthorize(int roleId, int[] modules);
        #endregion
    }
}
