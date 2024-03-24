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
            data.FrameworkElement = null;
            data.InitialState = new DockablePaneState
            {
                DockPosition = DockPosition.Tabbed,
                TabBehind = DockablePanes.BuiltInDockablePanes.ProjectBrowser
            };
        }
    }
}
