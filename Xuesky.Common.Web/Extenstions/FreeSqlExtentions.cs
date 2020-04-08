using FreeSql;
using Microsoft.Extensions.Configuration;
using NLog.Web;
using System.Diagnostics;

namespace Xuesky.Common.Web.Extenstions
{
    public static class FreeSqlExtentions
    {
        public static IFreeSql InitFreesql(IConfiguration configuration)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            var Fsql = new FreeSqlBuilder()
               .UseConnectionString(FreeSql.DataType.SqlServer, configuration.GetConnectionString("Master"))
               .UseLazyLoading(true)
               .UseNoneCommandParameter(true)
               .UseMonitorCommand(cmd => { }, (cmd, log) => Trace.WriteLine(log))
               .Build();
            Fsql.Aop.CurdAfter += (s, e) =>
            {
                logger.Info(e.Identifier + ": " + e.EntityType.FullName + " " + e.ElapsedMilliseconds + "ms, " + e.Sql);
            };
            return Fsql;
        }
    }
}
