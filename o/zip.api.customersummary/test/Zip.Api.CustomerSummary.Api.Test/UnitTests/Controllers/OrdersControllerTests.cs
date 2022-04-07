using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Orders.Command;
using Zip.Api.CustomerSummary.Application.Orders.Query.GetOrderInstallments;
using Zip.Api.CustomerSummary.Application.Orders.Query.GetOrderSummary;
using OrderDetailResponse = Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Models.OrderDetailResponse;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class OrdersControllerTests : CommonTestsFixture
    {
        private readonly OrdersController _target;

        public OrdersControllerTests()
        {
            _target = new OrdersController(MockMediator.Object);
        }

        [Fact]
        public async Task Given_Valid_Input_On_GetOrderSummary_Should_Return_200()
        {
            // Arrange
            var request = Fixture.Create<GetOrderSummaryQuery>();
            var response = Fixture.Create<GetOrderSummaryResponse>();
            MockMediator
               .Setup(x => x.Send(It.Is<GetOrderSummaryQuery>(y => y == request), default))
               .ReturnsAsync(response);

            // Act
            var actual = await _target.GetOrderSummary(request);

            // Assert
            actual.Should()
                  .BeOfType<OkObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should()
                           .Be(200);
        }

        [Fact]
        public async Task Given_Exception_On_GetOrderSummary_Should_Throw_To_Middleware()
        {
            // Arrange
            MockMediator
               .Setup(x => x.Send(It.IsAny<GetOrderSummaryQuery>(), default))
               .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(
                async () =>
                    await _target.GetOrderSummary(Fixture.Create<GetOrderSummaryQuery>())
            );
        }

        [Fact]
        public async Task Given_Valid_Input_On_GetOrderInstallments_Should_Return_200()
        {
            // Arrange
            var request = Fixture.Create<GetOrderInstallmentsQuery>();
            var response = Fixture.Create<OrderDetailResponse>();

            MockMediator
               .Setup(x => x.Send(It.IsAny<GetOrderInstallmentsQuery>(), default))
               .ReturnsAsync(response);

            // Act
            var actual = await _target.GetOrderInstallments(request);

            // Assert
            actual.Should()
                  .BeOfType<OkObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should()
                           .Be(200);
        }

        [Fact]
        public async Task Given_Exception_On_GetOrderInstallments_Should_Throw_To_Middleware()
        {
            // Arrange
            var request = Fixture.Create<GetOrderInstallmentsQuery>();

            MockMediator
               .Setup(x => x.Send(It.IsAny<GetOrderInstallmentsQuery>(), default))
               .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(
                async () =>
                    await _target.GetOrderInstallments(request)
            );
        }

        [Fact]
        public async Task Given_ValidInput_CancelOrderInstallments_ShouldReturn_200()
        {
            // Arrange
            MockMediator
               .Setup(x => x.Send(It.IsAny<CancelOrderInstallmentsCommand>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(Unit.Value);

            // Act
            var response = await _target.CancelOrderInstallments(Fixture.Create<CancelOrderInstallmentsCommand>());

            // Assert
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public async Task Given_InvalidInput_CancelOrderInstallments_Throws_Exception_Should_Throw()
        {
            // Arrange
            var request = Fixture.Build<CancelOrderInstallmentsCommand>()
                                 .With(x => x.AccountId, Fixture.Create<long>())
                                 .With(x => x.OrderId, Fixture.Create<long>())
                                 .Create();

            MockMediator
               .Setup(x => x.Send(It.IsAny<CancelOrderInstallmentsCommand>(), It.IsAny<CancellationToken>()))
               .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _target.CancelOrderInstallments(request));
        }
    }
}