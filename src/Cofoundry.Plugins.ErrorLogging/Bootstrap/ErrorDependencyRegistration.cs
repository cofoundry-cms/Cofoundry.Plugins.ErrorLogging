using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofoundry.Core.DependencyInjection;
using Cofoundry.Plugins.ErrorLogging.Domain;
using Cofoundry.Plugins.ErrorLogging.Data;

namespace Cofoundry.Plugins.ErrorLogging.Bootstrap
{
    public class ErrorDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            var overrideOptions = RegistrationOptions.Override(RegistrationOverridePriority.Low);

            container
                .RegisterType<IErrorLoggingService, ErrorLoggingService>(overrideOptions)
                .RegisterType<ErrorLoggingDbContext>()
                ;
        }
    }
}
