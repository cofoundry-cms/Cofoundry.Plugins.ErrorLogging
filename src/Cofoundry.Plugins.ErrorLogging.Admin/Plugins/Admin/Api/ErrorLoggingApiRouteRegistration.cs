using Cofoundry.Web;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using Cofoundry.Web.Admin;

namespace Cofoundry.Plugins.ErrorLogging.Admin
{
    public class ErrorLoggingApiRouteRegistration : IOrderedRouteRegistration
    {
        public int Ordering => (int)RouteRegistrationOrdering.Early;

        public void RegisterRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder
                .ForAdminApiController<ErrorsApiController>("plugins/errors")
                .MapGet()
                .MapGetById("{errorId:int}")
                ;
        }
    }
}
