namespace Xuesky.Common.Service
{
    /// <summary>
    /// 用户信息视图模型
    /// </summary>
    public class SysUserOutput
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 登录ID
        /// </summary>
        public string UserLogin { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string UserGender { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUse { get; set; }

        /// <summary>
        /// 关联老师ID
        /// </summary>
        public int TeacherId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
