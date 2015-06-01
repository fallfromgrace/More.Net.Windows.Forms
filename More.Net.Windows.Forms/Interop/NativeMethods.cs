using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace More.Net.Windows.Forms.Interop
{
    /// <summary>
    /// 
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pszPath"></param>
        /// <param name="pbc"></param>
        /// <param name="riid"></param>
        /// <param name="ppv"></param>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern HRESULT SHCreateItemFromParsingName(
            [In, MarshalAs(UnmanagedType.LPWStr)] String pszPath,
            [In] IntPtr pbc,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [Out, MarshalAs(UnmanagedType.Interface, IidParameterIndex = 2)] out IShellItem ppv);
    }
}
