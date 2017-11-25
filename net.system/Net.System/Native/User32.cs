using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Net.System.Native.Delegate;
using Net.System.Native.Type;

namespace Net.System.Native
{
    /// <summary>
    /// Native access to user32.dll
    /// </summary>
    public static class User32
    {
        /// <summary>
        /// Default User32 DLL name
        /// </summary>
        public const string DllName = "user32.dll";

        /// <summary>
        /// Special window functions
        /// </summary>
        public static class Window
        {
            public const uint WindowCmdHide = 0;
            public const uint WindowCmdShowMinimize = 2;
            public const uint WindowCmdShowMaximize = 3;
            public const uint WindowCmdMinimize = 6;
            public const uint WindowCmdMaximize = 3;
            public const uint WindowCmdRestore = 9;
            

            [DllImport(DllName)]
            public static extern bool ShowWindow(IntPtr intPtr, uint cmd);

            [DllImport(DllName)]
            public static extern bool SetForegroundWindow(IntPtr intPtr);

            [DllImport(DllName)]
            public static extern IntPtr GetForegroundWindow();

            [DllImport(DllName)]
            public static extern uint GetWindowThreadProcessId(IntPtr hwnd, out int processId);

            [DllImport(DllName)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool EnumChildWindows(IntPtr hwnd, EnumWindowProc callback, IntPtr i);

            [DllImport(DllName)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool EnumWindows(EnumWindowProc callback, IntPtr i);

            [DllImport(DllName)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetWindowRect(IntPtr hwnd, out NativeRect rect);

            [DllImport(DllName)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetWindowPlacement(IntPtr hwnd, ref NativeWindowPlacement placement);
        }

        /// <summary>
        /// Special mouse functions
        /// </summary>
        public static class Mouse
        {
            [DllImport(DllName)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetCursorPos(int x, int y);

            [DllImport(DllName)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetCursorPos(out NativePoint point);
        }
    }
}
