using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace More.Net.Windows.Forms.Interop
{
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
    internal struct COMDLG_FILTERSPEC
    {
        /// <summary>
        /// 
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public String pszName;

        /// <summary>
        /// 
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public String pszSpec;
    }
}
