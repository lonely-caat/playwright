using Zip.Api.CustomerSummary.Domain.Entities.Countries;
using Zip.Api.CustomerSummary.Domain.Entities.Products;

namespace Zip.Api.CustomerSummary.Domain.Entities.PayNowUrlGenerator
{
    public class PayNowLinkAccount
    {
        public long AccountId { get; set; }

        public ProductClassification Classification { get; set; }

        public CountryCode CountryId { get; set; }
    }
}
