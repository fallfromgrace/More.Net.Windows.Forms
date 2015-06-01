using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace More.Net.Common.Forms.GraphicsOverlay
{
    /// <summary>
    /// Base class for all overlay objects.
    /// </summary>
    public abstract class Overlay : IOverlay
    {
        /// <summary>
        /// Gest or sets the name of this overlay.
        /// </summary>
        public virtual String Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets the required Opacity of this overlay.
        /// </summary>
        public virtual Double Opacity
        {
            get { return opacity; }
            set
            {
                if (Opacity != value)
                {
                    opacity = value;
                    OnOpacityChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets whether the overlay is allowed to be painted.
        /// </summary>
        public virtual Boolean CanDraw
        {
            get { return canDraw; }
            set
            {
                if (CanDraw != value)
                {
                    canDraw = value;
                    OnCanDrawChanged();
                }
            }
        }

        /// <summary>
        /// Raised when the CanDraw value changes.
        /// </summary>
        public event EventHandler CanDrawChanged;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler ContentChanged;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OpacityChanged;

        /// <summary>
        /// Initializes a new instance of the More.Net.UI.Forms.Overlay class.
        /// </summary>
        public Overlay()
        {
            OnInitializing();
        }

        /// <summary>
        /// Draws to a GDI+ graphics object.
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            if (CanDraw)
            {
                OnDraw(g);
            }
        }

        protected virtual void OnInitializing()
        {
            Opacity = DefaultOpacity;
            CanDraw = false;
        }

        /// <summary>
        /// When overridden in a derived class, draws to a GDI+ graphics object.
        /// </summary>
        /// <param name="g"></param>
        protected abstract void OnDraw(Graphics g);

        protected virtual void OnCanDrawChanged()
        {
            if (CanDrawChanged != null)
                CanDrawChanged(this, new EventArgs());
        }

        protected virtual void OnContentChanged()
        {
            if (ContentChanged != null)
                ContentChanged(this, new EventArgs());
        }

        protected virtual void OnOpacityChanged()
        {
            if (OpacityChanged != null)
                OpacityChanged(this, new EventArgs());
        }

        private String name;
        private Double opacity;
        private const Double DefaultOpacity = 1.0;
        private Boolean canDraw;
    }
}
