using Microsoft.Extensions.DependencyInjection;
using Sample.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Stubs
{
    public static class Extensions
    {
        public static IServiceCollection AddSampleStubServices(this IServiceCollection services) => services
            .AddSingleton<IGetPointService, GetPointServiceStub>();
    }
}
