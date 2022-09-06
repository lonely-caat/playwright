using AutoMapper;
using Zip.Api.CustomerSummary.Domain.Common;
using Zip.Api.CustomerSummary.Domain.Entities.Beam;

namespace Zip.Api.CustomerSummary.Application.Beam.Mapper
{
    public class BeamProfile : Profile
    {
        public BeamProfile()
        {
            CreateMap<RewardActivityResponse, Pagination<RewardActivity>>()
                .ForMember(dest => dest.TotalCount,
                    o => o.MapFrom((src, dest) => src.TotalCount))
                .ForMember(dest => dest.Items,
                    o => o.MapFrom((src, dest) => src.Elements));
        }
    }
}
