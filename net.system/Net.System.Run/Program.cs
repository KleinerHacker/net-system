using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Net.System.Management;
using Net.System.Util.Extension;

namespace Net.System.Run
{
    class Program
    {
        static void Main(string[] args)
        {
            WindowManager.MinimizeAll();
            Thread.Sleep(2000);
            WindowManager.RestoreAll();
        }
    }
}
