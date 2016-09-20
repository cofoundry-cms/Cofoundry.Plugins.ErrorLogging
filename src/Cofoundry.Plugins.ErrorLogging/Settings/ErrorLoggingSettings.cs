using Cofoundry.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Plugins.ErrorLogging
{
    public class ErrorLoggingSettings : PluginConfigurationSettingsBase
    {
        public string LogToEmailAddress { get; set; }
    }
}
