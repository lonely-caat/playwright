using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Orders.Mapper;
using Zip.Api.CustomerSummary.Application.Orders.Query.GetOrderSummary;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Interface;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Models.Requests;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Models.Responses;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Orders
{
    public class GetOrderSummaryQueryHandlerTests
    {
        private readonly GetOrderSummaryQueryHandler _target;
        
        private readonly Mock<IMerchantDashboardService> _merchantDashboardService;

        private readonly Fixture _fixture;

        public GetOrderSummaryQueryHandlerTests()
        {
            _merchantDashboardService = new Mock<IMerchantDashboardService>();
            _fixture = new Fixture();

            var logger = new Mock<ILogger<GetOrderSummaryQueryHandler>>();
            var configurationProvider = new MapperConfiguration(cfg => cfg.AddMaps(typeof(OrdersProfile)));
            var mapper = configurationProvider.CreateMapper();

            _target = new GetOrderSummaryQueryHandler(mapper, logger.Object, _merchantDashboardService.Object);
        }
        
        [Fact]
        public async Task Given_Valid_Request_Handle_Should_Work_Correctly()
        {
            // Arrange
            var request = _fixture.Create<GetOrderSummaryQuery>();
            var orderSummaryResponse = _fixture.Create<OrderSummaryResponse>();

            _merchantDashboardService.Setup(x => x.GetOrderSummaryAsync(It.Is<OrderSummaryRequest>(y => y.OrderId == request.OrderId), CancellationToken.None))
                                     .ReturnsAsync(orderSummaryResponse);

            // Act
            var actual = await _target.Handle(request, CancellationToken.None);

            // Assert
            actual.Should().BeEquivalentTo(orderSummaryResponse);
        }
    }
}
