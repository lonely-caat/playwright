using System.Collections.Generic;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Models
{
    public class GenerateStatementsRequest
    {
        public List<string> Accounts { get; set; } = new List<string>();

        public string Classification { get; set; }

        public string StatementDate { get; set; }
    }
}
