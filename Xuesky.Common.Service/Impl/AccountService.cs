using Omu.ValueInjecter;
using System;
using System.ComponentModel;
using System.Linq;
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
        /// <summary>
        /// 获取帐户基本数据
        /// </summary>
        /// <param name="sysUserId"></param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<VM_UserInfo> GetAccountInfo(int sysUserId, string loginId)
        {
            var sysUser = await context.SysUsers.Where(s => s.UserId == sysUserId && s.UserLogin == loginId).FirstAsync();
            Mapper.AddMap<SysUser, VM_UserInfo>(src =>
            {
                var vm = new VM_UserInfo();
                vm.InjectFrom(src);
                vm.RoleId = sysUser.sys_user_roles.First().RoleId;
                return vm;
            });
            return Mapper.Map<SysUser, VM_UserInfo>(sysUser);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="System.Reflection.TargetInvocationException">Ignore.</exception>
        /// <exception cref="ObjectDisposedException">Ignore.</exception>
        public async Task<SysUser> Login(string loginId, string password)
        {
            password = Md5Crypto.Md5Hash(password);
            return await context.SysUsers
                .Where(s => s.UserLogin == loginId && s.UserPwd == password).FirstAsync();
        }

        public async Task<int> UpdateAccountInfo(VM_UserInfo myinfo)
        {
            int result = await context.Orm.Update<SysUser>()
                .IgnoreColumns(s => new { s.UserId })
                .SetDto(myinfo)
                .Where(s => s.UserId == myinfo.UserId)
                .ExecuteAffrowsAsync();
            return result;
        }
    }
}
