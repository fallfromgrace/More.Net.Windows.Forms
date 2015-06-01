using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace More.Net.Windows.Forms.Interop
{
    /// <summary>
    /// Contains information about a low-level keyboard input event.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct KBDLLHOOKSTRUCT
    {
        /// <summary>
        /// A virtual-key code. The code must be a value in the range 1 to 254.
        /// </summary>
        public Keys vkCode;
        /// <summary>
        /// A hardware scan code for the key.
        /// </summary>
        public UInt32 scanCode;
        /// <summary>
        /// The extended-key flag, event-injected flag, context code, and transition-state flag.
        /// </summary>
        public KBDLLHOOKSTRUCTFlags flags;
        /// <summary>
        /// The time stamp for this message, equivalent to what GetMessageTime would return for 
        /// this message.
        /// </summary>
        public UInt32 time;
        /// <summary>
        /// Additional information associated with the message.
        /// </summary>
        public UIntPtr dwExtraInfo;
    }
}
