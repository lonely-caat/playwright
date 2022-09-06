using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Domain.Entities.Contract
{
    public class AccountTests
    {
        [Fact]
        public void Given_AccountStatusActive_Account_Should_Active()
        {
            var activeAccount = new Account { AccountStatus = AccountStatus.Active };
            Assert.True(activeAccount.IsActive);
        }

        [Fact]
        public void Dummy_Properties_Test()
        {
            var target = new Fixture().Create<Account>();

            target.Should()
                   .Be(target);
        }
    }
}
