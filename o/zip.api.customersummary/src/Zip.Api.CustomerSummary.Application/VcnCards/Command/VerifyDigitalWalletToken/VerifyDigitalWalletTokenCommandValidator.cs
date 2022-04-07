using System;
using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Command.VerifyDigitalWalletToken
{
	public class VerifyDigitalWalletTokenCommandValidator : AbstractValidator<VerifyDigitalWalletTokenCommand>
	{
		public VerifyDigitalWalletTokenCommandValidator()
		{
			RuleFor(x => x.DigitalWalletToken)
				.NotEmpty()
				.Must(y => y != Guid.Empty)
				.WithMessage("Invalid Digital Wallet Token");
		}
	}
}
