using Rhino.Geometry;
using Rhino.Input.Custom;
using Rhino.Input;
using Rhino.UI;

using Sample.ActionResultTool;
using Sample.Core.Interfaces;
using Sample.Geometry;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sample.Rhino.Utils;

namespace Sample.Rhino.Services
{
    internal class RhinoGetPointService : IGetPointService
    {
        public async Task<ActionResult<Sample.Geometry.Point>> GetPoint()
        {
            // 二开容易导致软件崩溃的地方需要做专门的捕获
            try
            {
                Point3d point = await GetRhinoPoint();
                return point.ToSampleGeometry();
            }
            catch (Exception e)
            {
                return ActionResult.FailWithMessage($"操作因为某些内部错误失败了，以下是错误信息：\n{e.Message}");
            }
        }

        private async Task<Point3d> GetRhinoPoint()
        {
            var getPoint = new GetPoint();
            var optionName = new LocalizeStringPair("Exception", "老子要抛出异常");
            getPoint.AddOption(optionName);
            getPoint.SetCommandPrompt("选取定位点或");

            var getResult = getPoint.Get(); // 没有做异步包装，可以仿照AutoCAD的写法进行包装
            if (getResult == GetResult.Point)
            {
                return await Task.FromResult(getPoint.Point());
            }
            else if (getResult == GetResult.Option)
            {
                var option = getPoint.Option();
                if (option.EnglishName == "Exception")// 现在只有一个选项，所以其实不需要判断
                {
                    throw new Exception("用户选择抛出一个嚣张的异常");
                }
            }
            throw new Exception("用户取消了操作");
        }
    }
}
