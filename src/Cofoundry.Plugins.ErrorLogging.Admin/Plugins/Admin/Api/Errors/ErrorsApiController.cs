using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.CQS;
using Cofoundry.Web.Admin;
using Cofoundry.Plugins.ErrorLogging.Domain;
using Microsoft.AspNetCore.Mvc;
using Cofoundry.Web;

namespace Cofoundry.Plugins.ErrorLogging.Admin
{
    public class ErrorsApiController : BaseAdminApiController
    {
        private const string ID_ROUTE = "{errorId:int}";

        private readonly IQueryExecutor _queryExecutor;
        private readonly IApiResponseHelper _apiResponseHelper;

        public ErrorsApiController(
            IQueryExecutor queryExecutor,
            IApiResponseHelper apiResponseHelper
            )
        {
            _queryExecutor = queryExecutor;
            _apiResponseHelper = apiResponseHelper;
        }

        public async Task<IActionResult> Get([FromQuery] SearchErrorSummariesQuery query)
        {
            if (query == null) query = new SearchErrorSummariesQuery();

            var results = await _queryExecutor.ExecuteAsync(query);
            return _apiResponseHelper.SimpleQueryResponse(this, results);
        }
        
        public async Task<IActionResult> GetById(int errorId)
        {
            var query = new GetErrorDetailsByIdQuery(errorId);
            var result = await _queryExecutor.ExecuteAsync(query);

            return _apiResponseHelper.SimpleQueryResponse(this, result);
        }
    }
}