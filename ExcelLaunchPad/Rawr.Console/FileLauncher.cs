using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rawr.LaunchPad.ConsoleApp
{
    public class FileLauncher : IFileLauncher
    {
        readonly string filePath;
        readonly FileSystem fileSystem;

        public FileLauncher(string filePath, FileSystem fileSystem)
        {
            this.filePath = filePath;
            this.fileSystem = fileSystem;
        }

        public bool FileExists
        {
            get { return fileSystem.File.Exists(filePath); } 
        }

        public void Launch()
        {
            throw new NotImplementedException();
        }
    }
}
