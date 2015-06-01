using More.Net.Windows.Interop;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace More.Net.Windows.Forms.Interop
{
    /// <summary>
    /// 
    /// </summary>
    public static class WndProc
    {
        /// <summary>
        /// Disables all task switching keyboard shortcuts. 
        /// (Windows Key, ALT-TAB, CTRL-ESC, CTRL-SHIFT-ESC)
        /// </summary>
        public static void DisableTaskSwitching()
        {
            if (hKeyboardHook == IntPtr.Zero)
            {
                using (Process process = Process.GetCurrentProcess())
                using (ProcessModule module = process.MainModule)
                {
                    //Must keep handler reference to prevent GC from premature collection of this delegate.
                    lowLevelKeyboardProc = new HookProcHandler(LowLevelKeyboardProc);
                    hKeyboardHook = User32.SetWindowsHookEx(
                        HookType.WH_KEYBOARD_LL, lowLevelKeyboardProc,
                        More.Net.Windows.Interop.Win32.Kernel32.GetModuleHandle(module.ModuleName), GLOBAL_THREADID);

                    if (hKeyboardHook == IntPtr.Zero)
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        /// <summary>
        /// Enables all task switching keyboard shortcuts.
        /// (Windows Key, ALT-TAB, CTRL-ESC, CTRL-SHIFT-ESC)
        /// </summary>
        public static void EnableTaskSwitching()
        {
            if (hKeyboardHook != IntPtr.Zero)
            {
                if (!User32.UnhookWindowsHookEx(hKeyboardHook))
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                hKeyboardHook = IntPtr.Zero;
                lowLevelKeyboardProc = null;
            }
        }

        /// <summary>
        /// Hides the task bar and start button.
        /// </summary>
        public static void HideTaskBarAndStartButton()
        {
            User32.ShowWindow(
                User32.FindWindowEx(IntPtr.Zero, IntPtr.Zero, StartButtonId, null),
                ShowWindowCommands.Hide);
            User32.ShowWindow(
                User32.FindWindow("Shell_TrayWnd", null),
                ShowWindowCommands.Hide);
        }

        /// <summary>
        /// Shows the task bar and start button.
        /// </summary>
        public static void ShowTaskBarAndStartButton()
        {
            User32.ShowWindow(
                User32.FindWindowEx(IntPtr.Zero, IntPtr.Zero, StartButtonId, null),
                ShowWindowCommands.Show);
            User32.ShowWindow(
                User32.FindWindow("Shell_TrayWnd", null),
                ShowWindowCommands.Show);
        }

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
        private static IntPtr LowLevelKeyboardProc(Int32 nCode, IntPtr wParam, IntPtr lParam)
        {
            KBDLLHOOKSTRUCT p;

            if (nCode == (Int32)HookProcCode.HC_ACTION)
            {
                p = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));

                if (((p.vkCode == Keys.LWin) || (p.vkCode == Keys.RWin)) ||                         // WIN key (for Start Menu)
                    (p.vkCode == Keys.F4 && (p.flags & KBDLLHOOKSTRUCTFlags.LLKHF_ALTDOWN) ==
                        KBDLLHOOKSTRUCTFlags.LLKHF_ALTDOWN) ||  
                    (p.vkCode == Keys.Tab && (p.flags & KBDLLHOOKSTRUCTFlags.LLKHF_ALTDOWN) == 
                        KBDLLHOOKSTRUCTFlags.LLKHF_ALTDOWN) ||                                      // ALT+TAB
                    (p.vkCode == Keys.Escape && (p.flags & KBDLLHOOKSTRUCTFlags.LLKHF_ALTDOWN) == 
                        KBDLLHOOKSTRUCTFlags.LLKHF_ALTDOWN) ||                                      // ALT+ESC
                    ((p.vkCode == Keys.Escape) &&
                        ((User32.GetAsyncKeyState(Keys.Control) & 0x8000) != 0)) ||                        // CTRL+ESC
                    ((p.vkCode == Keys.Escape) &&
                        ((User32.GetAsyncKeyState(Keys.Control) & 0x8000) != 0) &&
                        ((User32.GetAsyncKeyState(Keys.Shift) & 0x8000) != 0)))                            // CTRL+SHIFT+ESC
                {
                    return new IntPtr(1);
                }
            }

            return new IntPtr(User32.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam));
        }

        private static HookProcHandler lowLevelKeyboardProc;
        private static IntPtr hKeyboardHook = IntPtr.Zero;
        private static readonly IntPtr StartButtonId = new IntPtr(0xC017);

        private const UInt32 GLOBAL_THREADID = 0;
    }
}
