using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace More.Net.Common.Forms.GraphicsOverlay
{
    /// <summary>
    /// Paints an animated image, such as a *.gif.
    /// </summary>
    public class AnimatedOverlay : Overlay//, IDisposable
    {
        public Bitmap AnimatedImage
        {
            get { return animatedImage; }
            set
            {
                if (!ImageAnimator.CanAnimate(value))
                    throw new ArgumentException("Image does not support animation.");
                if (AnimatedImage != value)
                {
                    animatedImage = value;
                    OnAnimatedImageChanged();
                }
            }
        }

        //public void Dispose()
        //{
        //    Dispose(true);
        //}

        private void OnAnimatedImageChanged()
        {
            OnContentChanged();
        }

        protected override void OnDraw(Graphics g)
        {
            ImageAnimator.UpdateFrames(animatedImage);

            g.DrawImageUnscaledAndClipped(
                animatedImage,
                new Rectangle(
                    (Int32)g.VisibleClipBounds.Width / 2 - animatedImage.Width / 2,
                    (Int32)g.VisibleClipBounds.Height / 2 - animatedImage.Height / 2,
                    animatedImage.Width,
                    animatedImage.Height));
        }

        protected override void OnCanDrawChanged()
        {
            if (CanDraw)
                ImageAnimator.Animate(animatedImage, Loader_FrameChanged);
            else
                ImageAnimator.StopAnimate(animatedImage, Loader_FrameChanged);

            base.OnCanDrawChanged();
        }

        //protected virtual void Dispose(Boolean disposing)
        //{
        //    if (disposing)
        //    {
        //        if (AnimatedImage != null)
        //        {
        //            AnimatedImage.Dispose();
        //            AnimatedImage = null;
        //        }
        //    }
        //}

        private void Loader_FrameChanged(Object sender, EventArgs e)
        {
            OnContentChanged();
        }

        private Bitmap animatedImage;
    }
}
