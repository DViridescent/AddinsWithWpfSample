using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Sample.AutoCAD.Entry;
using Sample.UI.Interfaces;
using Sample.UI.ViewModels;
using Sample.UI.WPF;
using Sample.UI.WPF.Views;
using System;
using System.Collections.Generic;
using System.Text;


namespace Sample.AutoCAD.UI
{
    internal class CustomPaletteSet : PaletteSet
    {
        private static bool _wasVisible;

        public CustomPaletteSet() : base("SAMPLE", "SAMPLE_SHOW_PALETTE_SET", new Guid("7FF0C743-8FE6-8A39-364A-18CF9FAFEC0A"))
        {
            Style = PaletteSetStyles.ShowAutoHideButton | PaletteSetStyles.ShowCloseButton | PaletteSetStyles.ShowPropertiesMenu;
            MinimumSize = new System.Drawing.Size(250, 150);

            var view = App.ServiceProvider?.GetRequiredService<MainControl>() ?? throw new Exception("插件初始化失败");


            AddVisual("SampleView", view);

            // 当没有 Document 实例时自动隐藏调色板
            // 处于活动状态（无文档状态）
            var docs = Application.DocumentManager;
            docs.DocumentBecameCurrent += (s, e) => Visible = e.Document != null && _wasVisible;
            docs.DocumentCreated += (s, e) => Visible = _wasVisible;
            docs.DocumentToBeDeactivated += (s, e) => _wasVisible = Visible;
            docs.DocumentToBeDestroyed += (s, e) =>
            {
                _wasVisible = Visible;
                if (docs.Count == 1)
                    Visible = false;
            };
        }
    }
}
