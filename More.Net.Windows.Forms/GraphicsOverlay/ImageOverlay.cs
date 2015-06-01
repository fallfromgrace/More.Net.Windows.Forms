using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace More.Net.Common.Forms.GraphicsOverlay
{
    /// <summary>
    /// Paints an unscaled and unclipped image.
    /// </summary>
    public class ImageOverlay : Overlay
    {
        /// <summary>
        /// Gets or sets the current image.
        /// </summary>
        public Bitmap Image
        {
            get { return image; }
            set
            {
                if (Image != value)
                {
                    image = value;
                    OnImageChanged();
                }
            }
        }

        protected override void OnDraw(Graphics g)
        {
            g.DrawImageUnscaledAndClipped(
                Image,
                new Rectangle(
                    (Int32)g.VisibleClipBounds.Width / 2 - Image.Width / 2,
                    (Int32)g.VisibleClipBounds.Height / 2 - Image.Height / 2,
                    Image.Width,
                    Image.Height));
        }

        private void OnImageChanged()
        {
            OnContentChanged();
        }

        private Bitmap image;
    }
}
