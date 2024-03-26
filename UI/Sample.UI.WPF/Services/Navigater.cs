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
    /// <summary>
    /// 导航器，初始化时将会显示默认的页面
    /// <para>内部使用了反射机制，注意View和ViewModel的命名</para>
    /// </summary>
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

        /// <summary>
        /// <inheritdoc/>
        /// <para>此处使用了反射来从ViewModel找到View，所以对命名空间和类命名有要求</para>
        /// </summary>
        /// <typeparam name="T"><inheritdoc/></typeparam>
        /// <exception cref="Exception">切换页面失败时触发</exception>
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
