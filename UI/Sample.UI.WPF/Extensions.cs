﻿using Sample.UI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Core;
using Sample.UI.Interfaces;
using Sample.UI.WPF.Services;
using Sample.UI.WPF.Views;

namespace Sample.UI.WPF
{
    public static class Extensions
    {
        /// <summary>
        /// 注册Core、ViewModel、View层的所有服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSampleAllServices(this IServiceCollection services) => services
            .AddSampleCoreServices()
            .AddSampleViewModelServices()
            .AddSingleton<IUiExecutor, Executor>()
            .AddSingleton<INavigater, Navigater>()
            .AddSingleton<MainControl>();
    }
}
