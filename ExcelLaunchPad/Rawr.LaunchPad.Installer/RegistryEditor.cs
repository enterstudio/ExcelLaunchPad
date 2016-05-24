using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using NLog;

namespace Rawr.LaunchPad.Installer
{
    public class RegistryEditor
    {
        static readonly Logger logger = LogManager.GetCurrentClassLogger();

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

        public void AddOrUpdateEntries(string targetPath)
        {
            foreach (var name in keyNames)
            {
                using (var registryKey = Registry.ClassesRoot.OpenSubKey(name, true))
                using (var shell = registryKey?.CreateSubKey("shell"))
                using (var newWindow = shell?.CreateSubKey("Open in new window"))
                using (var command = newWindow?.CreateSubKey("command"))
                {
                    if (command == null)
                        throw new InvalidOperationException($"Unable to set key for '{name}'. Command subkey returned as null. This is likely a permissions issue.");

                    command.SetValue(null, $"{targetPath} -f \"%1\"");
                }
            }
        }

        public void RemoveEntries()
        {
            foreach (var name in keyNames)
            {
                using (var registryKey = Registry.ClassesRoot.OpenSubKey(name, true))
                using (var shell = registryKey?.OpenSubKey("shell", true))
                {
                    if (shell == null)
                        continue;

                    try
                    {
                        shell.DeleteSubKeyTree("Open in new window");
                    }
                    catch (Exception e)
                    {
                        // Assumption: exception == the key simply doesn't exist
                        logger.Error(e, $"Attempt to delete subkey failed for '{name}'");
                    }
                }
            }
        }
    }
}
