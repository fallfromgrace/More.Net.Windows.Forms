using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace More.Net.Windows.Forms.Interop
{
    /// <summary>
    /// A code the hook procedure uses to determine how to process the message. If nCode is less 
    /// than zero, the hook procedure must pass the message to the CallNextHookEx function without 
    /// further processing and should return the value returned by CallNextHookEx.
    /// </summary>
    internal enum HookProcCode
    {
        /// <summary>
        /// The wParam and lParam parameters contain information about a keyboard message.
        /// </summary>
        HC_ACTION = 0
    }
}
