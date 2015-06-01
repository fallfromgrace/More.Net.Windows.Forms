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
    /// Exposes methods that initialize, show, and get results from the common file dialog.
    /// </summary>
    [ComImport]
    [Guid(IIDGuid.IFileDialog)] 
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IFileDialog : IModalWindow
    {
        #region IModalWindow

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
        new HRESULT Show([In] IntPtr parent);

        #endregion

        #region IFileDialog

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT SetFileTypes(
            [In] UInt32 cFileTypes,
            [In, MarshalAs(UnmanagedType.LPArray)] COMDLG_FILTERSPEC[] rgFilterSpec);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT SetFileTypeIndex([In] UInt32 iFileType);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetFileTypeIndex(out UInt32 piFileType);

        /// <summary>
        /// Assigns an event handler that listens for events coming from the dialog.
        /// </summary>
        /// <param name="pfde">An IFileDialogEvents implementation that will receive events from the dialog.</param>
        /// <param name="pdwCookie">Receives a value identiying this event handler. When the client is finished with the dialog, that client must call the IFileDialog.Unadvise method with this value.</param>
        /// <returns>If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        /// <remarks>
        /// SHSetTemporaryPropertyForItem can be used to set a temporary PKEY_ItemNameDisplay property on the item represented by the psi parameter. The value for this property will be used in place of the item's UI name.
        /// </remarks>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT Advise([In, MarshalAs(UnmanagedType.Interface)] IFileDialogEvents pfde, out UInt32 pdwCookie);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT Unadvise([In] UInt32 dwCookie);

        /// <summary>
        /// Sets flags to control the behavior of the dialog.
        /// </summary>
        /// <param name="fos">
        /// One or more of the FILEOPENDIALOGOPTIONS values.
        /// </param>
        /// <returns>
        /// If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.
        /// </returns>
        /// <remarks>
        /// Generally, this method should take the value that was retrieved by 
        /// IFileDialog::GetOptions and modify it to include or exclude options by setting the 
        /// appropriate flags.
        /// </remarks>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT SetOptions([In] FILEOPENDIALOGOPTIONS fos);

        /// <summary>
        /// Gets the current flags that are set to control dialog behavior.
        /// </summary>
        /// <param name="pfos">
        /// When this method returns successfully, contains a value made up of one or more of the 
        /// FILEOPENDIALOGOPTIONS values.
        /// </param>
        /// <returns>
        /// If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.
        /// </returns>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetOptions(out FILEOPENDIALOGOPTIONS pfos);

        /// <summary>
        /// Sets the folder used as a default if there is not a recently used folder value 
        /// available.
        /// </summary>
        /// <param name="psi">
        /// The interface that represents the folder.
        /// </param>
        /// <returns>
        /// If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.
        /// </returns>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT SetDefaultFolder([In, MarshalAs(UnmanagedType.Interface)] IShellItem psi);

        /// <summary>
        /// Sets a folder that is always selected when the dialog is opened, regardless of previous 
        /// user action.
        /// </summary>
        /// <param name="psi">
        /// The interface that represents the folder.
        /// </param>
        /// <returns>
        /// If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This folder overrides any "most recently used" folder. If this method is called while 
        /// the dialog is displayed, it causes the dialog to navigate to the specified folder.
        /// </para>
        /// <para>
        /// In general, we do not recommended the use of this method. If you call SetFolder before 
        /// you display the dialog box, the most recent location that the user saved to or opened 
        /// from is not shown. Unless there is a very specific reason for this behavior, it is not 
        /// a good or expected user experience and should therefore be avoided. In almost all 
        /// instances, IFileDialog::SetDefaultFolder is the better method.
        /// </para>
        /// <para>
        /// As of Windows 7, if the path of the folder specified through psi is the default path of 
        /// a known folder, the known folder's current path is used in the dialog. That path might 
        /// not be the same as the path specified in psi; for instance, if the known folder has 
        /// been redirected. If the known folder is a library (virtual folders Documents, Music, 
        /// Pictures, and Videos), the library's path is used in the dialog. If the specified 
        /// library is hidden (as they are by default as of Windows 8.1), the library's default 
        /// save location is used in the dialog, such as the Microsoft SkyDrive Documents folder 
        /// for the Documents library. Because of these mappings, the folder location used in the 
        /// dialog might not be exactly as you specified when you called this method.
        /// </para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT SetFolder([In, MarshalAs(UnmanagedType.Interface)] IShellItem psi);

        /// <summary>
        /// Gets either the folder currently selected in the dialog, or, if the dialog is not 
        /// currently displayed, the folder that is to be selected when the dialog is opened.
        /// </summary>
        /// <param name="ppsi">The interface that represents the folder.</param>
        /// <returns>
        /// If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.
        /// </returns>
        /// <remarks>
        /// The calling application is responsible for releasing the retrieved IShellItem when it 
        /// is no longer needed.
        /// </remarks>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetFolder([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

        /// <summary>
        /// Gets the user's current selection in the dialog.
        /// </summary>
        /// <param name="ppsi">
        /// Represents the item currently selected in the dialog. This item can be a file or folder 
        /// selected in the view window, or something that the user has entered into the dialog's 
        /// edit box. The latter case may require a parsing operation (cancelable by the user) that 
        /// blocks the current thread.
        /// </param>
        /// <returns>
        /// If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.
        /// </returns>
        /// <remarks>
        /// The calling application is responsible for releasing the retrieved IShellItem when it 
        /// is no longer needed.
        /// </remarks>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetCurrentSelection([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT SetFileName([In, MarshalAs(UnmanagedType.LPWStr)] String pszName);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetFileName([MarshalAs(UnmanagedType.LPWStr)] out String pszName);

        /// <summary>
        /// Sets the title of the dialog.
        /// </summary>
        /// <param name="pszTitle">
        /// The title text.
        /// </param>
        /// <returns>
        /// If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.
        /// </returns>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT SetTitle([In, MarshalAs(UnmanagedType.LPWStr)] String pszTitle);

        /// <summary>
        /// Sets the text of the Open or Save button.
        /// </summary>
        /// <param name="pszText">
        /// The button text.
        /// </param>
        /// <returns>
        /// If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.
        /// </returns>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT SetOkButtonLabel([In, MarshalAs(UnmanagedType.LPWStr)] String pszText);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT SetFileNameLabel([In, MarshalAs(UnmanagedType.LPWStr)] String pszLabel);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetResult([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

        /// <summary>
        /// Adds a folder to the list of places available for the user to open or save items.
        /// </summary>
        /// <param name="psi">
        /// An IShellItem that represents the folder to be made available to the user. This can 
        /// only be a folder.
        /// </param>
        /// <param name="fdap">
        /// Specifies where the folder is placed within the list.
        /// </param>
        /// <returns>
        /// If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.
        /// </returns>
        /// <remarks>
        /// SHSetTemporaryPropertyForItem can be used to set a temporary PKEY_ItemNameDisplay 
        /// property on the item represented by the psi parameter. The value for this property will 
        /// be used in place of the item's UI name.
        /// </remarks>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT AddPlace([In, MarshalAs(UnmanagedType.Interface)] IShellItem psi, FDAP fdap);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT SetDefaultExtension([In, MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        /// <param name="hr">
        /// The code that will be returned by Show to indicate that the dialog was closed before a 
        /// selection was made.
        /// </param>
        /// <returns>
        /// If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.
        /// </returns>
        /// <remarks>
        /// An application can call this method from a callback method or function while the dialog 
        /// is open. The dialog will close and the Show method will return with the HRESULT 
        /// specified in hr.
        /// If this method is called, there is no result available for the IFileDialog::GetResult 
        /// or GetResults methods, and they will fail if called.
        /// </remarks>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT Close([MarshalAs(UnmanagedType.Error)] HRESULT hr);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT SetClientGuid([In] ref Guid guid);

        /// <summary>
        /// Instructs the dialog to clear all persisted state information.
        /// </summary>
        /// <returns>
        /// If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.
        /// </returns>
        /// <remarks>
        /// Persisted information can be associated with an application or a GUID. If a GUID was 
        /// set by using IFileDialog::SetClientGuid, that GUID is used to clear persisted 
        /// information.
        /// </remarks>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT ClearClientData();

        #region Not Supported

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT SetFilter([MarshalAs(UnmanagedType.Interface)] IntPtr pFilter);

        #endregion

        #endregion
    }
}

