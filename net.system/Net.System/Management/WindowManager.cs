using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net.System.Type;
using Net.System.Util.Extension;

namespace Net.System.Management
{
    /// <summary>
    /// Represent a window manager
    /// </summary>
    public static class WindowManager
    {
        /// <summary>
        /// Get front window. It is the current active window.
        /// </summary>
        /// <returns></returns>
        public static ProcessWindow GetFrontWindow()
        {
            return Process.GetProcesses()
                .FirstOrDefault(process => process.IsInFront())?
                .GetMainWindow();
        }

        /// <summary>
        /// Get all main windows from all windowed processes.
        /// </summary>
        /// <returns></returns>
        public static ProcessWindow[] GetMainWindows()
        {
            return Process.GetProcesses()
                .Where(process => process.HasWindow())
                .Select(process => process.GetMainWindow())
                .ToArray();
        }

        /// <summary>
        /// Returns all windows of all windowed processes
        /// </summary>
        /// <returns></returns>
        public static IDictionary<ProcessKey, ProcessWindow[]> GetAllWindows()
        {
            var processes = Process.GetProcesses()
                .Where(process => process.HasWindow());

            var dict = new Dictionary<ProcessKey, ProcessWindow[]>();
            foreach (var process in processes)
            {
                dict.Add(new ProcessKey(process), process.GetAllWindows());
            }

            return dict;
        }

        #region Helper Classes

        public sealed class ProcessKey
        {
            public Process Process { get; }

            private readonly int _id;

            internal ProcessKey(Process process)
            {
                Process = process;
                _id = process.Id;
            }

            #region Equals / Hashcode

            private bool Equals(ProcessKey other)
            {
                return _id == other._id;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj is ProcessKey && Equals((ProcessKey) obj);
            }

            public override int GetHashCode()
            {
                return _id;
            }

            #endregion

            public override string ToString()
            {
                return Process.ToString();
            }
        }

        #endregion
    }
}