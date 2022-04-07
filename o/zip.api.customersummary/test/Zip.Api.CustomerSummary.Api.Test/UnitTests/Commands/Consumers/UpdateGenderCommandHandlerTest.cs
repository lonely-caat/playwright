using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateGender;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Consumers
{
    public class UpdateGenderCommandHandlerTest
    {
        [Fact]
        public void Given_NullContext_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new UpdateGenderCommandHandler(null);
            });
        }

        [Fact]
        public async Task Given_AllGood()
        {
            var context = new Mock<IConsumerContext>();
            context.Setup(x => x.UpdateGenderAsync(It.IsAny<long>(), It.IsAny<Gender>()));

            var handler = new UpdateGenderCommandHandler(context.Object);
            var result = await handler.Handle(new UpdateGenderCommand(), CancellationToken.None);

            Assert.Equal(Unit.Value, result);
        }
    }
}
