using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Payments.Command.HoldPayment;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Services.Accounts.Contract.Invalidation;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Payments
{
    public class HoldPaymentCommandHandlerTest
    {
        private readonly Mock<IAccountsService> _accountsService;
        private readonly Mock<IAccountContext> _accountContext;

        public HoldPaymentCommandHandlerTest()
        {
            _accountsService = new Mock<IAccountsService>();
            _accountContext = new Mock<IAccountContext>();
        }

        [Fact]
        public async Task Given_DependencyInjection_Null_ShouldThrow_ArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(
                async () =>
                {
                    var handler = new HoldPaymentCommandHandler(null, _accountsService.Object);
                    await handler.Handle(null, default);
                });
        }

        [Fact]
        public async Task Given_ValidRequest_Should_Update()
        {
            _accountContext
                .Setup(x =>
                        x.HoldPaymentDateAsync(
                            It.IsAny<long>(),
                            It.IsAny<DateTime>()));

            _accountsService.Setup(x => x.Invalidate(It.IsAny<InvalidationRequest>()));
            var handler = new HoldPaymentCommandHandler(_accountContext.Object, _accountsService.Object);
            var result = await handler.Handle(new HoldPaymentCommand(), default);

            Assert.Equal(Unit.Value, result);
        }

        [Fact]
        public async Task Given_ExceptionThrown_ShouldThrow_Same()
        {
            _accountContext.Setup(x => x.HoldPaymentDateAsync(It.IsAny<long>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new Exception());
            _accountsService.Setup(x => x.Invalidate(It.IsAny<InvalidationRequest>()));

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                var handler = new HoldPaymentCommandHandler(_accountContext.Object, _accountsService.Object);
                await handler.Handle(new HoldPaymentCommand(), CancellationToken.None);
            });
        }
    }
}
