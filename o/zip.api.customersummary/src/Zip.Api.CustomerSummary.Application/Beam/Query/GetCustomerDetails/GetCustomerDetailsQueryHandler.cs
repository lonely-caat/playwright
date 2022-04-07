using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Beam;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Services.BeamService.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Beam.Query.GetCustomerDetails
{
    public class GetCustomerDetailsQueryHandler : BaseRequestHandler<GetCustomerDetailsQuery, CustomerDetails>
    {
        private readonly IBeamService _beamService;

        public GetCustomerDetailsQueryHandler(
            IBeamService beamService,
            ILogger<GetCustomerDetailsQueryHandler> logger) : base(logger)
        {
            _beamService = beamService;
        }

        protected override string HandlerName => nameof(GetCustomerDetailsQueryHandler);

        public override async Task<CustomerDetails> Handle(GetCustomerDetailsQuery request, CancellationToken cancellationToken)
        {
            var logSuffix = $"for Request: {request.ToJsonString()}";
            try
            {
                LogInformation($"Start processing {nameof(GetCustomerDetailsQuery)} {logSuffix}");
            
                var result = await _beamService.GetCustomerDetails(request.CustomerId, request.Region);

                LogInformation($"Successfully processed {nameof(GetCustomerDetailsQuery)} with Result: {result?.ToJsonString()} {logSuffix}");

                return result;
            }
            catch (Exception ex)
            {
                LogError(ex, $"Failed to process {nameof(GetCustomerDetailsQuery)} {logSuffix}");

                throw;
            }
        }
    }
}
