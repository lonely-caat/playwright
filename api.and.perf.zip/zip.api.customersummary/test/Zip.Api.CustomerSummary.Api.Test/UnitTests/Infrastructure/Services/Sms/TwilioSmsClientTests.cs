using System;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Xunit;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.SmsService;
using Zip.Api.CustomerSummary.Infrastructure.Services.SmsService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services.Sms
{
    public class TwilioSmsClientTests
    {
        private readonly ISmsClient _target;

        private readonly Fixture _fixture;

        public TwilioSmsClientTests()
        {
            _fixture = new Fixture();

            var twilioOptions = _fixture.Build<TwilioSettings>()
                   .With(x => x.CountryCode, "AU")
                   .Create();
            var options = Options.Create(twilioOptions);
            
            _target = new TwilioSmsClient(options);
        }
        
        [Fact]
        public void Given_Valid_TwilioSettings_When_Instantiation_Should_Have_Correct_CountryCode()
        {
            ((TwilioSmsClient)_target).CountryCode.Should()
                   .Be("AU");
        }

        [Fact]
        public void Given_TwilioSettings_Is_Null_When_Instantiation_Should_Throw_ArgumentNullException()
        {
            Action action = () => { new TwilioSmsClient(null); };

            action.Should()
                   .Throw<ArgumentException>();
        }


        [Fact]
        public void Given_No_Valid_Settings_TwilioSmsClient_Send_Should_Throw_ApiException()
        {
            Action action = () => { _target.Send(_fixture.Create<string>(), _fixture.Create<string>()); };

            action.Should()
                   .Throw<Twilio.Exceptions.ApiException>();
        }
    }
}
