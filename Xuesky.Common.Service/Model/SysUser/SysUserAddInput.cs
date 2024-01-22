namespace Xuesky.Common.Service
{
    /// <summary>
    /// 用户信息添加视图模型
    /// </summary>
    public class SysUserAddInput : SysUserOutput
    {
        /// <summary>
        /// 组织ID
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string UserPwd { get; set; }

    }
}
