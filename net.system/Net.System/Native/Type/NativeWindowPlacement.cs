using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Net.System.Native.Type
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NativeWindowPlacement
    {
        public int Length { get; set; }
        public int Flags { get; set; }
        public User32.Window.WindowCmd ShowCmd { get; set; }
        public Point MinPosition { get; set; }
        public Point MaxPosition { get; set; }
        public Rectangle NormalPosition { get; set; }

        public NativeWindowPlacement(int length, int flags, User32.Window.WindowCmd showCmd, Point minPosition, Point maxPosition, Rectangle normalPosition)
        {
            Length = length;
            Flags = flags;
            ShowCmd = showCmd;
            MinPosition = minPosition;
            MaxPosition = maxPosition;
            NormalPosition = normalPosition;
        }
    }
}
