using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentConsole.Library;
using NLog;

namespace Rawr.LaunchPad.Installer
{
    class Program
    {
        static readonly Logger logger = LogManager.GetCurrentClassLogger();

        static void Main()
        {
            var target = new Target();
            var targetPath = target.FullPath();
            if (targetPath == null)
            {
                logger.Error("Install failed: ExcelLaunchPad.exe does not exist in current directory");
                "Whoops!".WriteLine(ConsoleColor.Red, 2);

                ("Both ExcelLaunchPad.exe and ExcelLaunchPadInstaller.exe MUST be placed in the same directory. You can move the directory anywhere you want, " +
                 "as long as both files are in the same folder.").WriteLine(2);

                "Please move the files back into the same folder and try re-running the installer.".WriteLineWait();
                return;
            }

            "***ExcelLaunchPad Installer***".WriteLine(ConsoleColor.Yellow, 2);
            "(1) Install or Update".WriteLine();
            "(2) Uninstall".WriteLine(1);
            var key = "Please press 1 or 2 to continue...".WriteLineWait();

            var validKeys = new List<ConsoleKey> { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.NumPad1, ConsoleKey.NumPad2 };
            while (!validKeys.Contains(key.Key))
                key = "That was not a valid response. Please enter 1 or 2.".WriteLineWait(ConsoleColor.Red);

            switch (key.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Install(targetPath);
                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    UnInstall();
                    break;
            }
        }

        static void Install(string targetPath)
        {
            "Installing...".WriteLine(ConsoleColor.Yellow, 1);

            var regEdit = new RegistryEditor();
            try
            {
                regEdit.AddOrUpdateEntries(targetPath);
                "Done!".WriteLine(ConsoleColor.Yellow, 1);
                "You should now be able to right-click any Excel-compatible files and select 'Open in new window'. Enjoy! :)".WriteLineWait();
            }
            catch (Exception e)
            {
                logger.Error(e, "Installation failed.");
                "Whoops!".WriteLine(ConsoleColor.Red, 1);
                "Sorry, but something unexpected happened. Installation failed. Please check the logs for more details.".WriteLineWait();
            }
        }

        static void UnInstall()
        {
            "Uninstalling...".WriteLine(ConsoleColor.Yellow, 1);

            var regEdit = new RegistryEditor();
            try
            {
                regEdit.RemoveEntries();
                "Done!".WriteLine(ConsoleColor.Yellow, 1);
                "ExcelLaunchPad has been uninstalled. You may now delete all ExcelLaunchPad files from your computer.".WriteLineWait();
            }
            catch (Exception e)
            {
                logger.Error(e, "UnInstall failed.");
                "Whoops!".WriteLine(ConsoleColor.Red, 1);
                "Sorry, but something unexpected happened. ExcelLaunchPad was not removed. Please check the logs for more details.".WriteLineWait();
            }
        }
    }
}