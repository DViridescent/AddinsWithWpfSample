using Sample.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using Sample.UI.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.UI.ViewModels
{
    internal partial class HomePageViewModel : ViewModelBase, IDefaultViewModel
    {
        private ITimeService TimeService => _serviceProvider.GetRequiredService<ITimeService>();

        public HomePageViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            StartCounter();
        }

        private async void StartCounter()
        {
            while (true)
            {
                await Task.Delay(1000);
                Count++;
            }
        }
        [ObservableProperty]
        private int _count = 0;

        [RelayCommand]
        private void ResetCounter()
        {
            Count = 0;
        }

        [ObservableProperty]
        private DateTime _currentTime;
        [RelayCommand]
        private void QueryTime()
        {
            CurrentTime = TimeService.GetCurrentTime();
        }

        [RelayCommand]
        private void SwitchToSubPage1()
        {
            Navigater.NavigateWithViewModel<SubPage1ViewModel>();
        }
    }
}
