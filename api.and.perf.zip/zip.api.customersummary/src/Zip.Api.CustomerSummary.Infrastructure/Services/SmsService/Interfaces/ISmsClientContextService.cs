using Zip.Api.CustomerSummary.Domain.Entities.Sms;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.SmsService.Interfaces
{
    public interface ISmsClientContextService
    {
        SmsClientContext GetContextByPhoneNumber(string phoneNumber);

        SmsClientContext GetContextByEmail(string email);
    }
}
