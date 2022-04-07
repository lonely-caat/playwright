using System;
using System.Collections.Generic;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Dto.OrderActivity;

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetOrderActivities
{
    public class GetOrderActivitiesQuery : IRequest<IEnumerable<OrderActivityDto>>
    {
        public long ConsumerId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool ShowAll { get; set; }

        public GetOrderActivitiesQuery(long consumerId, DateTime? fromDate, DateTime? toDate, bool showAll)
        {
            ConsumerId = consumerId;
            FromDate = fromDate;
            ToDate = toDate;
            ShowAll = showAll;
        }
    }
}
