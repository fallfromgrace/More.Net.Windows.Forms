using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace More.Net.Windows.Forms.Interop
{
    /// <summary>
    /// 
    /// </summary>
    [ComImport]
    [Guid(IIDGuid.IFileOpenDialog)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IFileSaveDialog : IFileDialog
    {
        #region IModalWindow

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT Show([In] IntPtr parent);

        #endregion

        #region IFileDialog

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cFileTypes"></param>
        /// <param name="rgFilterSpec"></param>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT SetFileTypes([In] UInt32 cFileTypes, [In] COMDLG_FILTERSPEC[] rgFilterSpec);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iFileType"></param>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT SetFileTypeIndex([In] UInt32 iFileType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="piFileType"></param>
        /// <returns></returns>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT GetFileTypeIndex(out UInt32 piFileType);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT Advise([In, MarshalAs(UnmanagedType.Interface)] IFileDialogEvents pfde, out UInt32 pdwCookie);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT Unadvise([In] UInt32 dwCookie);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT SetOptions([In] FILEOPENDIALOGOPTIONS fos);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT GetOptions(out FILEOPENDIALOGOPTIONS pfos);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT SetDefaultFolder([In, MarshalAs(UnmanagedType.Interface)] IShellItem psi);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT SetFolder([In, MarshalAs(UnmanagedType.Interface)] IShellItem psi);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT GetFolder([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT GetCurrentSelection([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT SetFileName([In, MarshalAs(UnmanagedType.LPWStr)] String pszName);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT GetFileName([MarshalAs(UnmanagedType.LPWStr)] out String pszName);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT SetTitle([In, MarshalAs(UnmanagedType.LPWStr)] String pszTitle);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT SetOkButtonLabel([In, MarshalAs(UnmanagedType.LPWStr)] String pszText);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT SetFileNameLabel([In, MarshalAs(UnmanagedType.LPWStr)] String pszLabel);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT GetResult([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT AddPlace([In, MarshalAs(UnmanagedType.Interface)] IShellItem psi, FDAP fdap);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT SetDefaultExtension([In, MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT Close([MarshalAs(UnmanagedType.Error)] HRESULT hr);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT SetClientGuid([In] ref Guid guid);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT ClearClientData();

        #region Not Supported

        // Not supported:  IShellItemFilter is not defined, converting to IntPtr
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new HRESULT SetFilter([MarshalAs(UnmanagedType.Interface)] IntPtr pFilter);

        #endregion

        #endregion

        #region IFileSaveDialog



        #endregion
    }
}
