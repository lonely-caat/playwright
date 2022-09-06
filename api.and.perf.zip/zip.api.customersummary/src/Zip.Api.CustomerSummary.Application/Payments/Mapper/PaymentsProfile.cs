using AutoMapper;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Domain.Entities.Payments;
using Zip.Api.CustomerSummary.Domain.Entities.Tango;
using ZipMoney.Services.Payments.Contract.PaymentMethods;
using ZipMoney.Services.Payments.Contract.Payments;
using Frequency = Zip.Api.CustomerSummary.Domain.Entities.Tango.Frequency;

namespace Zip.Api.CustomerSummary.Application.Payments.Mapper
{
    public class PaymentsProfile : Profile
    {
        public PaymentsProfile()
        {
            CreateMap<PaymentResponse, PaymentDto>();

            CreateMap<PaymentMethodResponse, PaymentMethodDto>();

            CreateMap<Domain.Entities.Tango.Frequency, Frequency>()
               .ConvertUsing<FrequencyTypeConverter>();

            CreateMap<Frequency, Domain.Entities.Tango.Frequency>()
               .ConvertUsing<FrequencyReverseTypeConverter>();

            CreateMap<LoanMgtRepaymentScheduleVariation, Repayment>()
               .ForMember(dest => dest.Amount, o => o.MapFrom((source, dest) => source.OverrideRepaymentAmount))
               .ForMember(dest => dest.Frequency, o => o.MapFrom((source, dest) => source.GetFrequency()))
               .ForMember(dest => dest.StartDate, o => o.MapFrom(src => src.RepaymentVariationStart));
        }
    }
}
