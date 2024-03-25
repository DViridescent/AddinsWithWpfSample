using Sample.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using Sample.UI.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Sample.Geometry;

namespace Sample.UI.ViewModels
{
    internal partial class HomePageViewModel : ViewModelBase, IDefaultViewModel
    {
        //由于基类已经注入了ServiceProvider，所以可以直接使用
        private ITimeService TimeService => _serviceProvider.GetRequiredService<ITimeService>(); 
        private IGetPointService GetPointService => _serviceProvider.GetRequiredService<IGetPointService>();

        public HomePageViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            StartCounter();
        }


        private async void StartCounter()
        {
            while (true)
            {
                // 使用await不会导致脱离WPF的UI线程
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
        private DateTime? _currentTime;
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


        [ObservableProperty]
        private Point? _point;
        [ObservableProperty]
        private string _getPointErrorMessage = "";
        [RelayCommand]
        private async Task GetPoint()
        {
            Point = null;
            GetPointErrorMessage = "";
            var result = await GetPointService.GetPoint();
            if (result.Succeeded)
            {
                Point = result;
                // Point = result.Value;
                // 以上两行等价
            }
            else
            {
                GetPointErrorMessage = result.Message ?? "出现了未知错误";
            }
        }
    }
}
