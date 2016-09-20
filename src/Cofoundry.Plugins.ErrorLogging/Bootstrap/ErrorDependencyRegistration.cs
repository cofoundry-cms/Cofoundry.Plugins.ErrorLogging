using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofoundry.Core.DependencyInjection;
using Cofoundry.Core.ErrorLogging;
using Cofoundry.Plugins.ErrorLogging.Domain;
using Cofoundry.Domain.Data;
using Cofoundry.Plugins.ErrorLogging.Data;
using Cofoundry.Core.Configuration;

namespace Cofoundry.Plugins.ErrorLogging.Bootstrap
{
    public class ErrorDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            var overrideOptions = new RegistrationOptions()
            {
                ReplaceExisting = true,
                RegistrationOverridePriority = (int)RegistrationOverridePriority.Low
            };

            container
                .RegisterType<IErrorLoggingService, ErrorLoggingService>(overrideOptions)
                .RegisterDatabase<ErrorLoggingDbContext>()
                .RegisterFactory<ErrorLoggingSettings, ConfigurationSettingsFactory<ErrorLoggingSettings>>()
                ;
        }
    }
}
