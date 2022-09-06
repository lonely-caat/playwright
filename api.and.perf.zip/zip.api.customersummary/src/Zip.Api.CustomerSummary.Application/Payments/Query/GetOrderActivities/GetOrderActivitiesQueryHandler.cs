using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using Serilog;
using Zip.Api.CustomerSummary.Domain.Entities.Dto.OrderActivity;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetOrderActivities
{
    public class GetOrderActivitiesQueryHandler : IRequestHandler<GetOrderActivitiesQuery, IEnumerable<OrderActivityDto>>
    {
        private readonly IConsumerOperationRequestContext _consumerOperationRequestContext;

        public GetOrderActivitiesQueryHandler(IConsumerOperationRequestContext consumerOperationRequestContext)
        {
            _consumerOperationRequestContext = consumerOperationRequestContext ?? throw new ArgumentNullException(nameof(_consumerOperationRequestContext));
        }

        public async Task<IEnumerable<OrderActivityDto>> Handle(GetOrderActivitiesQuery request, CancellationToken cancellationToken)
        {
            var fromDate = request.FromDate ?? DateTime.Now.AddMonths(-2).AddDays(-1);
            var toDate = request.ToDate ?? DateTime.Now.AddDays(1);
            var orderActivitiesWithinPastTwoMonths = await _consumerOperationRequestContext.GetOrderActivitiesAsync(request.ConsumerId, fromDate, toDate);

            if (!request.ShowAll)
            {
                // filter out Operation Requests that are parents
                var parents = orderActivitiesWithinPastTwoMonths.Where(x => x.ParentOperationRequestId != null)
                    .Select(x => x.ParentOperationRequestId);

                orderActivitiesWithinPastTwoMonths = orderActivitiesWithinPastTwoMonths.Where(x => !parents.Contains(x.Id)).ToList();
            }

            foreach (var order in orderActivitiesWithinPastTwoMonths)
            {
                if (order.Metadata != null)
                {
                    try
                    {
                        dynamic metadata = JsonConvert.DeserializeObject(order.Metadata);

                        order.ShippingAddress = metadata.shipping_address != null ? metadata.shipping_address.OneLineAddress.ToString() : string.Empty;
                        order.Amount = metadata.order != null ? metadata.order.total : 0;
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, ex.Message, request.ConsumerId);
                        continue;
                    }    
                }
            }
            
            return orderActivitiesWithinPastTwoMonths;
        }
    }
}
