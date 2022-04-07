using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Beam;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Services.BeamService.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Beam.Query.PollReconciliationReport
{
    public class PollReconciliationReportQueryHandler : BaseRequestHandler<PollReconciliationReportQuery, PollReconciliationReportResponse>
    {
        private readonly IBeamService _beamService;

        public PollReconciliationReportQueryHandler(
            IBeamService beamService,
            ILogger<PollReconciliationReportQueryHandler> logger): base(logger)
        {
            _beamService = beamService;
        }

        protected override string HandlerName => nameof(PollReconciliationReportQueryHandler);

        public override async Task<PollReconciliationReportResponse> Handle(PollReconciliationReportQuery request, CancellationToken cancellationToken)
        {
            var logSuffix = $"for Request: {request.ToJsonString()}";
            try
            {
                LogInformation($"Start processing {nameof(PollReconciliationReportQuery)} {logSuffix}");

                var result = await _beamService.PollReconciliationReportAsync(request.Uuid, request.RequestedBy);

                LogInformation($"Successfully processed {nameof(PollReconciliationReportQuery)} with Result: {result?.ToJsonString()} {logSuffix}");

                return result;
            }
            catch (Exception ex)
            {
                LogError(ex, $"Failed to process {nameof(PollReconciliationReportQuery)} {logSuffix}");

                throw;
            }
        }
    }
}
