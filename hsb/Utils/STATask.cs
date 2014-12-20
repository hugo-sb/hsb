using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hsb.Utils
{
    #region 【Static Class : STATask】
    /// <summary>
    /// STAで実行するTaskの生成
    /// </summary>
    public static class STATask
    {
        #region 【Static Methods】

        #region **** Staitc Method : Run(1)
        /// <summary>
        /// FuncをSTAな別スレッドで実行する
        /// </summary>
        /// <typeparam name="T">T: 型</typeparam>
        /// <param name="func">Func: 別スレッドで実行する関数</param>
        /// <returns>Task</returns>
        public static Task Run<T>(Func<T> func)
        {
            var tcs = new TaskCompletionSource<T>();
            var thread = new Thread(() =>
            {
                try
                {
                    tcs.SetResult(func());
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }
        #endregion

        #region **** Static Method : Run(2)
        /// <summary>
        /// ActionをSTAな別スレッドで実行する
        /// </summary>
        /// <param name="act">Action: 別スレッドで実行する関数</param>
        /// <returns>Task</returns>
        public static Task Run(Action act)
        {
            return Run(() =>
            {
                act();
                return true;
            });
        }
        #endregion

        #endregion
    }
    #endregion

}
