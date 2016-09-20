using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cofoundry.Core.AutoUpdate;
using Cofoundry.Core;

namespace Cofoundry.Plugins.ErrorLogging
{
    public class ErrorLoggingUpdatePackageFactory : BaseDbOnlyUpdatePackageFactory
    {
        public override string ModuleIdentifier
        {
            get
            {
                return ErrorLoggingModuleInfo.ModuleIdentifier;
            }
        }

        public override IEnumerable<string> DependentModules
        {
            get
            {
                yield return CofoundryModuleInfo.ModuleIdentifier;
            }
        }
    }
}