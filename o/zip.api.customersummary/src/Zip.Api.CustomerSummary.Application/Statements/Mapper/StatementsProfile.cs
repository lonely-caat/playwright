using AutoMapper;
using Zip.Api.CustomerSummary.Application.Statements.Command.GenerateStatement;
using Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Models;

namespace Zip.Api.CustomerSummary.Application.Statements.Mapper
{
    public class StatementsProfile : Profile
    {
        public StatementsProfile()
        {
            CreateMap<GenerateStatementCommand, GenerateStatementsRequest>()
               .ForMember(dest => dest.StatementDate, opt => opt.MapFrom(src => src.EndDate.Date.ToString("yyyy-MM-dd")))
               .ForMember(dest => dest.Classification, opts => opts.MapFrom((src, dest, destMember, context) => context.Items["Classification"]))
               .AfterMap((src, dest) => dest.Accounts.Add(src.AccountId.ToString()));
        }
    }
}
