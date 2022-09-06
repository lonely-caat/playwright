namespace Zip.Api.CustomerSummary.Infrastructure.Services.SmsService.Interfaces
{
    public interface ISmsClient
    {
        bool Send(string toNo, string message);
    }
}
