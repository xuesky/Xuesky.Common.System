using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xuesky.Common.ClassLibary.Security;
using Xuesky.Common.DataAccess;

namespace Xuesky.Common.Service
{
    [Description("帐户信息管理")]
    public class AccountService : IAccountService
    {
        private readonly SystemDbContext context;

        public AccountService(SystemDbContext context)
        {
            this.context = context;
        }
        public Task<int> ChangePassword(int sysUserId, string loginId, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<SysUser> GetAccountInfo(int sysUserId, string loginId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="System.Reflection.TargetInvocationException">Ignore.</exception>
        /// <exception cref="ObjectDisposedException">Ignore.</exception>
        public async Task<SysUser> Login(string loginId, string password)
        {
            password = Md5Crypto.Md5Hash(password);
            return await context.SysUsers.Where(s => s.UserLogin == loginId && s.UserPwd == password).FirstAsync();
        }
    }
}
