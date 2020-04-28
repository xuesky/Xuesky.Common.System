using Xuesky.Common.ClassLibary.Cache;

namespace Xuesky.Common.Web.ConfigModels
{
    /// <summary>
    /// 缓存配置
    /// </summary>
    public class CacheConfig
    {
        /// <summary>
        /// 缓存类型
        /// </summary>
        public CacheType Type { get; set; }

        /// <summary>
        /// Redis配置
        /// </summary>
        public RedisConfig Redis { get; set; }
    }
    public class RedisConfig
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
    }

}
