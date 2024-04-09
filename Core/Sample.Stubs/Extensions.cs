using Microsoft.Extensions.DependencyInjection;
using Sample.Core.Interfaces;

namespace Sample.Stubs
{
    public static class Extensions
    {
        /// <summary>
        /// 注册在Stub项目中定义的所有服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSampleStubServices(this IServiceCollection services) => services
            .AddSingleton<IGetPointService, GetPointServiceStub>();
    }
}
