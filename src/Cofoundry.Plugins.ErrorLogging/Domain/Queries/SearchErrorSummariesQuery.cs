using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain;

namespace Cofoundry.Plugins.ErrorLogging.Domain
{
    public class SearchErrorSummariesQuery : SimplePageableQuery, IQuery<PagedQueryResult<ErrorSummary>>
    {
        public string Text { get; set; }
    }
}
