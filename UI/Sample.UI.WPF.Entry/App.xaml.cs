using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

using Sample.Core;
using Sample.Stubs;
using Sample.UI.Interfaces;
using Sample.UI.WPF;
using Sample.UI.WPF.Views;
using Sample.UI.ViewModels;

namespace SampleUI.WPF.Entry
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        public App()
        {
            //当应用程序引发但未处理异常时出现，UI线程的异常,无法捕获多线程异常
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;

            //当某个异常未被捕获时出现
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //未被观察到的Task多线程异常
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            ServiceProvider = new ServiceCollection()
                .AddSampleAllServices()
                .AddSampleStubServices()
                .AddSingleton<MainWindow>()
                .BuildServiceProvider();

            ServiceProvider.GetRequiredService<MainWindow>().Show();
            ServiceProvider.GetRequiredService<INavigater>();

            base.OnStartup(e);
        }
    }

}
