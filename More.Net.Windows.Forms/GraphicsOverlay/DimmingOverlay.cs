using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace More.Net.Common.Forms.GraphicsOverlay
{
    /// <summary>
    /// Dims the underlying pixels by filling the visibly area with black.  The amount of dimming 
    /// depends on the opacity.
    /// </summary>
    public class DimmingOverlay : Overlay
    {
        public Color Color
        {
            get { return color; }
            set
            {
                if (Color != value)
                {
                    color = value;
                    OnContentChanged();
                }
            }
        }

        public Rectangle Region
        {
            get { return dimmingRegion; }
            set
            {
                if (Region != value)
                {
                    dimmingRegion = value;
                    OnContentChanged();
                }
            }
        }

        public Boolean UseCustomRegion
        {
            get;
            set;
        }

        protected override void OnInitializing()
        {
            base.OnInitializing();
            UseCustomRegion = false;
            Color = Color.Black;
            Opacity = 0.75;
        }

        protected override void OnDraw(Graphics g)
        {
            if (!UseCustomRegion)
                Region = g.VisibleClipBounds.ToRectangle();
            g.FillRectangle(new SolidBrush(Color), Region);
        }

        private Rectangle dimmingRegion;
        private Color color;
    }
}
