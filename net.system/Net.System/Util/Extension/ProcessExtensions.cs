using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Net.System.Native;
using Net.System.Native.Delegate;
using Net.System.Type;

namespace Net.System.Util.Extension
{
    /// <summary>
    /// Process Extensions
    /// </summary>
    public static class ProcessExtensions
    {
        /// <summary>
        /// Returns the process icon
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public static Icon GetProcessIcon(this Process process)
        {
            return Icon.ExtractAssociatedIcon(process.MainModule.FileName);
        }

        /// <summary>
        /// TRUE if process has a window, otherwise FALSE
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public static bool HasWindow(this Process process)
        {
            try
            {
                return process.MainWindowHandle != IntPtr.Zero;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// TRUE if process window is in front now, otherwise FALSE
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public static bool IsInFront(this Process process)
        {
            IntPtr hwnd;
            return IsInFront(process, out hwnd);
        }

        /// <summary>
        /// TRUE if process window is in front now, otherwise FALSE
        /// </summary>
        /// <param name="process"></param>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static bool IsInFront(this Process process, out IntPtr hwnd)
        {
            hwnd = IntPtr.Zero;

            var foregroundWindow = User32.Window.GetForegroundWindow();
            if (foregroundWindow == IntPtr.Zero)
                return false;

            int id;
            User32.Window.GetWindowThreadProcessId(foregroundWindow, out id);

            var isInFront = process.Id == id;
            if (isInFront)
            {
                hwnd = foregroundWindow;
            }

            return isInFront;
        }

        /// <summary>
        /// Returns the main window of this process to control it
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public static ProcessWindow GetMainWindow(this Process process)
        {
            if (process.HasExited)
                throw new InvalidOperationException("Process with id " + process.Id + " has died");
            if (!process.HasWindow())
                throw new InvalidOperationException("Process with id " + process.Id + " has no window.");

            try
            {
                return new ProcessWindow(process.MainWindowHandle, process);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Process with id " + process.Id + " cannot get window.", e);
            }
        }

        public static ProcessWindow[] GetAllWindows(this Process process)
        {
            if (process.HasExited)
                throw new InvalidOperationException("Process with id " + process.Id + " has died");
            if (!process.HasWindow())
                throw new InvalidOperationException("Process with id " + process.Id + " has no window.");

            var windowHandles = new List<IntPtr>();
            var listHandle = GCHandle.Alloc(windowHandles);
            try
            {
                EnumWindowProc callback = (hwnd, parameter) =>
                {
                    windowHandles.Add(hwnd);
                    return true;
                };
                User32.Window.EnumChildWindows(process.MainWindowHandle, callback, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }

            windowHandles.Insert(0, process.MainWindowHandle);

            return windowHandles
                .Select(ptr => new ProcessWindow(ptr, process))
                .ToArray();
        }
    }
}