using Microsoft.Extensions.DependencyInjection;
using Xuesky.Common.Service;

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
            services.AddTransient<ISystemService, SystemService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<IClassService, ClassService>();
            services.AddTransient<IStuService, StuService>();

            services.AddScoped<IdentityExtentions>();
            return services;
        }
    }
}
