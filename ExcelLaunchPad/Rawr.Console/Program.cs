using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Reflection;
using System.Text;
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
                                select new { Service = type.GetInterfaces().First(), Implementation = type };

            var container = new Container();
            foreach (var reg in registrations)
                container.Register(reg.Service, reg.Implementation, Lifestyle.Transient);

            container.Register<IFileSystem, FileSystem>();
            container.Verify();

            var launcher = container.GetInstance<IFileLauncher>();
            launcher.Launch(parser.Object.FilePath);
        }
    }
}
