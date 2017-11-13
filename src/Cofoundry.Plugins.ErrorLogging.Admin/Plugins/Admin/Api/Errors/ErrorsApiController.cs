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
    [Route(RouteConstants.PluginApiRoutePrefix + "/Errors")]
    public class ErrorsApiController : BaseAdminApiController
    {
        #region private member variables

        private const string ID_ROUTE = "{errorId:int}";

        private readonly IQueryExecutor _queryExecutor;
        private readonly ApiResponseHelper _apiResponseHelper;

        #endregion

        #region constructor

        public ErrorsApiController(
            IQueryExecutor queryExecutor,
            ApiResponseHelper apiResponseHelper
            )
        {
            _queryExecutor = queryExecutor;
            _apiResponseHelper = apiResponseHelper;
        }

        #endregion

        #region routes

        #region queries

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchErrorSummariesQuery query)
        {
            if (query == null) query = new SearchErrorSummariesQuery();

            var results = await _queryExecutor.ExecuteAsync(query);
            return _apiResponseHelper.SimpleQueryResponse(this, results);
        }
        
        [HttpGet(ID_ROUTE)]
        public async Task<IActionResult> Get(int errorId)
        {
            var result = await _queryExecutor.GetByIdAsync<ErrorDetails>(errorId);
            return _apiResponseHelper.SimpleQueryResponse(this, result);
        }

        #endregion

        #endregion
    }
}