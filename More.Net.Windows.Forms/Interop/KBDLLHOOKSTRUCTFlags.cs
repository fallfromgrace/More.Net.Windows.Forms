using System;

namespace More.Net.Windows.Forms.Interop
{
    /// <summary>
    /// The extended-key flag, event-injected flag, context code, and transition-state flag.
    /// </summary>
    [Flags]
    internal enum KBDLLHOOKSTRUCTFlags
    {
        /// <summary>
        /// Key is an extended key (i.e. function key or numeric keypad)
        /// </summary>
        LLKHF_EXTENDED = 0x01,
        /// <summary>
        /// Event was injected.
        /// </summary>
        LLKHF_INJECTED = 0x10,
        /// <summary>
        /// Alt key is pressed.
        /// </summary>
        LLKHF_ALTDOWN = 0x20,
        /// <summary>
        /// Key is being released.
        /// </summary>
        LLKHF_UP = 0x80,
    }
}
