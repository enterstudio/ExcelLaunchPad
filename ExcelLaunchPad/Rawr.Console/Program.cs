using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Abstractions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fclp;
using NLog;
using SimpleInjector;

namespace Rawr.LaunchPad.ConsoleApp
{
    class Program
    {
        static readonly Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            var parser = new FluentCommandLineParser<AppArguments>();
            parser.Setup(x => x.FilePath)
                .As('f', "filepath")
                .WithDescription("Excel file path")
                .Required();

            ICommandLineParserResult result = parser.Parse(args);
            if (result.HasErrors)
            {
                foreach (ICommandLineParserError error in result.Errors)
                    logger.Error("{0}: '{1}'", error, error.Option.Description);

                return;
            }

            var registrations = from type in Assembly.GetExecutingAssembly().GetExportedTypes()
                                where type.GetInterfaces().Any()
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
            launcher.Launch(parser.Object.FilePath);
        }
    }
}
