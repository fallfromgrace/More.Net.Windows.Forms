using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace More.Net.Windows.Forms.Interop
{
    /// <summary>
    /// Callback function used with the SetWindowsHookEx function. The system calls this 
    /// function every time a new keyboard input event is about to be posted into a thread 
    /// input queue.
    /// </summary>
    /// <param name="nCode">
    /// A code the hook procedure uses to determine how to process the message. If nCode is 
    /// less than zero, the hook procedure must pass the message to the CallNextHookEx function 
    /// without further processing and should return the value returned 
    /// by CallNextHookEx.
    /// </param>
    /// <param name="wParam">
    /// The identifier of the keyboard message.
    /// </param>
    /// <param name="lParam">
    /// A pointer to a KBDLLHOOKSTRUCT structure.
    /// </param>
    /// <returns></returns>
    public delegate IntPtr HookProcHandler(Int32 nCode, IntPtr wParam, IntPtr lParam);
}
