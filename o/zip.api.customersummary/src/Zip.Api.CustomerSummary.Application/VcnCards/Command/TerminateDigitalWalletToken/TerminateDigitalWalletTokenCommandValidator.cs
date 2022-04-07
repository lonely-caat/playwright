using System;
using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Command.TerminateDigitalWalletToken
{
	public class TerminateDigitalWalletTokenCommandValidator : AbstractValidator<TerminateDigitalWalletTokenCommand>
	{
		public TerminateDigitalWalletTokenCommandValidator()
		{
			RuleFor(x => x.DigitalWalletToken)
				.NotEmpty()
				.Must(y => y != Guid.Empty)
				.WithMessage("Invalid Digital Wallet Token");
		}
	}
}
