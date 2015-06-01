using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace More.Net.Windows.Forms.Interop
{
    /// <summary>
    /// Run-time callable wrapper for a FileOpenDialog.
    /// </summary>
    [ComImport]
    [ClassInterface(ClassInterfaceType.None)]
    [TypeLibType(TypeLibTypeFlags.FCanCreate)]
    [Guid(CLSIDGuid.FileOpenDialog)]
    internal class FileOpenDialogRCW
    {

    }
}
