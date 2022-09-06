using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class PhoneContext : IPhoneContext
    {
        private readonly IDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;

        public PhoneContext(IDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Phone> GetAsync(long id)
        {
            const string sql = @"
SELECT * FROM [Phone] WHERE Id=@id
";

            return await _dbContext.QuerySingleOrDefaultAsync<Phone>(sql, new { id });
        }

        public async Task<ConsumerPhone> GetConsumerPhoneAsync(long consumerId, long phoneId)
        {
            const string sql = @"
SELECT * FROM [ConsumerPhone] WHERE ConsumerId=@consumerId AND PhoneId=@phoneId
";

            return await _dbContext.QueryFirstOrDefaultAsync<ConsumerPhone>(sql, new { consumerId, phoneId });
        }

        public async Task<IEnumerable<Phone>> GetConsumerPhoneHistoryAsync(long consumerId)
        {
            const string sql = @"
SELECT p.* FROM [Phone] p 
 INNER JOIN [ConsumerPhone] cp ON cp.PhoneId = p.Id
 WHERE cp.ConsumerId = @consumerId 
 ORDER by cp.Id
";

            return await _dbContext.QueryAsync<Phone>(sql, new { consumerId });
        }

        public async Task<Phone> UpdateStatusAsync(long consumerId, Phone phone)
        {
            using (var trans = _unitOfWork.GetTransaction())
            {
                if (phone.Preferred)
                {
                    const string untickPreferredSql = @"
UPDATE [Phone] 
   SET [Preferred]=0 
 WHERE [Id] IN (SELECT PhoneId 
                  FROM ConsumerPhone 
                 WHERE ConsumerId = @consumerId 
                   AND PhoneType = @phoneType)
";
                    await trans.Connection.ExecuteAsync(untickPreferredSql, new { consumerId, phoneType = phone.PhoneType }, trans);
                }


                const string updateSql = @"
UPDATE [Phone] 
   SET [Preferred]=@Preferred,[Active]=@Active,[Deleted]=@Deleted,[TimeStamp]=GETDATE() WHERE Id=@Id
";

                await trans.Connection.ExecuteAsync(updateSql,  phone, trans);



                trans.Commit();
            }

            return await GetAsync(phone.Id);
        }
    }
}
