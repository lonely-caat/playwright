using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator.Interfaces
{
    public interface IPayNowUrlGenerator
    {
        Task<GeneratePayNowUrlResponse> GeneratePayNowUrlAsync(ProductClassification product, CountryCode country, decimal amount, string accountId);

        Task<GeneratePayNowUrlResponse> GeneratePayNowUrlAsync(GeneratePayNowUrlRequest generatePayNowUrlRequest);
    }
}
