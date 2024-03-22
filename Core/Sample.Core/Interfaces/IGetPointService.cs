using Sample.ActionResultTool;
using Sample.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Core.Interfaces
{
    public interface IGetPointService
    {
        Task<ActionResult<Point>> GetPoint();
    }
}
