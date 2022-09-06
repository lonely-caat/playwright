using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Interface;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Models.Requests;

namespace Zip.Api.CustomerSummary.Application.Orders.Query.GetOrderSummary
{
    public class GetOrderSummaryQueryHandler : IRequestHandler<GetOrderSummaryQuery, GetOrderSummaryResponse>
    {
        private readonly ILogger<GetOrderSummaryQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IMerchantDashboardService _merchantDashboardService;

        public GetOrderSummaryQueryHandler(
            IMapper mapper,
            ILogger<GetOrderSummaryQueryHandler> logger,
            IMerchantDashboardService merchantDashboardService)
        {
            _logger = logger;
            _mapper = mapper;
            _merchantDashboardService = merchantDashboardService;
        }
        
        public async Task<GetOrderSummaryResponse> Handle(GetOrderSummaryQuery request, CancellationToken cancellationToken)
        {
            var logSuffix = $"Request: {request.ToJsonString()}";

            _logger.LogInformation($"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                                   nameof(GetOrderSummaryQueryHandler),
                                   nameof(Handle),
                                   $"Start processing {nameof(GetOrderSummaryQuery)}. {logSuffix}");

            var orderSummaryRequest = _mapper.Map<OrderSummaryRequest>(request);
            var orderSummaryResponse = await _merchantDashboardService.GetOrderSummaryAsync(orderSummaryRequest, cancellationToken);

            var result = _mapper.Map<GetOrderSummaryResponse>(orderSummaryResponse);

            _logger.LogInformation($"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                                   nameof(GetOrderSummaryQueryHandler),
                                   nameof(Handle),
                                   $"Successfully processed {nameof(GetOrderSummaryQuery)}. {logSuffix}");

            return result;
        }
    }
}