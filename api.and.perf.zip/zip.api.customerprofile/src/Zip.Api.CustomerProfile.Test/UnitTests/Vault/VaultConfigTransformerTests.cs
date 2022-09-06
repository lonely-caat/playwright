using FluentAssertions;
using Xunit;
using Zip.Api.CustomerProfile.Vault;

namespace Zip.Api.CustomerProfile.Test.UnitTests.Vault
{
    public class VaultConfigTransformerTests
    {
        private readonly VaultConfigTransformer _vaultConfigTransformer;

        public VaultConfigTransformerTests()
        {
            _vaultConfigTransformer = new VaultConfigTransformer();
        }

        [Fact]
        public void TransformKey_should_return_empty_string_when_vaultKey_is_null()
        {
            // Arrange
            string vaultKey = null;

            // Act
            var transformedKey = _vaultConfigTransformer.TransformKey(vaultKey);

            // Assert
            transformedKey.Should().BeEmpty();
        }

        [Fact]
        public void TransformKey_should_return_postgres_username_when_vaultKey_is_dbuser()
        {
            // Arrange
            const string vaultKey = "customerprofileresourcesdbusername";
            const string transformedUsernameVaultKey = "PostgreSQL:Username";

            // Act
            var transformedKey = _vaultConfigTransformer.TransformKey(vaultKey);

            // Assert
            transformedKey.Should().Be(transformedUsernameVaultKey);
        }

        [Fact]
        public void TransformKey_should_return_postgres_password_when_vaultKey_is_dbpassword()
        {
            // Arrange
            const string vaultKey = "customerprofileresourcesdbpassword";
            const string transformedPasswordVaultKey = "PostgreSQL:Password";

            // Act
            var transformedKey = _vaultConfigTransformer.TransformKey(vaultKey);

            // Assert
            transformedKey.Should().Be(transformedPasswordVaultKey);
        }

        [Fact]
        public void TransformKey_should_return_empty_string_password_when_vaultKey_is_neither_dbuser_or_dbpassword()
        {
            // Arrange
            const string vaultKey = "test vaultKey";

            // Act
            var transformedKey = _vaultConfigTransformer.TransformKey(vaultKey);

            // Assert
            transformedKey.Should().Be(vaultKey);
        }

        [Fact]
        public void TransformValue_should_return_null_if_vaultSecret_is_null()
        {
            // Arrange
            string vaultSecret = null;

            // Act
            var transformedValue = _vaultConfigTransformer.TransformValue(vaultSecret);

            // Assert
            transformedValue.Should().BeNull();
        }

        [Fact]
        public void TransformValue_should_return_valid_string_if_vaultSecret_is_not_null()
        {
            // Arrange
            const string vaultSecret = "test vaultSecret";

            // Act
            var transformedValue = _vaultConfigTransformer.TransformValue(vaultSecret);

            // Assert
            transformedValue.Should().Be(vaultSecret);
        }
    }
}