using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace More.Net.Windows.Forms.Interop
{
    /// <summary>
    /// 
    /// </summary>
    public static class User32
    {
        /// <summary>
        /// Passes the hook information to the next hook procedure in the current hook chain. A 
        /// hook procedure can call this function either before or after processing the hook 
        /// information.
        /// </summary>
        /// <param name="hhk">
        /// This parameter is ignored.
        /// </param>
        /// <param name="nCode">
        /// The hook code passed to the current hook procedure. The next hook procedure uses this 
        /// code to determine how to process the hook information.
        /// </param>
        /// <param name="wParam">
        /// The wParam value passed to the current hook procedure. The meaning of this parameter 
        /// depends on the type of hook associated with the current hook chain.
        /// </param>
        /// <param name="lParam">
        /// The lParam value passed to the current hook procedure. The meaning of this parameter 
        /// depends on the type of hook associated with the current hook chain.
        /// </param>
        /// <returns>
        /// This value is returned by the next hook procedure in the chain. The current hook 
        /// procedure must also return this value. The meaning of the return value depends on the 
        /// hook type.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern Int32 CallNextHookEx(
            IntPtr hhk, Int32 nCode, IntPtr wParam, IntPtr lParam);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vKey"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern Int16 GetAsyncKeyState(Keys vKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hDlg"></param>
        /// <param name="nIDDlgItem"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDlgItem(IntPtr hDlg, Int32 nIDDlgItem);

        /// <summary>
        /// Retrieves a handle to the top-level window whose class name and window name match the 
        /// specified strings. This function does not search child windows. This function does not
        /// perform a case-sensitive search.
        /// </summary>
        /// <param name="lpClassName">
        /// The class name or a class atom created by a previous call to the RegisterClass or 
        /// RegisterClassEx function. The atom must be in the low-order word of lpClassName; 
        /// the high-order word must be zero.  
        /// 
        /// If lpClassName points to a string, it specifies the window class name. The class name 
        /// can be any name registered with RegisterClass or RegisterClassEx, or any of the 
        /// predefined control-class names.  
        /// 
        /// If lpClassName is IntPtr.Zero, it finds any window whose title matches the lpWindowName 
        /// parameter.
        /// </param>
        /// <param name="lpWindowName">
        /// The window name (the window's title). If this parameter is IntPtr.Zerp, all window 
        /// names match.
        /// </param>
        /// <returns>
        /// Handle to the window that has the specified class name and window name or IntPtr.Zero 
        /// if failure.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentHwnd"></param>
        /// <param name="childAfterHwnd"></param>
        /// <param name="className"></param>
        /// <param name="windowText"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHwnd,
            IntPtr childAfterHwnd, IntPtr className, String windowText);

        /// <summary>
        /// Installs an application-defined hook procedure into a hook chain. You would install a 
        /// hook procedure to monitor the system for certain types of events. These events are 
        /// associated either with a specific thread or with all threads in the same desktop as the 
        /// calling thread.
        /// </summary>
        /// <param name="hookType">
        /// The type of hook procedure to be installed.
        /// </param>
        /// <param name="lpfn">
        /// A delegate for the hook procedure. If the dwThreadId parameter is zero or specifies the 
        /// identifier of a thread created by a different process, the lpfn parameter must point to 
        /// a hook procedure in a DLL. Otherwise, lpfn can point to a hook procedure in the code 
        /// associated with the current process.
        /// </param>
        /// <param name="hMod">
        /// A handle to the DLL containing the hook procedure pointed to by the lpfn parameter. The 
        /// hMod parameter must be set to NULL if the dwThreadId parameter specifies a thread 
        /// created by the current process and if the hook procedure is within the code associated 
        /// with the current process.
        /// </param>
        /// <param name="dwThreadId">
        /// The identifier of the thread with which the hook procedure is to be associated. For 
        /// desktop apps, if this parameter is zero, the hook procedure is associated with all 
        /// existing threads running in the same desktop as the calling thread.
        /// </param>
        /// <returns>Handle to the hook procedure or IntPtr.Zero if failure.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(
            HookType hookType, HookProcHandler lpfn, IntPtr hMod, UInt32 dwThreadId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hWndInsertAfter"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            Int32 X, Int32 Y, Int32 cx, Int32 cy, SetWindowPosFlags uFlags);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hhk"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean UnhookWindowsHookEx(IntPtr hhk);
    }
}
