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
        public static IHostBuilder AddConfigsSetting(this IHostBuilder hostBuilder, string directory = "configs", string environmentName = "", bool reloadOnChange = false)
        {
            var fullPath = Path.Combine(AppContext.BaseDirectory, directory);
            if (!Directory.Exists(fullPath))
                return hostBuilder;
            DirectoryInfo directoryInfo = new DirectoryInfo(fullPath);
            var fileInfos = directoryInfo.GetFiles();
            fileInfos.ForEach(file =>
            {
                if (file.Extension == ".json")
                {
                    hostBuilder.ConfigureAppConfiguration((context, config) =>
                    {
                        config.SetBasePath(fullPath).AddJsonFile(file.Name.ToLower(), true, reloadOnChange);
                        if (environmentName.NotNull())
                        {
                            config.AddJsonFile(file.Name.ToLower() + "." + environmentName + ".json", true, reloadOnChange);
                        }
                    });
                }
            });
            return hostBuilder;
        }
    }
}
