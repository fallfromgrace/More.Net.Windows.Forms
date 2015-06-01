using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace More.Net.Windows.Forms.Interop
{

    // ---------------------------------------------------------
    // Coclass interfaces - designed to "look like" the object 
    // in the API, so that the 'new' operator can be used in a 
    // straightforward way. Behind the scenes, the C# compiler
    // morphs all 'new CoClass()' calls to 'new CoClassWrapper()'

    /// <summary>
    /// Coclass interface for a FileOpenDialog.
    /// </summary>
    [ComImport]
    [Guid(IIDGuid.IFileOpenDialog)]
    [CoClass(typeof(FileOpenDialogRCW))]
    internal interface NativeFileOpenDialog : IFileOpenDialog
    {

    }
}
