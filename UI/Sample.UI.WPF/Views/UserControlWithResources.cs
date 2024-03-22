using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Sample.UI.WPF.Views
{
    public abstract class UserControlWithResources : UserControl
    {
        protected UserControlWithResources()
        {
            var resourceDict = new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/Sample.UI.WPF;component/Resources/ResourcesDict.xaml")
            };

            Resources.MergedDictionaries.Add(resourceDict);
        }
    }
}
