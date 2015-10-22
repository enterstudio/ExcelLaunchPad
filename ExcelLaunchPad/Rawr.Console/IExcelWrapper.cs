using Excel = BitterMinion.Excel9.Interop;

namespace Rawr.LaunchPad.ConsoleApp
{
    public interface IExcelWrapper 
    {
        void OpenFile(string filePath, string personalWorkbookPath);
    }
}