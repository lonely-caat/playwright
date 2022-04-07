using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Accounts.Command.CloseAccount;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo.Models;
using Zip.Api.CustomerSummary.Application.Communications.Command.SendAccountClosedEmail;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetContact;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Models;
using Zip.Api.CustomerSummary.Infrastructure.Services.KinesisProducer.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Accounts
{
    public class CloseAccountCommandHandlerTest
    {
        private readonly Mock<ICreditProfileContext> creditProfileContext = new Mock<ICreditProfileContext>();
        private readonly Mock<IKinesisProducer> kinesisProducer = new Mock<IKinesisProducer>();
        private readonly Mock<IOptions<KinesisSettings>> options = new Mock<IOptions<KinesisSettings>>();
        private readonly Mock<IAccountsService> _accountsService = new Mock<IAccountsService>();
        private readonly Mock<IMediator> mediator = new Mock<IMediator>();
        private readonly Mock<ICommunicationsServiceProxy> communicationsServiceProxy = new Mock<ICommunicationsServiceProxy>();
        private CloseAccountCommandHandler _target;
        private readonly Fixture _fixture;

        public CloseAccountCommandHandlerTest()
        {
            var kinesisSettingsModel = new KinesisSettings()
            {
                Enabled = true
            };
            options.Setup(x => x.Value).Returns(kinesisSettingsModel);

            _fixture = new Fixture();
        }

        [Fact]
        public void Given_NullCreditProfileContext_ShouldThrow()
        {
            ICreditProfileContext creditProfileContext = null;

            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    new CloseAccountCommandHandler(
                        creditProfileContext,
                        kinesisProducer.Object,
                        options.Object,
                        _accountsService.Object,
                        mediator.Object,
                        communicationsServiceProxy.Object);
                });
        }

        [Fact]
        public void Given_NullKinesisProducer_ShouldThrow()
        {
            IKinesisProducer kinesisProducer = null;

            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    new CloseAccountCommandHandler(
                        creditProfileContext.Object,
                        kinesisProducer,
                        options.Object,
                        _accountsService.Object,
                        mediator.Object,
                        communicationsServiceProxy.Object);
                });
        }

        [Fact]
        public void Given_NullOptions_ShouldThrow()
        {
            IOptions<KinesisSettings> options = null;

            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    new CloseAccountCommandHandler(
                        creditProfileContext.Object,
                        kinesisProducer.Object,
                        options,
                        _accountsService.Object,
                        mediator.Object,
                        communicationsServiceProxy.Object);
                });
        }

        [Fact]
        public void Given_NullOptionsValue_ShouldThrow()
        {
            options.Setup(x => x.Value).Returns(null as KinesisSettings);

            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    new CloseAccountCommandHandler(
                        creditProfileContext.Object,
                        kinesisProducer.Object,
                        options.Object,
                        _accountsService.Object,
                        mediator.Object,
                        communicationsServiceProxy.Object);
                });
        }

        [Fact]
        public async Task Given_HappyPath_Should_ReturnValue()
        {
            // Arrange
            _target = new CloseAccountCommandHandler(creditProfileContext.Object,
                kinesisProducer.Object,
                options.Object,
                _accountsService.Object,
                mediator.Object,
                communicationsServiceProxy.Object);

            var request = _fixture.Create<CloseAccountCommand>();

            var getCustomerQueryResponse = _fixture.Build<Consumer>()
                .Without(x => x.LinkedConsumer)
                .Create();
            mediator.Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(getCustomerQueryResponse);

            var getAccountInforQueryResponse = _fixture.Create<GetAccountInfoQueryResult>();
            mediator.Setup(x => x.Send(It.IsAny<GetAccountInfoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(getAccountInforQueryResponse);

            _accountsService.Setup(x => x.CloseAccount(It.IsAny<long>(), It.IsAny<string>()));

            creditProfileContext.Setup(x => x.CreateCreditProfileStateAsync(It.IsAny<CreditProfileState>()));
            creditProfileContext.Setup(x => x.CreateCreditProfileAttributeAsync(It.IsAny<long>(), It.IsAny<long>()));
            creditProfileContext.Setup(x => x.CreateCreditProfileClassificationAsync(It.IsAny<long>(), It.IsAny<long>()));
            kinesisProducer.Setup(x => x.PutRecord(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            var getContractQueryResponse = _fixture.Create<ContactDto>();
            mediator.Setup(x => x.Send(It.IsAny<GetContactQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(getContractQueryResponse);

            communicationsServiceProxy.Setup(x => x.SendCloseAccountEmailAsync(It.IsAny<CloseAccount>()));

            mediator.Setup(x => x.Send(It.IsAny<SendAccountClosedEmailCommand>(), It.IsAny<CancellationToken>()));

            // Act
            var response = await _target.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, response);
            communicationsServiceProxy.Verify(x => x.SendCloseAccountEmailAsync(It.IsAny<CloseAccount>()), Times.Once);
        }
    }
}
