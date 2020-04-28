using System.ComponentModel;

namespace Xuesky.Common.ClassLibary.Cache
{
    /// <summary>
    /// 缓存键
    /// </summary>
    public static class CacheKey
    {
        /// <summary>
        /// 角色模块权限 xuesky:role:用户主键:modules
        /// </summary>
        [Description("用户权限")]
        public const string UserPermissions = "xuesky:role:{0}:modules";
    }
}
