using Sample.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Core.Services
{
    public class TimeService : ITimeService
    {
        public DateTime GetCurrentTime() => DateTime.Now;
    }
}
