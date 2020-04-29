using CSScriptLib;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Xuesky.Common.ClassLibary;

namespace Xuesky.Common.Web.Extenstions
{
    public class ConfigExtentions
    {
        /// <summary>
        /// 加载目录下配置文件
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="environmentName"></param>
        /// <param name="reloadOnChange"></param>
        /// <returns></returns>
        public static IConfiguration GetConfigsSetting(string environmentName, string directory = "configs", bool reloadOnChange = false)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "configs");
            if (!Directory.Exists(filePath))
                return null;

            var builder = new ConfigurationBuilder().SetBasePath(filePath);
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
            var fileInfos = directoryInfo.GetFiles();
            fileInfos.ForEach(file =>
            {
                if (file.Extension == ".json")
                {
                    builder.AddJsonFile(file.Name.ToLower(), true, reloadOnChange);
                    if (environmentName.NotNull())
                    {
                        builder.AddJsonFile($"{file.Name.ToLower()}.{environmentName}.json", true, reloadOnChange);
                    }
                }
            });
            return builder.Build();
        }
        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="environmentName">环境名称</param>
        /// <param name="reloadOnChange">自动更新</param>
        /// <returns></returns>
        public static IConfiguration LoadFile(string fileName, string environmentName = "", bool reloadOnChange = false)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "configs");
            if (!Directory.Exists(filePath))
                return null;

            var builder = new ConfigurationBuilder()
                .SetBasePath(filePath)
                .AddJsonFile(fileName.ToLower() + ".json", true, reloadOnChange);

            if (environmentName.NotNull())
            {
                builder.AddJsonFile(fileName.ToLower() + "." + environmentName + ".json", true, reloadOnChange);
            }

            return builder.Build();
        }

        /// <summary>
        /// 获得配置信息
        /// </summary>
        /// <typeparam name="T">配置信息</typeparam>
        /// <returns></returns>
        public static T Get<T>(string fileName, string environmentName = "", bool reloadOnChange = false)
        {
            var configuration = LoadFile(fileName, environmentName, reloadOnChange);
            if (configuration == null)
                return default;

            return configuration.Get<T>();
        }
    }
}
