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
        static readonly Logger logger = LogManager.GetCurrentClassLogger();
        

        public FileLauncher(IFileSystem fileSystem, IExcelWrapper excelWrapper)
        {
            this.fileSystem = fileSystem;
            this.excelWrapper = excelWrapper;
        }

        public void Launch(string filePath)
        {
            if (filePath.IsNullOrEmpty())
            {
                // todo: display warning to user
                logger.Error("Provided file path is null or empty.");
                return;
            }

            if (!fileSystem.File.Exists(filePath))
            {
                // todo: display warning to user
                logger.Error("File does not exist: " + filePath);
                return;
            }

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
                // todo: display warning to user
                logger.Error(ex, string.Format("Failed to open file: '{0}'", filePath));
            }
        }
    }
}
