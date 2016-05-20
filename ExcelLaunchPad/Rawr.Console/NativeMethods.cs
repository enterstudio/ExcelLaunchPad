using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming

namespace Rawr.LaunchPad.ConsoleApp
{
    class NativeMethods
    {
        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOMOVE = 0x0002;
        const uint TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        public static void SetTopMostWindow(IntPtr handle)
        {
            SetWindowPos(handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }
    }
}
