using MediatR;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetUpcomingInstallments
{
    public class GetUpcomingInstallmentsQuery : IRequest<GetUpcomingInstallmentsResponse>
    {
        [FromQuery]
        public long AccountId { get; set; }

        [FromQuery]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Data to be queried will be up to this date
        /// </summary>
        [FromQuery]
        public DateTime ToDate { get; set; }
    }
}
