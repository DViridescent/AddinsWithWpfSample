using Microsoft.Extensions.DependencyInjection;
using Rhino.PlugIns;
using Rhino.UI;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using Sample.UI.WPF;
using Sample.Core.Interfaces;
using Sample.UI.Interfaces;
using Sample.UI.WPF.Views;
using Sample.Rhino.UI;
using Sample.Rhino.Services;

[assembly: Guid("26BDDF35-43C1-E594-734B-08D39A5A857A")]
namespace Sample.Rhino.Entry
{
    public class App : PlugIn
    {
        public static IServiceProvider? ServiceProvider { get; private set; }
        public override PlugInLoadTime LoadTime => PlugInLoadTime.AtStartup;


        protected override LoadReturnCode OnLoad(ref string errorMessage)
        {
            // 服务注册
            ServiceProvider = new ServiceCollection()
                .AddSampleAllServices()
                .AddSingleton<IGetPointService, RhinoGetPointService>()
                .BuildServiceProvider();

            // 初始化Executor
            ServiceProvider.GetRequiredService<IUiExecutor>().InitializeWithDispatcher();
            // 初始化导航器
            ServiceProvider.GetRequiredService<INavigater>();
             

            Type panelType = typeof(MainView);

            Panels.RegisterPanel(this, panelType, "Sample", null, PanelType.System);// PanelType.System代表所有文档共享同一个对象

            return LoadReturnCode.Success;
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
        }
    }
}
