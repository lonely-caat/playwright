using System;
using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Command.BlockDigitalWalletToken
{
	public class BlockDigitalWalletTokenCommandValidator : AbstractValidator<BlockDigitalWalletTokenCommand>
	{
		public BlockDigitalWalletTokenCommandValidator()
		{
			RuleFor(x => x.DigitalWalletToken)
				.NotEmpty()
				.Must(y => y != Guid.Empty)
				.WithMessage("Invalid Digital Wallet Token");
		}
	}
}
