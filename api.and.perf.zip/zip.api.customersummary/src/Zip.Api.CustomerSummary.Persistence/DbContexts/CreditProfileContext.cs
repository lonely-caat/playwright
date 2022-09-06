using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class CreditProfileContext : ICreditProfileContext
    {
        private readonly IDbContext _dbContext;

        public CreditProfileContext(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }


        public async Task<IEnumerable<ProfileAttribute>> GetProfileAttributesAsync(CreditProfileStateType creditProfileStateType)
        {
            var sql = @"
SELECT *
  FROM ProfileAttribute (NOLOCK)
 WHERE Active = 1
   AND [Type] = @creditProfileStateType
";


            return await _dbContext.QueryAsync<ProfileAttribute>(sql, new { creditProfileStateType });

        }

        public async Task<IEnumerable<ProfileClassification>> GetProfileClassificationsAsync(CreditProfileStateType creditProfileStateType)
        {
            var sql = @"
SELECT *
  FROM ProfileClassification (NOLOCK)
 WHERE [Type] = @creditProfileStateType
   AND Active = 1
";

            return await _dbContext.QueryAsync<ProfileClassification>(sql, new { creditProfileStateType });

        }

        public async Task<CreditProfileState> GetStateTypeByConsumerIdAsync(long consumerId)
        {
            var sql = @"
SELECT DISTINCT CPS.*
  FROM Consumer (NOLOCK) C
 INNER JOIN CreditProfile (NOLOCK) CP on CP.ConsumerId = C.Id
 INNER JOIN CreditProfileState (NOLOCK) CPS ON CP.CurrentCreditProfileStateId = CPS.Id
 WHERE C.Id = @consumerId
 ORDER BY CPS.Id DESC
";

            return await _dbContext.QueryFirstOrDefaultAsync<CreditProfileState>(sql, new { consumerId });
        }

        public async Task CreateCreditProfileStateAsync(CreditProfileState creditProfileState)
        {
            var sql = @"
INSERT INTO [dbo].[CreditProfileState]
           ([CreditStateType]
           ,[TimeStamp]
           ,[CreditProfileId]
           ,[ChangedBy]
           ,[Comments])
     VALUES
           (@CreditStateType
           ,getdate()
           ,@CreditProfileId
           ,@ChangedBy
           ,@Comments);
";

            await _dbContext.ExecuteAsync(sql, creditProfileState);
        }

        public async Task CreateCreditProfileAttributeAsync(long creditProfileId, long profileAttributeId)
        {
            var sql = @"
INSERT INTO [CreditProfileAttribute]
            ([ProfileAttributeId]
            ,[CreditProfileId]
            ,TimeStamp)
        VALUES
            (@profileAttributeId
            ,@creditProfileId
            ,GETDATE())
";

            await _dbContext.ExecuteAsync(sql, new { creditProfileId, profileAttributeId });
        }

        public async Task CreateCreditProfileClassificationAsync(long creditProfileId, long profileClassificationId)
        {
            var sql = @"
INSERT INTO [CreditProfileClassification]
            ([ProfileClassificationId]
            ,[CreditProfileId]
            ,[TimeStamp])
        VALUES
            (@profileClassificationId
            ,@creditProfileId
            ,GETDATE())
";

            await _dbContext.ExecuteAsync(sql, new { creditProfileId, profileClassificationId });
        }

        public async Task<string> GetProfileDateOfClosureAsync(long consumerId)
        {
            var sql = @"
                        select top 1 cps.[TimeStamp]
                        from CreditProfile cp
                        inner join CreditProfileState cps on cp.Id = cps.CreditProfileId
                        where  cps.CreditStateType = 9 and cp.ConsumerId = @consumerId
                        order by cps.[TimeStamp] DESC
                        ";

            return await _dbContext.QuerySingleOrDefaultAsync<string>(sql, new { consumerId });
        }
    }
}
