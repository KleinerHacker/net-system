using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Net.System.Native;
using Net.System.Native.Type;
using Net.System.Util.Extension;

namespace Net.System.Type
{
    /// <summary>
    /// Represent a window of a process
    /// </summary>
    public sealed class ProcessWindow
    {
        #region Basic Properties

        /// <summary>
        /// Internal handle
        /// </summary>
        internal IntPtr WindowHandle { get; }

        /// <summary>
        /// Parent process of this window
        /// </summary>
        public Process Process { get; }

        #endregion

        public string Title => Process.MainWindowTitle;

        public Rectangle Rectangle
        {
            get
            {
                NativeRect rect;
                User32.Window.GetWindowRect(WindowHandle, out rect);
                
                return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            }
        }
        public Point Location => Rectangle.Location;
        public Size Size => Rectangle.Size;
        public int Width => Rectangle.Width;
        public int Height => Rectangle.Height;
        public int Right => Rectangle.Right;
        public int Bottom => Rectangle.Bottom;
        public int Left => Rectangle.Left;
        public int Top => Rectangle.Top;
        public ProcessWindowState State
        {
            get
            {
                var placement = new NativeWindowPlacement();
                placement.Length = Marshal.SizeOf(placement);
                User32.Window.GetWindowPlacement(WindowHandle, ref placement);

                return (ProcessWindowState) placement.ShowCmd;
            }
        }

        public bool IsInFront => User32.Window.GetForegroundWindow() == WindowHandle;

        public bool IsEnabled
        {
            get { return User32.Window.IsWindowEnabled(WindowHandle); }
            set { User32.Window.EnableWindow(WindowHandle, value); }
        }

        internal ProcessWindow(IntPtr windowHandle, Process process)
        {
            WindowHandle = windowHandle;
            Process = process;
        }

        /// <summary>
        /// Show this window if it is minimized
        /// </summary>
        public void Show()
        {
            if (Process.HasExited)
                throw new InvalidOperationException("Process with id " + Process.Id + " has died");
            if (!Process.HasWindow())
                throw new InvalidOperationException("Process with id " + Process.Id + " has no window.");

            User32.Window.ShowWindow(Process.MainWindowHandle, User32.Window.WindowCmd.Restore);
        }

        /// <summary>
        /// Hide this window with its process. <b>The process will be hide with this action!</b>
        /// </summary>
        public void Hide()
        {
            if (Process.HasExited)
                throw new InvalidOperationException("Process with id " + Process.Id + " has died");
            if (!Process.HasWindow())
                throw new InvalidOperationException("Process with id " + Process.Id + " has no window.");

            User32.Window.ShowWindow(Process.MainWindowHandle, User32.Window.WindowCmd.Hide);
        }

        /// <summary>
        /// Minimize this window
        /// </summary>
        public void Minimize()
        {
            if (Process.HasExited)
                throw new InvalidOperationException("Process with id " + Process.Id + " has died");
            if (!Process.HasWindow())
                throw new InvalidOperationException("Process with id " + Process.Id + " has no window.");

            User32.Window.ShowWindow(Process.MainWindowHandle, User32.Window.WindowCmd.Minimize);
        }

        /// <summary>
        /// Maximize this window
        /// </summary>
        public void Maximize()
        {
            if (Process.HasExited)
                throw new InvalidOperationException("Process with id " + Process.Id + " has died");
            if (!Process.HasWindow())
                throw new InvalidOperationException("Process with id " + Process.Id + " has no window.");

            User32.Window.ShowWindow(Process.MainWindowHandle, User32.Window.WindowCmd.Maximize);
        }

        /// <summary>
        /// Bring this window to front (activate it)
        /// </summary>
        public void BringToFront()
        {
            if (Process.HasExited)
                throw new InvalidOperationException("Process with id " + Process.Id + " has died");
            if (!Process.HasWindow())
                throw new InvalidOperationException("Process with id " + Process.Id + " has no window.");

            User32.Window.SetForegroundWindow(Process.MainWindowHandle);
        }

        /// <summary>
        /// Switch to this window
        /// </summary>
        public void SwitchToThis()
        {
            if (Process.HasExited)
                throw new InvalidOperationException("Process with id " + Process.Id + " has died");
            if (!Process.HasWindow())
                throw new InvalidOperationException("Process with id " + Process.Id + " has no window.");

            User32.Window.SwitchToThisWindow(Process.MainWindowHandle, true);
        }

        /// <summary>
        /// Close this window
        /// </summary>
        public void Close()
        {
            Process.CloseMainWindow();
        }
    }

    public enum ProcessWindowState : uint
    {
        Hide = 0,
        Normal = 1,
        Minimized = 2,
        Maximized = 3,
        NoActive = 4
    }
}