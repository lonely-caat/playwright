using System;
using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Command.BlockCard
{
	public class BlockCardCommandValidator : AbstractValidator<BlockCardCommand>
	{
		public BlockCardCommandValidator()
		{
			RuleFor(x => x.CardId)
				.NotEmpty()
				.Must(y => y != Guid.Empty)
				.WithMessage("Invalid Card Id");
		}
	}
}
