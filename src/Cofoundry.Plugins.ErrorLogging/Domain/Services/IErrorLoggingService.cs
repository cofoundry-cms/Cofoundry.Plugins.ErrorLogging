using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Plugins.ErrorLogging.Domain
{
    public interface IErrorLoggingService
    {
        Task LogAsync(Exception ex);
    }
}
