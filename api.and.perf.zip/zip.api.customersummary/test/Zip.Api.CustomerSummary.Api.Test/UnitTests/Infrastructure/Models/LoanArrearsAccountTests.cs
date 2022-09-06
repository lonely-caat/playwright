using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Tango;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Models
{
    public class LoanArrearsAccountTests
    {
        private readonly Fixture _fixture;

        public LoanArrearsAccountTests()
        {
            _fixture = new Fixture();
        }
        
        [Theory]
        [InlineData(null, "Not Set")]
        [InlineData("A", "Active")]
        [InlineData("C", "Closed")]
        [InlineData("", "Creating")]
        [InlineData(" ", "Creating")]
        [InlineData("DEFAULT", "DEFAULT")]
        public void GetLoanStatus_Should_Be_Correct(string loanStatus, string expect)
        {
            var target = _fixture.Build<LoanArrearsAccount>()
                   .With(x => x.LoanStatus, loanStatus)
                   .Create();

            target.GetLoanStatus()
                   .Should()
                   .Be(expect);
        }
        
        [Theory]
        [InlineData("A", true)]
        [InlineData("C", true)]
        [InlineData("L", true)]
        [InlineData("NotReady", false)]
        public void IsAccountReady_Should_Be_Correct(string loanStatus, bool expect)
        {
            var target = _fixture.Build<LoanArrearsAccount>()
                   .With(x => x.LoanStatus, loanStatus)
                   .Create();

            target.IsAccountReady()
                   .Should()
                   .Be(expect);
        }
        
        [Theory]
        [InlineData("C", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("NotC", false)]
        public void IsAccountClosed_Should_Be_Correct(string loanStatus, bool expect)
        {
            var target = _fixture.Build<LoanArrearsAccount>()
                   .With(x => x.LoanStatus, loanStatus)
                   .Create();

            target.IsAccountClosed()
                   .Should()
                   .Be(expect);
        }
    }
}
