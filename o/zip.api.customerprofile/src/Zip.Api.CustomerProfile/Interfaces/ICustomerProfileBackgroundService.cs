using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zip.Api.CustomerProfile.Interfaces
{
    public interface ICustomerProfileBackgroundService
    {
        Task DeleteCustomerById(Guid id, string correlationId, CancellationToken cancellationToken = default);
        Task UpdateCustomerById(Guid id, string correlationId, CancellationToken cancellationToken = default);
    }
}