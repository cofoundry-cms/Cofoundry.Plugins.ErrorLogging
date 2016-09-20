using Cofoundry.Web.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cofoundry.Plugins.ErrorLogging.Admin
{
    public class ErrorsRouteLibrary : ModuleRouteLibrary
    {
        #region statics

        public const string RoutePrefix = "errors";

        public static readonly ErrorsRouteLibrary Urls = new ErrorsRouteLibrary();

        public static readonly ModuleJsRouteLibrary Js = new ModuleJsRouteLibrary(Urls);

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
            return CreateAngularRoute();
        }

        public string Details(int id)
        {
            return CreateAngularRoute(id.ToString());
        }

        #endregion
    }
}