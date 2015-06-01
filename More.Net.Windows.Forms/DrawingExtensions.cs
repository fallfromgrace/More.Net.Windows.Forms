using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace More.Net.Common.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public static class DrawingExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Rectangle ToRectangle(this RectangleF rectangleF)
        {
            return new Rectangle(
                (Int32)rectangleF.X, (Int32)rectangleF.Y,
                (Int32)rectangleF.Width, (Int32)rectangleF.Height);
        }
    }
}
