using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Orders.Query.GetOrderInstallments;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Services.Accounts.Contract.Order;
using OrderDetailResponse = Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Models.OrderDetailResponse;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Orders
{
    public class GetOrderInstallmentsQueryHandlerTests : CommonTestsFixture
    {
        private readonly GetOrderInstallmentsQueryHandler _target;

        private readonly Mock<IAccountsService> _accountsService;
        
        public GetOrderInstallmentsQueryHandlerTests()
        {
            _accountsService = new Mock<IAccountsService>();
            var logger = new Mock<ILogger<GetOrderInstallmentsQueryHandler>>();

            _target = new GetOrderInstallmentsQueryHandler(_accountsService.Object, logger.Object);
        }
        
        [Fact]
        public async Task Given_ValidRequest_Should_Call_AccountsService_GetOrderDetail()
        {
            // Arrange
            var request = Fixture.Create<GetOrderInstallmentsQuery>();

            _accountsService.Setup(x => x.GetOrderDetail(
                                       It.Is<long>(y => y == request.AccountId),
                                       It.Is<long>(y => y == request.OrderId)))
                            .ReturnsAsync(Fixture.Create<OrderDetailResponse>());
            
            // Act
            var response = await _target.Handle(request, default);
            
            // Assert
            Assert.NotNull(response);
            _accountsService.Verify(x => x.GetOrderDetail(
                                        It.Is<long>(y => y == request.AccountId),
                                        It.Is<long>(y => y == request.OrderId)),
                Times.Once);
        }

        [Fact]
        public async Task Given_Exception_On_AccountsService_GetOrderDetail_ShouldThrow()
        {
            // Arrange
            var request = Fixture.Create<GetOrderInstallmentsQuery>();

            _accountsService.Setup(x => x.GetOrderDetail(
                                       It.Is<long>(y => y == request.AccountId),
                                       It.Is<long>(y => y == request.OrderId)))
                            .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _target.Handle(request, default));
        }
    }
}
