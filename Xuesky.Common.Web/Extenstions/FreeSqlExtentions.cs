using FreeSql;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NLog.Web;
using System.Diagnostics;
using Xuesky.Common.Web.ConfigModels;

namespace Xuesky.Common.Web.Extenstions
{
    public static class FreeSqlExtentions
    {
        public static IFreeSql InitFreesql(IConfiguration configuration, IWebHostEnvironment env)
        {
            var dbConfig = ConfigExtentions.Get<DbConfig>("dbconfig", env.EnvironmentName);
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            var Fsql = new FreeSqlBuilder()
               .UseConnectionString(FreeSql.DataType.SqlServer, dbConfig.Master)
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
