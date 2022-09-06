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
    public class ProductNotFoundExceptionTests
    {
        private readonly IFixture _fixture;

        private readonly Exception _innerException;

        private readonly long _expectedProductId;

        public ProductNotFoundExceptionTests()
        {
            _fixture = new Fixture();
            _innerException = new Exception("I am an inner exception");
            _expectedProductId = _fixture.Create<long>();
        }

        [Fact]
        public void When_Instantiation_Then_Exception_Is_Not_Null()
        {
            var target = new ProductNotFoundException();

            target.Should().NotBeNull();
        }

        [Fact]
        public void Given_Message_When_Instantiation_Then_Exception__Should_Be_Correct()
        {
            var expectedMessage = _fixture.Create<string>();

            var target = new ProductNotFoundException(expectedMessage);

            target.Message.Should().BeEquivalentTo(expectedMessage);
        }


        [Fact]
        public void Given_ProductId_When_Instantiation_Then_Exception__Should_Be_Correct()
        {
            var target = new ProductNotFoundException(_expectedProductId);

            target.Message.Should().Contain(_expectedProductId.ToString());
        }

        [Fact]
        public void Given_Message_And_InnerException_When_Instantiation_Then_Exception_Should_Be_Correct()
        {
            var expectedMessage = _fixture.Create<string>();

            var target = new ProductNotFoundException(expectedMessage, _innerException);

            target.Message.Should().BeEquivalentTo(expectedMessage);
            target.InnerException.Should().BeEquivalentTo(_innerException);
        }

        [Fact]
        public void Exception_Serialization_Should_Be_Correct()
        {
            // Arrange
            var expect = new ProductNotFoundException("message", _innerException)
            {
                ProductId = _expectedProductId
            };

            // Action
            ProductNotFoundException actual;
            var buffer = new byte[4096];

            using (var serializeMemoryStream = new MemoryStream(buffer))
            using (var deserializeMemoryStream = new MemoryStream(buffer))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(serializeMemoryStream, expect);
                actual = (ProductNotFoundException)formatter.Deserialize(deserializeMemoryStream);
            }

            // Assert
            actual.ProductId.Should()
                   .Be(_expectedProductId);
            actual.InnerException?.Message.Should()
                   .BeEquivalentTo(expect.InnerException?.Message);
            actual.Message.Should()
                   .BeEquivalentTo(expect.Message);
        }

        [Fact]
        public void Give_SerializationInfo_Is_Null_When_GetObjectData_Throws_ArgumentNullException()
        {
            var target = new ProductNotFoundException("message");

            Action action = () => { target.GetObjectData(null, new StreamingContext()); };

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
