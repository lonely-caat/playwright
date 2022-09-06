using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Payments;
using Zip.Api.CustomerSummary.Persistence.DbContexts;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence.DbContexts
{
    public class AccountContextTest
    {
        private readonly Mock<IDbContext> _dbContext;

        public AccountContextTest()
        {
            _dbContext = new Mock<IDbContext>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNulLException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new AccountContext(null);
            });
        }

        [Fact]
        public async Task Given_RepaymentAdded_ShouldReturn_Repayment()
        {
            var expected = new Random().Next(100, 999);

            _dbContext.Setup(x => x.ExecuteScalarAsync<long>(It.IsAny<string>(), It.IsAny<Repayment>()))
                .ReturnsAsync(expected);

            _dbContext.SetupSequence(x => x.QuerySingleOrDefaultAsync<Repayment>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync((Repayment)null)
                .ReturnsAsync(new Repayment { Id = expected });

            var accountContext = new AccountContext(_dbContext.Object);
            var repayment = await accountContext.AddRepaymentAsync(new Repayment());

            Assert.Equal(expected, repayment.Id);
        }

        [Fact]
        public async Task Given_NoRepaymentAdded_ShouldReturn_Repayment()
        {
            var expected = 0;

            _dbContext.Setup(x => x.ExecuteScalarAsync<long>(It.IsAny<string>(), It.IsAny<Repayment>()))
                .ReturnsAsync(expected);

            _dbContext.Setup(x => x.QuerySingleOrDefaultAsync<Repayment>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new Repayment() { Id = 372 });

            var accountContext = new AccountContext(_dbContext.Object);
            var repayment = await accountContext.AddRepaymentAsync(new Repayment());

            Assert.Null(repayment);
        }

        [Fact]
        public async Task Given_AccountTypeFound_ShouldReturn_AccountType()
        {
            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<Account>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new Account());

            _dbContext.Setup(x => x.QuerySingleOrDefaultAsync<AccountType>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new AccountType());

            var accountContext = new AccountContext(_dbContext.Object);
            var account = await accountContext.GetAsync(281);

            Assert.NotNull(account.AccountType);
        }

        [Fact]
        public async Task Given_AccountTypeNotFound_ShouldReturn_Null()
        {
            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<Account>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new Account());

            _dbContext.Setup(x => x.QuerySingleOrDefaultAsync<AccountType>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(null as AccountType);

            var accountContext = new AccountContext(_dbContext.Object);
            var account = await accountContext.GetAsync(281);

            Assert.Null(account.AccountType);
        }

        [Fact]
        public async Task Given_RepaymentNotFound_ShouldReturn_Null()
        {
            _dbContext.Setup(x => x.QuerySingleOrDefaultAsync<Repayment>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(null as Repayment);

            var accountContext = new AccountContext(_dbContext.Object);
            var repayment = await accountContext.GetRepaymentAsync(281);

            Assert.Null(repayment);
        }

        [Fact]
        public async Task Given_RepaymentFound_ShouldReturn()
        {
            _dbContext.Setup(x => x.QuerySingleOrDefaultAsync<Repayment>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new Repayment());

            var accountContext = new AccountContext(_dbContext.Object);
            var repayment = await accountContext.GetRepaymentAsync(281);

            Assert.NotNull(repayment);
        }

        [Fact]
        public async Task Given_RepaymentScheduleNotFound_ShouldReturn_Null()
        {
            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<RepaymentSchedule>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(null as RepaymentSchedule);

            var accountContext = new AccountContext(_dbContext.Object);
            var repayment = await accountContext.GetRepaymentScheduleAsync(281);

            Assert.Null(repayment);
        }

        [Fact]
        public async Task Given_RepaymentScheduleFound_ShouldReturn()
        {
            var expected = 22m;

            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<RepaymentSchedule>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new RepaymentSchedule());

            _dbContext.Setup(x => x.ExecuteScalarAsync<decimal>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(expected);

            var accountContext = new AccountContext(_dbContext.Object);
            var repayment = await accountContext.GetRepaymentScheduleAsync(281);

            Assert.Equal(expected, repayment.EstablishmentFee);
        }

        [Fact]
        public async Task Should_hold_payment_date()
        {
            _dbContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()));

            var accountContext = new AccountContext(_dbContext.Object);
            await accountContext.HoldPaymentDateAsync(281, new DateTime());


            _dbContext.Verify(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }
    }
}
