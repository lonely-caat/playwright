using System.Collections.Generic;
using System.Linq;
using Zip.Api.CustomerSummary.Domain.Entities.Sms;
using Zip.Api.CustomerSummary.Infrastructure.Common.Utilities;
using Zip.Api.CustomerSummary.Infrastructure.Services.SmsService.Interfaces;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.SmsService
{
    public class SmsClientContextService : ISmsClientContextService
    {
        private static readonly List<string> TestMobiles = new List<string> { "0400000000", "02000000000" };

        public SmsClientContext GetContextByPhoneNumber(string phoneNumber)
        {
            if (TestMobiles.Select(PhoneNumberHelper.ToMobileIntFormat).Contains(PhoneNumberHelper.ToMobileIntFormat(phoneNumber)))
            {
                return new SmsClientContext
                {
                    IsTest = true,
                    Token = "123456"
                };
            }

            return new SmsClientContext
            {
                IsTest = false
            };
        }

        public SmsClientContext GetContextByEmail(string email)
        {
            if (email.ToLowerInvariant().EndsWith("@mailinator.com"))
            {
                return new SmsClientContext
                {
                    IsTest = true,
                    Token = "123456"
                };
            }

            return new SmsClientContext
            {
                IsTest = false
            };
        }
    }
}
