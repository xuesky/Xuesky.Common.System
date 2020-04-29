using CSScriptLib;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using Xuesky.Common.ClassLibary;

namespace Xuesky.Common.Web.Extenstions
{
    public static class IHostBuilderExtentions
    {
        public static IHostBuilder AddConfigsSetting(this IHostBuilder HostBuilder, string directory = "configs", bool reloadOnChange = false)
        {
            var fullPath = Path.Combine(AppContext.BaseDirectory, directory);
            if (!Directory.Exists(fullPath))
                return HostBuilder;
            DirectoryInfo directoryInfo = new DirectoryInfo(fullPath);
            var fileInfos = directoryInfo.GetFiles();
            HostBuilder.ConfigureAppConfiguration((context, config) =>
            {
                config.Sources.Clear();
                config.SetBasePath(fullPath);
                var env = context.HostingEnvironment;
                fileInfos.ForEach(file =>
                {
                    if (file.Extension == ".json")
                    {
                        config.AddJsonFile(file.Name.ToLower(), true, reloadOnChange);
                        if (env.EnvironmentName.NotNull())
                        {
                            config.AddJsonFile($"{file.Name.ToLower()}.{env.EnvironmentName}.json", true, reloadOnChange);
                        }

                    }
                });
            });
            return HostBuilder;
        }
    }
}
