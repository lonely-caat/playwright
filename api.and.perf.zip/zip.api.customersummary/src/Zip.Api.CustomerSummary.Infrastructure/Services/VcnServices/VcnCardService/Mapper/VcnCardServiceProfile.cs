using AutoMapper;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Mapper
{
    public class VcnCardServiceProfile : Profile
    {
        public VcnCardServiceProfile()
        {
            CreateMap<RootCardsResponse, RootCards>()
               .ForMember(dest => dest.Cards, opt => opt.MapFrom(src => src.Cards));

            CreateMap<CardResponse, Card>()
               .ForMember(dest => dest.DigitalWalletTokens, opt => opt.MapFrom(src => src.DigitalWalletTokens));
            
            CreateMap<DigitalWalletTokenResponse, DigitalWalletToken>();
            
            CreateMap<DeviceResponse, Device>();
            
            CreateMap<TokenServiceProviderResponse, TokenServiceProvider>()
               .ForMember(dest => dest.TokenAssuranceLevel, opt => opt.MapFrom(src => src.TokenAssurancelevel));
        }
    }
}
