namespace Xuesky.Common.Service
{
    /// <summary>
    /// 用户密码更改视图模型
    /// </summary>
    public class SysUserChangePasswordInput
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 旧密码
        /// </summary>
        public string UserOldPwd { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string UserNewPwd { get; set; }

        public string UserNewPwdAgain { get; set; }
    }
}
