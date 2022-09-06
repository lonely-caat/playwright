using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class MfaContext : IMfaContext
    {
        private readonly IDbContext _dbContext;

        public MfaContext(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public string CreateToken(string secret, long iterationNumber, string destination, MfaType mfaType, int digits = 6)
        {
            var counter = BitConverter.GetBytes(iterationNumber);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(counter);
            }

            var key = Encoding.ASCII.GetBytes(secret);
            var hmac = new HMACSHA1(key, true);
            var hash = hmac.ComputeHash(counter);
            var offset = hash[hash.Length - 1] & 0xf;

            // Convert the 4 bytes into an integer, ignoring the sign.
            var binary =
                ((hash[offset] & 0x7f) << 24)
                | (hash[offset + 1] << 16)
                | (hash[offset + 2] << 8)
                | (hash[offset + 3]);

            // Limit the number of digits
            var password = binary % (int)Math.Pow(10, digits);

            // Pad to required digits
            return password.ToString(new string('0', digits));
        }

        public async Task<int> ExpireTokensAsync(MfaEntityType mfaEntityType, long entityId)
        {
            const string sql = @"
UPDATE [SmsCode] 
   SET Active = 0 
 WHERE Active = 1 
   AND EntityType = @mfaEntityType 
   AND EntityId = @entityId
";

            return await _dbContext.ExecuteAsync(sql, new { mfaEntityType, entityId });
        }

        public async Task<MfaCode> GetAsync(long id)
        {
            const string sql = @"
SELECT * 
  FROM [SmsCode] 
 WHERE [Id]=@id
";

            return await _dbContext.QuerySingleOrDefaultAsync<MfaCode>(sql, new { id });
        }

        public async Task<MfaCode> GetTokenAsync(MfaEntityType mfaEntityType, long entityId)
        {
            const string sql = @"
SELECT * 
  FROM [SmsCode] 
 WHERE EntityType=@entityType 
   AND EntityId=@entityId 
 ORDER BY [TimeStamp] DESC
";

            return await _dbContext.QueryFirstOrDefaultAsync<MfaCode>(sql, new { entityType = mfaEntityType, entityId = entityId });
        }

        public async Task<MfaCode> SaveAsync(MfaCode code)
        {
            const string sql = @"
BEGIN TRAN

IF EXISTS (SELECT * FROM [SmsCode] WHERE [Id] = @Id)
BEGIN
  UPDATE [SmsCode] 
     SET Attempt=@Attempt,
         Active=1
   WHERE [Id]=@Id
END
ELSE
BEGIN
  INSERT INTO [SmsCode] 
    ( Attempt, Code, ConsumerId, Active, TimeStamp, PhoneNumber, Verified, EntityType, EntityId, Destination, MfaType )
  VALUES
    (@Attempt,@Code,@ConsumerId, 1     , GETDATE(),@PhoneNumber, 0       ,          1,@EntityId,@Destination,@MfaType )
END

SELECT CAST(SCOPE_IDENTITY() as bigint);
COMMIT TRAN";

            var id = await _dbContext.ExecuteScalarAsync<long>(sql, code);
            return await GetAsync(id);
        }
    }
}
