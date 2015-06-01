using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace More.Net.Windows.Forms.Interop
{
    /// <summary>
    /// 
    /// </summary>
    internal enum SIGDN : uint
    {
        /// <summary>
        /// 
        /// </summary>
        NORMALDISPLAY = 0,

        /// <summary>
        /// 
        /// </summary>
        PARENTRELATIVEPARSING = 0x80018001,

        /// <summary>
        /// 
        /// </summary>
        PARENTRELATIVEFORADDRESSBAR = 0x8001c001,

        /// <summary>
        /// 
        /// </summary>
        DESKTOPABSOLUTEPARSING = 0x80028000,

        /// <summary>
        /// 
        /// </summary>
        PARENTRELATIVEEDITING = 0x80031001,

        /// <summary>
        /// 
        /// </summary>
        DESKTOPABSOLUTEEDITING = 0x8004c000,

        /// <summary>
        /// 
        /// </summary>
        FILESYSPATH = 0x80058000,

        /// <summary>
        /// 
        /// </summary>
        URL = 0x80068000
    }
}
