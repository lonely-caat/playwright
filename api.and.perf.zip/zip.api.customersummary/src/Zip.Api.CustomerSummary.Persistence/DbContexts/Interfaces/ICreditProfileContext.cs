using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface ICreditProfileContext
    {
        Task<CreditProfileState> GetStateTypeByConsumerIdAsync(long consumerId);

        Task<IEnumerable<ProfileClassification>> GetProfileClassificationsAsync(CreditProfileStateType creditProfileStateType);

        Task<IEnumerable<ProfileAttribute>> GetProfileAttributesAsync(CreditProfileStateType creditProfileStateType);

        Task CreateCreditProfileStateAsync(CreditProfileState creditProfileState);

        Task CreateCreditProfileAttributeAsync(long creditProfileId, long profileAttributeId);

        Task CreateCreditProfileClassificationAsync(long creditProfileId, long profileClassificationId);

        Task<string> GetProfileDateOfClosureAsync(long consumerId);
    }
}