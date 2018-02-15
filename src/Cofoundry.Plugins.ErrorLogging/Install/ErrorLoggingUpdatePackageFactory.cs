using System;
using System.Collections.Generic;
using System.Linq;
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

        public override ICollection<string> DependentModules { get; } = new string[] { CofoundryModuleInfo.ModuleIdentifier };
    }
}