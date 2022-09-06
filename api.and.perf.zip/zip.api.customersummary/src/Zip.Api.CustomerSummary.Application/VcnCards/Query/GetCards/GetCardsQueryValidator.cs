using System;
using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCards
{
	public class GetCardsQueryValidator : AbstractValidator<GetCardsQuery>
	{
		public GetCardsQueryValidator()
		{
			RuleFor(x => x.CustomerId)
				.NotEmpty()
				.Must(y => y != Guid.Empty)
				.WithMessage("Invalid Customer Id");
		}
	}
}
