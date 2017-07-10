using Cofoundry.Web;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;

namespace Cofoundry.Plugins.ErrorLogging
{
    public class ErrorLoggingMiddlewareStartupTask : IStartupTask
    {
        public int Ordering
        {
            get { return (int)StartupTaskOrdering.Early; }
        }

        public void Run(IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorLoggingMiddleware>();
        }
    }
}
