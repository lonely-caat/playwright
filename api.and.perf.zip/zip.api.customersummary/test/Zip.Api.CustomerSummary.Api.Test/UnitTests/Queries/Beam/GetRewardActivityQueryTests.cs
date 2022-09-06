using System;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Constants;
using Zip.Api.CustomerSummary.Application.Beam.Query.GetRewardActivity;
using Zip.Api.CustomerSummary.Domain.Common.Constants;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Beam
{
    public class GetRewardActivityQueryTests
    {
        public GetRewardActivityQueryTests()
        {
        }

        [Theory]
        [InlineData(null, PaginationDefault.PageSize)]
        [InlineData(0, 0)]
        [InlineData(5, 5)]
        public async Task GetRewardActivityQuery_Constructor_Should_Construct_With_PageSize_And_Default_To_10_If_Null(long? input, long expected)
        {
            var actual = new GetRewardActivityQuery(Guid.NewGuid(), 1, input, Regions.Australia);

            Assert.Equal(expected, actual.PageSize);
        }

        [Theory]
        [InlineData(null, "au")]
        [InlineData(Regions.NewZealand, "nz")]
        [InlineData(Regions.Australia, "au")]
        public async Task GetRewardActivityQuery_Constructor_Should_Construct_With_Region_And_Default_To_AU_If_Null(string input, string expected)
        {
            var actual = new GetRewardActivityQuery(Guid.NewGuid(), 1, 10, input);

            Assert.Equal(expected, actual.Region);
        }
    }
}
