using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Utilities;
using Zip.Api.CustomerSummary.Infrastructure.Services.SmsService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.SmsService
{
    public class SmsService : ISmsService
    {
        private readonly ISmsClient _smsClient;
        private readonly ISmsContentContext _smsContentContext;
        private readonly ISmsClientContextService _smsClientContextService;

        public SmsService(
            ISmsClient smsClient,
            ISmsContentContext smsContext,
            ISmsClientContextService smsClientContextService)
        {
            _smsContentContext = smsContext ?? throw new ArgumentNullException(nameof(smsContext));
            _smsClient = smsClient ?? throw new ArgumentNullException(nameof(smsClient));
            _smsClientContextService = smsClientContextService ?? throw new ArgumentNullException(nameof(smsClientContextService));
        }

        public bool Sent { get; set; }

        public async Task<SmsContent> GetSmsContentAsync(int id)
        {
            return await _smsContentContext.GetAsync(id);
        }

        public async Task<SmsContent> GetSmsContentByNameAsync(string name)
        {
            return await _smsContentContext.GetAsync(name);
        }

        public SmsResponse SendAsync(
            string mobilePhone,
            string message,
            Dictionary<string, string> replacementValues = null)
        {
            message = message.ReplaceTags(replacementValues);
            
            var response = SendSmsToConsumer(mobilePhone, message);

            return response;
        }

        public SmsResponse SendSmsToConsumer(string phoneNumber, string message)
        {
            Sent = false;
            var response = new SmsResponse { Success = false };

            // skip sending messages to test numbers
            var smsClientContext = _smsClientContextService.GetContextByPhoneNumber(phoneNumber);
            
            if (smsClientContext.IsTest)
            {
                response.Success = true;
                return response;
            }

            try
            {
                var numberToSms = phoneNumber != null
                        ? PhoneNumberHelper.ToMobileIntFormat(phoneNumber)
                        : string.Empty;
                
                response.Success = _smsClient.Send(numberToSms, message);
                
                Log.Debug($"Sms sent to {numberToSms} using {_smsClient.GetType().Name} provider with result {response.Success}.");
                
                Sent = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                
                Log.Error(ex, "SmsService.SendSmsToConsumerAsync :: phoneNumber:{phoneNumber}, message:{message}", phoneNumber, message);
            }

            return response;
        }
    }
}
