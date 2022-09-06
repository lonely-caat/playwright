using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Infrastructure.Configuration.OutgoingMessages;
using Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator.Models;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator
{
    public class PayNowUrlGenerator : IPayNowUrlGenerator
    {
        private readonly IPaymentsServiceProxy _paymentsServiceProxy;
        private readonly IOutgoingMessagesConfig _outgoingMessageConfig;

        public PayNowUrlGenerator(IPaymentsServiceProxy paymentsServiceProxy, IOutgoingMessagesConfig outgoingMessageConfig)
        {
            _paymentsServiceProxy = paymentsServiceProxy ?? throw new ArgumentNullException(nameof(paymentsServiceProxy));
            _outgoingMessageConfig = outgoingMessageConfig ?? throw new ArgumentNullException(nameof(outgoingMessageConfig));
        }

        public async Task<GeneratePayNowUrlResponse> GeneratePayNowUrlAsync(ProductClassification product, CountryCode country, decimal amount, string accountId)
        {
            var reference = $"paynow:{product}:{accountId}:{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
            int amountInCents = Convert.ToInt32(amount * 100);
            var hashDetails = await _paymentsServiceProxy.GetHash(GetClassification(product), GetCountryCode(country), reference, amountInCents, "OnceOffCard");
            var url = $"{_outgoingMessageConfig.ZipPayNowBaseUrl}?merchant_name={hashDetails.Account}&reference={reference}&currency=AUD&amount={amountInCents}&hash={hashDetails.Hash}&utm_medium=SMS&utm_campaign=paynow";
            return new GeneratePayNowUrlResponse()
            {
                PayNowUrl = url,
                Reference = reference
            };
        }

        public async Task<GeneratePayNowUrlResponse> GeneratePayNowUrlAsync(GeneratePayNowUrlRequest generatePayNowUrlRequest)
        {
            throw new NotImplementedException();
        }

        private ZipMoney.Services.Payments.Contract.Types.ProductClassification GetClassification(ProductClassification productClassification)
        {
            if (productClassification == ProductClassification.zipBiz)
            {
                return ZipMoney.Services.Payments.Contract.Types.ProductClassification.zipBiz;
            }
            else if (productClassification == ProductClassification.zipMoney)
            {
                return ZipMoney.Services.Payments.Contract.Types.ProductClassification.zipMoney;
            }
            else if (productClassification == ProductClassification.zipPay)
            {
                return ZipMoney.Services.Payments.Contract.Types.ProductClassification.zipPay;
            }

            throw new ArgumentOutOfRangeException(nameof(productClassification));
        }

        private ZipMoney.Services.Payments.Contract.Types.CountryCode GetCountryCode(CountryCode countryCode)
        {
            if (countryCode == CountryCode.AU)
            {
                return ZipMoney.Services.Payments.Contract.Types.CountryCode.AU;
            }
            else if (countryCode == CountryCode.NZ)
            {
                return ZipMoney.Services.Payments.Contract.Types.CountryCode.NZ;
            }
            else if (countryCode == CountryCode.NotSet)
            {
                return ZipMoney.Services.Payments.Contract.Types.CountryCode.NotSet;
            }

            throw new ArgumentOutOfRangeException(nameof(countryCode));
        }
    }
}
