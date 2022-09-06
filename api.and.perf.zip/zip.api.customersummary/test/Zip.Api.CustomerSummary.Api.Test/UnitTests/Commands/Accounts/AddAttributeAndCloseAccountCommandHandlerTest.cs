using AutoFixture;
using MediatR;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Accounts.Command.AddAttributeAndLockAccount;
using Zip.Api.CustomerSummary.Application.Accounts.Command.LockAccount;
using Zip.Api.CustomerSummary.Application.Consumers.Command.SetConsumerAttributes;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetAttributes;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumerAttributes;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Accounts
{
    public class AddAttributeAndCloseAccountCommandHandlerTest : CommonTestsFixture
    {
        private readonly Mock<IMediator> _mediator;

        private readonly AddAttributeAndLockAccountCommandHandler _target;

        public AddAttributeAndCloseAccountCommandHandlerTest()
        {
            _mediator = new Mock<IMediator>();

            _target = new AddAttributeAndLockAccountCommandHandler(_mediator.Object, Mapper);
        }

        [Fact]
        public void Given_NullInjection_Should_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new AddAttributeAndLockAccountCommandHandler(null, Mapper);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                new AddAttributeAndLockAccountCommandHandler(_mediator.Object, null);
            });
        }

        [Fact]
        public async Task Given_Valid_ShouldReturn_Result()
        {
            // Arrange
            var mockInput = Fixture.Build<AddAttributeAndLockAccountCommand>().With(x => x.Attribute, "TestAttribute").Create();
            var mockAttributeResponse = Fixture.CreateMany<ConsumerAttributeDto>();
            var mockAllAttributeResponse = Fixture.Build<CustomerSummary.Domain.Entities.Consumers.Attribute>().With(x => x.Name, "TestAttribute").CreateMany();
            var mockUnitResponse = Unit.Value;

            _mediator.Setup(x =>
                x.Send(It.IsAny<GetConsumerAttributesQuery>(), CancellationToken)
            ).ReturnsAsync(mockAttributeResponse);

            _mediator.Setup(x =>
                x.Send(It.IsAny<SetConsumerAttributesCommand>(), CancellationToken)
            ).ReturnsAsync(mockUnitResponse);

            _mediator.Setup(x =>
                x.Send(It.IsAny<GetAttributesQuery>(), CancellationToken)
            ).ReturnsAsync(mockAllAttributeResponse);

            _mediator.Setup(x =>
                x.Send(It.IsAny<LockAccountCommand>(), CancellationToken))
                .ReturnsAsync(mockUnitResponse);

            // Action
            var result = await _target.Handle(mockInput, CancellationToken);

            // Assert
            _mediator.Verify(
                x => x.Send(
                    It.IsAny<LockAccountCommand>(),
                    CancellationToken),
                Times.Once
            );

            Assert.Equal(Unit.Value, result);
        }

        [Fact]
        public async Task Given_Invalid_ShouldReturn_Error()
        {
            // Arrange
            var mockAllAttributeResponse = Fixture.Build<CustomerSummary.Domain.Entities.Consumers.Attribute>().With(x => x.Name, "TestAttribute").CreateMany();
            var mockInput = Fixture.Build<AddAttributeAndLockAccountCommand>().With(x => x.Attribute, "Test").Create();
            var mockAttributeResponse = Fixture.CreateMany<ConsumerAttributeDto>();

            _mediator.Setup(x =>
                x.Send(It.IsAny<GetAttributesQuery>(), CancellationToken)
            ).ReturnsAsync(mockAllAttributeResponse);

            _mediator.Setup(x =>
               x.Send(It.IsAny<GetConsumerAttributesQuery>(), CancellationToken)
           ).ReturnsAsync(mockAttributeResponse);

            // Action & Assert
            await Assert.ThrowsAsync<NotSupportedException>(() => _target.Handle(mockInput, CancellationToken));
        }
    }
}
