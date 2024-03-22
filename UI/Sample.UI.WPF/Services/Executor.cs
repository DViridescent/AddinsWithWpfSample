using Sample.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Sample.UI.WPF
{
    /// <summary>
    /// 用于解决WPF的多线程问题，后台线程通过此类的实例在外部线程中执行UI修改。
    /// </summary>
    public class Executor : IUiExecutor
    {
        private Dispatcher? _dispatcher;
        private Dispatcher Dispatcher => _dispatcher ?? throw new Exception("Executor未初始化");

        /// <summary>
        /// Initializes the framework using the current dispatcher.
        /// </summary>
        public void InitializeWithDispatcher()
        {
            var dispatcher = Dispatcher.CurrentDispatcher;
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// 将任务放到UI线程执行，该调用可等待。
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public async Task RunInUIThread(Action action)
        {
            await Dispatcher.InvokeAsync(action);
        }

        /// <summary>
        /// 将任务放到UI线程执行，该调用可等待。
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public async Task<TResult> RunInUIThread<TResult>(Func<TResult> func)
        {
            return await Dispatcher.InvokeAsync(func);
        }
    }
}
