using System;

namespace Zip.Api.CustomerSummary.Domain.Entities.Statement
{
    public class StatementDate
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public StatementDate(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public StatementDate()
        {

        }
    }
}
