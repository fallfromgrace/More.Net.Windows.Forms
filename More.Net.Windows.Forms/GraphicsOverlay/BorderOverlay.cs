using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace More.Net.Common.Forms.GraphicsOverlay
{
    /// <summary>
    /// 
    /// </summary>
    public class BorderOverlay : Overlay
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        public Color Color
        {
            get { return color; }
            set
            {
                if (Color != value)
                {
                    color = value;
                    OnColorChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the border thickness.
        /// </summary>
        public Int32 Thickness
        {
            get { return thickness; }
            set
            {
                if (thickness != value)
                {
                    thickness = value;
                    OnThicknessChanged();
                }
            }
        }

        #endregion

        protected override void OnInitializing()
        {
            base.OnInitializing();

            Thickness = 5;
            Color = Color.Black;
        }

        protected override void OnDraw(Graphics g)
        {
            Rectangle border = new Rectangle(
                (Int32)g.VisibleClipBounds.X,
                (Int32)g.VisibleClipBounds.Y,
                (Int32)g.VisibleClipBounds.Width,
                (Int32)g.VisibleClipBounds.Height);
            SolidBrush brush = new SolidBrush(Color);
            Pen pen = new Pen(brush, Thickness);
            g.DrawRectangle(pen, border);
        }

        private void OnColorChanged()
        {
            OnContentChanged();
        }

        private void OnThicknessChanged()
        {
            OnContentChanged();
        }

        private Int32 thickness;
        private Color color;
    }
}
