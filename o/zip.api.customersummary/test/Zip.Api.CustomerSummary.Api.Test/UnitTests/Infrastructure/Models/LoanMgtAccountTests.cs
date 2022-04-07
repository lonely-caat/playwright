using System;
using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Tango;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Models
{
    public class LoanMgtAccountTests
    {
        private readonly Fixture _fixture;

        public LoanMgtAccountTests()
        {
            _fixture = new Fixture();
        }
        
        [Fact]
        public void ArrearsStartDate_Should_Be_Correct()
        {
            var daysInArrearsAsAt = _fixture.Create<int>();
            var expect = DateTime.Now.AddDays(daysInArrearsAsAt * -1).Date;

            var target = _fixture.Build<LoanMgtAccount>()
                   .With(x => x.DaysInArrearsAsAt, daysInArrearsAsAt)
                   .Create();
            
            target.ArrearsStartDate.Should()
                   .NotBeNull();
            target.ArrearsStartDate?.Date.Should()
                   .Be(expect);
        }

        [Fact]
        public void Given_DaysInArrearsAsAt_Is_Null_ArrearsStartDate_Should_Be_Null()
        {
            var target = _fixture.Build<LoanMgtAccount>()
                   .Without(x => x.DaysInArrearsAsAt)
                   .Create();

            target.ArrearsStartDate.Should()
                   .BeNull();
        }
        
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData("21/06/1986", "21/06/1986")]
        [InlineData("1986/06/21", "21/06/1986")]
        public void GetContractualDueDate_Should_Be_Correct(string contractualNextDueDateAsAt, string expect)
        {
            var target = _fixture.Build<LoanMgtAccount>()
                   .With(x => x.ContractualNextDueDateAsAt, contractualNextDueDateAsAt)
                   .Create();

            target.GetContractualDueDate().Should()
                   .Be(expect);
        }
        
        [Theory]
        [InlineData("No Further Drawdown", "NotL",  true)]
        [InlineData("Stop Credit", "NotL", true)]
        [InlineData("X", "L", true)]
        [InlineData("Recovery Ledger", "NotL", true)]
        [InlineData("Loss Recovery", "NotL", true)]
        [InlineData("", "", false)]
        [InlineData(null, "", false)]
        [InlineData("", null, false)]
        public void IsAccountLocked_Should_Be_Correct(string generalPurpose1, string loanStatus, bool expect)
        {
            var target = _fixture.Build<LoanMgtAccount>()
                   .With(x => x.GeneralPurpose1, generalPurpose1)
                   .With(x => x.LoanStatus, loanStatus)
                   .Create();

            target.IsAccountLocked().Should()
                   .Be(expect);
        }
        
        [Theory]
        [InlineData("No Further Drawdown", "No Further Drawdown")]
        [InlineData("Stop Credit", "Stop Credit")]
        [InlineData("Loss Recovery", "Loss Recovery")]
        [InlineData("Recovery Ledger", "Lost")]
        [InlineData("SomethingElse", "SomethingElse")]
        public void Given_GeneralPurpose1_GetAccountStatus_Should_Be_Correct(string generalPurpose1, string expect)
        {
            var target = _fixture.Build<LoanMgtAccount>()
                   .With(x => x.GeneralPurpose1, generalPurpose1)
                         .Create();

            target.GetAccountStatus().Should()
                   .Be(expect);
        }

        [Theory]
        [InlineData("L", "Lost")]
        [InlineData("C", "Closed")]
        public void Given_LoanStatus_GetAccountStatus_Should_Be_Correct(string loanStatus, string expect)
        {
            var target = _fixture.Build<LoanMgtAccount>()
                   .With(x => x.LoanStatus, loanStatus)
                   .Create();

            target.GetAccountStatus().Should()
                   .Be(expect);
        }
        
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("21/06/1986", "21/06/1986")]
        [InlineData("1986/06/21", "21/06/1986")]
        public void GetContractualDueDateTime_Should_Be_Correct(string contractualNextDueDateAsAt, string expect)
        {
            var target = _fixture.Build<LoanMgtAccount>()
                         .With(x => x.ContractualNextDueDateAsAt, contractualNextDueDateAsAt)
                         .Create();

            target.GetContractualDueDateTime()?.Date.ToString(App.DateFormat).Should()
                         .Be(expect);
        }

        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData("21/06/1986", "21/06/1986")]
        [InlineData("1986/06/21", "21/06/1986")]
        public void GetDirectDebitDueDate_Should_Be_Correct(string directDebitNextDateDueAsAt, string expect)
        {
            var target = _fixture.Build<LoanMgtAccount>()
                   .With(x => x.DirectDebitNextDateDueAsAt, directDebitNextDateDueAsAt)
                   .Create();

            target.GetDirectDebitDueDate().Should()
                   .Be(expect);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("21/06/1986", "21/06/1986")]
        [InlineData("1986/06/21", "21/06/1986")]
        public void GetDirectDebitDueDateTime_Should_Be_Correct(string directDebitNextDateDueAsAt, string expect)
        {
               var target = _fixture.Build<LoanMgtAccount>()
                            .With(x => x.DirectDebitNextDateDueAsAt, directDebitNextDateDueAsAt)
                            .Create();

               target.GetDirectDebitDueDateTime()?.Date.ToString(App.DateFormat).Should()
                            .Be(expect);
        }

        [Fact]
        public void Given_No_DirectDebitNextDateDueAsAt_GetFrequency_Should_Be_Monthly()
        {
            var target = _fixture.Build<LoanMgtAccount>()
                         .Without(x => x.DirectDebitNextDateDueAsAt)
                         .Create();

            target.GetFrequency()
                   .Should()
                   .Be(Frequency.Monthly);
        }
        
        [Theory]
        [InlineData("W", Frequency.Weekly)]
        [InlineData("F", Frequency.Fortnightly)]
        [InlineData("M", Frequency.Monthly)]
        [InlineData("SomethingElse", Frequency.Monthly)]
        public void Given_DirectDebitNextDateDueAsAt_GetFrequency_Should_Be_Correct(string directDebitFrequencyAsAt, Frequency expectFrequency)
        {
            var target = _fixture.Build<LoanMgtAccount>()
                         .With(x => x.DirectDebitFrequencyAsAt, directDebitFrequencyAsAt)
                         .Create();

            target.GetFrequency()
                         .Should()
                         .Be(expectFrequency);
        }
        
        [Theory]
        [InlineData(true, "No Further Drawdown")]
        [InlineData(false, "Operational")]
        public void ChangeAccountLockedStatus_Should_Assign_GeneralPurpose1_Correctly(bool lockAccount, string expectGeneralPurpose1)
        {
               var target = _fixture.Create<LoanMgtAccount>();
               
               target.ChangeAccountLockedStatus(lockAccount);

               target.GeneralPurpose1.Should()
                            .Be(expectGeneralPurpose1);
        }
        
        [Theory]
        [InlineData("NOT AVAILABLE", false)]
        [InlineData("SomethingElse", true)]
        public void IsAccountAvailable_Should_Be_Correct(string accountHash, bool expect)
        {
            var target = _fixture.Build<LoanMgtAccount>()
                         .With(x => x.AccountHash, accountHash)
                         .Create();

            target.IsAccountAvailable()
                         .Should()
                         .Be(expect);
        }
        
        [Theory]
        [InlineData(-0.1, false)]
        [InlineData(0, false)]
        [InlineData(0.1, true)]
        public void IsAccountInArrears_Should_Be_Correct(decimal arrearsBalanceAsAt, bool expect)
        {
            var target = _fixture.Build<LoanMgtAccount>()
                   .With(x => x.ArrearsBalanceAsAt, arrearsBalanceAsAt)
                   .Create();

            target.IsAccountInArrears.Should()
                   .Be(expect);
        }
    }
}
