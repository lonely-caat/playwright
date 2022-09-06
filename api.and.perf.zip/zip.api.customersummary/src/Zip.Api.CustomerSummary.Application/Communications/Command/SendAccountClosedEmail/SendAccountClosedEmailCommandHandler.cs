using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Zip.Api.CustomerSummary.Infrastructure.Configuration.EmailSettings;

namespace Zip.Api.CustomerSummary.Application.Communications.Command.SendAccountClosedEmail
{
    public class SendAccountClosedEmailCommandHandler : IRequestHandler<SendAccountClosedEmailCommand>
    {
        private readonly EmailSettings _emailSettings;
        private readonly ISendGridClient _sendGridClient;

        public SendAccountClosedEmailCommandHandler(IOptions<EmailSettings> emailSettings, ISendGridClient sendGridClient)
        {
            _emailSettings = emailSettings?.Value ?? throw new ArgumentNullException(nameof(emailSettings));
            _sendGridClient = sendGridClient ?? throw new ArgumentNullException(nameof(sendGridClient));
        }

        public async Task<Unit> Handle(SendAccountClosedEmailCommand request, CancellationToken cancellationToken)
        {
            var validator = new SendAccountClosedEmailCommandValidator();
            var validateResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }

            var sendGridMessage = new SendGridMessage
            {
                From = new EmailAddress
                {
                    Email = _emailSettings.CloseAccountEmail.SenderEmailAddress,
                    Name = _emailSettings.CloseAccountEmail.SenderName
                },
                TemplateId = _emailSettings.CloseAccountEmail.Id
            };

            sendGridMessage.AddTo(request.Email);
            sendGridMessage.SetTemplateData(request);

            await _sendGridClient.SendEmailAsync(sendGridMessage, cancellationToken);

            return Unit.Value;
        }
    }
}
