using Omu.ValueInjecter;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xuesky.Common.ClassLibary;
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
        public async Task<int> ChangePassword(SysUserChangePasswordInput sysUserChangePasswordInput)
        {
            string password = Md5Crypto.Md5Hash(sysUserChangePasswordInput.UserNewPwd);
            int result = await context.Orm.Update<SysUser>(sysUserChangePasswordInput.UserId)
                .Set(s => s.UserPwd, password)
                .ExecuteAffrowsAsync();
            await context.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// CheckAccountPass
        /// </summary>
        /// <param name="sysUserChangePasswordInput"></param>
        /// <returns></returns>
        /// <exception cref="System.Reflection.TargetInvocationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public async Task<bool> CheckAccountPass(SysUserChangePasswordInput sysUserChangePasswordInput)
        {
            string password = Md5Crypto.Md5Hash(sysUserChangePasswordInput.UserOldPwd);
            return await context.SysUsers.Where(s => s.UserId == sysUserChangePasswordInput.UserId && s.UserPwd == password).AnyAsync();
        }

        /// <summary>
        /// 获取帐户基本数据
        /// </summary>
        /// <param name="sysUserId"></param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Ignore.</exception>
        public async Task<SysUserAddInput> GetAccountInfo(int sysUserId, string loginId)
        {
            var sysUser = await context.SysUsers.Where(s => s.UserId == sysUserId && s.UserLogin == loginId).FirstAsync();
            Mapper.AddMap<SysUser, SysUserAddInput>(src =>
            {
                var vm = new SysUserAddInput();
                vm.InjectFrom(src);
                vm.RoleId = sysUser.sys_user_roles.First(s => s.UserId == sysUser.UserId).RoleId;
                return vm;
            });
            return Mapper.Map<SysUser, SysUserAddInput>(sysUser);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sysUserLoginInput"></param>
        /// <returns></returns>
        /// <exception cref="ObjectDisposedException">Ignore.</exception>
        /// <exception cref="System.Reflection.TargetInvocationException"></exception>
        public async Task<SysUserLoginOutput> Login(SysUserLoginInput sysUserLoginInput)
        {
            string password = Md5Crypto.Md5Hash(sysUserLoginInput.UserPwd);
            var sysUser = await context.SysUsers
                .Where(s => s.UserLogin == sysUserLoginInput.UserLogin && s.UserPwd == password).FirstAsync();
            if (sysUser == null)
            {
                return null;
            }
            var userRole = sysUser.sys_user_roles.FirstOrDefault(s => s.UserId == sysUser.UserId);

            Mapper.AddMap<SysUser, SysUserLoginOutput>(src =>
            {
                var vm = new SysUserLoginOutput();
                vm.InjectFrom(src);
                vm.RoleId = userRole != null ? userRole.RoleId : 2;//2:普通用户;
                return vm;
            });
            var sysUserLoginOutput = Mapper.Map<SysUser, SysUserLoginOutput>(sysUser);
            return sysUserLoginOutput;
        }

        public async Task<int> UpdateAccountInfo(SysUserUpdateInput sysUserUpdateInput)
        {
            int result = await context.Orm.Update<SysUser>()
                .SetDto(sysUserUpdateInput)
                .Where(s => s.UserId == sysUserUpdateInput.UserId)
                .ExecuteAffrowsAsync();
            await context.SaveChangesAsync();
            return result;
        }
    }
}
