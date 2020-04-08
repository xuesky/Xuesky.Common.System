using System.Threading.Tasks;
using Xuesky.Common.DataAccess;

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
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<SysUser> Login(string loginId, string password);
        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="sysUserId">key</param>
        /// <param name="loginId">登录ID</param>
        /// <param name="password">newPassword</param>
        /// <returns></returns>
        Task<int> ChangePassword(int sysUserId, string loginId, string newPassword);
        /// <summary>
        /// 获取帐户信息
        /// </summary>
        /// <param name="sysUserId"></param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        Task<SysUser> GetAccountInfo(int sysUserId, string loginId);
    }
}
