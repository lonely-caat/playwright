using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Domain.Entities.Contract
{
    public class ProfileAttributeTests
    {
        [Fact]
        public void Dummy_Properties_Test2()
        {
            var target = new Fixture().Create<ProfileAttribute>();

            target.Should()
                   .Be(target);
        }
    }
}
