using ZipMoney.Services.Payments.Contract.PaymentMethods;

namespace Zip.Api.CustomerSummary.Domain.Entities.Dto
{
    public class PaymentMethodDto : PaymentMethodResponse
    {
        public string StateString => State.ToString();

        public string CountryCodeString => CountryCode.ToString();

        public string TypeString => Type.ToString();
    }
}
