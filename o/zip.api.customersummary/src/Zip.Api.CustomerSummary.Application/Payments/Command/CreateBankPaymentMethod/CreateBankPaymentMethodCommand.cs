using MediatR;
using ZipMoney.Services.Payments.Contract.PaymentMethods;

namespace Zip.Api.CustomerSummary.Application.Payments.Command.CreateBankPaymentMethod
{
    public class CreateBankPaymentMethodCommand : IRequest<PaymentMethodResponse>
    {
        public long ConsumerId { get; set; }

        public string AccountName { get; set; }

        public string AccountNumber { get; set; }

        public string BSB { get; set; }

        public string OriginatorEmail { get; set; }
        
        public bool IsDefault { get; set; }
    }
}
