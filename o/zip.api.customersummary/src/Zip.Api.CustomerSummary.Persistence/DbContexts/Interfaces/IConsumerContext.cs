using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface IConsumerContext
    {
        Task<Consumer> GetAsync(long consumerId);

        Task<Consumer> GetByCustomerIdAndProductAsync(Guid customerId, int product);

        Task<Consumer> GetAsyncV2(long consumerId);

        Task UpdateNameAsync(long consumerId, string firstName, string lastName, string middleName, string otherName);

        Task UpdateDateOfBirthAsync(long consumerId, DateTime dateOfBirth);

        Task<long?> GetLinkedConsumerIdAsync(long consumerId);

        Task UpdateGenderAsync(long consumerId, Gender gender);

        Task<IEnumerable<Document>> GetDocumentsAsync(long consumerId);

        Task<AccountInfo> GetAccountInfoAsync(long consumerId);

        Task SetTrustScoreAsync(long consumerId, int trustScore);
    }
}