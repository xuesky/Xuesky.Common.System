namespace Xuesky.Common.Service
{
    /// <summary>
    /// 用户登录视图模型
    /// </summary>
    public class SysUserLoginOutput
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 角色编码
        /// </summary>
        public string RoleCode { get; set; }
        /// <summary>
        /// 登录ID
        /// </summary>
        public string UserLogin { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }

    }
}
