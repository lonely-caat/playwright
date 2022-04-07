using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using VaultSharp;

namespace Zip.Api.CustomerSummary.Api.Vault
{
    [ExcludeFromCodeCoverage]
    internal class VaultConfigurationSource : IConfigurationSource
    {
        public IVaultClient Client { get; set; }

        public string SecretLocationPath { get; set; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new VaultConfigurationProvider(Client, SecretLocationPath);
        }
    }
}
