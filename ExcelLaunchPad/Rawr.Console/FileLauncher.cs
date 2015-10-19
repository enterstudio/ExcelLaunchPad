using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Rawr.LaunchPad.ConsoleApp
{
    public class FileLauncher : IFileLauncher
    {
        readonly IFileSystem fileSystem;
        static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public FileLauncher(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public void Launch(string filePath)
        {
            logger.Info("About to launch: " + filePath);
        }
    }
}
