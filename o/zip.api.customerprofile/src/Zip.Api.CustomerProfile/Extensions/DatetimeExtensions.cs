using System;

namespace Zip.Api.CustomerProfile.Extensions
{
    public static class DatetimeExtensions
    {
        public static DateTimeOffset? ToUniversalTime(this DateTimeOffset? datetime)
        {
            return datetime.HasValue ? datetime.Value.ToUniversalTime() : (DateTimeOffset?) null;
        }
    }
}