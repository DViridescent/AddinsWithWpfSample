using Sample.Core.Interfaces;
using Sample.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddSampleCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<ITimeService, TimeService>();
            return services;
        }
    }
}
