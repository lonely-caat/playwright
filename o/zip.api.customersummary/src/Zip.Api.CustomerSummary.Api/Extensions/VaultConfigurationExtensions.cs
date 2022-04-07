using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.Extensions.Configuration;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;
using Zip.Api.CustomerSummary.Api.Vault;

namespace Zip.Api.CustomerSummary.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class VaultConfigurationExtensions
    {
        public static IConfigurationBuilder AddVaultConfiguration(
            this IConfigurationBuilder configurationBuilder,
            string vaultUri,
            string tokenPath,
            string secretLocationPath)
        {
            var vaultToken = File.ReadAllText(tokenPath).Trim();

            configurationBuilder.AddVault(vaultUri, vaultToken, secretLocationPath);

            return configurationBuilder;
        }

        private static IConfigurationBuilder AddVault(
            this IConfigurationBuilder configurationBuilder,
            string vaultUri,
            string vaultToken,
            string secretLocationPath)
        {
            if (configurationBuilder == null)
            {
                throw new ArgumentNullException(nameof(configurationBuilder));
            }

            if (string.IsNullOrWhiteSpace(vaultUri))
            {
                throw new ArgumentException("VaultUri must be a valid URI", nameof(vaultUri));
            }

            if (secretLocationPath == null)
            {
                throw new ArgumentNullException(nameof(secretLocationPath));
            }

            var vaultClientSettings = new VaultClientSettings(vaultUri, new TokenAuthMethodInfo(vaultToken));

            var vaultClient = new VaultClient(vaultClientSettings);

            configurationBuilder.Add(new VaultConfigurationSource
            {
                Client = vaultClient,
                SecretLocationPath = secretLocationPath
            });

            return configurationBuilder;
        }
    }

}
