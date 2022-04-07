using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.KleberService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.KleberService.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.KleberService
{
    public class AddressValidator : IAddressValidator
    {
        private readonly IAddressVerificationProxy _proxy;
        
        private readonly AddressVerificationProxyOptions _options;

        public AddressValidator(
            IAddressVerificationProxy proxy,
            IOptions<AddressVerificationProxyOptions> options)
        {
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
            _options = options.Value;
        }

        public bool IsEnabled => _options.Enabled;

        public async Task<VerifyAddressResponse> ValidateByKelberAsync(
            string unitNumber,
            string streetNumber,
            string streetName,
            string locality,
            string postcode,
            string state)
        {
            var verifyAddressRequest = new VerifyAddressRequest(unitNumber, streetNumber, streetName, locality, postcode, state);

            var verifyAddressResponse = await _proxy.VerifyAddress(_options.RequestKey, verifyAddressRequest);

            if (string.IsNullOrEmpty(verifyAddressResponse.DtResponse.ErrorMessage))
            {
                var failedValidationResult = verifyAddressResponse.DtResponse.Result
                       .Where(x => x.MatchType != "0")
                       .ToList();

                if (failedValidationResult.Any())
                {
                    var validationMessage = failedValidationResult
                           .Select(x => {
                                switch (x.MatchType)
                                {
                                    case "9":
                                    case "18":
                                        return "Unit number or level is incorrect or unspecified.";

                                   case "5":
                                   case "20":
                                        return "Address incorrect. Please input more address details.";

                                    default:
                                        return x.MatchTypeDescription;
                                }
                            })
                           .ToArray();
                    
                    throw new AddressValidationException(string.Join(Environment.NewLine, validationMessage));
                }
            }
            else
            {
                throw new AddressValidationException($"{verifyAddressResponse.DtResponse.ErrorMessage} - Kleber");
            }

            return verifyAddressResponse;
        }
    }
}
