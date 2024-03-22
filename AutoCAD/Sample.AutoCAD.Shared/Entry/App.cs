using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Sample.UI.WPF;
using Sample.Core.Interfaces;
using Sample.AutoCAD.Service;
using Sample.UI.Interfaces;
using Exception = System.Exception;
using System.Linq;


[assembly: ExtensionApplication(typeof(Sample.AutoCAD.Entry.App))]
namespace Sample.AutoCAD.Entry
{
    internal class App : IExtensionApplication
    {
        public static IServiceProvider ServiceProvider { get; private set; }


        public void Initialize()
        {
            // 修改程序集加载路径，有时候会避免一些问题
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(OnAssemblyResolve);

            //当某个异常未被捕获时出现
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //未被观察到的Task多线程异常
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            // 服务注册
            ServiceProvider = new ServiceCollection()
                .AddSampleAllServices()
                .AddSingleton<IGetPointService, AutoCADGetPointService>()
                .BuildServiceProvider();

            ServiceProvider.GetRequiredService<IUiExecutor>().InitializeWithDispatcher();
            ServiceProvider.GetRequiredService<INavigater>();

            LoadRibbon();
        }

        public void Terminate()
        {

        }

        private static Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            string folderPath = Path.GetDirectoryName(typeof(App).Assembly.Location);

            string name = new AssemblyName(args.Name).Name;

            // 注意逻辑
            string assemblyPath = Path.Combine(folderPath, name + ".dll");

            if (!File.Exists(assemblyPath)) return null;
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            return assembly;
        }


        private string BuildExceptionText(Exception? e)
        {
            if (e == null) return "";
            var sb = new StringBuilder();
            sb.AppendLine("UI线程异常：" + e.Message);
            if (e.InnerException != null)
            {
                sb.AppendLine("内部异常：" + e.InnerException.Message);
            }
            sb.AppendLine("异常堆栈：" + e.StackTrace);
            return sb.ToString();
        }
        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show(BuildExceptionText(e.Exception));
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(BuildExceptionText(e.ExceptionObject as Exception));
        }
        private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            MessageBox.Show(BuildExceptionText(e.Exception));
        }

        #region 按钮相关
        /// <summary>
        /// 加载按钮和相关UI
        /// </summary>
        public static void LoadRibbon()
        {
            if (ComponentManager.Ribbon == null)
            {
                //load the custom Ribbon on startup, but at this point
                //the Ribbon control is not available, so register for
                //an event and wait
                ComponentManager.ItemInitialized += ComponentManager_ItemInitialized;
            }
            else
            {
                //the assembly was loaded using NETLOAD, so the ribbon
                //is available and we just create the ribbon
                CreateRibbon();
            }
        }

        static void ComponentManager_ItemInitialized(object sender, RibbonItemEventArgs e)
        {
            //now one Ribbon item is initialized, but the Ribbon control
            //may not be available yet, so check if before
            if (ComponentManager.Ribbon == null) return;
            //ok, create Ribbon
            CreateRibbon();
            //and remove the event handler
            ComponentManager.ItemInitialized -= ComponentManager_ItemInitialized;
        }

        private const string TAB_ID = "C5A95505-6465-9B60-B3BA-07DCE97D01AF";
        private const string PANEL_TITLE = "Sample";
        private const string TAB_TITLE = "Sample";
        private const string SHOW_COMMAND = Commands.SHOW_COMMAND;

        /// <summary>
        /// 建立菜单
        /// </summary>
        static void CreateRibbon()
        {
            var ribbonControl = ComponentManager.Ribbon;
            if (ribbonControl.Tabs.Any(t => t.Id.Equals(TAB_ID)))
            {
                return;
            }
            //添加Tab
            var ribbonTab = new RibbonTab
            {
                Title = TAB_TITLE,
                Id = TAB_ID
            };


            ribbonControl.Tabs.Add(ribbonTab);
            var ribbonPanelSource = new RibbonPanelSource { Title = PANEL_TITLE };
            var ribbonPanel = new RibbonPanel { Source = ribbonPanelSource };
            ribbonTab.Panels.Add(ribbonPanel);


            ribbonPanelSource.Items.Add(new RibbonButton
            {
                Text = "Sample",
                CommandParameter = SHOW_COMMAND + "\n",
                //Image = AssetsLoader.LoadPngImgSource("Sample.png"),
                ToolTip = new RibbonToolTip
                {
                    Title = "Sample",
                    Content = "显示Sample面板",
                    Command = SHOW_COMMAND
                },
                Orientation = System.Windows.Controls.Orientation.Horizontal,
                CommandHandler = new CommandHandler(),
                ShowText = true,
            });

            ribbonTab.IsActive = true;
        }
        #endregion
    }
}
