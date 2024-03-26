using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Microsoft.Extensions.DependencyInjection;
using Sample.Core.Interfaces;
using Sample.Revit.Services;
using Sample.Revit.Utils;
using Sample.UI.Interfaces;
using Sample.UI.WPF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Sample.Revit.Entry
{
    public class App : IExternalApplication
    {
        private static IServiceProvider? serviceProvider;
        public static IServiceProvider ServiceProvider 
        { 
            get => serviceProvider ?? throw new Exception("Revit未正确加载，Sample插件加载失败"); 
            private set => serviceProvider = value; 
        }

        public static UIApplication? UiApplication { get; private set; }
        public Result OnStartup(UIControlledApplication application)
        {
            // 修改Revit的Dll加载机制，添加程序集解析事件
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(OnAssemblyResolve);

            // Revit初始化完成后的事件，在这个事件中进行插件的初始化
            application.ControlledApplication.ApplicationInitialized += ControlledApplication_ApplicationInitialized;


            //注册面板
            string tabName = "Sample";
            string buttonName = "显示面板";
            string panelName = "Sample";
            string registTitle = "Sample";
            var buttonData = new PushButtonData(registTitle, buttonName, typeof(App).Assembly.Location, typeof(ShowPanelCommand).FullName)
            {
                AvailabilityClassName = typeof(CmdAvailabilityViews).FullName
                // 此处可以添加图标
            };

            try
            {
                //有可能和别的插件重复，会导致报错
                application.CreateRibbonTab(tabName);
            }
            catch { }
            application
                .CreateRibbonPanel(tabName, panelName)
                .AddItem(buttonData);

            return Result.Succeeded;
        }

        private void ControlledApplication_ApplicationInitialized(object sender, ApplicationInitializedEventArgs e)
        {
            try
            {
                // 预加载UI程序
                // 初始化Services
                ConfigureServices();

                //初始化外部事件，必须在App类中进行创建
                RevitEventRaiser.Init();

                var uiApplication = new UIApplication(sender as Application);
                UiApplication = uiApplication;
                //注册Pane
                ShowPanelCommand.RegisterPane(uiApplication);

                //初始化用于异步返回UI调度线程的Executor类
                ServiceProvider.GetRequiredService<IUiExecutor>().InitializeWithDispatcher();

                // 初始化页面调度器
                ServiceProvider.GetRequiredService<INavigater>();
            }
            catch (Exception ex)
            {
                //Log.Error(ex, ex.Message);
                var td = new TaskDialog("无法加载Sample插件");
                td.MainContent = $"加载Sample插件时出现了某些错误：\n{ex.Message}";

                // 可以选择提供web链接
                //td.AddCommandLink(TaskDialogCommandLinkId.CommandLink1, "联系我们提交问题");

                //TaskDialogResult tResult = td.Show();

                //if (TaskDialogResult.CommandLink1 == tResult)
                //{
                //    Process.Start(Constants.WebsiteUrl);
                //}
            }
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        private static void ConfigureServices()
        {
            //注册服务
            ServiceProvider = new ServiceCollection()
                .AddSampleAllServices()
                .AddSingleton<PaneProvider>()
                .AddSingleton<IGetPointService, RevitGetPointService>()
                .BuildServiceProvider();
        }

        /// <summary>
        /// 由于Revit的插件的程序集解析问题，设置在当前文件夹下寻找程序集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            string folderPath = Path.GetDirectoryName(typeof(App).Assembly.Location);

            // 目前只匹配程序集名称，不匹配版本号等信息，有需要的话需要进一步使用args的其他参数
            string name = new AssemblyName(args.Name).Name;

            // 注意程序集匹配逻辑
            string assemblyPath = Path.Combine(folderPath, name + ".dll");

            if (!File.Exists(assemblyPath)) return null;
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            return assembly;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
