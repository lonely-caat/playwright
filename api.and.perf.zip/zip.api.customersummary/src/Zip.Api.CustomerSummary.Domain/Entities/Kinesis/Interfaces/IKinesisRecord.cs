using System;

namespace Zip.Api.CustomerSummary.Domain.Entities.Kinesis.Interfaces
{
    public interface IKinesisRecord
    {
        DateTime TimeStamp { get; set; }

        KinesisRecordType Type { get; }
    }
}
