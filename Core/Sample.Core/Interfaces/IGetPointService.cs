using System.Threading.Tasks;
using Sample.ActionResultTool;
using Sample.Geometry;

namespace Sample.Core.Interfaces
{
    public interface IGetPointService
    {
        /// <summary>
        /// 希望用户（或者什么别的东西）返回一个点
        /// <para>Task意味着期望直接返回这个任务，尽可能不要长时间阻塞线程</para>
        /// <para><see cref="ActionResult{TValue}"/>是我写的一个工具类，希望调用失败时返回<see cref="ActionResult.Fail"/>或<see cref="ActionResult.FailWithMessage(string?)"/></para>
        /// </summary>
        /// <returns></returns>
        Task<ActionResult<Point>> GetPoint();
    }
}
