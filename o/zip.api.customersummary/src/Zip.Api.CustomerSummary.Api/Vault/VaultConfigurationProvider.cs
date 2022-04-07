using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Extensions.Configuration;
using VaultSharp;

namespace Zip.Api.CustomerSummary.Api.Vault
{
    [ExcludeFromCodeCoverage]
    internal class VaultConfigurationProvider : ConfigurationProvider
    {
        private readonly IVaultClient _client;

        private readonly string _secretPath;

        public VaultConfigurationProvider(IVaultClient client, string secretPath)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _secretPath = secretPath ?? throw new ArgumentNullException(nameof(secretPath));
        }

        public override void Load()
        {
            var configuredTaskAwaitable = _client.V1.Secrets.KeyValue.V2.ReadSecretAsync(_secretPath, mountPoint: "secret").ConfigureAwait(false);
            var secrets = configuredTaskAwaitable.GetAwaiter().GetResult().Data.Data;

            Data = secrets.ToDictionary(x => x.Key, x => x.Value.ToString(), StringComparer.OrdinalIgnoreCase);
        }
    }
}
