using Sample.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.UI.Interfaces;

namespace Sample.UI
{
    public static class Extensions
    {
        public static IServiceCollection AddSampleViewModelServices(this IServiceCollection services) => services
            //.AddSingleton<HomePageViewModel>()
            .AddSingleton<IDefaultViewModel, HomePageViewModel>()
            .AddSingleton<SubPage1ViewModel>();
    }
}
