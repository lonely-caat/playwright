namespace Zip.Api.CustomerSummary.Domain.Entities.Kinesis
{
    public enum KinesisRecordType : byte
    {
        Unknown = 0,

        Customer,

        WatchlistItems
    }
}