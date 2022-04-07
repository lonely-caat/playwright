using AutoMapper;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateLoginStatus;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Kinesis;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Models;

namespace Zip.Api.CustomerSummary.Application.Consumers.Mapper
{
    public class ConsumersProfile : Profile
    {
        public ConsumersProfile()
        {
            CreateMap<Address, AddressDetail>()
               .ForMember(dest => dest.PostCode, member => member.MapFrom(source => source.PostCode))
               .ForMember(dest => dest.UnitNumber, member => member.MapFrom(source => source.UnitNumber))
               .ForMember(dest => dest.Suburb, member => member.MapFrom(source => source.Suburb))
               .ForMember(dest => dest.StreetNumber, member => member.MapFrom(source => source.StreetNumber))
               .ForMember(dest => dest.StreetName, member => member.MapFrom(source => source.StreetName))
               .ForMember(dest => dest.State, member => member.MapFrom(source => source.State));
        }
    }
}
