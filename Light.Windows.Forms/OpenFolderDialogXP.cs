using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace More.Net.Windows.Forms
{
    /// <summary>
    /// Wrapper around a System.Windows.Forms.FolderBrowserDialog to conform to the new common 
    /// More.Net.Windows.Forms.IOpenFolderDialog interface
    /// </summary>
    internal class OpenFolderDialogXP : CommonDialog, IOpenFolderDialog
    {
        #region Public Properties

        /// <summary>
        /// Gets the folder name.
        /// </summary>
        public String FolderName
        {
            get { return folderBrowserDialog.SelectedPath; }
        }

        /// <summary>
        /// Gets or sets the initial directory.  This directory must must an 
        /// Environment.SpecialFolder.
        /// </summary>
        public String InitialDirectory
        {
            get { return Environment.GetFolderPath(folderBrowserDialog.RootFolder); }
            set { folderBrowserDialog.RootFolder = Environment.SpecialFolder.AdminTools; }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public String Title
        {
            get { return folderBrowserDialog.Description; }
            set { folderBrowserDialog.Description = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of More.Net.Windows.Forms.OpenFolderDialogXP with default 
        /// values.
        /// </summary>
        public OpenFolderDialogXP()
        {
            folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = false;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                folderBrowserDialog.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Reset()
        {
            folderBrowserDialog.Reset();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwndOwner"></param>
        /// <returns></returns>
        protected override Boolean RunDialog(IntPtr hwndOwner)
        {
            return folderBrowserDialog.ShowDialog() == DialogResult.OK;
        }

        private readonly FolderBrowserDialog folderBrowserDialog;
    }
}
