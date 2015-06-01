using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace More.Net.Common.Forms.GraphicsOverlay
{
    public class MessageOverlay : Overlay
    {
        public String Message
        {
            get { return message; }
            set
            {
                if (Message != value)
                {
                    message = value;
                    OnMessageChanged();
                }
            }
        }

        private void OnMessageChanged()
        {
            OnContentChanged();
        }

        protected override void OnDraw(Graphics g)
        {

        }

        private String message;
    }
}
