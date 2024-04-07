using Microsoft.Extensions.DependencyInjection;
using Sample.Core.Interfaces;
using Sample.Core.Services;

namespace Sample.Core
{
    public static class Extensions
    {
        /// <summary>
        /// 注册在Core项目中定义的所有服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSampleCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<ITimeService, TimeService>();
            return services;
        }
    }
}
