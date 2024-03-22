using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UI.Interfaces
{
    public interface INavigater
    {
        void NavigateWithViewModel<T>() where T : ObservableObject;
    }
}
