using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UI.Interfaces
{
    /// <summary>
    /// 导航器，用于页面之间的切换
    /// </summary>
    public interface INavigater
    {
        /// <summary>
        /// 切换页面到指定的ViewModel，T是对应的ViewModel
        /// </summary>
        /// <typeparam name="T">对应的ViewModel</typeparam>
        void NavigateWithViewModel<T>() where T : ObservableObject;
    }
}
