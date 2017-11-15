using Cofoundry.Plugins.ErrorLogging.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Plugins.ErrorLogging
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorLoggingMiddleware(
            RequestDelegate next
            )
        {
            _next = next;
        }

        public async Task Invoke(
            HttpContext context,
            ILogger<ErrorLoggingMiddleware> logger,
            IErrorLoggingService errorLoggingService
            )
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                try
                {
                    await errorLoggingService.LogAsync(ex);
                }
                catch (Exception loggingEx)
                {
                    // The original exception should still be logged by the outer handler, here we
                    // just log loggingEx
                    var msg = "An error occured logging exception {0} using handler type {1}";
                    logger.LogError(0, loggingEx, msg, ex.GetType().FullName, errorLoggingService.GetType().FullName);
                }

                throw;
            }
        }
    }
}
