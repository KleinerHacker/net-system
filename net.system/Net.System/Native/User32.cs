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
            public enum WindowCmd : uint
            {
                Hide = 0,
                ShowMinimize = 2,
                ShowMaximize = 3,
                Minimize = 6,
                Maximize = 3,
                Restore = 9,
            }

            public enum AccessRights : uint
            {
                None = 0,
                ReadObjects = 0x0001,
                CreateWindow = 0x0002,
                CreateMenu = 0x0004,
                HookControl = 0x0008,
                JurnalRecord = 0x0010,
                JurnalPlayback = 0x0020,
                Enumerate = 0x0040,
                WriteObjects = 0x0080,
                SwitchDesktop = 0x0100,

                All = (ReadObjects | CreateWindow | CreateMenu | HookControl | JurnalRecord | JurnalPlayback | Enumerate | WriteObjects | SwitchDesktop)
            }

            [DllImport(DllName)]
            public static extern IntPtr FindWindow(string className, string windowName);

            [DllImport(DllName)]
            public static extern bool ShowWindow(IntPtr intPtr, WindowCmd cmd);

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

            [DllImport(DllName)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool IsWindowEnabled(IntPtr hwnd);

            [DllImport(DllName)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool EnableWindow(IntPtr hwnd, bool enable);

            [DllImport(DllName)]
            public static extern void SwitchToThisWindow(IntPtr hwnd, bool altTab);

            [DllImport(DllName)]
            public static extern IntPtr GetDesktopWindow();

            [DllImport(DllName)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SwitchDesktop(IntPtr hwnd);

            [DllImport(DllName)]
            public static extern IntPtr CreateDesktop(
                [MarshalAs(UnmanagedType.LPWStr)] string name,
                [MarshalAs(UnmanagedType.LPWStr)] string device,
                [MarshalAs(UnmanagedType.LPWStr)] string devMode,
                [MarshalAs(UnmanagedType.U4)] int flags,
                [MarshalAs(UnmanagedType.U4)] AccessRights desiredAccess,
                [In] ref NativeSecurityAttributes lpsa
            );
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

        public enum Messages : int
        {
            WindowCommand = 0x111
        }

        [DllImport(DllName)]
        public static extern IntPtr SendMessage(IntPtr hwnd, Messages msg, IntPtr wParam, IntPtr lParam);
    }
}