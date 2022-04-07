using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInstallments;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Accounts
{
    public class GetAccountInstallmentsHandlerTest
    {
        private readonly GetAccountInstallmentsQueryHandler _target;
        
        private readonly Mock<IAccountsService> _accountsService;

        private readonly Mock<ILogger<GetAccountInstallmentsQueryHandler>> _logger;

        public GetAccountInstallmentsHandlerTest()
        {
            _logger = new Mock<ILogger<GetAccountInstallmentsQueryHandler>>();
            _accountsService = new Mock<IAccountsService>();
            
            _target = new GetAccountInstallmentsQueryHandler(_accountsService.Object, _logger.Object);
        }

        [Fact]
        public async Task Given_ErrorInAccountsProxy_WhenCall_GetOrders_ShouldThrow_Exception()
        {
            _accountsService.Setup(x => x.GetOrders(It.IsAny<long>()))
                .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _target.Handle(new GetAccountInstallmentsQuery(), CancellationToken.None);
            });
        }
    }
}