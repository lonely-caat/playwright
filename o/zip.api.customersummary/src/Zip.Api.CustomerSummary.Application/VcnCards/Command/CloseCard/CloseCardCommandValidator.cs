using System;
using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Command.CloseCard
{
	public class CloseCardCommandValidator : AbstractValidator<CloseCardCommand>
	{
		public CloseCardCommandValidator()
		{
			RuleFor(x => x.CardId)
				.NotEmpty()
				.Must(y => y != Guid.Empty)
				.WithMessage("Invalid Card Id");
		}
	}
}
