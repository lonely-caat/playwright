using Zip.Api.CustomerSummary.Domain.Entities.Products;

namespace Zip.Api.CustomerSummary.Domain.Entities.Accounts
{
    public class AccountType
    {
        public long Id { get; set; }

        public string ReferenceCode { get; set; }

        public ProductClassification Classification { get; set; }

        public decimal MinimumMonthlyRepayment { get; set; }

        public decimal MonthlyFee { get; set; }

        public decimal MinimumRepaymentPercentage { get; set; }

        public long ProductId { get; set; }

        public decimal LatePaymentFee { get; set; }
    }
}
