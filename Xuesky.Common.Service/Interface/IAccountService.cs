using System.Threading.Tasks;

namespace Xuesky.Common.Service
{
    /// <summary>
    /// 账户登录服务
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sysUserLoginInput">登录实体对象</param>
        /// <returns></returns>
        Task<SysUserLoginOutput> Login(SysUserLoginInput sysUserLoginInput);
        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="sysUserChangePasswordInput">更改密码视图</param>
        /// <returns></returns>
        Task<int> ChangePassword(SysUserChangePasswordInput sysUserChangePasswordInput);
        /// <summary>
        /// 获取帐户信息
        /// </summary>
        /// <param name="sysUserId">用户主键</param>
        /// <param name="loginId">登录ID</param>
        /// <returns></returns>
        Task<SysUserOutput> GetAccountInfo(int sysUserId, string loginId);
        /// <summary>
        /// 更新用户基本数据
        /// </summary>
        /// <param name="sysUserUpdateInput">更新实体对象</param>
        /// <returns></returns>
        Task<int> UpdateAccountInfo(SysUserUpdateInput sysUserUpdateInput);
        /// <summary>
        /// 核对用户密码
        /// </summary>
        /// <param name="sysUserChangePasswordInput">更新实体对象</param>
        /// <returns></returns>
        Task<bool> CheckAccountPass(SysUserChangePasswordInput sysUserChangePasswordInput);
    }
}
