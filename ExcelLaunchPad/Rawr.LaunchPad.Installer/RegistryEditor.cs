using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Rawr.LaunchPad.Installer
{
    public class RegistryEditor
    {
        readonly string targetPath;

        readonly List<string> keyNames = new List<string>
        {
            "Excel.CSV",
            "Excel.Sheet.8",
            "Excel.Macrosheet",
            "Excel.SheetBinaryMacroEnabled.12",
            "Excel.Addin",
            "Excel.AddInMacroEnabled",
            "Excel.SheetMacroEnabled.12",
            "Excel.Sheet.12",
            "Excel.Template.8",
        };

        public RegistryEditor(string targetPath)
        {
            this.targetPath = targetPath;
        }

        public void AddEntries()
        {
            foreach (var name in keyNames)
            {
                using (var registryKey = Registry.ClassesRoot.OpenSubKey(name, true))
                {
                    var shell = registryKey.CreateSubKey("shell");
                    var newWindow = shell.CreateSubKey("Open in new window");
                    var command = newWindow.CreateSubKey("command");
                    command.SetValue(null, $"{targetPath} -f \"%1\"");
                }
            }
        }
    }
}
