using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetUpcomingInstallments;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Payments
{
    public class GetUpcomingInstallmentsQueryHandlerTests : CommonTestsFixture
    {
        private readonly GetUpcomingInstallmentsQueryHandler _target;

        private readonly Mock<ICoreGraphService> _coreGraphService;

        public GetUpcomingInstallmentsQueryHandlerTests()
        {
            var logger = new Mock<ILogger<GetUpcomingInstallmentsQueryHandler>>();
            _coreGraphService = new Mock<ICoreGraphService>();

            _target = new GetUpcomingInstallmentsQueryHandler(logger.Object, _coreGraphService.Object);
        }
        
        [Fact]
        public async Task Given_Valid_Request_Should_Call_CoreGraphService_GetUpcomingInstallmentsAsync()
        {
            // Arrange
            var request = Fixture.Create<GetUpcomingInstallmentsQuery>();

            // Act
            await _target.Handle(request, default);

            // Assert
            _coreGraphService.Verify(x => x.GetUpcomingInstallmentsAsync(
                                         It.Is<long>(y => y == request.AccountId),
                                         It.Is<Guid>(y => y == request.CustomerId),
                                         It.Is<DateTime>(y => y == request.ToDate)),
                                    Times.Once);
        }

        [Fact]
        public async Task Given_CoreGraphService_GetUpcomingInstallmentsAsync_Successful_Should_Calculate_TotalAmount()
        {
            // Arrange
            var request = Fixture.Create<GetUpcomingInstallmentsQuery>();

            var response = Fixture.CreateMany<Installment>();

            var expectedSum = response.Sum(x => x.Amount);

            _coreGraphService.Setup(x => x.GetUpcomingInstallmentsAsync(It.Is<long>(y => y == request.AccountId),
                                                                        It.Is<Guid>(y => y == request.CustomerId),
                                                                        It.Is<DateTime>(y => y == request.ToDate)))
                             .ReturnsAsync(response);

            // Act
            var result = await _target.Handle(request, default);

            // Assert
            _coreGraphService.Verify(x => x.GetUpcomingInstallmentsAsync(
                                         It.Is<long>(y => y == request.AccountId),
                                         It.Is<Guid>(y => y == request.CustomerId),
                                         It.Is<DateTime>(y => y == request.ToDate)),
                                     Times.Once);
            
            Assert.Equal(expectedSum, result.TotalAmount);
        }

        [Fact]
        public async Task Given_Exception_On_CoreGraphService_GetUpcomingInstallmentsAsync_Should_Throw()
        {
            // Arrange
            var request = Fixture.Create<GetUpcomingInstallmentsQuery>();

            _coreGraphService
               .Setup(x => x.GetUpcomingInstallmentsAsync(
                          It.Is<long>(y => y == request.AccountId),
                          It.Is<Guid>(y => y == request.CustomerId),
                          It.Is<DateTime>(y => y == request.ToDate)))
               .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _target.Handle(request, default));
        }
    }
}
