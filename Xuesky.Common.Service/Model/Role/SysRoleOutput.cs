namespace Xuesky.Common.Service
{
    /// <summary>
    /// 系统角色视图
    /// </summary>
    public class SysRoleOutput
    {
        /// <summary>
        /// 角色主键
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 角色说明
        /// </summary>
        public string RoleDesc { get; set; }
        /// <summary>
        /// 启用
        /// </summary>
        public bool IsUse { get; set; }

    }

}
