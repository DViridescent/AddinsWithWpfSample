using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UI.ViewModels
{
    internal partial class SubPage1ViewModel : ViewModelBase
    {
        public SubPage1ViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [ObservableProperty]
        private string _title = "我是子页面1";

        [RelayCommand]
        private void Back()
        {
            Navigater.NavigateWithViewModel<HomePageViewModel>();
        }
    }
}
