using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace More.Net.Windows.Forms
{
    /// <summary>
    /// Dialog that allows the user to select a folder.
    /// </summary>
    public class OpenFolderDialog : CommonDialog, IOpenFolderDialog
    {
        #region Constants

        /// <summary>
        /// 
        /// </summary>
        private const String DefaultTitle = "Select a Folder";

        /// <summary>
        /// 
        /// </summary>
        private static readonly String DefaultInitialDirectory = Environment.CurrentDirectory;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the initial folder to be selected. 
        /// A null value selects the current directory.
        /// </summary>
        public String InitialDirectory
        {
            get { return OpenFolderDialogImpl.InitialDirectory; }
            set { OpenFolderDialogImpl.InitialDirectory = value ?? DefaultInitialDirectory; }
        }

        /// <summary>
        /// Gets or sets the title to show in the dialog.
        /// </summary>
        public String Title
        {
            get { return OpenFolderDialogImpl.Title; }
            set { OpenFolderDialogImpl.Title = value ?? DefaultTitle; }
        }

        /// <summary>
        /// Gets the selected folder.
        /// </summary>
        public String FolderName
        {
            get { return OpenFolderDialogImpl.FolderName; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of More.Net.Windows.Forms.OpenFolderDialog with default 
        /// values.
        /// </summary>
        public OpenFolderDialog()
        {
            if (Environment.OSVersion.Version.Major < 6)
                openFolderDialogImpl = new OpenFolderDialogXP();
            else
                openFolderDialogImpl = new OpenFolderDialogInterop();
            //InitialDirectory = DefaultInitialDirectory;
            //Title = DefaultTitle;
        }

        #endregion

        /// <summary>
        /// Gets the dialog implementation.  TODO: allow derived classes to alter.
        /// </summary>
        protected IOpenFolderDialog OpenFolderDialogImpl
        {
            get { return openFolderDialogImpl; }
        }

        #region Overrides

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                OpenFolderDialogImpl.Dispose();
            }

            base.Dispose(disposing);
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

        protected override Boolean RunDialog(IntPtr hwndOwner)
        {
            return OpenFolderDialogImpl.ShowDialog(null) == DialogResult.OK;
        }

        #endregion

        #region Private Fields

        private readonly IOpenFolderDialog openFolderDialogImpl;

        #endregion
    }
}
