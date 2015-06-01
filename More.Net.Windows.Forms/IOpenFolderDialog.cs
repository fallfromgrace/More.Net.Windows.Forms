using System;
using System.Windows.Forms;

namespace More.Net.Windows.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOpenFolderDialog : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        String FolderName { get; }

        /// <summary>
        /// 
        /// </summary>
        String InitialDirectory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        String Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        DialogResult ShowDialog(IWin32Window owner);
    }

}
