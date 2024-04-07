using System.Windows;
using Sample.UI.WPF.Views;

namespace SampleUI.WPF.Entry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainControl mainControl)
        {
            InitializeComponent();
            WindowContentControl.Content = mainControl;
        }
    }
}
