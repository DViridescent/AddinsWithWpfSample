using Sample.ActionResultTool;
using Sample.Core.Interfaces;
using Sample.Geometry;
using System;
using System.Threading.Tasks;

namespace Sample.Stubs
{
    public class GetPointServiceStub : IGetPointService
    {
        private static readonly Random _random = new Random();
        public async Task<ActionResult<Point>> GetPoint()
        {
            if (_random.NextDouble() < 0.3)
            {
                return ActionResult.FailWithMessage("操作因为某些内部错误失败了，以下是错误信息：\n这是一个模拟的错误");
            }
            return await Task.FromResult(new Point(_random.NextDouble() * 100, _random.NextDouble() * 100, _random.NextDouble() * 100));
        }
    }
}
