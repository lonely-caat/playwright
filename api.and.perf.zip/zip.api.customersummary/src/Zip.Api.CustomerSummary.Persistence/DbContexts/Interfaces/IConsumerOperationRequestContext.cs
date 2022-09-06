using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Dto.OrderActivity;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface IConsumerOperationRequestContext
    {
        Task<IEnumerable<OrderActivityDto>> GetOrderActivitiesAsync(long consumerId, DateTime fromDate, DateTime toDate);
    }
}