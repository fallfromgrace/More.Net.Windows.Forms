using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace More.Net.Windows.Forms
{
    /// <summary>
    /// Wrapper around a System.Windows.Forms.OpenFileDialog, using reflection and native calls to 
    /// show the normally unavailable vista-style dialog that allows the selecting of a folder.
    /// </summary>
    internal class OpenFolderDialogVista : CommonDialog, IOpenFolderDialog
    {
        /// <summary>
        /// 
        /// </summary>
        public String FolderName
        {
            get { return openFolderDialog.FileName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public String InitialDirectory
        {
            get { return openFolderDialog.InitialDirectory; }
            set { openFolderDialog.InitialDirectory = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public String Title
        {
            get { return openFolderDialog.Title; }
            set { openFolderDialog.Title = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public OpenFolderDialogVista()
        {
            openFolderDialog = new OpenFileDialog();
            openFolderDialog.Filter = "Folders|\n";
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Reset()
        {
            openFolderDialog.Reset();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwndOwner"></param>
        /// <returns></returns>
        protected override Boolean RunDialog(IntPtr hwndOwner)
        {
            BindingFlags bindingFlags = 
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            UInt32 eventHandle = 0;
            Boolean ok = false;
            Type iFileDialog = null;

            Object vistaDialog = typeof(OpenFileDialog)
                .GetMethod("CreateVistaDialog", bindingFlags)
                .Invoke(openFolderDialog, new Object[] { });
            typeof(OpenFileDialog)
                .GetMethod("OnBeforeVistaDialog", bindingFlags)
                .Invoke(openFolderDialog, new Object[] { vistaDialog });
            Interop.FILEOPENDIALOGOPTIONS options = (Interop.FILEOPENDIALOGOPTIONS)typeof(FileDialog)
                .GetMethod("GetOptions", bindingFlags)
                .Invoke(openFolderDialog, new Object[] { });
            iFileDialog
                .GetMethod("SetOptions", bindingFlags)
                .Invoke(vistaDialog, new Object[] { options | Interop.FILEOPENDIALOGOPTIONS.FOS_PICKFOLDERS });
            Object vistaDialogEvents = typeof(FileDialog)
                .GetNestedType("VistaDialogEvents", bindingFlags)
                .GetConstructor(new Type[] { typeof(OpenFileDialog) })
                .Invoke(new Object[] { openFolderDialog });
            Object[] parameters = new Object[] { vistaDialogEvents, eventHandle };
            iFileDialog
                .GetMethod("Advise", bindingFlags)
                .Invoke(vistaDialog, parameters);
            eventHandle = (UInt32)parameters[1];
            try
            {
                ok = iFileDialog
                    .GetMethod("Show", bindingFlags)
                    .Invoke(vistaDialog, new Object[] { hwndOwner }).Equals(0);
            }
            finally
            {
                iFileDialog
                    .GetMethod("Unadvise", bindingFlags)
                    .Invoke(vistaDialog, new Object[] { eventHandle });
                GC.KeepAlive(vistaDialogEvents);
            }

            return ok;
        }

        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                openFolderDialog.Dispose();
            }

            base.Dispose(disposing);
        }

        private readonly OpenFileDialog openFolderDialog;
    }
}
