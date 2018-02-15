using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.CQS;

namespace Cofoundry.Plugins.ErrorLogging.Domain
{
    public class GetErrorDetailsByIdQuery : IQuery<ErrorDetails>
    {
        public GetErrorDetailsByIdQuery() { }

        public GetErrorDetailsByIdQuery(int errorId)
        {
            ErrorId = errorId;
        }

        public int ErrorId { get; set; }
    }
}
