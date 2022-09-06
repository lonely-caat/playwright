using System;
using ZipMoney.Services.Payments.Contract.Payments;

namespace Zip.Api.CustomerSummary.Domain.Entities.Dto
{
    public class PaymentDto : PaymentResponse
    {
        public string CountryCodeString => CountryCode.ToString();
        public string GatewayString => Gateway.ToString();
        public string MethodTypeString => MethodType.ToString();
        public string TypeString => Type.ToString();
        public string StatusString => Status.ToString();
        public DateTime CreatedDateTimeLocal => CreatedDateTime.ToLocalTime();
    }
}
