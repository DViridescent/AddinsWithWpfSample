using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Revit.Utils
{
    /// <summary>
    /// 封装了Revit的外部事件调用，需要在Revit的命令上下文中初始化，调用<see cref="Init"/>
    /// <para>这个类内部实现了队列，允许同时调用多个Invoke</para>
    /// <para>队列中未被Revit执行的委托将会在Revit空闲时依次执行</para>
    /// </summary>
    internal class RevitEventRaiser
    {
        private class ExternalEventHandeler : IExternalEventHandler
        {
            Queue<Action<UIApplication>> _actionQueue = new Queue<Action<UIApplication>>();
            internal int ActionCount => _actionQueue.Count;

            public void Execute(UIApplication app)
            {
                if (_actionQueue.Count > 0)
                    _actionQueue.Dequeue().Invoke(app);
            }

            public void Enqueue(Action<UIApplication> action) => _actionQueue.Enqueue(action);

            public string GetName() => "Sample.Revit.ExternalEventHandeler";
        }

        private readonly ExternalEventHandeler _handeler;
        private readonly ExternalEvent _event;

        private RevitEventRaiser()
        {
            _handeler = new ExternalEventHandeler();
            _event = ExternalEvent.Create(_handeler);
        }

        private static RevitEventRaiser? _instance;
        private static RevitEventRaiser Instance => _instance ?? throw new Exception("需要在Revit上下文中先调用Init方法进行初始化。");

        /// <summary>
        /// 需要在Revit的命令上下文中调用，用于初始化
        /// </summary>
        internal static void Init() => _instance = new RevitEventRaiser();
        /// <summary>
        /// 封装一个<see cref="Action{UIApplication}"/>到Revit的外部事件中执行
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        internal static async Task InvokeAsync(Action<UIApplication> action)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            var internalAction = new Action<UIApplication>(app =>
            {
                try
                {
                    action(app);
                    tcs.SetResult(true);
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
                finally
                {
                    if (Instance._handeler.ActionCount > 0)
                        Instance._event.Raise();
                }
            });
            Instance._handeler.Enqueue(internalAction);
            Instance._event.Raise();
            await tcs.Task;
        }
        /// <summary>
        /// 封装一个<see cref="Func{UIApplication, TResult}"/>到Revit的外部事件中执行
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        internal static async Task<TResult> InvokeAsync<TResult>(Func<UIApplication, TResult> func)
        {
            TaskCompletionSource<TResult> tcs = new TaskCompletionSource<TResult>();
            var internalAction = new Action<UIApplication>(app =>
            {
                try
                {
                    var result = func(app);
                    tcs.SetResult(result);
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
                finally
                {
                    if (Instance._handeler.ActionCount > 0)
                        Instance._event.Raise();
                }
            });
            Instance._handeler.Enqueue(internalAction);
            Instance._event.Raise();
            return await tcs.Task;
        }
    }
}
