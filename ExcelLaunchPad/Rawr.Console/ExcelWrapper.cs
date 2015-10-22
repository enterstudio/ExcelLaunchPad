using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = BitterMinion.Excel9.Interop;

namespace Rawr.LaunchPad.ConsoleApp
{
    public class ExcelWrapper : IExcelWrapper
    {
        public void OpenFile(string filePath, string personalWorkbookPath)
        {
            if (filePath.IsNullOrEmpty())
                throw new ArgumentNullException("filePath");

            Excel.Application excel = null;
            Excel.Workbooks workbooks = null;

            try
            {
                excel = new Excel.Application { Visible = true };
                workbooks = excel.Workbooks;
                workbooks.Open(filePath);

                foreach (Excel.AddIn addin in excel.AddIns)
                {
                    if (addin.Installed)
                    {
                        addin.Installed = false;
                        addin.Installed = true;
                    }

                    Marshal.FinalReleaseComObject(addin);
                }

                if (personalWorkbookPath.IsNullOrEmpty())
                    return;

                workbooks.Open(personalWorkbookPath);
            }
            finally
            {
                if (excel != null)
                    Marshal.FinalReleaseComObject(excel);

                if (workbooks != null)
                    Marshal.FinalReleaseComObject(workbooks);
            }
        }
    }
}
