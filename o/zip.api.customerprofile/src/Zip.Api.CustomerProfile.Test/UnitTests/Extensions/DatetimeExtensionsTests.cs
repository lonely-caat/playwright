using System;
using Xunit;
using Zip.Api.CustomerProfile.Extensions;

namespace Zip.Api.CustomerProfile.Test.UnitTests.Extensions
{
    public class DatetimeExtensionsTests
    {
        [Fact]
        public void ToUniversalTime_should_return_null_if_timestamp_not_provided()
        {
            DateTimeOffset? timestamp = null;
            Assert.Null(timestamp.ToUniversalTime());
        }

        [Fact]
        public void ToUniversalTime_should_convert_to_universal_time_when_datetime_is_provided()
        {
            DateTimeOffset?
                timestamp = new DateTimeOffset(2019, 10, 10, 0, 0, 0,
                    TimeSpan.FromHours(10)); // 10 Oct 2019 12:00:00 AM +10:00
            DateTimeOffset?
                universal = new DateTimeOffset(2019, 10, 9, 14, 0, 0,
                    TimeSpan.FromHours(0)); // 09 Oct 2019 14:00:00 PM UTC

            Assert.Equal(universal, timestamp.ToUniversalTime());
        }
    }
}