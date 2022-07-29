using Cofoundry.Core;
using Cofoundry.Core.AutoUpdate;

namespace Cofoundry.Plugins.ErrorLogging;

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