using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using WpfWindow = System.Windows.Window;
using FormWindow = System.Windows.Forms.Form;
using Net.System.Native;
using Net.System.Type;

namespace Net.System.Util.Extension
{
    public static class WindowExtensions
    {
        public static ProcessWindow ToProcessWindow(this FormWindow window)
        {
            return new ProcessWindow(window.Handle, Process.GetCurrentProcess());
        }

        public static ProcessWindow ToProcessWindow(this WpfWindow window)
        {
            return new ProcessWindow(new WindowInteropHelper(window).Handle, Process.GetCurrentProcess());
        }
    }
}
