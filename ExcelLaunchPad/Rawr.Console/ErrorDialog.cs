using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timers = System.Timers;
using Timer = System.Timers.Timer;

namespace Rawr.LaunchPad.ConsoleApp
{
    public partial class ErrorDialog : Form
    {
        readonly Timer timer = new Timer(20000);

        public ErrorDialog()
        {
            InitializeComponent();

            timer.AutoReset = false;
            timer.Elapsed += Timer_Elapsed;
            timer.SynchronizingObject = this;
        }

        public string ErrorMessage
        {
            get { return labelErrorMessage.Text; }
            set { labelErrorMessage.Text = "Whoops! I couldn't open that file. :( \r\n\r\n" + value; }
        }

        void Timer_Elapsed(object sender, Timers.ElapsedEventArgs e)
        {
            Application.Exit();
        }

        void ErrorDialog_Shown(object sender, EventArgs e)
        {
            timer.Start();
        }
    }
}
