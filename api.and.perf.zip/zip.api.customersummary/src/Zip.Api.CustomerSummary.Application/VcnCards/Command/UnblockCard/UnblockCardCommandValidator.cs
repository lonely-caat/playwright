using System;
using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Command.UnblockCard
{
	public class UnblockCardCommandValidator : AbstractValidator<UnblockCardCommand>
	{
		public UnblockCardCommandValidator()
		{
			RuleFor(x => x.CardId)
				.NotEmpty()
				.Must(y => y != Guid.Empty)
				.WithMessage("Invalid Card Id");
		}
	}
}
