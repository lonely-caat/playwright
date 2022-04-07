using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo.Models;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetRepaymentSchedule;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Payments;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Payments
{
    public class GetRepaymentScheduleQueryHandlerTest
    {
        private readonly Mock<IAccountContext> _accountContext;
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IAccountsService> _accountsService;

        public GetRepaymentScheduleQueryHandlerTest()
        {
            _accountContext = new Mock<IAccountContext>();
            _mediator = new Mock<IMediator>();
            _accountsService = new Mock<IAccountsService>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new GetRepaymentScheduleQueryHandler(null, _mediator.Object, _accountsService.Object));
            Assert.Throws<ArgumentNullException>(() => new GetRepaymentScheduleQueryHandler(_accountContext.Object, null, _accountsService.Object));
        }

        [Fact]
        public async Task Given_NoAccountFound_ShouldThrow_AccountNotFoundException()
        {
            _accountContext.Setup(x => x.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(null as Account);

            var handler = new GetRepaymentScheduleQueryHandler(_accountContext.Object, _mediator.Object, _accountsService.Object);

            await Assert.ThrowsAsync<AccountNotFoundException>(async () =>
            {
                 await handler.Handle(new GetRepaymentScheduleQuery(), CancellationToken.None);
            });
        }

        [Fact]
        public async Task Given_AccountInfoNotNull_ShouldReturn_LMSConnnnnstractualDate()
        {
            _accountContext.Setup(x => x.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new Account());

            _accountContext.Setup(x => x.GetRepaymentScheduleAsync(It.IsAny<long>()))
                .ReturnsAsync(new RepaymentSchedule() { ContractualDate = new DateTime(2000, 1, 1) });

            _mediator.Setup(x => x.Send(It.IsAny<GetAccountInfoQuery>(), CancellationToken.None))
                .ReturnsAsync(new GetAccountInfoQueryResult()
                {
                    LmsAccount = new LmsAccountDto()
                    {
                        ContractualDate = new DateTime(2010, 1, 1)
                    }
                });

            var handler = new GetRepaymentScheduleQueryHandler(_accountContext.Object, _mediator.Object, _accountsService.Object);
            var result = await handler.Handle(new GetRepaymentScheduleQuery(), CancellationToken.None);

            Assert.Equal(2010, result.Schedule.ContractualDate.Value.Year);
        }
    }
}
