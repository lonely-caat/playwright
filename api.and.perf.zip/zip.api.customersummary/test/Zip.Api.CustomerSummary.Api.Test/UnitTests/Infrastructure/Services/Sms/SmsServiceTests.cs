using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Sms;
using Zip.Api.CustomerSummary.Infrastructure.Services.SmsService;
using Zip.Api.CustomerSummary.Infrastructure.Services.SmsService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services.Sms
{
    public class SmsServiceTests
    {
        private readonly SmsService _target;
        
        private readonly Fixture _fixture;

        private readonly Mock<ISmsClient> _smsClient;

        private readonly Mock<ISmsContentContext> _smsContentContext;

        private readonly Mock<ISmsClientContextService> _smsClientContextService;

        public SmsServiceTests()
        {
            _fixture = new Fixture();
            _smsClient = new Mock<ISmsClient>();
            _smsContentContext = new Mock<ISmsContentContext>();
            _smsClientContextService = new Mock<ISmsClientContextService>();

            _target = new SmsService(_smsClient.Object, _smsContentContext.Object, _smsClientContextService.Object); 
        }
        
        [Fact]
        public async Task Given_Id_When_GetSmsContentAsync_Should_Invoke_SmsContentContext_Correctly()
        {
            var expect = _fixture.Create<int>();

            await _target.GetSmsContentAsync(expect);
            
            _smsContentContext.Verify(x => x.GetAsync(expect), Times.Once);
        }

        [Fact]
        public async Task Given_Name_When_GetSmsContentAsync_Should_Invoke_SmsContentContext_Correctly()
        {
            var expect = _fixture.Create<string>();

            await _target.GetSmsContentByNameAsync(expect);

            _smsContentContext.Verify(x => x.GetAsync(expect), Times.Once);
        }

        [Fact]
        public void Given_PhoneNumber_Is_For_Test_Context_When_SendSmsToConsumer_Should_Return_Success_Response_Without_Invoking_SmsClient()
        {
            // Arrange
            var phoneNumber = _fixture.Create<string>();
            var testContext = new SmsClientContext
            {
                IsTest = true,
                Token = _fixture.Create<string>(),
            };
            
            _smsClientContextService.Setup(x => x.GetContextByPhoneNumber(phoneNumber))
                   .Returns(testContext);

            // Action
            var actual = _target.SendSmsToConsumer(phoneNumber, _fixture.Create<string>());

            // Assert
            actual.Success.Should().BeTrue();
            _smsClient.Verify(x => x.Send(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Given_PhoneNumber_Is_Not_For_Test_Context_When_SendSmsToConsumer_Should_Invoke_SmsClient()
        {
            // Arrange
            const string phoneNumber = "0400000000";
            const string mobileIntFormat = "+61400000000";

            var nonTestContext = new SmsClientContext
            {
                IsTest = false,
                Token = _fixture.Create<string>(),
            };
            
            _smsClientContextService.Setup(x => x.GetContextByPhoneNumber(phoneNumber))
                   .Returns(nonTestContext);

            // Action
            _target.SendSmsToConsumer(phoneNumber, _fixture.Create<string>());

            // Assert
            _smsClient.Verify(
                    x => x.Send(
                            It.Is<string>(y => y == mobileIntFormat),
                            It.IsAny<string>()),
                    Times.Once);
        }

        [Fact]
        public void Given_PhoneNumber_Is_Not_For_Test_Context_And_SmsClient_Throws_Exception_When_SendSmsToConsumer_Should_Catch_Exception_In_response()
        {
            // Arrange
            var expectExceptionMessage = _fixture.Create<string>();
            const string phoneNumber = "0400000000";
            const string mobileIntFormat = "+61400000000";

            var nonTestContext = new SmsClientContext
            {
                IsTest = false,
                Token = _fixture.Create<string>(),
            };

            _smsClientContextService.Setup(x => x.GetContextByPhoneNumber(phoneNumber))
                   .Returns(nonTestContext);

            _smsClient.Setup(x => x.Send(mobileIntFormat, It.IsAny<string>()))
                   .Throws(new Exception(expectExceptionMessage));

            // Action
            var actual = _target.SendSmsToConsumer(phoneNumber, _fixture.Create<string>());

            // Assert
            actual.ErrorMessage.Should()
                   .Be(expectExceptionMessage);
        }
    }
}
