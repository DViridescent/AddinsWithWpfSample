using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Microsoft.Extensions.DependencyInjection;
using Sample.Revit.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Sample.Revit.Entry
{
    [Transaction(TransactionMode.Manual)]
    internal class ShowPanelCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                var uiApplication = commandData.Application;
                RegisterPane(uiApplication);
                var panel = uiApplication.GetDockablePane(PanelId);
                panel.Show();
            }
            catch (Exception ex)
            {
                RevitDialog.Error($"显示UI时遇到错误：\n{ex.Message}");
            }

            return Result.Succeeded;
        }


        internal static DockablePaneId PanelId = new DockablePaneId(new Guid("D58CF5BA-84A6-22D3-70AB-EF9C7B33D5E1"));
        internal static void RegisterPane(UIApplication uiApplication)
        {
            var registered = DockablePane.PaneIsRegistered(PanelId);
            var created = DockablePane.PaneExists(PanelId);

            if (registered && created)
            {
                return;
            }

            if (!registered)
            {
                var paneProvider = App.ServiceProvider.GetRequiredService<PaneProvider>();
                uiApplication.RegisterDockablePane(PanelId, "Sample", paneProvider);
            }

            created = DockablePane.PaneExists(PanelId);

            // 关于某种偶发的BUG的解决
            // https://github.com/specklesystems/speckle-sharp/issues/1469
            if (!created && uiApplication.Application.Documents.Size > 0)
            {
                RevitDialog.Show("可停靠面板问题", "当通过双击.rvt文件打开程序时，Revit可能出现插件面板注册失败的问题。请关闭Revit程序并通过应用图标打开Revit来重新注册面板。");
            }
        }
    }
}
