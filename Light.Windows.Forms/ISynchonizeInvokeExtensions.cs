using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace More.Net.Common.Forms
{
    /// <summary>
    /// Extension for any object that inherits from ISynchronizeInvoke,
    /// such as Windows Forms controls.
    /// </summary>
    public static class ISynchonizeInvokeExtensions
    {
        #region BeginInvoke

        /// <summary>
        /// Executes the specified action asynchronously on the UI thread 
        /// if currently operating on a thread other than UI thread.
        /// </summary>
        /// <typeparam name="T">ISynchronizeInvoke</typeparam>
        /// <param name="action">Delegate to be executed.</param>
        public static void BeginInvoke<TSource>(
            this TSource source, 
            Action action)
            where TSource : ISynchronizeInvoke
        {
            if (source.InvokeRequired)
                source.BeginInvoke(action, new Object[0]);
            else
                action();
        }

        /// <summary>
        /// Executes the specified action asynchronously on the UI thread 
        /// if currently operating on a thread other than UI thread.
        /// </summary>
        /// <typeparam name="T">ISynchronizeInvoke</typeparam>
        /// <param name="action">Delegate to be executed.</param>
        public static void BeginInvoke<TSource, TParam1>(
            this TSource source, 
            Action<TParam1> action, 
            TParam1 param1)
            where TSource : ISynchronizeInvoke
        {
            if (source.InvokeRequired)
                source.BeginInvoke(action, new Object[] { param1 });
            else
                action(param1);
        }

        /// <summary>
        /// Executes the specified action asynchronously on the UI thread 
        /// if currently operating on a thread other than UI thread.
        /// </summary>
        /// <typeparam name="T">ISynchronizeInvoke</typeparam>
        /// <param name="action">Delegate to be executed.</param>
        public static void BeginInvoke<TSource, TParam1, TParam2>(
            this TSource source,
            Action<TParam1, TParam2> action,
            TParam1 param1,
            TParam2 param2)
            where TSource : ISynchronizeInvoke
        {
            if (source.InvokeRequired)
                source.BeginInvoke(action, new Object[] { param1, param2 });
            else
                action(param1, param2);
        }

        #endregion

        #region Invoke

        /// <summary>
        /// Executes the specified action synchronously on the UI thread 
        /// if currently operating on a thread other than UI thread.
        /// </summary>
        /// <typeparam name="T">ISynchronizeInvoke</typeparam>
        /// <param name="action">Delegate to be executed.</param>
        public static void Invoke<TSource>(this TSource source, Action action)
            where TSource : ISynchronizeInvoke
        {
            if (source.InvokeRequired)
                source.Invoke(action, new Object[0]);
            else
                action();
        }

        /// <summary>
        /// Executes the specified action synchronously on the UI thread 
        /// if currently operating on a thread other than UI thread.
        /// </summary>
        /// <typeparam name="T">ISynchronizeInvoke</typeparam>
        /// <param name="action">Delegate to be executed.</param>
        public static void Invoke<TSource, TParam1>(
            this TSource source,
            Action<TParam1> action,
            TParam1 param1)
            where TSource : ISynchronizeInvoke
        {
            if (source.InvokeRequired)
                source.Invoke(action, new Object[] { param1 });
            else
                action(param1);
        }

        /// <summary>
        /// Executes the specified action synchronously on the UI thread 
        /// if currently operating on a thread other than UI thread.
        /// </summary>
        /// <typeparam name="T">ISynchronizeInvoke</typeparam>
        /// <param name="action">Delegate to be executed.</param>
        public static void Invoke<TSource, TParam1, TParam2>(
            this TSource source,
            Action<TParam1, TParam2> action,
            TParam1 param1,
            TParam2 param2)
            where TSource : ISynchronizeInvoke
        {
            if (source.InvokeRequired)
                source.Invoke(action, new Object[] { param1, param2 });
            else
                action(param1, param2);
        }

        #endregion
    }
}


