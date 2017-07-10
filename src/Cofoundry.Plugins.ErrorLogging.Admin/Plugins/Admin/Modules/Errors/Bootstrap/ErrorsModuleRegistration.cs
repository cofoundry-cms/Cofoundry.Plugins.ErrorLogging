using System;
using System.Collections.Generic;
using System.Linq;
using Cofoundry.Domain;
using Cofoundry.Web.Admin;
using Cofoundry.Plugins.ErrorLogging.Domain;

namespace Cofoundry.Plugins.ErrorLogging.Admin
{
    public class ErrorsModuleRegistration : IPluginAngularModuleRegistration
    {
        public AdminModule GetModule()
        {
            var module = new AdminModule()
            {
                AdminModuleCode = "COFERR",
                Title = "Error Log",
                Description = "View the site error logs.",
                MenuCategory = AdminModuleMenuCategory.Settings,
                PrimaryOrdering = AdminModuleMenuPrimaryOrdering.Tertiary,
                Url = new ErrorsRouteLibrary().List(),
                RestrictedToPermission = new ErrorLogReadPermission()
            };

            return module;
        }

        public string RoutePrefix
        {
            get { return ErrorsRouteLibrary.RoutePrefix; }
        }
    }
}