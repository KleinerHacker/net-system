using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.System.Management;

namespace Net.System.Test
{
    [TestClass]
    public class ProcessWindowTest
    {
        [TestMethod]
        public void WindowDataTest()
        {
            var processWindows = WindowManager.GetAllWindows();
            foreach (var key in processWindows.Keys)
            {
                Console.WriteLine(key.Process.ProcessName);
                foreach (var window in processWindows[key])
                {
                    Console.WriteLine("\t" + window.Title);
                    Console.WriteLine("\t" + window.Rectangle);
                    Console.WriteLine("\t" + window.State);
                }
            }
        }
    }
}
