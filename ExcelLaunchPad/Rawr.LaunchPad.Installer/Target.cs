using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rawr.LaunchPad.Installer
{
    public class Target
    {
        public string FullPath()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "ExcelLaunchPad.exe");
            return File.Exists(path) ? path : null;
        }
    }
}
