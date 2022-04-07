using System;
using Xunit;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Exceptions
{
    public class SmsDeliveryExceptionTests
    {
        [Fact]
        public void Should_instantiate()
        {
            var ex = new SmsDeliveryException();
            Assert.NotNull(ex);
        }

        [Fact]
        public void Should_represent_error_message()
        {
            var errMsg = "Somewhere is wrong";
            var ex = new SmsDeliveryException(errMsg);
            Assert.Equal(errMsg, ex.Message);
        }

        [Fact]
        public void Should_represent_same_error_and_message()
        {
            var errMsg = "Somewhere is wrong";
            var innerEx = new ArgumentOutOfRangeException("name");
            var ex = new SmsDeliveryException(errMsg, innerEx);
            Assert.Equal(errMsg, ex.Message);
            Assert.Equal(innerEx, ex.InnerException);
        }
    }
}
