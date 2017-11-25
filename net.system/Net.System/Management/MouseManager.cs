using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net.System.Native;
using Net.System.Native.Type;

namespace Net.System.Management
{
    /// <summary>
    /// Manager to control mouse
    /// </summary>
    public static class MouseManager
    {
        public static Point GetMousePosition()
        {
            NativePoint point;
            User32.Mouse.GetCursorPos(out point);

            return new Point(point.X, point.Y);
        }

        public static void SetMousePosition(int x, int y)
        {
            User32.Mouse.SetCursorPos(x, y);
        }

        public static void SetMousePosition(Point point)
        {
            SetMousePosition(point.X, point.Y);
        }
    }
}
