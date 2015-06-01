using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace More.Net.Common.Forms.Interop
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    internal enum HRESULT2 : int
    {
        /// <summary>
        /// Success code.
        /// </summary>
        S_OK = 0x00000000,

        /// <summary>
        /// Success code false.
        /// </summary>
        S_FALSE = 0x00000001,

        /// <summary>
        /// The operation completed successfully.
        /// </summary>
        ERROR_SUCCESS = 0x00070000,
    }

    /// <summary>
    /// 
    /// </summary>
    internal static class HRESULTExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Boolean Failed(this HRESULT2 source)
        {
            return source.Value() < 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Int32 Value(this HRESULT2 source)
        {
            return (Int32)source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Boolean Succeeded(this HRESULT2 source)
        {
            return source.Value() >= 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public static void ThrowIfError(this HRESULT2 source)
        {
            if (source.Failed())
                Marshal.ThrowExceptionForHR(source.Value());
        }
    }
}
