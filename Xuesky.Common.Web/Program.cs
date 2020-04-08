using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System.IO;

namespace Xuesky.Common.Web
{
    public class Program
    {
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
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //.AddJsonFile("hostsettings.json", optional: true, reloadOnChange: true)
            .AddCommandLine(args)//命令行指定参数
            .Build();
            return Host.CreateDefaultBuilder(args)
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
                            .UseStartup<Startup>();
                        });
        }
    }
}
