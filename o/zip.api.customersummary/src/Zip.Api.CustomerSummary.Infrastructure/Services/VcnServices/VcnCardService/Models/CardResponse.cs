using System;
using System.Collections.Generic;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Models
{
    public class CardResponse
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int Balance { get; set; }

        public string MaskedCardNumber { get; set; }

        public string ExternalId { get; set; }

        public DateTime PlasticExpiry { get; set; }

        public string Product { get; set; }

        public string Status { get; set; }

        public List<DigitalWalletTokenResponse> DigitalWalletTokens { get; set; }

        public string CustomerId { get; set; }

        public string AccountId { get; set; }

        public string MerchantId { get; set; }

        public string FormattedAccountId { get; set; }

        public int Usages { get; set; }

        public string MerchantURL { get; set; }
    }

    public class CardsResponse
    {
        public CardResponse[] Cards { get; set; }
    }
}