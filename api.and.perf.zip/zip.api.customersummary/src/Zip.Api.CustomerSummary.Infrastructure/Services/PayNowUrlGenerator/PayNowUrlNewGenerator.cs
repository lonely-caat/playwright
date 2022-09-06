using System;
using System.Threading.Tasks;
using Serilog;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator.Models;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.PayNow;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.PayNow.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator
{
    public class PayNowUrlNewGenerator : IPayNowUrlGenerator
    {
        private readonly IPayNowLinkServiceProxy _paynowlinkServiceProxy;

        public PayNowUrlNewGenerator(IPayNowLinkServiceProxy paynowlinkServiceProxy)
        {
            _paynowlinkServiceProxy = paynowlinkServiceProxy ?? throw new ArgumentNullException(nameof(paynowlinkServiceProxy));
        }

        public async Task<GeneratePayNowUrlResponse> GeneratePayNowUrlAsync(ProductClassification product, CountryCode country, decimal amount, string accountId)
        {
            throw new NotImplementedException();
        }

        public async Task<GeneratePayNowUrlResponse> GeneratePayNowUrlAsync(GeneratePayNowUrlRequest generatePayNowUrlRequest)
        {
            try
            {
                var payNowLinkRequest = new PayNowLinkRequest
                {
                    ConsumerId = generatePayNowUrlRequest.ConsumerId,
                    Amount = generatePayNowUrlRequest.Amount
                };
                
                var response = await _paynowlinkServiceProxy.PayNowLinkAsync(payNowLinkRequest);

                return new GeneratePayNowUrlResponse
                {
                    PayNowUrl = response.PayNowUrl,
                    Reference = response.Reference
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"{nameof(PayNowUrlNewGenerator)}.{nameof(GeneratePayNowUrlAsync)} :: Error while getting paynowlink");
                
                return null;
            }
        }
    }
}
