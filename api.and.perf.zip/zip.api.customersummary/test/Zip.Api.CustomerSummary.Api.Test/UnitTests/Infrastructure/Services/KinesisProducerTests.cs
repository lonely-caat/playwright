using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Infrastructure.Services.KinesisProducer;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services
{
    public class KinesisProducerTests
    {
        private readonly KinesisProducer _target;

        private readonly IFixture _fixture;

        private readonly Mock<IAmazonKinesis> _amazonKinesis;
        
        public KinesisProducerTests()
        {
            _fixture = new Fixture();
            _amazonKinesis = new Mock<IAmazonKinesis>();
            
            _target = new KinesisProducer(_amazonKinesis.Object);
        }

        [Fact]
        public void Given_AmazonKinesisClient_Is_Null_ShouldThrow_ArgumentNullException()
        {
            Action action = () => { new KinesisProducer(null); };

            action.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public async Task When_PutRecord_Then_KinesisClient_PutRecordAsync_Should_Be_Invoked_Correctly()
        {
            // Arrange
            var streamName = _fixture.Create<string>();
            var data = _fixture.Create<string>();
            var partitionKey = _fixture.Create<string>();

            var expectedRequest = new PutRecordRequest
            {
                StreamName = streamName,
                Data = new MemoryStream(Encoding.UTF8.GetBytes(data)),
                PartitionKey = partitionKey
            };

            var response = _fixture
                   .Build<PutRecordResponse>()
                   .With(x => x.HttpStatusCode, HttpStatusCode.OK)
                   .Create();

            _amazonKinesis
                   .Setup(x => x.PutRecordAsync(It.IsAny<PutRecordRequest>(), default))
                   .Returns(Task.FromResult(response));

            // Action
            await _target.PutRecord(streamName, data, partitionKey);
            
            // Assert
            _amazonKinesis.Verify(
                x => x.PutRecordAsync(
                    It.Is<PutRecordRequest>(
                        r =>
                                r.StreamName == expectedRequest.StreamName &&
                                GetString(r.Data) == GetString(expectedRequest.Data) &&
                                r.PartitionKey == expectedRequest.PartitionKey),
                    default),
                Times.Once);
        }

        [Fact]
        public void Given_Not_OK_KinesisClient_PutRecordAsync_Response_When_PutRecord_Should_Throw_AmazonKinesisException()
        {
            // Arrange
            var response = _fixture
                   .Build<PutRecordResponse>()
                   .With(x => x.HttpStatusCode, HttpStatusCode.InternalServerError)
                   .Create();

            _amazonKinesis.Setup(x => x.PutRecordAsync(It.IsAny<PutRecordRequest>(), default))
                   .Returns(Task.FromResult(response));

            // Action
            Func<Task> func = async () => 
            {
                await _target.PutRecord(string.Empty, string.Empty, string.Empty);
            };

            // Assert
            func.Should().Throw<AmazonKinesisException>();
        }
        
        private string GetString(MemoryStream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
