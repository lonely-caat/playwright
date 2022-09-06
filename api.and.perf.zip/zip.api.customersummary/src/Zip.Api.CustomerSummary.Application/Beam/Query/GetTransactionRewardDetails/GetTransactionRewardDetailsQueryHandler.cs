using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Beam;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Services.BeamService.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Beam.Query.GetTransactionRewardDetails
{
    public class
        GetTransactionRewardDetailsQueryHandler : BaseRequestHandler<GetTransactionRewardDetailsQuery,
            TransactionRewardDetailsResponse>
    {
        private readonly IBeamService _beamService;

        public GetTransactionRewardDetailsQueryHandler(
            IBeamService beamService,
            ILogger<GetTransactionRewardDetailsQueryHandler> logger) : base(logger)
        {
            _beamService = beamService;
        }

        protected override string HandlerName => nameof(GetTransactionRewardDetailsQueryHandler);

        public override async Task<TransactionRewardDetailsResponse> Handle(GetTransactionRewardDetailsQuery request, CancellationToken cancellationToken)
        {
            var logSuffix = $"for Request: {request.ToJsonString()}";

            try
            {
                LogInformation($"Start processing {nameof(GetTransactionRewardDetailsQuery)} {logSuffix}");

                var result =
                    await _beamService.GetTransactionRewardDetailsAsync(request.CustomerId, request.TransactionId);

                LogInformation( $"Successfully processed {nameof(GetTransactionRewardDetailsQuery)} with Result {result?.ToJsonString()} {logSuffix}");

                return result;
            }
            catch (Exception ex)
            {
                LogError(ex,$"Failed to process {nameof(GetTransactionRewardDetailsQuery)} {logSuffix} with Message: {ex.Message}");

                throw;
            }
        }
    }
}