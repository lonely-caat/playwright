using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.SmsService.Interfaces
{
    public interface ISmsService
    {
        bool Sent { get; set; }

        SmsResponse SendSmsToConsumer(string phoneNumber, string message);

        SmsResponse SendAsync(
            string mobilePhone,
                string message,
                Dictionary<string, string> replacementValues = null);

        Task<SmsContent> GetSmsContentAsync(int id);

        Task<SmsContent> GetSmsContentByNameAsync(string name);
    }
}
