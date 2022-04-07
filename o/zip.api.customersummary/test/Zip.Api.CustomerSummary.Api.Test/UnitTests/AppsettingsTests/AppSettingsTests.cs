using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Configuration.EmailSettings;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.AppsettingsTests
{
    public class AppSettingsTests
    {
        private readonly IConfigurationRoot _configurationRoot;

        private readonly Fixture _fixture;
        
        public AppSettingsTests()
        {           
            _configurationRoot = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json")
                   .Build();

            _fixture = new Fixture();
        }

        [Fact]
        public void When_Configure_EmailSettings_Should_Load_ResetPassword_Template_Correctly()
        {
            // Arrange
            var expected = new EmailTemplate
            { 
                Id = "d-20f16c56cae046059ae1a4098b982b52",
                SenderName = "Zip Co",
                SenderEmailAddress = "hello@send.zip.co",
            };
            
            var configuration = _configurationRoot.GetSection(nameof(EmailSettings));
            
            // Action
            var emailSettings = new EmailSettings();
            configuration.Bind(emailSettings);

            // Assert
            emailSettings.ApiKey.Should().NotBeNullOrEmpty();

            var actual = emailSettings.ResetPasswordEmail;
            
            actual.Should().NotBeNull();
            actual?.Id.Should().Be(expected.Id);
            actual?.SenderName.Should().Be(expected.SenderName);
            actual?.SenderEmailAddress.Should().Be(expected.SenderEmailAddress);
        }
        
        [Fact]
        public void When_Configure_KinesisSettings_Should_Load_Configuration_Correctly()
        {
            // Arrange
            var expected = new KinesisSettings
            {
                Enabled = true,
                KinesisStreamName = "customer-data-dev1",
                Region = "ap-southeast-2",
                RoleSessionName = "customer-summary",
                Duration = 3600
            };

            var configuration = _configurationRoot.GetSection(nameof(KinesisSettings));

            // Action
            var actual = new KinesisSettings();
            configuration.Bind(actual);

            // Assert
            actual.Should().NotBeNull();
            actual.AccessKeyId.Should().NotBeNullOrEmpty();
            actual.SecretAccessKey.Should().NotBeNullOrEmpty();
            actual.Enabled.Should().Be(expected.Enabled);
            actual.KinesisStreamName.Should().BeEquivalentTo(expected.KinesisStreamName);
            actual.Region.Should().BeEquivalentTo(expected.Region);
            actual.RoleArn.Should().NotBeNullOrEmpty();
            actual.RoleSessionName.Should().BeEquivalentTo(expected.RoleSessionName);
            actual.Duration.Should().Be(expected.Duration);
        }

        [Fact]
        public void When_Configure_TwilioSettings_Should_Load_Configuration_Correctly()
        {
            // Arrange
            var expected = new TwilioSettings
            {
                Sid = _fixture.Create<string>(),
                FromNumber = "+61428245234",
                AuthToken = _fixture.Create<string>(),
                CountryCode = "AU",
            };

            var configuration = _configurationRoot.GetSection(nameof(TwilioSettings));

            // Action
            var actual = new TwilioSettings();
            configuration.Bind(actual);

            // Assert
            actual.Should().NotBeNull();
            actual.Sid.Should().NotBeNullOrEmpty();
            actual.FromNumber.Should().BeEquivalentTo(expected.FromNumber);
            actual.AuthToken.Should().NotBeNullOrEmpty();
            actual.CountryCode.Should().BeEquivalentTo(expected.CountryCode);
        }
    }
}
