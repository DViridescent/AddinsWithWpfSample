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

        protected ViewModelBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
