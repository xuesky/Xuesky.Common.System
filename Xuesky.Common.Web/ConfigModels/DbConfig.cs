namespace Xuesky.Common.Web.ConfigModels
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DbConfig
    {
        /// <summary>
        /// 主库
        /// </summary>
        public string Master { get; set; }
        /// <summary>
        /// 从库
        /// </summary>
        public Slave[] Slaves { get; set; }
    }
    public class Slave
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string DbConn { get; set; }
    }
}
