using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Net.System.Native.Type
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NativePoint
    {
        public int X { get; }
        public int Y { get; }

        public NativePoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
