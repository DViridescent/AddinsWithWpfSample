using Autodesk.Revit.UI;
using Microsoft.Extensions.DependencyInjection;
using Sample.UI.WPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sample.Revit.Entry
{
    /// <summary>
    /// Sample停靠面板的提供者，注册到Revit以后，由Revit调用来获取<see cref="FrameworkElement"/>对象
    /// </summary>
    internal class PaneProvider : IDockablePaneProvider
    {
        private class Creator : IFrameworkElementCreator
        {
            private readonly IServiceProvider _serviceProvider;
            public Creator(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
            }

            public FrameworkElement CreateFrameworkElement() => _serviceProvider.GetRequiredService<MainControl>();
        }

        private readonly IServiceProvider _serviceProvider;
        public PaneProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void SetupDockablePane(DockablePaneProviderData data)
        {
            // 使用Creator可以尽可能延后创建FrameworkElement的时间点（或许有某些好处）
            data.FrameworkElementCreator = new Creator(_serviceProvider);
            data.FrameworkElement = null; // 置空见该属性的注释说明
            data.InitialState = new DockablePaneState
            {
                DockPosition = DockPosition.Tabbed,
                TabBehind = DockablePanes.BuiltInDockablePanes.ProjectBrowser
            };
        }
    }
}
