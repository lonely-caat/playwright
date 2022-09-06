using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Payments;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface IAccountContext
    {
        Task<bool> HasMatchingRepayment(Repayment repayment);
        Task<Repayment> GetMatchingRepayment(Repayment repayment);
        Task<RepaymentSchedule> GetRepaymentScheduleAsync(long accountId);
        Task HoldPaymentDateAsync(long accountId, DateTime holdDate);
        Task<Account> GetAsync(long accountId);
        Task<Repayment> AddRepaymentAsync(Repayment repayment);
        Task<Repayment> GetRepaymentAsync(long id);
    }
}