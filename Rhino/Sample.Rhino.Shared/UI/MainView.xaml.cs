using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using Sample.Rhino.Entry;
using Sample.UI.WPF.Views;

namespace Sample.Rhino.UI
{
    [Guid("91563EDD-3065-1736-8C1A-2E28DE40FB54")]
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();

            if (App.ServiceProvider != null)
            {
                this.MainContentControl.Content = App.ServiceProvider.GetRequiredService<MainControl>();
            } 
        }
    }
}
