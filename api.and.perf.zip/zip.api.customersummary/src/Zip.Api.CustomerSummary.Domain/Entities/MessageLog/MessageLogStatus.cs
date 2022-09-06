namespace Zip.Api.CustomerSummary.Domain.Entities.MessageLog
{
    public enum MessageLogStatus : byte
    {
        Sent = 0,

        SendFailed = 1,

        Processed = 2,

        Delivered = 3,

        Deferred = 4,

        Bounce = 5,

        Open = 6,

        Click = 7,

        Spam = 8,

        Unsubscribe = 9,

        Unknown = 99
    }
}
