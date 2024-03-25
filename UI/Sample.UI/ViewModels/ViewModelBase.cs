using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Sample.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UI.ViewModels
{
    internal abstract class ViewModelBase : ObservableObject
    {
        protected readonly IServiceProvider _serviceProvider;
        protected INavigater Navigater => _serviceProvider.GetRequiredService<INavigater>();

        /// <summary>
        /// 基类构造函数，注入<see cref="IServiceProvider"/>以后提供服务
        /// </summary>
        /// <param name="serviceProvider"></param>
        protected ViewModelBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
