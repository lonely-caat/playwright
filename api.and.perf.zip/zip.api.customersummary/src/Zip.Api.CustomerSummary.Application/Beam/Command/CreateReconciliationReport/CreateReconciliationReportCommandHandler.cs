using System;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Beam;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Services.BeamService.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Beam.Command.CreateReconciliationReport
{
    public class CreateReconciliationReportCommandHandler : BaseRequestHandler<CreateReconciliationReportCommand, CreateReconciliationReportResponse>
    {
        private readonly IBeamService _beamService;

        public CreateReconciliationReportCommandHandler(
            IBeamService beamService,
            ILogger<CreateReconciliationReportCommandHandler> logger) : base(logger)
        {
            _beamService = beamService;
        }

        protected override string HandlerName => nameof(CreateReconciliationReportCommandHandler);

        public override async Task<CreateReconciliationReportResponse> Handle(CreateReconciliationReportCommand request, CancellationToken cancellationToken)
        {
            var logSuffix = $"for {request.ToJsonString()}";
            try
            {
                LogInformation($"Start processing {nameof(CreateReconciliationReportCommand)} {logSuffix}");

                var result = await _beamService.CreateReconciliationReportAsync(request.SelectedDate, request.RequestedBy, request.Region);

                LogInformation($"Successfully processed {nameof(CreateReconciliationReportCommand)} with Result: {result?.ToJsonString()} {logSuffix}");

                return result;
            }
            catch (Exception ex)
            {
                LogError(ex, $"Failed to process {nameof(CreateReconciliationReportCommand)} {logSuffix}");

                throw;
            }
        }
    }
}
