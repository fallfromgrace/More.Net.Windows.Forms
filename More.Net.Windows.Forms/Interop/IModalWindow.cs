using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace More.Net.Windows.Forms.Interop
{
    /// <summary>
    /// Exposes a method that represents a modal window.
    /// </summary>
    [ComImport]
    [Guid(IIDGuid.IModalWindow)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IModalWindow
    {
        /// <summary>
        /// Launches the modal window.
        /// </summary>
        /// <param name="parent">
        /// The handle of the owner window. This value can be INtPtr.Zero.
        /// </param>
        /// <returns>
        /// If the method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code, 
        /// including the following:
        /// ERROR_CANCELLED (0x800704C7)
        /// </returns>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT Show([In] IntPtr parent);
    }
}
