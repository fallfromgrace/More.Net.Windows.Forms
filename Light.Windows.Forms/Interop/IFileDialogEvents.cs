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
    /// 
    /// </summary>
    [ComImport]
    [Guid(IIDGuid.IFileDialogEvents)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IFileDialogEvents
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pfd"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        HRESULT OnFileOk(
            [In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pfd"></param>
        /// <param name="psiFolder"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        HRESULT OnFolderChanging(
            [In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd,
            [In, MarshalAs(UnmanagedType.Interface)] IShellItem psiFolder);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pfd"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT OnFolderChange(
            [In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pfd"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT OnSelectionChange(
            [In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pfd"></param>
        /// <param name="psi"></param>
        /// <param name="pResponse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT OnShareViolation(
            [In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd,
            [In, MarshalAs(UnmanagedType.Interface)] IShellItem psi,
            [Out] out FDE_SHAREVIOLATION_RESPONSE pResponse);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pfd"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT OnTypeChange([In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pfd"></param>
        /// <param name="psi"></param>
        /// <param name="pResponse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT OnOverwrite(
            [In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd,
            [In, MarshalAs(UnmanagedType.Interface)] IShellItem psi,
            [Out] out FDE_OVERWRITE_RESPONSE pResponse);
    }
}
