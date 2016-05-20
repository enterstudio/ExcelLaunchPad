using System;
using System.Collections.Generic;
using System.IO;
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
        readonly IExcelWrapper excelWrapper;

        public FileLauncher(IFileSystem fileSystem, IExcelWrapper excelWrapper)
        {
            this.fileSystem = fileSystem;
            this.excelWrapper = excelWrapper;
        }

        public void Launch(string filePath)
        {
            if (filePath.IsNullOrEmpty())
                throw new InvalidOperationException("Provided file path is null or empty.");

            if (!fileSystem.File.Exists(filePath))
                throw new InvalidOperationException("File does not exist: " + filePath);

            var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Excel\XLStart\");
            var directory = fileSystem.DirectoryInfo.FromDirectoryName(folderPath);

            string personalWorkbookPath = null;
            if (directory.Exists)
            {
                var personalWorkbook = directory.GetFiles().FirstOrDefault(x => x.Name.Contains("personal", StringComparison.OrdinalIgnoreCase));
                if (personalWorkbook != null)
                    personalWorkbookPath = personalWorkbook.FullName;
            }

            try
            {
                excelWrapper.OpenFile(filePath, personalWorkbookPath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to open file: '{filePath}'", ex);
            }
        }
    }
}
