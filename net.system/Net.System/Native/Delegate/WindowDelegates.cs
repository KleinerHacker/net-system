using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.System.Native.Delegate
{
    /// <summary>
    /// Callback for <see cref="User32.Window.EnumChildWindows"/> to enumerate over all child windows
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public delegate bool EnumWindowProc(IntPtr hwnd, IntPtr parameter);
}
