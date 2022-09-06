using Zip.Core.Vault;

namespace Zip.Api.CustomerProfile.Vault
{
    public class VaultConfigTransformer : IVaultConfigTransformer
    {
        public string TransformKey(string vaultKey)
        {
            if (string.IsNullOrEmpty(vaultKey)) return string.Empty;

            switch (vaultKey.ToLowerInvariant())
            {
                case "customerprofileresourcesdbpassword":
                    return "PostgreSQL:Password";
                case "customerprofileresourcesdbusername":
                    return "PostgreSQL:Username";
                default:
                    return vaultKey;
            }
        }

        public string TransformValue(object vaultSecret)
        {
            return vaultSecret?.ToString();
        }
    }
}