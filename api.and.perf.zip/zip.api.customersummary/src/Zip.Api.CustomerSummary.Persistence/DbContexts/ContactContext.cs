using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class ContactContext : IContactContext
    {
        private readonly IDbContext _dbContext;

        public ContactContext(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ContactDto> GetContactAsync(long consumerId)
        {
            var sql = @"
SELECT C.Id ConsumerId, C.Email, C.AuthorityFullName, C.AuthorityDateOfBirth, C.AuthorityMobile,
  (SELECT TOP 1 P.PhoneNumber 
     FROM ConsumerPhone CP 
    INNER JOIN Phone P ON CP.PhoneId = P.Id 
    WHERE P.Active = 1 AND CP.ConsumerId = C.Id) Mobile 
  FROM Consumer C 
  WHERE C.Id=@consumerId ORDER BY C.Id";

            return await _dbContext.QueryFirstOrDefaultAsync<ContactDto>(sql, new { consumerId });
        }

        public async Task<string> GetMobileAsync(long consumerId)
        {
            const string sql = @"
SELECT TOP 1 P.PhoneNumber 
     FROM ConsumerPhone CP 
    INNER JOIN Phone P ON CP.PhoneId = P.Id 
    WHERE P.Active = 1 AND CP.ConsumerId = @consumerId
";

            return await _dbContext.ExecuteScalarAsync<string>(sql, new { consumerId });
        }
    }
}
