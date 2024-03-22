using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sample.Core.Services;
using Sample.Geometry;
using Sample.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UI.ViewModels.Design
{
    public class HomePageDesignViewModel
    {
        public int Count => 10086;
        public DateTime CurrentTime => DateTime.Now;
        public Point Point { get; } = new Point(1, 2, 3);
        public string GetPointErrorMessage => "";
    }
}
