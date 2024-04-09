using System;
using System.Reflection;
using Sample.Core.Interfaces;

namespace Sample.Core.Services
{
    public class TimeService : ITimeService
    {
        public DateTime GetCurrentTime() => DateTime.Now;
    }
}
