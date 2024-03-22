using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UI.Interfaces
{
    public interface IUiExecutor
    {
        void InitializeWithDispatcher();
        Task RunInUIThread(Action action);
        Task<TResult> RunInUIThread<TResult>(Func<TResult> func);
    }
}
