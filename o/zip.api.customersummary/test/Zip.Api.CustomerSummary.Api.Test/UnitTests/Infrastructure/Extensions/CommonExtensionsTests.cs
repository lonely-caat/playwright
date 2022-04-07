using System.Collections.Generic;
using Xunit;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Extensions
{
    public class CommonExtensionsTests
    {
        [Fact]
        public void test_IsEmpty()
        {
            List<object> list = null;

            Assert.True(list.IsEmpty());

            List<object> list1 = new List<object>();

            Assert.True(list1.IsEmpty());

            List<object> list2 = new List<object>()
            {
                new object(),
                new object()
            };

            Assert.False(list2.IsEmpty());
        }

        [Fact]
        public void test_IsNotEmpty()
        {
            List<object> list = null;

            Assert.False(list.IsNotEmpty());

            List<object> list1 = new List<object>();

            Assert.False(list1.IsNotEmpty());

            List<object> list2 = new List<object>()
            {
                new object(),
                new object()
            };

            Assert.True(list2.IsNotEmpty());
        }
    }
}
