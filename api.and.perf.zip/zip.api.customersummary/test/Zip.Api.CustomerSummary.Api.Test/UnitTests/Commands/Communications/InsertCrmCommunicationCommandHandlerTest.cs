using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.UnitTests.Common;
using Zip.Api.CustomerSummary.Application.Communications.Command.InsertCrmCommunication;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using AutoFixture;
using System;
using Zip.Api.CustomerSummary.Domain.Entities.MessageLog;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Communications
{
    public class InsertCrmCommunicationCommandHandlerTest : CommonTestFixture
    {

        private readonly Mock<IMessageLogContext> _messageLog;
        private readonly Mock<ILogger<InsertCrmCommunicationCommandHandler>> _logger;
        private InsertCrmCommunicationCommandHandler _target;

        public InsertCrmCommunicationCommandHandlerTest()
        {
            _messageLog = new Mock<IMessageLogContext>();
            _logger = new Mock<ILogger<InsertCrmCommunicationCommandHandler>>();
            _target = new InsertCrmCommunicationCommandHandler(_messageLog.Object, _logger.Object);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Given_Valid_Input_Handle_Should_Return_200(bool mockResponse)
        {
            // Arrange
            var mockInput = _fixture.Create<InsertCrmCommunicationCommand>();

            _messageLog.Setup(x => x.InsertAsync(It.IsAny<long>(), It.IsAny<Guid>(), It.IsAny<string>(),
                                                     It.IsAny<string>(), It.IsAny<MessageLogSettings>(),
                                                     It.IsAny<DateTime>(), It.IsAny<string>()))
                                                     .ReturnsAsync(mockResponse);
            // Act
            var response = await _target.Handle(mockInput, CancellationToken.None);

            // Assert
            Assert.Equal(mockResponse, response);
        }

        [Theory]
        [InlineData(-1, "subject", "detail")]
        [InlineData(10, "", "detail")]
        [InlineData(10, "subject", null)]
        public async Task Given_Invalid_Input_Handle_Should_Return_500(long referenceId, string subject, string detail)
        {
            // Arrange
            var mockInput = _fixture.Build<InsertCrmCommunicationCommand>()
                                   .With(x => x.ReferenceId, referenceId)
                                   .With(x => x.Subject, subject)
                                   .With(x => x.Detail, detail)
                                   .Create();

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(async() => await _target.Handle(mockInput, CancellationToken.None));
        }
    }
}