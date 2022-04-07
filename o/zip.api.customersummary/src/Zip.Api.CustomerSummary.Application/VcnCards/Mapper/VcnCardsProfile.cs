using AutoMapper;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.BlockDigitalWalletToken;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.TerminateDigitalWalletToken;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.UnblockDigitalWalletToken;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.VerifyDigitalWalletToken;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Models;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Mapper
{
    public class VcnCardsProfile : Profile
    {
        public VcnCardsProfile()
        {
            CreateMap<BlockDigitalWalletTokenCommand, TokenTransitionRequest>()
               .ForMember(dest => dest.DigitalWalletToken, opt => opt.MapFrom(src => src.DigitalWalletToken))
               .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => Vcn.DigitalWalletTokenProvider))
               .ForMember(dest => dest.State, opt => opt.MapFrom(src => nameof(DigitalWalletTokenState.SUSPENDED)))
               .ForMember(dest => dest.ReasonCode, opt => opt.MapFrom(src => Vcn.DigitalWalletTokenTransitionReasonCode_01));

            CreateMap<TerminateDigitalWalletTokenCommand, TokenTransitionRequest>()
               .ForMember(dest => dest.DigitalWalletToken, opt => opt.MapFrom(src => src.DigitalWalletToken))
               .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => Vcn.DigitalWalletTokenProvider))
               .ForMember(dest => dest.State, opt => opt.MapFrom(src => nameof(DigitalWalletTokenState.TERMINATED)))
               .ForMember(dest => dest.ReasonCode, opt => opt.MapFrom(src => Vcn.DigitalWalletTokenTransitionReasonCode_01));

            CreateMap<UnblockDigitalWalletTokenCommand, TokenTransitionRequest>()
               .ForMember(dest => dest.DigitalWalletToken, opt => opt.MapFrom(src => src.DigitalWalletToken))
               .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => Vcn.DigitalWalletTokenProvider))
               .ForMember(dest => dest.State, opt => opt.MapFrom(src => nameof(DigitalWalletTokenState.ACTIVE)))
               .ForMember(dest => dest.ReasonCode, opt => opt.MapFrom(src => Vcn.DigitalWalletTokenTransitionReasonCode_01));

            CreateMap<VerifyDigitalWalletTokenCommand, TokenTransitionRequest>()
               .ForMember(dest => dest.DigitalWalletToken, opt => opt.MapFrom(src => src.DigitalWalletToken))
               .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => Vcn.DigitalWalletTokenProvider))
               .ForMember(dest => dest.State, opt => opt.MapFrom(src => nameof(DigitalWalletTokenState.ACTIVE)))
               .ForMember(dest => dest.ReasonCode, opt => opt.MapFrom(src => Vcn.DigitalWalletTokenTransitionReasonCode_18));
        }
    }
}
