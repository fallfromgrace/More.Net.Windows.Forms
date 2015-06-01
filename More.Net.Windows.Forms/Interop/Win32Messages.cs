namespace More.Net.Windows.Forms.Interop
{
    /// <summary>
    /// List of Win32 messages.
    /// </summary>
    public enum Win32Messages
    {
        /// <summary>
        /// Sent to a window in order to determine what part of the window corresponds to a 
        /// particular screen coordinate. This can happen, for example, when the cursor moves, 
        /// when a mouse button is pressed or released, or in response to a call to a function 
        /// such as WindowFromPoint. If the mouse is not captured, the message is sent to the 
        /// window beneath the cursor. Otherwise, the message is sent to the window that has 
        /// captured the mouse.
        /// </summary>
        WM_NCHITTEST = 0x0084
    }
}
