﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Zip.Api.CustomerSummary.Domain.Entities.Statement
{
    [ExcludeFromCodeCoverage]
    public class BillingPeriodClosedDetails
    {
        public long CustomerId { get; set; }

        public long AccountId { get; set; }

        public decimal OpeningBalance { get; set; }

        public decimal ClosingBalance { get; set; }

        public decimal AccountCreditLimit { get; set; }

        public decimal MinimumPayment { get; set; }

        public decimal MinimumMonthlyPayment { get; set; }

        public decimal MinimumMonthlyPercentage { get; set; }

        public DateTime DueDate { get; set; }

        public string StatementName { get; set; }

        public DateTime StartOfPeriod { get; set; }

        public DateTime EndOfPeriod { get; set; }

        public decimal InterestFreeBalance { get; set; }

        public decimal InterestRate { get; set; }

        public string CountryId { get; set; }
    }
}
