using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo.Models;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateContact;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateEmail;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateMobile;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetContact;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Consumers
{
    public class UpdateContactCommandHandlerTest
    {
        private readonly Mock<IMediator> _mediator;

        public UpdateContactCommandHandlerTest()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UpdateContactCommandHandler(null));
        }

        [Fact]
        public async Task Given_NoConsumerFound_ShouldThrow_ConsumerNotFoundException()
        {
            var handler = new UpdateContactCommandHandler(_mediator.Object);

            await Assert.ThrowsAsync<ConsumerNotFoundException>(async () =>
            {
                await handler.Handle(new UpdateContactCommand(), CancellationToken.None);
            });
        }

        [Fact]
        public async Task Given_NoAccountInfoFound_ShouldThrow_AccountNotFoundException()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Consumer());

            _mediator.Setup(x => x.Send(It.IsAny<GetAccountInfoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(null as GetAccountInfoQueryResult);

            var handler = new UpdateContactCommandHandler(_mediator.Object);

            await Assert.ThrowsAsync<AccountNotFoundException>(async () =>
            {
                await handler.Handle(new UpdateContactCommand(), CancellationToken.None);
            });
        }

        [Fact]
        public async Task Given_ChangesDetected_ShouldCall_RelevantCommands()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Consumer());

            _mediator.Setup(x => x.Send(It.IsAny<GetContactQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ContactDto()
                {
                    Email = "test@zip.co",
                    Mobile = "049876543"
                });

            _mediator.Setup(x => x.Send(It.IsAny<GetAccountInfoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetAccountInfoQueryResult());

            var handler = new UpdateContactCommandHandler(_mediator.Object);

            await handler.Handle(new UpdateContactCommand() { 
                Email = "test@zip.co1",
                Mobile = "0412345678"
            }, CancellationToken.None);

            _mediator.Verify(x => x.Send(It.IsAny<UpdateEmailCommand>(), It.IsAny<CancellationToken>()), Times.Once());
            _mediator.Verify(x => x.Send(It.IsAny<UpdateMobileCommand>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
