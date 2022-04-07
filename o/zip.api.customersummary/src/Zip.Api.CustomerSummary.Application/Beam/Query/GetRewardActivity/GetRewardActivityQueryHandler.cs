using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Common;
using Zip.Api.CustomerSummary.Domain.Entities.Beam;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Services.BeamService.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Beam.Query.GetRewardActivity
{
    public class GetRewardActivityQueryHandler : BaseRequestHandler<GetRewardActivityQuery, Pagination<RewardActivity>>
    {
        private readonly IBeamService _beamService;

        public GetRewardActivityQueryHandler(
            IBeamService beamService,
            ILogger<GetRewardActivityQueryHandler> logger,
            IMapper mapper) : base(logger, mapper)
        {
            _beamService = beamService;
        }

        protected override string HandlerName => nameof(GetRewardActivityQueryHandler);

        public override async Task<Pagination<RewardActivity>> Handle(GetRewardActivityQuery request, CancellationToken cancellationToken)
        {
            var logSuffix = $"for Request: {request.ToJsonString()}";
            try
            {
                LogInformation($"Start processing {nameof(GetRewardActivityQuery)} {logSuffix}");
                
                var (skip, take) = ConvertToSkipAndTake(request.PageNumber, request.PageSize);

                var result = await _beamService.GetRewardActivityAsync(request.CustomerId, skip, take, request.Region);

                var response = Mapper.Map<Pagination<RewardActivity>>(result);
                if (response == null)
                {
                    return null;
                }

                response.Current = request.PageNumber;
                response.PageSize = request.PageSize;
                
                LogInformation($"Successfully processed {nameof(GetRewardActivityQuery)} with Result: {response?.ToJsonString()} {logSuffix}");

                return response;
            }
            catch (Exception ex)
            {
                LogError(ex, $"Failed to process {nameof(GetRewardActivityQuery)} {logSuffix}");

                throw;
            }
        }

        private (long, long) ConvertToSkipAndTake(long pageNumber, long pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;

            return (skip, take);
        }
    }
}
