using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UI.Interfaces
{
    public interface IUiExecutor
    {
        /// <summary>
        /// 需要在UI线程调用，初始化以后才可以使用该类
        /// </summary>
        void InitializeWithDispatcher();
        /// <summary>
        /// 封装一个<see cref="Action"/>到UI线程执行
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        Task RunInUIThread(Action action);
        /// <summary>
        /// 封装一个<see cref="Func{TResult}"/>到UI线程执行
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<TResult> RunInUIThread<TResult>(Func<TResult> func);
    }
}
