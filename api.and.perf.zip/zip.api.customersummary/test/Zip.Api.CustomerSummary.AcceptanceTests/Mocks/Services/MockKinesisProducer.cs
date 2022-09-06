using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.KinesisProducer.Interfaces;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.Services
{
    public class MockKinesisProducer : IKinesisProducer
    {
        public Task PutRecord(string streamName, string data, string partitionKey)
        {
            return Task.FromResult(0);
        }
    }
}
