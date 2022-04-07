using System;
using System.Threading.Tasks;
using AutoFixture;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Payments;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.DbContexts
{
    public class MockAccountContext : IAccountContext
    {
        private readonly IFixture _fixture;

        public MockAccountContext()
        {
            _fixture = new Fixture();
        }

        public Task<Repayment> AddRepaymentAsync(Repayment repayment)
        {
            repayment.Id = _fixture.Create<long>();
            return Task.FromResult<Repayment>(repayment);
        }

        public Task<Account> GetAsync(long accountId)
        {
            Account account = accountId == int.MaxValue
                ? null
                : new Account
                {
                    Id = accountId,
                    StatementDate = DateTime.Now.AddDays(7),
                    AccountStatus = AccountStatus.Active,
                    AccountType = new AccountType(),
                    AccountTypeId = 2912,
                    ConsumerId = 123456,
                    TimeStamp = DateTime.Now
                };
            return Task.FromResult(account);
        }

        public Task<Repayment> GetMatchingRepayment(Repayment repayment)
        {
            return Task.FromResult<Repayment>(new Repayment
            {
                AccountId = repayment.AccountId,
                StartDate = repayment.StartDate,
                Amount = repayment.Amount,
                ChangedBy = repayment.ChangedBy,
                Frequency = repayment.Frequency
            });
        }

        public Task<Repayment> GetNextRepaymentAsync(long accountId)
        {
            Repayment repayment = accountId == int.MaxValue
                ? null
                : new Repayment
                {
                    AccountId = accountId,
                    StartDate = DateTime.Now.AddDays(5),
                    Amount = 66m,
                    ChangedBy = "shan.ke@zip.co",
                    Frequency = Frequency.Monthly
                };
            return Task.FromResult(repayment);
        }

        public Task<Repayment> GetRepaymentAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<RepaymentSchedule> GetRepaymentScheduleAsync(long accountId)
        {
            return Task.FromResult<RepaymentSchedule>(new RepaymentSchedule
            {
                Description = "test repayment schedule",
                ArrearsHoldDate = DateTime.Now.AddDays(7),
                ContractualDate = DateTime.Now.AddYears(-1),
                EstablishmentFee = 29.2m,
                MinimumMonthlyRepayment = 40m
            });
        }

        public Task<bool> HasMatchingRepayment(Repayment repayment)
        {
            return Task.FromResult<Boolean>(false);
        }

        public Task HoldPaymentDateAsync(long accountId, DateTime holdDate)
        {
            if(accountId == int.MaxValue)
                throw new Exception("test exception");
            return Task.FromResult(0);
        }
    }
}
