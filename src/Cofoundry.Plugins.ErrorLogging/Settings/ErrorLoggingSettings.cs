using Cofoundry.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Plugins.ErrorLogging
{
    /// <summary>
    /// Configuration settings for the Cofoundry.Plugins.ErrorLogging
    /// </summary>
    public class ErrorLoggingSettings : PluginConfigurationSettingsBase
    {
        /// <summary>
        /// If set, an email notification will be sent to this address
        /// every time an error occurs.
        /// </summary>
        public string LogToEmailAddress { get; set; }
    }
}
