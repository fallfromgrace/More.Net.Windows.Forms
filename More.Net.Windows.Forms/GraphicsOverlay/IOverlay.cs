using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace More.Net.Common.Forms.GraphicsOverlay
{
    public interface IOverlay
    {
        String Name { get; }
        Boolean CanDraw { get; }
        Double Opacity { get; }
        event EventHandler CanDrawChanged;
        event EventHandler ContentChanged;
        event EventHandler OpacityChanged;
        void Draw(Graphics g);
    }
}
