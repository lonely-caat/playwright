using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Exceptions
{
    public class PaymentMethodNotFoundExceptionTests
    {
        private readonly IFixture _fixture;

        private readonly Exception _innerException;

        private readonly long _expectedConsumerId;

        public PaymentMethodNotFoundExceptionTests()
        {
            _fixture = new Fixture();
            _innerException = new Exception("I am an inner exception");
            _expectedConsumerId = _fixture.Create<long>();
        }

        [Fact]
        public void When_Instantiation_Then_Exception_Is_Not_Null()
        {
            var target = new PaymentMethodNotFoundException();

            target.Should().NotBeNull();
        }

        [Fact]
        public void Given_Message_When_Instantiation_Then_Exception__Should_Be_Correct()
        {
            var expectedMessage = _fixture.Create<string>();

            var target = new PaymentMethodNotFoundException(expectedMessage);

            target.Message.Should().BeEquivalentTo(expectedMessage);
        }

        [Fact]
        public void Given_Message_And_InnerException_When_Instantiation_Then_Exception_Should_Be_Correct()
        {
            var expectedMessage = _fixture.Create<string>();

            var target = new PaymentMethodNotFoundException(expectedMessage, _innerException);

            target.Message.Should().BeEquivalentTo(expectedMessage);
            target.InnerException.Should().BeEquivalentTo(_innerException);
        }

        [Fact]
        public void Exception_Serialization_Should_Be_Correct()
        {
            // Arrange
            var expect = new PaymentMethodNotFoundException("message", _innerException)
            {
                ConsumerId = _expectedConsumerId
            };

            // Action
            PaymentMethodNotFoundException actual;
            var buffer = new byte[4096];

            using (var serializeMemoryStream = new MemoryStream(buffer))
            using (var deserializeMemoryStream = new MemoryStream(buffer))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(serializeMemoryStream, expect);
                actual = (PaymentMethodNotFoundException)formatter.Deserialize(deserializeMemoryStream);
            }

            // Assert
            actual.ConsumerId.Should()
                   .Be(_expectedConsumerId);
            actual.InnerException?.Message.Should()
                   .BeEquivalentTo(expect.InnerException?.Message);
            actual.Message.Should()
                   .BeEquivalentTo(expect.Message);
        }

        [Fact]
        public void Give_SerializationInfo_Is_Null_When_GetObjectData_Throws_ArgumentNullException()
        {
            var target = new PaymentMethodNotFoundException("message");

            Action action = () => { target.GetObjectData(null, new StreamingContext()); };

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
