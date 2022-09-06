using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateAddress;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateConsumer;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateDateOfBirth;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateGender;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateName;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Consumers
{
    public class UpdateConsumerCommandHandlerTest
    {
        private readonly Mock<IMediator> _mediator;

        public UpdateConsumerCommandHandlerTest()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UpdateConsumerCommandHandler(null));
        }

        [Fact]
        public async Task Given_ConsumerNotFound_ShouldThrow_ConsumerNotFoundException()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(null as Consumer);

            var handler = new UpdateConsumerCommandHandler(_mediator.Object);

            await Assert.ThrowsAsync<ConsumerNotFoundException>(async () =>
            {
                await handler.Handle(new UpdateConsumerCommand(), CancellationToken.None);
            });
        }

        [Fact]
        public async Task Given_Changes_ShouldCall_RelevantCommands()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    new Consumer() 
                    { 
                        FirstName = "James",
                        LastName = "Bond",
                        Gender = Gender.FemaleWithKids,
                        Address = new Address()
                        {
                            CountryCode = "NZ"
                        },
                        DateOfBirth = new DateTime(1999,1,1)
                    });

            var handler = new UpdateConsumerCommandHandler(_mediator.Object);
            await handler.Handle(
                new UpdateConsumerCommand() 
                { 
                    FirstName = "Shan",
                    LastName = "Ke",
                    Gender = Gender.Male,
                    Address = new Address()
                    {
                        CountryCode = "AU"
                    },
                    DateOfBirth = new DateTime(2000,1,1)
                }, CancellationToken.None);

            _mediator.Verify(x => x.Send(It.IsAny<UpdateNameCommand>(), It.IsAny<CancellationToken>()), Times.Once());
            _mediator.Verify(x => x.Send(It.IsAny<UpdateDateOfBirthCommand>(), It.IsAny<CancellationToken>()), Times.Once());
            _mediator.Verify(x => x.Send(It.IsAny<UpdateAddressCommand>(), It.IsAny<CancellationToken>()), Times.Once());
            _mediator.Verify(x => x.Send(It.IsAny<UpdateGenderCommand>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
