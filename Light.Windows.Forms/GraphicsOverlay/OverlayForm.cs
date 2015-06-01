using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using More.Net.Windows.Forms.Interop;

namespace More.Net.Common.Forms.GraphicsOverlay
{
    /// <summary>
    /// Implements a Windows Forms overlay by using a transparent System.Windows.Forms.Form
    /// to draw on top the owner of this form.
    /// </summary>
    /// <remarks>
    /// TODO:
    ///     Move p/invoke stuff to seperate assembly.
    ///     Override Show and ShowDialog?
    /// </remarks>
    public class OverlayForm : Form
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the overlay object for this form.
        /// </summary>
        public IOverlay Overlay
        {
            get { return overlay; }
            set
            {
                if (overlay != value)
                {
                    if (value == null)
                        throw new ArgumentNullException();
                    overlay = value;
                    OnOverlayChanged();
                }
            }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of More.Net.UI.Forms.OverlayForm.
        /// </summary>
        public OverlayForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            //SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.Manual;
            ShowInTaskbar = false;
            BackColor = Color.FromArgb(255, 16, 16, 16);
            TransparencyKey = Color.FromArgb(255, 16, 16, 16);
            DoubleBuffered = true;
        }

        /// <summary>
        /// Prevents disposing when Dispose is explicitly called.  This is necessary because 
        /// DevExpress is somehow disposes these child forms.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (!disposing)
                base.Dispose(disposing);
        }

        public void DisposeCustom()
        {
            base.Dispose(true);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Shows 
        /// </summary>
        public void ShowWithoutActivate()
        {
            IsShowNoActivate = User32.ShowWindow(Handle, ShowWindowCommands.ShowNoActivate);
        }

        public Boolean IsShowNoActivate
        {
            get;
            set;
        }

        #endregion

        #region Property Changed Handlers

        private void OnOverlayChanged()
        {
            Opacity = overlay.Opacity;
            if (overlay.CanDraw)
                Invalidate();
            overlay.CanDrawChanged += Overlay_CanDrawChanged;
            overlay.ContentChanged += Overlay_ContentChanged;
            overlay.OpacityChanged += Overlay_OpacityChanged;
        }

        #endregion

        #region Overlay Events

        private void Overlay_CanDrawChanged(Object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Overlay_ContentChanged(Object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Overlay_OpacityChanged(Object sender, EventArgs e)
        {
            Opacity = overlay.Opacity;
            Invalidate();
        }

        #endregion

        #region Overrides

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Overlay != null)
                Overlay.Draw(e.Graphics);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (Int32)Win32Messages.WM_NCHITTEST)
                m.Result = (IntPtr)(Int32)WM_NCHITTEST_Results.HTTRANSPARENT;
            else
                base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80;
                return cp;
            }
        }

        #endregion

        #region Private Fields

        private IOverlay overlay;

        #endregion
    }
}
