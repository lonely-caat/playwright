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
    public class MakePaymentFailedExceptionTests
    {
        private readonly IFixture _fixture;

        private readonly Exception _innerException;

        private readonly string _expectedReason;

        public MakePaymentFailedExceptionTests()
        {
            _fixture = new Fixture();
            _innerException = new Exception("I am an inner exception");
            _expectedReason = _fixture.Create<string>();
        }

        [Fact]
        public void When_Instantiation_Then_Exception_Is_Not_Null()
        {
            var target = new MakePaymentFailedException();

            target.Should().NotBeNull();
        }

        [Fact]
        public void Given_Reason_When_Instantiation_Then_Exception__Should_Be_Correct()
        {
            var target = new MakePaymentFailedException(_expectedReason);

            target.Message.Should()
                   .Contain(_expectedReason);
        }

        [Fact]
        public void Given_Message_And_InnerException_When_Instantiation_Then_Exception_Should_Be_Correct()
        {
            var expectedMessage = _fixture.Create<string>();

            var target = new MakePaymentFailedException(expectedMessage, _innerException);

            target.Message.Should().BeEquivalentTo(expectedMessage);
            target.InnerException.Should().BeEquivalentTo(_innerException);
        }

        [Fact]
        public void Exception_Serialization_Should_Be_Correct()
        {
            // Arrange
            var expect = new MakePaymentFailedException("message", _innerException)
            {
                Reason = _expectedReason
            };

            // Action
            MakePaymentFailedException actual;
            var buffer = new byte[4096];

            using (var serializeMemoryStream = new MemoryStream(buffer))
            using (var deserializeMemoryStream = new MemoryStream(buffer))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(serializeMemoryStream, expect);
                actual = (MakePaymentFailedException)formatter.Deserialize(deserializeMemoryStream);
            }

            // Assert
            actual.Reason.Should()
                   .Be(_expectedReason);
            actual.InnerException?.Message.Should()
                   .BeEquivalentTo(expect.InnerException?.Message);
            actual.Message.Should()
                   .BeEquivalentTo(expect.Message);
        }

        [Fact]
        public void Give_SerializationInfo_Is_Null_When_GetObjectData_Throws_ArgumentNullException()
        {
            var target = new MakePaymentFailedException("message");

            Action action = () => { target.GetObjectData(null, new StreamingContext()); };

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
