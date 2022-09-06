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
    public class AccountNotFoundExceptionTest
    {
        private readonly IFixture _fixture;
        
        private readonly Exception _innerException;

        private readonly long _expectedConsumerId;

        private readonly long _expectedAccountId;

        public AccountNotFoundExceptionTest()
        {
            _fixture = new Fixture();
            _innerException = new Exception("I am an inner exception");
            _expectedConsumerId = _fixture.Create<long>();
            _expectedAccountId = _fixture.Create<long>();
        }
        
        private static void ThrowExceptionWithDefaultConstructor() => throw new AccountNotFoundException();

        private static void ThrowExceptionWithMessageConstructor() => throw new AccountNotFoundException("test message");

        private void ThrowException() => throw new AccountNotFoundException(_expectedConsumerId, _expectedAccountId);

        [Fact]
        public void Given_InstanceWithDefaultConstructor_ShouldGet()
        {
            var ex = Assert.Throws<AccountNotFoundException>(ThrowExceptionWithDefaultConstructor);

            Assert.NotNull(ex);
            Assert.Equal($"Exception of type '{typeof(AccountNotFoundException)}' was thrown.", ex.Message);
        }

        [Fact]
        public void Given_InstanceWithMessageConstructor_ShouldGet()
        {
            var ex = Assert.Throws<AccountNotFoundException>(ThrowExceptionWithMessageConstructor);

            Assert.NotNull(ex);
            Assert.Equal($"test message", ex.Message);
        }
        
        [Fact]
        public void Given_Message_And_InnerException_When_Instantiation_Then_Exception_Should_Be_Correct()
        {
            var expectedMessage = _fixture.Create<string>();
            var innerException = new Exception("I am an inner exception");

            var target = new AccountNotFoundException(expectedMessage, innerException);

            target.Message.Should().BeEquivalentTo(expectedMessage);
            target.InnerException.Should().BeEquivalentTo(innerException);
        }

        [Fact]
        public void ShouldGet_Message()
        {
            var ex = Assert.Throws<AccountNotFoundException>(ThrowException);

            Assert.NotNull(ex);
            Assert.Equal(_expectedConsumerId, ex.ConsumerId);
            Assert.Equal(_expectedAccountId, ex.AccountId);
            Assert.Equal($"Account not found with ConsumerId:{_expectedConsumerId} or AccountId:{_expectedAccountId}.",
                         ex.Message);
        }

        [Fact]
        public void Exception_Serialization_Should_Be_Correct()
        {
            // Arrange
            var expect = new AccountNotFoundException("message", _innerException)
            {
                ConsumerId = _expectedConsumerId,
                AccountId = _expectedAccountId
            };

            // Action
            AccountNotFoundException actual;
            var buffer = new byte[4096];
            
            using (var serializeMemoryStream = new MemoryStream(buffer))
            using (var deserializeMemoryStream = new MemoryStream(buffer))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(serializeMemoryStream, expect);
                actual = (AccountNotFoundException)formatter.Deserialize(deserializeMemoryStream);
            }

            // Assert
            actual.ConsumerId.Should().Be(_expectedConsumerId);
            actual.AccountId.Should().Be(_expectedAccountId);
            actual.InnerException?.Message.Should().BeEquivalentTo(expect.InnerException?.Message);
            actual.Message.Should().BeEquivalentTo(expect.Message);
        }

        [Fact]
        public void Give_SerializationInfo_Is_Null_When_GetObjectData_Throws_ArgumentNullException()
        {
            var target = new AccountNotFoundException("message")
            {
                ConsumerId = _expectedConsumerId,
                AccountId = _expectedAccountId
            };

            Action action = () => { target.GetObjectData(null, new StreamingContext()); };

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
