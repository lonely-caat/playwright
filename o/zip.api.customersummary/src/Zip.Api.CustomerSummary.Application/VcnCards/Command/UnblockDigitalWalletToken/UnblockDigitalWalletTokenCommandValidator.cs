using System;
using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Command.UnblockDigitalWalletToken
{
	public class UnblockDigitalWalletTokenCommandValidator : AbstractValidator<UnblockDigitalWalletTokenCommand>
	{
		public UnblockDigitalWalletTokenCommandValidator()
		{
			RuleFor(x => x.DigitalWalletToken)
				.NotEmpty()
				.Must(y => y != Guid.Empty)
				.WithMessage("Invalid Digital Wallet Token");
		}
	}
}
