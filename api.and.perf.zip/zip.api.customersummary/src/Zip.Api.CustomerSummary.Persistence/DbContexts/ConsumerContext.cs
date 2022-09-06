using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class ConsumerContext : IConsumerContext
    {
        private readonly IDbContext _dbContext;
        private readonly ICountryContext _countryContext;

        public ConsumerContext(IDbContext dbContext, ICountryContext countryContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _countryContext = countryContext ?? throw new ArgumentNullException(nameof(countryContext));
        }

        public async Task<Consumer> GetAsync(long consumerId)
        {
            const string sql = @"
SELECT  C.Id ConsumerId,
        CN.FirstName,
        CN.LastName,
        C.CountryId,
        C.Gender,
        C.DateOfBirth,
        C.CustomerId,
        C.ApplicationId,
        C.OriginationMerchantId,
        C.ReferrerId,
        C.[TimeStamp] CreationDate,
        C.ActivationDate
FROM    Consumer C
            LEFT JOIN ConsumerName CN ON C.Id = CN.ConsumerId
WHERE   C.Id = @consumerId
AND     CN.Active = 1
";

            var consumer = await _dbContext.QueryFirstOrDefaultAsync<Consumer>(sql, new { consumerId });

            if (consumer != null)
            {
                consumer.Country = await _countryContext.GetAsync(consumer.CountryId);
            }

            return consumer;
        }

        public async Task<Consumer> GetByCustomerIdAndProductAsync(Guid customerId, int product)
        {
            const string sql = @"
SELECT  Id ConsumerId,
        CountryId,
        CustomerId,
        ApplicationId,
        OriginationMerchantId,
        ReferrerId,
        [TimeStamp] CreationDate,
        ActivationDate
FROM    Consumer
WHERE   CustomerId = @customerId
AND     Active = 1
AND     Classification = @product
";

            var consumer = await _dbContext.QueryFirstOrDefaultAsync<Consumer>(sql, new { customerId, product });

            return consumer;
        }

        public async Task<Consumer> GetAsyncV2(long consumerId)
        {
            const string sql = @"
SELECT  C.Id ConsumerId,
        C.CountryId,
        C.CustomerId,
        C.ApplicationId,
        C.OriginationMerchantId,
        C.ReferrerId,
        C.[TimeStamp] CreationDate,
        C.ActivationDate
FROM    Consumer C
        LEFT JOIN ConsumerName CN ON C.Id = CN.ConsumerId
WHERE   C.Id = @consumerId
AND     CN.Active = 1
";

            var consumer = await _dbContext.QueryFirstOrDefaultAsync<Consumer>(sql, new { consumerId });

            if (consumer != null)
            {
                consumer.Country = await _countryContext.GetAsync(consumer.CountryId);
            }

            return consumer;
        }

        public async Task<long?> GetLinkedConsumerIdAsync(long consumerId)
        {
            var sql = "select Id from Consumer where CustomerId in (select CustomerId from Consumer where Id = @consumerId) and Id <> @consumerId and Active = 1";

            return await _dbContext.QueryFirstOrDefaultAsync<long?>(sql, new { consumerId });
        }

        public async Task UpdateDateOfBirthAsync(long consumerId, DateTime dateOfBirth)
        {
            var updateSql = "UPDATE [Consumer] SET DateOfBirth=@dateOfBirth WHERE Id=@consumerId";

            await _dbContext.ExecuteAsync(updateSql, new { consumerId, dateOfBirth });
        }

        public async Task UpdateGenderAsync(long consumerId, Gender gender)
        {
            const string updateSql = "UPDATE [Consumer] SET Gender=@gender WHERE Id=@consumerId";

            await _dbContext.ExecuteAsync(updateSql, new { consumerId, gender });
        }

        public async Task UpdateNameAsync(long consumerId, string firstName, string lastName, string middleName, string otherName)
        {
            const string selectSql = "SELECT * FROM [ConsumerName] WHERE [Active]=1 AND ConsumerId=@consumerId";
            const string updateSql = "UPDATE [ConsumerName] SET [Active]=0,[TimeStamp]=getdate() WHERE ConsumerId=@consumerId;";
            const string insertSql = "INSERT INTO [ConsumerName] ([FirstName],[LastName],[MiddleName],[OtherName],[ConsumerId],[Active],[TimeStamp],[Title]) " +
                                     "VALUES (@firstName,@lastName,@middleName,@otherName,@consumerId,1,getdate(),@title)";

            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("consumerId", consumerId);
            dynamicParameters.Add("firstName", firstName);
            dynamicParameters.Add("lastName", lastName);
            dynamicParameters.Add("middleName", middleName);
            dynamicParameters.Add("otherName", otherName);

            var oldRecord = await _dbContext.QueryFirstOrDefaultAsync(selectSql, dynamicParameters);
            dynamicParameters.Add("title", oldRecord != null ? (int)oldRecord.Title : 0);

            await _dbContext.ExecuteAsync(updateSql, dynamicParameters);
            await _dbContext.ExecuteAsync(insertSql, dynamicParameters);
        }

        public async Task<IEnumerable<Document>> GetDocumentsAsync(long consumerId)
        {
            const string sql = "SELECT * FROM [Document] WHERE ConsumerId = @consumerId";

            return await _dbContext.QueryAsync<Document>(sql, new { consumerId });
        }

        public async Task<AccountInfo> GetAccountInfoAsync(long consumerId)
        {
            const string  query = @"
SELECT CA.ConsumerId,
       CA.AccountId,
       C.ApplicationId,
       C.CountryId,
       C.[Classification]   Product,
       A.AccountStatus,
       C.CustomerId,
       A.AccountTypeId
FROM   Consumer C
            INNER JOIN ConsumerAccount CA ON CA.ConsumerId = C.Id
            INNER JOIN Account A          ON A.Id          = CA.AccountId
WHERE C.Id = @consumerId
";

            return await _dbContext.QueryFirstOrDefaultAsync<AccountInfo>(query, new { consumerId });
        }

        public async Task SetTrustScoreAsync(long consumerId, int trustScore)
        {
            const string sql = @"
UPDATE  Consumer
SET     TrustScore = @trustScore
WHERE   Id = @consumerId
";

            await _dbContext.ExecuteAsync(sql, new { consumerId, trustScore });
        }
    }
}
