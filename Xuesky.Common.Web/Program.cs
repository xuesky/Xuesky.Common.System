using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Xuesky.Common.Web.ConfigModels;
using Xuesky.Common.Web.Extenstions;

namespace Xuesky.Common.Web
{
    public class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        /// <summary>
        /// CreateHostBuilder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var appConfig = ConfigExtentions.Get<AppConfig>("appconfig") ?? new AppConfig();
            var config = new ConfigurationBuilder()
            .Build();
            return Host.CreateDefaultBuilder(args)
                        //.AddConfigsSetting()
                        .ConfigureWebHostDefaults(webBuilder =>
                        {
                            webBuilder.UseConfiguration(config)
                            .ConfigureLogging(log =>
                            {
                                //log.ClearProviders();
                                log.SetMinimumLevel(LogLevel.Information);
                                NLogBuilder.ConfigureNLog("nlog.config");
                            })
                            .UseNLog()
                            .UseStartup<Startup>()
                            .UseUrls(appConfig.Urls)
                            ;
                        });
        }
    }
}
