using System;
using Microsoft.Extensions.Options;
using Moq;
using SendGrid;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.UnitTests.Common;
using Zip.Api.CustomerSummary.Application.Communications.Command.SendAccountClosedEmail;
using Zip.Api.CustomerSummary.Infrastructure.Configuration.EmailSettings;
using AutoFixture;
using FluentValidation;
using System.Threading;
using SendGrid.Helpers.Mail;
using MediatR;
using System.Threading.Tasks;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Emails
{
    public class SendAccountClosedEmailCommandHandlerTest : CommonTestFixture
    {
        private readonly Mock<ISendGridClient> sendGridClient = new Mock<ISendGridClient>();
        private readonly Mock<IOptions<EmailSettings>> options = new Mock<IOptions<EmailSettings>>();
        private readonly SendAccountClosedEmailCommandHandler _target;

        public SendAccountClosedEmailCommandHandlerTest()
        {
            var mockEmailSetting = new EmailSettings
            {
                ApiKey = "test",
                CloseAccountEmail = _fixture.Create<EmailTemplate>()
            };
            options.Setup(x => x.Value).Returns(mockEmailSetting);

            _target = new SendAccountClosedEmailCommandHandler(options.Object, sendGridClient.Object);
        }

        [Fact]
        public void Given_NullInjection_Should()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new SendAccountClosedEmailCommandHandler(null, sendGridClient.Object);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                options.Setup(x => x.Value).Returns(new EmailSettings());
                
                new SendAccountClosedEmailCommandHandler(options.Object, null);
            });
        }

        [Fact]
        public void Given_InvalidPayload_Handle_ShouldReturn_ValidationException()
        {
            // Arrange 
            var mockInput = _fixture.Create<SendAccountClosedEmailCommand>();

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(async () => await _target.Handle(mockInput, CancellationToken.None));
        }

        [Fact]
        public async Task Given_ValidPayload_Handle_ShouldReturn_UnitValueAsync()
        {
            // Arrange
            sendGridClient.Setup(x => x.SendEmailAsync(
                It.IsAny<SendGridMessage>(),
                It.IsAny<CancellationToken>()));
            var mockInput = _fixture.Build<SendAccountClosedEmailCommand>()
                                    .With(x => x.Email, "johnny.vuong@zip.co")
                                    .Create();

            // Act
            var result = await _target.Handle(mockInput, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
        }
    }
}
