using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.AutoCAD.Utils
{
    internal static class ContextUtils
    {
        /// <summary>
        /// 包装一个任务，在CAD命令上下文中进行异步调用
        /// </summary>
        /// <typeparam name="T">任务的返回类型</typeparam>
        /// <param name="function">需要被CAD调用的任务</param>
        /// <returns>Task封装的任务返回值</returns>
        /// <exception cref="Exception">任务抛出的异常</exception>
        internal static Task<T> ExecuteInCommandContextAsync<T>(Func<T> function)
        {
            var tcs = new TaskCompletionSource<T>();

            // 为了防止有可能从非CAD的线程中进行命令调用，使用以下方法将命令调用放入CAD的命令上下文中
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.ExecuteInCommandContextAsync((obj) =>
            {
                T result;
                try
                {
                    result = function();
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                    return Task.CompletedTask;
                }
                tcs.SetResult(result);
                return Task.CompletedTask;
            }, null);
            return tcs.Task;
        }
    }
}
