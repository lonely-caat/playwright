using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Exceptions
{
    public class StatementDateNotFoundExceptionTests
    {
        private readonly IFixture _fixture;

        private readonly Exception _innerException;

        private readonly long _expectedAccountId;

        public StatementDateNotFoundExceptionTests()
        {
            _fixture = new Fixture();
            _innerException = new Exception("I am an inner exception");
            _expectedAccountId = _fixture.Create<long>();
        }

        [Fact]
        public void When_Instantiation_Then_Exception_Is_Not_Null()
        {
            var target = new StatementDateNotFoundException();

            target.Should().NotBeNull();
        }

        [Fact]
        public void Given_Message_When_Instantiation_Then_Exception__Should_Be_Correct()
        {
            var expectedMessage = _fixture.Create<string>();

            var target = new StatementDateNotFoundException(expectedMessage);

            target.Message.Should().BeEquivalentTo(expectedMessage);
        }

        [Fact]
        public void Given_Message_And_InnerException_When_Instantiation_Then_Exception_Should_Be_Correct()
        {
            var expectedMessage = _fixture.Create<string>();

            var target = new StatementDateNotFoundException(expectedMessage, _innerException);

            target.Message.Should().BeEquivalentTo(expectedMessage);
            target.InnerException.Should().BeEquivalentTo(_innerException);
        }

        [Fact]
        public void Given_AccountId_When_Instantiation_Then_Exception__Should_Be_Correct()
        {
            var target = new StatementDateNotFoundException(_expectedAccountId);

            target.Message.Should()
                   .Contain(_expectedAccountId.ToString());
        }

        [Fact]
        public void Exception_Serialization_Should_Be_Correct()
        {
            // Arrange
            var expect = new StatementDateNotFoundException("message", _innerException)
            {
                AccountId = _expectedAccountId
            };

            // Action
            StatementDateNotFoundException actual;
            var buffer = new byte[4096];

            using (var serializeMemoryStream = new MemoryStream(buffer))
            using (var deserializeMemoryStream = new MemoryStream(buffer))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(serializeMemoryStream, expect);
                actual = (StatementDateNotFoundException)formatter.Deserialize(deserializeMemoryStream);
            }

            // Assert
            actual.AccountId.Should()
                   .Be(_expectedAccountId);
            actual.InnerException?.Message.Should()
                   .BeEquivalentTo(expect.InnerException?.Message);
            actual.Message.Should()
                   .BeEquivalentTo(expect.Message);
        }

        [Fact]
        public void Give_SerializationInfo_Is_Null_When_GetObjectData_Throws_ArgumentNullException()
        {
            var target = new StatementDateNotFoundException("message");

            Action action = () => { target.GetObjectData(null, new StreamingContext()); };

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
