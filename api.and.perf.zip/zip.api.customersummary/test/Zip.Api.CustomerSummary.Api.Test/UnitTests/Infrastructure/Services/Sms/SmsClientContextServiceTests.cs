using FluentAssertions;
using Xunit;
using Zip.Api.CustomerSummary.Infrastructure.Services.SmsService;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services.Sms
{
    public class SmsClientContextServiceTests
    {
        private readonly SmsClientContextService _target;

        public SmsClientContextServiceTests()
        {   
            _target = new SmsClientContextService();
        }
        
        [Theory]
        [InlineData("0400000000")]
        [InlineData("02000000000")]
        public void Given_Testing_Phone_Number_GetContextByPhoneNumber_Should_Be_Test_Context(string phoneNumber)
        {
            var actual = _target.GetContextByPhoneNumber(phoneNumber);

            actual.IsTest.Should()
                   .BeTrue();
        }

        [Theory]
        [InlineData("0400000001")]
        [InlineData("02000000002")]
        public void Given_Not_Testing_Phone_Number_GetContextByPhoneNumber_Should_Not_Be_Test_Context(string phoneNumber)
        {
            var actual = _target.GetContextByPhoneNumber(phoneNumber);

            actual.IsTest.Should()
                   .BeFalse();
        }

        [Theory]
        [InlineData("Test1@mailinator.com")]
        [InlineData("Test2@mailinator.com")]
        [InlineData("Test3@MAILINATOR.COM")]
        public void Given_Testing_Email_GetContextByEmail_Should_Be_Test_Context(string email)
        {
            var actual = _target.GetContextByEmail(email);

            actual.IsTest.Should()
                   .BeTrue();
        }

        [Theory]
        [InlineData("Test1@mailinator.co")]
        [InlineData("Test2@mailinatorX.com")]
        public void Given_Not_Testing_Email_GetContextByEmail_Should_Not_Be_Test_Context(string email)
        {
            var actual = _target.GetContextByEmail(email);

            actual.IsTest.Should()
                   .BeFalse();
        }
    }
}
