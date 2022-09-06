using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Zip.Api.CustomerSummary.Api.Chaos
{
    [ExcludeFromCodeCoverage]
    public class AppChaosSettings
    {
        public List<OperationChaosSetting> OperationChaosSettings { get; }

        public OperationChaosSetting GetSettingsFor(string operationKey) => OperationChaosSettings?.SingleOrDefault(i => i.OperationKey == operationKey);
    }
}
