using System;
using System.Reflection;
using Sample.Core.Interfaces;

namespace Sample.Core.Services
{
    public class TimeService : ITimeService
    {
        const string magicNumber = "42";
        private int A { get; set; }

        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
