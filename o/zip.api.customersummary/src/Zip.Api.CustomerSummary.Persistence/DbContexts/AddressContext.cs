using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class AddressContext : IAddressContext
    {
        private readonly IDbContext _dbContext;
        private readonly ICountryContext _countryContext;

        public AddressContext(IDbContext dbContext, ICountryContext countryContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _countryContext = countryContext ?? throw new ArgumentNullException(nameof(countryContext));
        }

        public async Task<Address> GetAsync(long consumerId)
        {
            var sql = @"
SELECT TOP 1 Id,
        ISNULL(Suburb,'') Suburb,
        ISNULL(State,'') State,
        ISNULL(PostCode,'') PostCode,
        ISNULL(StreetName,'') StreetName,
        ISNULL(StreetNumber,'') StreetNumber,
        ISNULL(UnitNumber,'') UnitNumber,
        ISNULL(CountryCode,'') CountryCode 
  FROM Address (NOLOCK) 
 WHERE id IN (select AddressId from ConsumerAddress where ConsumerId = @consumerId) and Active = 1 and AddressType = 3 order by Id desc";

            var address = await _dbContext.QueryFirstOrDefaultAsync<Address>(sql, new { consumerId });
            if (address != null && !string.IsNullOrEmpty(address.CountryCode))
            {
                address.Country = await _countryContext.GetAsync(address.CountryCode);
            }

            return address;
        }

        public async Task UpdateAsync(long consumerId, Address address)
        {
            var updateSql = @"
UPDATE [Address] 
   SET Active=0 
 WHERE Active=1 
   AND AddressType = 3 
   AND [Id] IN 
    (SELECT AddressId 
       FROM ConsumerAddress 
      WHERE ConsumerId=@consumerId)";

            var selectSql = @"
INSERT INTO [dbo].[Address] 
    ([Suburb],[State],[PostCode],[StreetNumber],
     [StreetName],[UnitNumber],[TimeStamp],[CountryCode],
     [Active],[AddressType],[TimeAtAddress]) 
VALUES 
    (@Suburb,@State,@PostCode,@StreetNumber,
     @StreetName,@UnitNumber,getdate(),@CountryCode,
     1,3,1);
SELECT CAST(SCOPE_IDENTITY() as bigint)";

            var insertSql = "INSERT INTO ConsumerAddress (ConsumerId,AddressId) VALUES (@consumerId, @addressId);";


            await _dbContext.ExecuteAsync(updateSql, new { consumerId });
            var addressId = await _dbContext.ExecuteScalarAsync<long>(selectSql, address);
            await _dbContext.ExecuteAsync(insertSql, new { consumerId, addressId });
        }
    }
}
