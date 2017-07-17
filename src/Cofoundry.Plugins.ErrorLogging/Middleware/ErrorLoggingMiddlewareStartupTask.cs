using Cofoundry.Web;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;

namespace Cofoundry.Plugins.ErrorLogging
{
    public class ErrorLoggingMiddlewareStartupTask : IStartupConfigurationTask
    {
        public int Ordering
        {
            get { return (int)StartupTaskOrdering.First; }
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorLoggingMiddleware>();
        }
    }
}
