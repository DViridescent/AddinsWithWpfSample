using Sample.ActionResultTool;
using Sample.Core.Interfaces;
using Sample.Geometry;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Revit.Services
{
    internal class RevitGetPointService : IGetPointService
    {
        public async Task<ActionResult<Point>> GetPoint()
        {
            // 还没在Revit里写过这个功能 (⇀‸↼‶)
            return await Task.FromResult(ActionResult.FailWithMessage("Revit暂时不支持获取点"));
        }
    }
}
