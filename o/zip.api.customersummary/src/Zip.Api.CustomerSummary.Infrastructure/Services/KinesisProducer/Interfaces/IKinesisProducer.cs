using System.Threading.Tasks;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.KinesisProducer.Interfaces
{
    public interface IKinesisProducer
    {
        Task PutRecord(string streamName, string data, string partitionKey);
    }
}
