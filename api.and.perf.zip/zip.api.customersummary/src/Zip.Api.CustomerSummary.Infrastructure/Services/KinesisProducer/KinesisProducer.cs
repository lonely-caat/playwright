using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using Zip.Api.CustomerSummary.Infrastructure.Services.KinesisProducer.Interfaces;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.KinesisProducer
{
    public class KinesisProducer : IKinesisProducer
    {
        private readonly IAmazonKinesis _kinesisClient;

        public KinesisProducer(IAmazonKinesis amazonKinesisClient)
        {
            _kinesisClient = amazonKinesisClient ?? throw new ArgumentNullException(nameof(amazonKinesisClient));
        }

        public async Task PutRecord(string streamName, string data, string partitionKey)
        {
            var request = new PutRecordRequest
            {
                StreamName = streamName,
                Data = new MemoryStream(Encoding.UTF8.GetBytes(data)),
                PartitionKey = partitionKey
            };
            
            var putResultResponse = await _kinesisClient.PutRecordAsync(request);
            
            if (putResultResponse?.HttpStatusCode != HttpStatusCode.OK)
            {
                throw new AmazonKinesisException($"Failed to put record into Kinesis stream: {streamName}. Partition Key: {partitionKey}");
            }
        }
    }
}
