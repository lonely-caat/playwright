using AutoFixture;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Api.Test.Factories;
using Zip.Api.CustomerSummary.Application.Accounts.Command.AddAttributeAndLockAccount;
using Zip.Api.CustomerSummary.Application.Accounts.Command.LockAccount;
using Zip.Api.CustomerSummary.Application.Accounts.Command.UpdateInstallmentsEnabled;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInstallments;
using Zip.Api.CustomerSummary.Application.Accounts.Query.SearchAccounts;
using Zip.Api.CustomerSummary.Application.Payments.Command.CreateRepayment;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetRepaymentSchedule;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetRepaymentSchedule.Models;
using Zip.Api.CustomerSummary.Application.Transactions.Query.GetLmsTransactions;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Domain.Entities.Payments;
using Zip.Api.CustomerSummary.Domain.Entities.Transactions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Models;
using Zip.Services.Accounts.Contract.Account;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class AccountsControllerTests : CommonTestsFixture
    {
        private readonly Mock<IMediator> _mediator;

        private readonly LockAccountCommand _invalidCommand = new LockAccountCommand() { AccountId = 0 };

        private readonly LockAccountCommand _validCommand = new LockAccountCommand() { AccountId = 1 };

        public AccountsControllerTests(ApiFactory factory) : base(factory)
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public void Given_NullMediator_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new AccountsController(null));
        }

        [Fact]
        public async Task Given_Keyword_WhenCall_SearchByKeywords_ShouldReturn_MatchingResults()
        {
            _mediator.Setup(x =>
                                x.Send(
                                    It.IsAny<SearchAccountsQuery>(),
                                    It.IsAny<CancellationToken>()))
                     .ReturnsAsync(
                          new List<AccountListItem>()
                          {
                              new AccountListItem() { Id = 111 },
                              new AccountListItem() { Id = 222 }
                          });
            var controller = new AccountsController(_mediator.Object);
            var response = await controller.SearchByKeywordAsync("Michael") as OkObjectResult;

            Assert.NotNull(response);

            var accountListItems = response.Value as IEnumerable<AccountListItem>;
            Assert.Equal(2, accountListItems?.Count());

            Assert.Equal(222,
                         accountListItems.Skip(1)
                                         .First()
                                         .Id);
        }

        [Fact]
        public async Task Given_ExceptionThrown_WhenCall_SearchByKeywords_ShouldReturn_Status500()
        {
            _mediator.Setup(x =>
                                x.Send(
                                    It.IsAny<SearchAccountsQuery>(),
                                    It.IsAny<CancellationToken>()))
                     .ThrowsAsync(new Exception());

            var controller = new AccountsController(_mediator.Object);
            var response = await controller.SearchByKeywordAsync("test") as ObjectResult;

            Assert.NotNull(response);
            Assert.Equal(500, response.StatusCode);
        }

        [Fact]
        public async Task Given_InvalidAccountId_WhenCall_GetLmsTransactions_ShouldReturn_BadRequest()
        {
            var controller = new AccountsController(_mediator.Object);

            var response = await controller.GetLmsTransactionsAsync(-20) as ObjectResult;

            Assert.Equal(400, response?.StatusCode);
        }

        [Fact]
        public async Task Given_AllGood_WhenCall_GetLmsTransactions_ShouldReturn_Transactions()
        {
            _mediator.Setup(x =>
                                x.Send(
                                    It.IsAny<GetLmsTransactionsQuery>(),
                                    It.IsAny<CancellationToken>()))
                     .ReturnsAsync(
                          new List<LmsTransaction>
                          {
                              new LmsTransaction() { Amount = 20.9m },
                              new LmsTransaction() { Amount = 23.2m }
                          }
                      );

            var controller = new AccountsController(_mediator.Object);
            var response = await controller.GetLmsTransactionsAsync(30) as OkObjectResult;
            var items = response?.Value as IEnumerable<LmsTransaction>;

            Assert.Equal(2, items?.Count());
        }

        [Fact]
        public async Task Given_ExceptionThrown_WhenCall_GetLmsTransactions_ShouldReturn_500()
        {
            _mediator.Setup(x =>
                                x.Send(
                                    It.IsAny<GetLmsTransactionsQuery>(),
                                    It.IsAny<CancellationToken>()))
                     .ThrowsAsync(new Exception());

            var controller = new AccountsController(_mediator.Object);

            var response = await controller.GetLmsTransactionsAsync(20) as ObjectResult;

            Assert.Equal(500, response?.StatusCode);
        }

        [Fact]
        public async Task Given_NullRepaymentSchedule_Found_WhenCall_GetRepaymentSchedule_ShouldReturn_NotContent()
        {
            _mediator.Setup(x =>
                                x.Send(
                                    It.IsAny<GetRepaymentScheduleQuery>(),
                                    It.IsAny<CancellationToken>()))
                     .ReturnsAsync(null as GetRepaymentScheduleQueryResult);

            var controller = new AccountsController(_mediator.Object);
            var response = await controller.GetRepaymentScheduleAsync(303);

            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async Task Given_InvalidAccountId_WhenCall_GetRepaymentSchedule_ShouldReturn_BadRequest()
        {
            var controller = new AccountsController(_mediator.Object);
            var response = await controller.GetRepaymentScheduleAsync(-303);

            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task Given_CreateRepaymentSucceed_WhenCall_AddRepayment_ShouldReturn_RepaymentCreated()
        {
            _mediator.Setup(x => x.Send(
                                It.IsAny<CreateRepaymentCommand>(),
                                It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new Repayment() { Id = 998 });

            var controller = new AccountsController(_mediator.Object);
            var response = await controller.AddRepaymentAsync(new CreateRepaymentCommand()) as OkObjectResult;

            var v = response?.Value as Repayment;
            Assert.NotNull(v);
            Assert.Equal(998, v.Id);
        }

        [Fact]
        public async Task Given_ErrorInAddRepayment_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(
                                It.IsAny<CreateRepaymentCommand>(),
                                It.IsAny<CancellationToken>()))
                     .ThrowsAsync(new Exception());

            var controller = new AccountsController(_mediator.Object);
            var response = await controller.AddRepaymentAsync(new CreateRepaymentCommand()) as ObjectResult;

            Assert.Equal(500, response?.StatusCode);
        }

        [Fact]
        public async Task Given_ValidLockAccountPayload_ShouldReturn_200()
        {
            _mediator.Setup(x => x.Send(
                                It.IsAny<LockAccountCommand>(),
                                It.IsAny<CancellationToken>()))
                     .ReturnsAsync(Unit.Value);

            var controller = new AccountsController(_mediator.Object);
            var response = await controller.LockAccountAsync(_validCommand);

            response.Should()
                    .BeOfType<OkResult>();
        }

        [Fact]
        public async Task Given_InvalidAccountId_ShouldReturn_400()
        {
            var controller = new AccountsController(_mediator.Object);
            var response = await controller.LockAccountAsync(_invalidCommand) as ObjectResult;

            Assert.Equal(400, response?.StatusCode);
        }

        [Fact]
        public async Task When_Handler_Throws_AccountNotFoundException_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(
                                It.IsAny<LockAccountCommand>(),
                                It.IsAny<CancellationToken>()))
                     .ThrowsAsync(new AccountNotFoundException());

            var controller = new AccountsController(_mediator.Object);
            var response = await controller.LockAccountAsync(_validCommand) as ObjectResult;

            Assert.Equal(500, response?.StatusCode);
        }

        [Fact]
        public async Task When_Handler_Throws_LockAccountException_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(
                                It.IsAny<LockAccountCommand>(),
                                It.IsAny<CancellationToken>()))
                     .ThrowsAsync(new LockAccountException());

            var controller = new AccountsController(_mediator.Object);
            var response = await controller.LockAccountAsync(_validCommand) as ObjectResult;

            Assert.Equal(500, response?.StatusCode);
        }

        [Fact]
        public async Task When_Handler_Throws_Exception_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(
                                It.IsAny<LockAccountCommand>(),
                                It.IsAny<CancellationToken>()))
                     .ThrowsAsync(new Exception());

            var controller = new AccountsController(_mediator.Object);
            var response = await controller.LockAccountAsync(_validCommand) as ObjectResult;

            Assert.Equal(500, response?.StatusCode);
        }

        [Fact]
        public async Task Given_ValidInput_AddAttributeAndLockAccountAsync_ShouldReturn_200()
        {
            // Arrange
            _mediator.Setup(x => x.Send(
                                It.IsAny<AddAttributeAndLockAccountCommand>(),
                                It.IsAny<CancellationToken>()))
                     .ReturnsAsync(Unit.Value);
            var mockInput = Fixture.Create<AddAttributeAndLockAccountCommand>();

            // Act
            var controller = new AccountsController(_mediator.Object);
            var response = await controller.AddAttributeAndLockAccountAsync(mockInput);

            // Assert
            response.Should()
                    .BeOfType<OkResult>();
        }

        [Fact]
        public async Task Given_Exception_AddAttributeAndLockAccountAsync_ShouldReturn_500()
        {
            // Arrange
            _mediator.Setup(x => x.Send(
                                It.IsAny<AddAttributeAndLockAccountCommand>(),
                                It.IsAny<CancellationToken>()))
                     .ThrowsAsync(new Exception());
            var mockInput = Fixture.Create<AddAttributeAndLockAccountCommand>();

            // Act
            var controller = new AccountsController(_mediator.Object);
            var response = await controller.AddAttributeAndLockAccountAsync(mockInput) as ObjectResult;

            // Assert
            Assert.Equal(500, response?.StatusCode);
        }

        [Fact]
        public async Task Given_ValidInput_GetInstallments_ShouldReturn_200()
        {
            // Arrange
            var request = Fixture.Create<GetAccountInstallmentsQuery>();

            _mediator
               .Setup(x => x.Send(It.IsAny<GetAccountInstallmentsQuery>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(
                    new OrdersResponse() { Orders = new List<OrderDetailResponse> { new OrderDetailResponse() } });

            // Act
            var controller = new AccountsController(_mediator.Object);
            var response = await controller.GetInstallments(request) as OkObjectResult;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task Given_ValidInput_GetInstallments_When_InstallmentsList_IsEmpty_ShouldReturn_404()
        {
            // Arrange
            var request = Fixture.Create<GetAccountInstallmentsQuery>();

            _mediator
               .Setup(x => x.Send(It.IsAny<GetAccountInstallmentsQuery>(),
                                  It.IsAny<CancellationToken>()))
               .ReturnsAsync(new OrdersResponse());

            // Act
            var controller = new AccountsController(_mediator.Object);
            var response = await controller.GetInstallments(request) as NotFoundResult;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(404, response.StatusCode);
        }

        [Fact]
        public async Task Given_Handler_Throws_Exception_Should_Throw()
        {
            // Arrange
            var request = Fixture.Create<GetAccountInstallmentsQuery>();
            
            _mediator
               .Setup(x => x.Send(It.IsAny<GetAccountInstallmentsQuery>(), It.IsAny<CancellationToken>()))
               .Throws(new Exception());

            // Act & Assert
            var controller = new AccountsController(_mediator.Object);
            await Assert.ThrowsAsync<Exception>(async () => await controller.GetInstallments(request));
        }

        [Fact]
        public async Task Given_ValidInput_UpdateInstallmentsEnabled_ShouldReturn_200()
        {
            // Arrange
            var request = Fixture.Build<UpdateInstallmentsEnabledCommand>()
                                 .With(x => x.AccountId, Fixture.Create<long>())
                                 .With(x => x.AccountTypeId, Fixture.Create<long>())
                                 .With(x => x.IsInstallmentsEnabled, Fixture.Create<bool>())
                                 .Create();

            var updatedConfiguration = Fixture.Build<AccountResponseConfiguration>()
                                              .With(x => x.InstallmentAccount, request.IsInstallmentsEnabled)
                                              .Create();

            _mediator
               .Setup(x => x.Send(It.Is<UpdateInstallmentsEnabledCommand>(y => y == request),
                                  It.IsAny<CancellationToken>()))
               .ReturnsAsync(Fixture.Build<AccountResponse>()
                                    .With(x => x.Id, request.AccountId.ToString())
                                    .With(x => x.Configuration, updatedConfiguration)
                                    .Create());

            // Act
            var controller = new AccountsController(_mediator.Object);
            var response = await controller.UpdateInstallmentsEnabled(request) as OkObjectResult;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task Given_InvalidInput_UpdateInstallmentsEnabled_Throws_Exception_Should_Throw()
        {
            // Arrange
            var request = Fixture.Build<UpdateInstallmentsEnabledCommand>()
                                 .With(x => x.AccountId, Fixture.Create<long>())
                                 .With(x => x.AccountTypeId, Fixture.Create<long>())
                                 .With(x => x.IsInstallmentsEnabled, Fixture.Create<bool>())
                                 .Create();

            _mediator
               .Setup(x => x.Send(It.IsAny<UpdateInstallmentsEnabledCommand>(), It.IsAny<CancellationToken>()))
               .ThrowsAsync(new Exception());

            // Act & Assert
            var controller = new AccountsController(_mediator.Object);
            await Assert.ThrowsAsync<Exception>(async () => await controller.UpdateInstallmentsEnabled(request));
        }
    }
}