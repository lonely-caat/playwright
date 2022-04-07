using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Domain.Entities.Contract
{
    public class CreditProfileAttributeTests
    {
        [Fact]
        public void Dummy_Properties_Test()
        {
            var target = new Fixture().Create<CreditProfileAttribute>();

            target.Should()
                   .Be(target);
        }
    }
}
