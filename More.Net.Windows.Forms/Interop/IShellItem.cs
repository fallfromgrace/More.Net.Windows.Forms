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
    /// Exposes methods that retrieve information about a Shell item.
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(IIDGuid.IShellItem)]
    internal interface IShellItem
    {
        /// <summary>
        /// Binds to a handler for an item as specified by the handler ID value.
        /// </summary>
        /// <param name="pbc"></param>
        /// <param name="bhid"></param>
        /// <param name="riid"></param>
        /// <param name="ppv"></param>
        /// <returns></returns>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT BindToHandler(
            IntPtr pbc,
            [MarshalAs(UnmanagedType.LPStruct)] Guid bhid,
            [MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            out IntPtr ppv);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ppsi"></param>
        /// <returns></returns>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetParent(out IShellItem ppsi);

        /// <summary>
        /// Gets the display name of the IShellItem object.
        /// </summary>
        /// <param name="sigdnName">
        /// One of the SIGDN values that indicates how the name should look.
        /// </param>
        /// <param name="ppszName">A value that, when this function returns successfully, receives 
        /// the retrieved display name.
        /// </param>
        /// <returns>
        /// If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.
        /// </returns>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetDisplayName(
            SIGDN sigdnName, 
            [MarshalAs(UnmanagedType.LPWStr)] out String ppszName);

        /// <summary>
        /// Gets a requested set of attributes of the IShellItem object.
        /// </summary>
        /// <param name="sfgaoMask">
        /// Specifies the attributes to retrieve. One or more of the SFGAO values. Use a bitwise OR 
        /// operator to determine the attributes to retrieve.
        /// </param>
        /// <param name="psfgaoAttribs">
        /// A value that, when this function returns successfully, contains the requested 
        /// attributes. One or more of the SFGAO values. Only those attributes specified by 
        /// sfgaoMask are returned; other attribute values are undefined.
        /// </param>
        /// <returns>
        /// Returns S_OK if the attributes returned exactly match those requested in sfgaoMask, 
        /// S_FALSE if the attributes do not exactly match, or a standard COM error value
        /// otherwise.
        /// </returns>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetAttributes(UInt32 sfgaoMask, out UInt32 psfgaoAttribs);

        /// <summary>
        /// Compares two IShellItem objects.
        /// </summary>
        /// <param name="psi">
        /// An IShellItem object to compare with the existing IShellItem object.
        /// </param>
        /// <param name="hint"></param>
        /// <param name="piOrder"></param>
        /// <returns></returns>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT Compare(IShellItem psi, UInt32 hint, out Int32 piOrder);
    };
}
