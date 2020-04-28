using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Xuesky.Common.ClassLibary.Cache;
using Xuesky.Common.Service;
using Xuesky.Common.Web.ConfigModels;

namespace Xuesky.Common.Web.Extenstions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ISysUserService, SysUserService>();
            services.AddTransient<IModuleService, ModuleService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<IClassService, ClassService>();
            services.AddTransient<IStuService, StuService>();

            services.AddScoped<IdentityExtentions>();

            return services;
        }
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCache(this IServiceCollection services, IWebHostEnvironment env)
        {
            var cacheConfig = ConfigExtentions.Get<CacheConfig>("cacheconfig", env.EnvironmentName);
            if (cacheConfig.Type == CacheType.Redis)
            {
                var csredis = new CSRedis.CSRedisClient(cacheConfig.Redis.ConnectionString);
                RedisHelper.Initialization(csredis);
                services.AddSingleton<ICache, RedisCache>();
            }
            else
            {
                services.AddMemoryCache();
                services.AddSingleton<ICache, MemoryCache>();
            }

            return services;
        }
    }
}
