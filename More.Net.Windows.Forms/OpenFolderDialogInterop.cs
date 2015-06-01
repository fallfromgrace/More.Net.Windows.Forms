using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using More.Net.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace More.Net.Windows.Forms
{
    /// <summary>
    /// Directly uses the native COM interfaces to show a file dialog with the ability to choose a 
    /// folder.
    /// </summary>
    internal class OpenFolderDialogInterop : CommonDialog, IOpenFolderDialog
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box displays a warning if the user 
        /// specifies a path that does not exist.
        /// </summary>
        public Boolean CheckPathExists
        {
            get { return Options.HasFlag(Interop.FILEOPENDIALOGOPTIONS.FOS_PATHMUSTEXIST); }
            set
            {
                if (value == true)
                    Options |= Interop.FILEOPENDIALOGOPTIONS.FOS_PATHMUSTEXIST;
                else
                    Options &= ~Interop.FILEOPENDIALOGOPTIONS.FOS_PATHMUSTEXIST;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box returns the location of the file 
        /// referenced by the shortcut or whether it returns the location of the shortcut (.lnk).
        /// </summary>
        public Boolean DereferenceLinks
        {
            get { return Options.HasFlag(Interop.FILEOPENDIALOGOPTIONS.FOS_NODEREFERENCELINKS) == false; }
            set
            {
                if (value == false)
                    Options |= Interop.FILEOPENDIALOGOPTIONS.FOS_ALLOWMULTISELECT;
                else
                    Options &= ~Interop.FILEOPENDIALOGOPTIONS.FOS_ALLOWMULTISELECT;
            }
        }

        /// <summary>
        /// Gets the selected folder.
        /// </summary>
        public String FolderName
        {
            get
            {
                Interop.IShellItem ppsi;
                String folderPath;
                fileOpenDialog.GetFolder(out ppsi).ThrowIfFailed();
                ppsi.GetDisplayName(Interop.SIGDN.FILESYSPATH, out folderPath).ThrowIfFailed();
                Marshal.FinalReleaseComObject(ppsi);
                return folderPath;
            }
        }

        /// <summary>
        /// Gets or sets the initial directory displayed by the file dialog box.
        /// </summary>
        public String InitialDirectory
        {
            get { return initialDirectory; }
            set
            {
                Interop.IShellItem ppv;
                if (initialDirectory != value)
                {
                    initialDirectory = value;
                    Interop.NativeMethods
                        .SHCreateItemFromParsingName(
                            InitialDirectory, 
                            IntPtr.Zero, 
                            Guid.Parse(Interop.IIDGuid.IShellItem), 
                            out ppv)
                        .ThrowIfFailed();
                    fileOpenDialog.SetDefaultFolder(ppv).ThrowIfFailed(); ;
                    Marshal.FinalReleaseComObject(ppv);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box allows multiple files to be 
        /// selected.
        /// </summary>
        public Boolean MultiSelect
        {
            get { return Options.HasFlag(Interop.FILEOPENDIALOGOPTIONS.FOS_ALLOWMULTISELECT); }
            set
            {
                if (value == true)
                    Options |= Interop.FILEOPENDIALOGOPTIONS.FOS_ALLOWMULTISELECT;
                else
                    Options &= ~Interop.FILEOPENDIALOGOPTIONS.FOS_ALLOWMULTISELECT;
            }
        }

        /// <summary>
        /// Gets or sets the OK button's label.
        /// </summary>
        public String OkButtonLabel
        {
            get { return okButtonLabel; }
            set
            {
                if (OkButtonLabel != value)
                {
                    okButtonLabel = value;
                    fileOpenDialog.SetOkButtonLabel(OkButtonLabel).ThrowIfFailed();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box restores the current directory 
        /// before closing.
        /// </summary>
        public Boolean RestoreDirectory
        {
            get { return Options.HasFlag(Interop.FILEOPENDIALOGOPTIONS.FOS_NOCHANGEDIR); }
            set
            {
                if (value == true)
                    Options |= Interop.FILEOPENDIALOGOPTIONS.FOS_NOVALIDATE;
                else
                    Options &= ~Interop.FILEOPENDIALOGOPTIONS.FOS_NOVALIDATE;
            }
        }

        /// <summary>
        /// Gets or sets the file dialog box title.
        /// </summary>
        public String Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    fileOpenDialog.SetTitle(Title).ThrowIfFailed();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box accepts only valid Win32 file 
        /// names.
        /// </summary>
        public Boolean ValidateNames
        {
            get { return Options.HasFlag(Interop.FILEOPENDIALOGOPTIONS.FOS_NOVALIDATE) == false; }
            set
            {
                if (value == false)
                    Options |= Interop.FILEOPENDIALOGOPTIONS.FOS_NOVALIDATE;
                else
                    Options &= ~Interop.FILEOPENDIALOGOPTIONS.FOS_NOVALIDATE;
            }
        }

        #endregion

        #region Interop Properties

        /// <summary>
        /// Gets or sets the file open dialog options.
        /// </summary>
        protected Interop.FILEOPENDIALOGOPTIONS Options
        {
            get
            {
                Interop.FILEOPENDIALOGOPTIONS pfos;
                fileOpenDialog.GetOptions(out pfos).ThrowIfFailed();
                return pfos;
            }
            set
            {
                fileOpenDialog.SetOptions(value).ThrowIfFailed();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public OpenFolderDialogInterop()
        {
            if (Environment.OSVersion.Version.Major < 6)
                throw new NotSupportedException("This folder dialog requires the target operating system to be Windows Vista or greater.");

            fileOpenDialog = new Interop.NativeFileOpenDialog();
            Options |= Interop.FILEOPENDIALOGOPTIONS.FOS_PICKFOLDERS;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// 
        /// </summary>
        public override void Reset()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwndOwner"></param>
        /// <returns></returns>
        protected override Boolean RunDialog(IntPtr hwndOwner)
        {
            Interop.HRESULT result = fileOpenDialog.Show(hwndOwner);
            switch (result)
            {
                case Interop.HRESULT.ERROR_CANCELLED:
                    return false;
                case Interop.HRESULT.S_OK:
                    return result.Succeeded;
                default:
                    result.ThrowIfFailed();
                    return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                Marshal.FinalReleaseComObject(fileOpenDialog);
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Private Fields

        private String initialDirectory;
        private String okButtonLabel;
        private String title;
        private readonly Interop.IFileOpenDialog fileOpenDialog;

        #endregion
    }
}
