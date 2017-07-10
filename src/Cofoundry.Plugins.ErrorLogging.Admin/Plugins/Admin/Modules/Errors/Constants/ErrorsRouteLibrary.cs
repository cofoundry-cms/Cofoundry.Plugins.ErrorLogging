using Cofoundry.Web.Admin;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cofoundry.Plugins.ErrorLogging.Admin
{
    public class ErrorsRouteLibrary : ModuleRouteLibrary
    {
        #region statics

        public const string RoutePrefix = "errors";

        #endregion

        #region constructor

        public ErrorsRouteLibrary()
            : base(RoutePrefix, RouteConstants.PluginModuleResourcePathPrefix)
        {
        }

        #endregion

        #region routes

        public string List()
        {
            return AngularRoute();
        }

        public string Details(int id)
        {
            return AngularRoute(id.ToString());
        }

        #endregion
    }
}