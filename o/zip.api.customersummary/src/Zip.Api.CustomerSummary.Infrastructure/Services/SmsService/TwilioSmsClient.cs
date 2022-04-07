using System;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.SmsService.Interfaces;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.SmsService
{
    public class TwilioSmsClient : ISmsClient
    {
        private readonly TwilioSettings _settings;

        public string CountryCode => _settings.CountryCode;

        public TwilioSmsClient(IOptions<TwilioSettings> configuration)
        {
            _settings = configuration?.Value ?? throw new ArgumentNullException(nameof(configuration));
        }

        public bool Send(string toNo, string message)
        {
            TwilioClient.Init(_settings.Sid, _settings.AuthToken);

            var messageResource = MessageResource.Create(
                body: message,
                @from: new PhoneNumber(_settings.FromNumber),
                to: new PhoneNumber(toNo)
            );
            
            return !string.IsNullOrEmpty(messageResource.Sid);
        }
    }
}
