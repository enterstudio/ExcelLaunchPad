using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rawr.LaunchPad.Installer
{
    public class ConfigSettings : IConfigSettings
    {
        public string UpdateUrl => ConfigurationManager.AppSettings["RegistryKeys"];
    }
}
