using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Abstractions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fclp;
using NLog;
using SimpleInjector;

namespace Rawr.LaunchPad.ConsoleApp
{
    class Program
    {
        static readonly Logger logger = LogManager.GetCurrentClassLogger();

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var parser = new FluentCommandLineParser<AppArguments>();
            parser.Setup(x => x.FilePath)
                .As('f', "filepath")
                .WithDescription("Excel file path")
                .Required();

            ICommandLineParserResult result = parser.Parse(args);
            if (result.HasErrors)
            {
                var errorList = new List<string>();
                foreach (var formattedError in result.Errors.Select(error => $"{error}: '{error.Option.Description}'"))
                {
                    errorList.Add(formattedError);
                    logger.Error(formattedError);
                }

                RunErrorDialog(errorList);
                return;
            }

            var registrations = from type in Assembly.GetExecutingAssembly().GetExportedTypes()
                                where type.GetInterfaces().Any(x => x.Namespace != null && x.Namespace.StartsWith("Rawr"))
                                where type.Namespace != null && !type.Namespace.StartsWith("BitterMinion")
                                select new { Service = type.GetInterfaces().First(), Implementation = type };

            var container = new Container();
            foreach (var reg in registrations)
                container.Register(reg.Service, reg.Implementation, Lifestyle.Transient);

            container.Register<IFileSystem, FileSystem>();
            container.Verify();

            // This is to resolve the following exceptions: "System.Runtime.InteropServices.COMException 
            // (0x80028018): Old format or invalid type library. (Exception from HRESULT: 0x80028018 (TYPE_E_INVDATAREAD))"
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            var launcher = container.GetInstance<IFileLauncher>();

            try
            {
                launcher.Launch(parser.Object.FilePath);
            }
            catch (Exception exception)
            {
                logger.Error(exception.InnerException, exception.Message);
                RunErrorDialog(exception.Message);
            }
        }

        static void RunErrorDialog(IEnumerable<string> errors)
        {
            var errorText = string.Join("\r\n", errors);
            RunErrorDialog(errorText);
        }

        static void RunErrorDialog(string error)
        {
            Application.Run(new ErrorDialog { ErrorMessage = error });
        }
    }
}
