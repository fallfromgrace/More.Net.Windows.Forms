using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace More.Net.Windows.Forms.Interop
{
    /// <summary>
    /// 
    /// </summary>
    [ComImport]
    [Guid(IIDGuid.IFileSaveDialog)]
    [CoClass(typeof(FileSaveDialogRCW))]
    internal interface NativeFileSaveDialog : IFileSaveDialog
    {

    }
}
