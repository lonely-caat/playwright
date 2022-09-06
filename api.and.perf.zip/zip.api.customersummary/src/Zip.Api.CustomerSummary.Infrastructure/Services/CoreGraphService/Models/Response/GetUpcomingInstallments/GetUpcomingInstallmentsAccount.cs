using System.Collections.Generic;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response
{
    public class GetUpcomingInstallmentsAccount
    {
        public IEnumerable<Installment> UpcomingInstallments { get; set; }
    }
}
