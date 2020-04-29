using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NLog.Web;
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
            return Host.CreateDefaultBuilder(args)
                        .ConfigureAppConfiguration((context, config) =>
                            {
                                config.AddConfiguration(ConfigExtentions.GetConfigsSetting(context.HostingEnvironment.EnvironmentName, reloadOnChange: true));
                            }
                        )
                        .AddConfigsSetting(reloadOnChange: true)
                        .ConfigureWebHostDefaults(webBuilder =>
                        {
                            webBuilder
                            .ConfigureLogging(log =>
                            {
                                //log.ClearProviders();
                                //log.SetMinimumLevel(LogLevel.Information);
                                NLogBuilder.ConfigureNLog("nlog.config");
                            })
                            .UseNLog()
                            .UseStartup<Startup>()
                            ;
                        })
                        ;
        }
    }
}
