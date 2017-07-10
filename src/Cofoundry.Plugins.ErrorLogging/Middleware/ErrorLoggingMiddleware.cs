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
        private readonly IErrorLoggingService _errorLoggingService;
        private readonly ILogger<ErrorLoggingMiddleware> _logger;

        public ErrorLoggingMiddleware(
            RequestDelegate next,
            ILogger<ErrorLoggingMiddleware> logger,
             IErrorLoggingService errorLoggingService
            )
        {
            _next = next;
            _logger = logger;
            _errorLoggingService = errorLoggingService;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                try
                {
                    await _errorLoggingService.LogAsync(ex);
                }
                catch (Exception loggingEx)
                {
                    // The original exception should still be logged by the outer handler, here we
                    // just log loggingEx
                    var msg = "An error occured logging exception {0} using handler type {1}";
                    _logger.LogError(0, loggingEx, msg, ex.GetType().FullName, _errorLoggingService.GetType().FullName);
                }

                throw;
            }
        }
    }
}
