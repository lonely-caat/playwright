using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Models;

namespace Zip.Api.CustomerSummary.Application.Communications.Command.SendResetPasswordEmailNew
{
    public class SendResetPasswordEmailNewCommandHandler : IRequestHandler<SendResetPasswordEmailNewCommand>
    {
        private readonly ICoreService _coreService;

        private readonly ILogger<SendResetPasswordEmailNewCommandHandler> _logger;

        public SendResetPasswordEmailNewCommandHandler(
            ICoreService coreService,
            ILogger<SendResetPasswordEmailNewCommandHandler> logger)
        {
            _coreService = coreService;
            _logger = logger;
        }

        public async Task<Unit> Handle(SendResetPasswordEmailNewCommand request, CancellationToken cancellationToken)
        {
            LogInformation($"Attempting to send reset password for consumer: {request.ConsumerId}");
            var token = await _coreService.GetCoreTokenAsync(cancellationToken);
            var coreResetPassword = new CoreResetPasswordModel { Email = request.Email };
            var authenticationHeader = $"{token.TokenType} {token.AccessToken}";
            await _coreService.SendResetPasswordEmailAsync(coreResetPassword, authenticationHeader, cancellationToken);

            return Unit.Value;
        }

        private void LogInformation(string message)
        {
            _logger.LogInformation(
                $"{SerilogProperty.ClassName}::{SerilogProperty.MethodName}::{SerilogProperty.Message}",
                nameof(SendResetPasswordEmailNewCommandHandler),
                nameof(Handle),
                message);
        }
    }
}