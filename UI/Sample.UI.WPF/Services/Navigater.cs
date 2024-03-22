using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Sample.UI.Interfaces;
using Sample.UI.ViewModels;
using Sample.UI.WPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sample.UI.WPF.Services
{
    internal class Navigater : INavigater
    {
        private readonly IServiceProvider _rootServiceProvider;
        private readonly MainControl _mainControl;
        private readonly Dictionary<Type, FrameworkElement> _viewsCache = new Dictionary<Type, FrameworkElement>();

        public Navigater(IServiceProvider serviceProvider, MainControl mainControl, IDefaultViewModel defaultViewModel)
        {
            _rootServiceProvider = serviceProvider;
            _mainControl = mainControl;
            NavigateWithViewModel(defaultViewModel);
        }
        private void NavigateWithViewModel(object viewModel)
        {
            Type viewModelType = viewModel.GetType();

            // 检查是否已缓存对应的View实例
            if (!_viewsCache.ContainsKey(viewModelType))
            {
                // 这里假设View和ViewModel具有相同的名称规则，只是命名空间不同
                string viewTypeName = viewModelType.FullName.Replace("ViewModels", "WPF.Views").Replace("ViewModel", "");
                Type viewType = Type.GetType(viewTypeName);

                if (viewType != null)
                {
                    object viewInstance = Activator.CreateInstance(viewType);
                    if (viewInstance is FrameworkElement view)
                    {
                        view.DataContext = viewModel;
                        _viewsCache[viewModelType] = view;
                        _mainControl.RootContentControl.Content = view;
                    }
                    else throw new Exception($"视图对象不是FrameworkElement，而是{viewInstance.GetType().FullName}");
                }
                else throw new Exception($"未找到视图对象[{viewTypeName}]");
            }
        }
        public void NavigateWithViewModel<T>() where T : ObservableObject
        {
            Type viewModelType = typeof(T);

            // 检查是否已缓存对应的View实例
            if (!_viewsCache.ContainsKey(viewModelType))
            {
                // 如果没有缓存，则创建ViewModel的新实例，并尝试创建对应的View实例
                T viewModel = _rootServiceProvider.GetRequiredService<T>();
                // 这里假设View和ViewModel具有相同的名称规则，只是命名空间不同
                string viewTypeName = viewModelType.FullName.Replace("ViewModels", "WPF.Views").Replace("ViewModel", "");
                Type viewType = Type.GetType(viewTypeName);

                if (viewType != null)
                {
                    object viewInstance = Activator.CreateInstance(viewType);
                    if (viewInstance is FrameworkElement view)
                    {
                        view.DataContext = viewModel;
                        _viewsCache[viewModelType] = view;
                    }
                    else throw new Exception($"视图对象不是FrameworkElement，而是{viewInstance.GetType().FullName}");
                }
                else throw new Exception($"未找到视图对象[{viewTypeName}]");
            }
            _mainControl.RootContentControl.Content = _viewsCache[viewModelType];
        }
    }
}
