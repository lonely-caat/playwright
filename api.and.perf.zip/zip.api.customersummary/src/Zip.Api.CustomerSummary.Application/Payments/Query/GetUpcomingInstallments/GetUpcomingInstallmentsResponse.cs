using System.Collections.Generic;
using System.Linq;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response;

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetUpcomingInstallments
{
    public class GetUpcomingInstallmentsResponse
    {
        public IEnumerable<Installment> Installments { get; set; }

        public decimal TotalAmount => Installments.Sum(x => x.Amount);
    }
}
