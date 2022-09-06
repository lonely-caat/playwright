using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Payments;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class AccountContext : IAccountContext
    {
        private readonly IDbContext _dbContext;

        public AccountContext(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<bool> HasMatchingRepayment(Repayment repayment)
        {
            return await GetMatchingRepayment(repayment) != null;
        }

        public async Task<Repayment> GetMatchingRepayment(Repayment repayment)
        {
            var sql = @"
SELECT TOP 1 *
FROM [Repayment]
WHERE AccountId=@AccountId AND Amount=@Amount AND ChangedBy=@ChangedBy AND Frequency=@Frequency AND StartDate=@StartDate
ORDER BY TimeStamp DESC
";

            var matchingRepayment = await _dbContext.QuerySingleOrDefaultAsync<Repayment>(sql, repayment);
            return matchingRepayment;
        }

        public async Task<Repayment> AddRepaymentAsync(Repayment repayment)
        {
            var hasMatchingRepayment = await HasMatchingRepayment(repayment);
            if (!hasMatchingRepayment)
            {
                var sql = @"
INSERT INTO [Repayment] (
    AccountId,Amount,Frequency,StartDate,Active,Reviewed,TimeStamp,ChangedBy) 
 VALUES (
    @AccountId,@Amount,@Frequency,cast(@StartDate as Date),1,0,getdate(),@ChangedBy);
SELECT CAST(SCOPE_IDENTITY() as bigint)
";

                var repaymentId = await _dbContext.ExecuteScalarAsync<long>(sql, repayment);

                if (repaymentId > 0)
                {
                    return await GetRepaymentAsync(repaymentId);
                }
            }

            return null;
        }

        public async Task<Account> GetAsync(long accountId)
        {
            var sql = @"
SELECT TOP 1 A.*,CA.ConsumerId  
  FROM Account (NOLOCK) A 
 INNER JOIN ConsumerAccount (NOLOCK) CA ON A.Id = CA.AccountId 
 WHERE A.Id=@accountId
";
            var accountTypeSql = @"
SELECT TOP 1 * 
  FROM AccountType (NOLOCK) 
 WHERE Id=@accountTypeId
";

            var account = await _dbContext.QueryFirstOrDefaultAsync<Account>(sql, new { accountId });
            if (account != null)
            {
                account.AccountType = await _dbContext.QuerySingleOrDefaultAsync<AccountType>(accountTypeSql, new { accountTypeId = account.AccountTypeId });
            }

            return account;
        }

        public async Task<Repayment> GetRepaymentAsync(long id)
        {
            var sql = @"
SELECT * 
  FROM Repayment (NOLOCK) 
 WHERE Id=@id
";

            return await _dbContext.QuerySingleOrDefaultAsync<Repayment>(sql, new { id });
        }

        public async Task<RepaymentSchedule> GetRepaymentScheduleAsync(long accountId)
        {
            var query = @"
SELECT  AT.[Description],
        AT.[MinimumMontlyRepayment] MinimumMonthlyRepayment,
        AT.MinimumRepaymentPercentage,
        AT.EstablishmentFee,
        AT.MonthlyFee,
        ArrearsHoldDate = CASE WHEN (A.ArrearsHoldDate IS NULL) OR (CAST(A.ArrearsHoldDate AS DATE) <= CAST(GETDATE() AS DATE)) THEN NULL
			ELSE A.ArrearsHoldDate
		END
  FROM Account (NOLOCK) A 
 INNER JOIN AccountType (NOLOCK) AT ON A.AccountTypeId = AT.Id 
 WHERE A.Id = @accountId
";

            var establishmentFeeSql = @"
SELECT Top 1 Amount 
  FROM TransactionHistory (NOLOCK) 
 WHERE [Type] = 13 
   AND ConsumerId = (SELECT TOP 1 ConsumerId FROM ConsumerAccount WHERE AccountId=@accountId) 
 ORDER BY [TimeStamp]
";


            var repaymentSchedule = await _dbContext.QueryFirstOrDefaultAsync<RepaymentSchedule>(query, new { accountId });
            if (repaymentSchedule != null)
            {
                repaymentSchedule.EstablishmentFee = await _dbContext.ExecuteScalarAsync<decimal>(establishmentFeeSql, new { accountId });
            }
            return repaymentSchedule;
        }

        public async Task HoldPaymentDateAsync(long accountId, DateTime holdDate)
        {
            var sql = @"
UPDATE [Account] 
   SET ArrearsHoldDate = cast(@holdDate as Date) 
 WHERE [Id]=@accountId
";

            await _dbContext.ExecuteAsync(sql, new { accountId, holdDate });
        }
    }
}
