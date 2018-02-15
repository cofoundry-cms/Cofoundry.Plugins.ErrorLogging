using Cofoundry.Web;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;

namespace Cofoundry.Plugins.ErrorLogging
{
    public class ErrorLoggingMiddlewareStartupTask : IRunAfterStartupConfigurationTask
    {
        public int Ordering
        {
            get { return (int)StartupTaskOrdering.First; }
        }

        public ICollection<Type> RunAfter => new Type[] { typeof(ErrorHandlingMiddlewareConfigurationTask) };

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorLoggingMiddleware>();
        }
    }
}
