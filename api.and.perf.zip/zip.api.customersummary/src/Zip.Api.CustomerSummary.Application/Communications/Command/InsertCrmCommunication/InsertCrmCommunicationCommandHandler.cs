using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Zip.Api.CustomerSummary.Domain.Entities.MessageLog;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;

namespace Zip.Api.CustomerSummary.Application.Communications.Command.InsertCrmCommunication
{
    public class InsertCrmCommunicationCommandHandler : IRequestHandler<InsertCrmCommunicationCommand, bool>
    {
        private readonly IMessageLogContext _messageLogContext;
        private readonly ILogger<InsertCrmCommunicationCommandHandler> _logger;

        public InsertCrmCommunicationCommandHandler(
            IMessageLogContext messageLogContext,
            ILogger<InsertCrmCommunicationCommandHandler> logger)
        {
            _messageLogContext = messageLogContext ?? throw new ArgumentNullException(nameof(messageLogContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(InsertCrmCommunicationCommand request, CancellationToken cancellationToken)
        {
            var validator = new InsertCrmCommunicationCommandValidator();
            var validateResult = await validator.ValidateAsync(request, cancellationToken);
            var messageId = Guid.NewGuid();
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }

            _logger.LogInformation($"{SerilogProperty.ClassName}::{SerilogProperty.MethodName}::{SerilogProperty.Message}",
                nameof(InsertCrmCommunicationCommandHandler),
                nameof(Handle),
                $"Logging Crm Communication into db for {request.ReferenceId} on {messageId}");

            var message = new MessageLogSettings
            {
                DeliveryMethod = request.DeliveryMethod,
                Status = request.Status,
                Category = request.Category,
                Type = request.Type
            };

            var isSuccessfulDatabaseResponse = await _messageLogContext.InsertAsync(
                request.ReferenceId,
                messageId,
                request.Subject,
                request.Detail,
                message,
                DateTime.Now,
                request.MessageBody);

            if (isSuccessfulDatabaseResponse)
            {
                _logger.LogInformation($"{SerilogProperty.ClassName}::{SerilogProperty.MethodName}::{SerilogProperty.Message}",
                nameof(InsertCrmCommunicationCommandHandler),
                nameof(Handle),
                $"Successful insertion into for Crm Communication into db for {request.ReferenceId} on {messageId}");
            }
            else
            {
                _logger.LogError($"{SerilogProperty.ClassName}::{SerilogProperty.MethodName}::{SerilogProperty.Message}",
                nameof(InsertCrmCommunicationCommandHandler),
                nameof(Handle),
                $"Unable to insert Crm Communication into db for {request.ReferenceId} on {messageId}");
            }

            return isSuccessfulDatabaseResponse;
        }
    }
}
