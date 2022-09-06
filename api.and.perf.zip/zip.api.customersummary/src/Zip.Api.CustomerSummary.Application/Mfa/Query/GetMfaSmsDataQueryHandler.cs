using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Mfa;
using Zip.Api.CustomerSummary.Infrastructure.Services.MfaService.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Mfa.Query
{
    public class GetMfaSmsDataQueryHandler : IRequestHandler<GetMfaSmsDataQuery, MfaSmsDataResponse>
    {
        private readonly IMfaService _mfaService;

        public GetMfaSmsDataQueryHandler(IMfaService mfaService)
        {
            _mfaService = mfaService;
        }

        public async Task<MfaSmsDataResponse> Handle(GetMfaSmsDataQuery request, CancellationToken cancellationToken)
        {
            return await _mfaService.GetMfaSmsDataAsync(request.ConsumerId);
        }
    }
}