using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Payments.Command.CreateRepayment;
using Zip.Api.CustomerSummary.Domain.Entities.Payments;
using Zip.Api.CustomerSummary.Domain.Entities.Tango;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Tango;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Frequency = Zip.Api.CustomerSummary.Domain.Entities.Payments.Frequency;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Payments
{
    public class CreateRepaymentCommandHandlerTest
    {
        private readonly Mock<IAccountsService> _accountsService;
        private readonly Mock<ITangoProxy> _tangoProxy;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IAccountContext> _accountContext;

        public CreateRepaymentCommandHandlerTest()
        {
            _accountsService = new Mock<IAccountsService>();
            _tangoProxy = new Mock<ITangoProxy>();
            _mapper = new Mock<IMapper>();
            _accountContext = new Mock<IAccountContext>();
        }

        [Fact]
        public void Given_NullArgument_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    new CreateRepaymentCommandHandler(_accountsService.Object, null, _mapper.Object, _accountContext.Object);
                });
            Assert.Throws<ArgumentNullException>(
                 () =>
                 {
                     new CreateRepaymentCommandHandler(_accountsService.Object, _tangoProxy.Object, null, _accountContext.Object);
                 });
            Assert.Throws<ArgumentNullException>(
                  () =>
                  {
                      new CreateRepaymentCommandHandler(_accountsService.Object, _tangoProxy.Object, _mapper.Object, null);
                  });
        }

        [Fact]
        public async Task Given_ValidArgument_Without_ExistingRepaymentSchedule_Should_AddNewSchedule()
        {
            var mockAccountId = 123;
            var mockAmount = 40;
            var mockChangedBy = "alvin.ho@zip.co";
            var mockStartDate = DateTime.Now.AddDays(7);
            var mockRequest = new CreateRepaymentCommand()
            {
                AccountId = mockAccountId,
                Amount = mockAmount,
                ChangedBy = mockChangedBy,
                Frequency = Frequency.Monthly,
                StartDate = mockStartDate
            };

            var expected = new Repayment()
            {
                AccountId = mockAccountId,
                Amount = mockAmount,
                ChangedBy = mockChangedBy,
                Frequency = Frequency.Monthly,
                StartDate = mockStartDate
            };

            _tangoProxy.Setup(x => x.ListAllRepaymentSchedulesAsync(It.IsAny<string>()))
                .ReturnsAsync(Enumerable.Empty<LoanMgtRepaymentScheduleVariation>());

            _tangoProxy.Setup(x => x.AddRepaymentScheduleAsync(It.IsAny<LoanMgtRepaymentScheduleVariation>()))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.OK
                });

            _mapper.Setup(x => x.Map<Repayment>(It.IsAny<LoanMgtRepaymentScheduleVariation>()))
                .Returns(expected);

            _accountContext.Setup(x => x.AddRepaymentAsync(It.IsAny<Repayment>()))
                .ReturnsAsync(expected);

            var handler = new CreateRepaymentCommandHandler(_accountsService.Object, _tangoProxy.Object, _mapper.Object, _accountContext.Object);
            var actual = await handler.Handle(mockRequest, CancellationToken.None);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task TangoProxy_Fail_To_AddNewSchedule_ShouldThrow_Exception()
        {
            var mockAccountId = 123;
            var mockAmount = 40;
            var mockChangedBy = "alvin.ho@zip.co";
            var mockStartDate = DateTime.Now.AddDays(7);
            var mockRequest = new CreateRepaymentCommand()
            {
                AccountId = mockAccountId,
                Amount = mockAmount,
                ChangedBy = mockChangedBy,
                Frequency = Frequency.Monthly,
                StartDate = mockStartDate
            };

            _tangoProxy.Setup(x => x.ListAllRepaymentSchedulesAsync(It.IsAny<string>()))
                .ReturnsAsync(Enumerable.Empty<LoanMgtRepaymentScheduleVariation>());

            _tangoProxy.Setup(x => x.AddRepaymentScheduleAsync(It.IsAny<LoanMgtRepaymentScheduleVariation>()))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.GatewayTimeout,
                    Content = new StringContent("Tango proxy failure test")
                });

            var handler = new CreateRepaymentCommandHandler(_accountsService.Object, _tangoProxy.Object, _mapper.Object, _accountContext.Object);
            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(mockRequest, CancellationToken.None));
            Assert.Equal("Failed to create new repayment schedule", exception.Message);
        }
    }
}
