using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.System.Management;

namespace Net.System.Test
{
    [TestClass]
    public class MouseTest
    {
        [TestMethod]
        public void TestMouseCursorSet()
        {
            Console.WriteLine(MouseManager.GetMousePosition());
            MouseManager.SetMousePosition(0, 0);
            Console.WriteLine(MouseManager.GetMousePosition());
        }
    }
}
