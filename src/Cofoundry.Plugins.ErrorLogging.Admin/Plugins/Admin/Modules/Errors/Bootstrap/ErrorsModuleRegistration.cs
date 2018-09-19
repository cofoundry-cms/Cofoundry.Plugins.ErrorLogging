﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cofoundry.Domain;
using Cofoundry.Web.Admin;
using Cofoundry.Plugins.ErrorLogging.Domain;

namespace Cofoundry.Plugins.ErrorLogging.Admin
{
    public class ErrorsModuleRegistration : IPluginAngularModuleRegistration
    {
        private readonly AdminSettings _adminSettings;

        public ErrorsModuleRegistration(AdminSettings adminSettings)
        {
            _adminSettings = adminSettings;
        }

        public AdminModule GetModule()
        {
            var module = new AdminModule()
            {
                AdminModuleCode = "COFERR",
                Title = "Error Log",
                Description = "View the site error logs.",
                MenuCategory = AdminModuleMenuCategory.Settings,
                PrimaryOrdering = AdminModuleMenuPrimaryOrdering.Tertiary,
                Url = new ErrorsRouteLibrary(_adminSettings).List(),
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