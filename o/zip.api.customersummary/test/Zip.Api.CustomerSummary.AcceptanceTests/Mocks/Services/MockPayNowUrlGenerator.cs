using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator.Models;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.Services
{
    public class MockPayNowUrlGenerator : IPayNowUrlGenerator
    {
        public Task<GeneratePayNowUrlResponse> GeneratePayNowUrlAsync(ProductClassification product, CountryCode country, decimal amount, string accountId)
        {
            return Task.FromResult(new GeneratePayNowUrlResponse
            {
                PayNowUrl = "http://test.zip.co",
                Reference = "190213kljsdklfdklj"
            });
        }

         public Task<GeneratePayNowUrlResponse> GeneratePayNowUrlAsync(GeneratePayNowUrlRequest generatePayNowUrlRequest)
        {
            return Task.FromResult(new GeneratePayNowUrlResponse
            {
                PayNowUrl = "http://test.zip.co",
                Reference = "190213kljsdklfdklj"
            });
        }

        public bool IsNewPayNowLink{ get; set; }
    }
}
