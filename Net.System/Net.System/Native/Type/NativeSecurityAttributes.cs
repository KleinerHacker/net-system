using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Net.System.Native.Type
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NativeSecurityAttributes
    {
        public int Length { get; set; }
        public IntPtr SecurityDesktop { get; set; }
        public int InheritHandle { get; set; }

        public NativeSecurityAttributes(int length, IntPtr securityDesktop, int inheritHandle)
        {
            Length = length;
            SecurityDesktop = securityDesktop;
            InheritHandle = inheritHandle;
        }
    }
}
