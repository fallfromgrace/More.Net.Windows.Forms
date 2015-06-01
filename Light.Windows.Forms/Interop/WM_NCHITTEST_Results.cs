namespace More.Net.Windows.Forms.Interop
{
    /// <summary>
    /// 
    /// </summary>
    public enum WM_NCHITTEST_Results : int
    {
        /// <summary>
        /// HTNOWHERE or Error
        /// </summary>
        HTERROR = -2,

        /// <summary>
        /// In a window currently covered by another window in the same thread (the message will 
        /// be sent to underlying windows in the same thread until one of them returns a code that 
        /// is not HTTRANSPARENT).
        /// </summary>
        HTTRANSPARENT = -1,

        /// <summary>
        /// On the screen background or on a dividing line between windows.
        /// </summary>
        HTNOWHERE = 0,

        /// <summary>
        /// In a client area.
        /// </summary>
        HTCLIENT = 1
    }
}
