using System;
using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCard
{
	public class GetCardQueryValidator : AbstractValidator<GetCardQuery>
	{
		public GetCardQueryValidator()
		{
			RuleFor(x => x.CardId).NotEmpty().When(m => m.ExternalId == Guid.Empty).WithMessage("Invalid Card Id");
			RuleFor(x => x.ExternalId).NotEmpty().When(m => m.CardId == Guid.Empty).WithMessage("Invalid ExternalId");
		}
	}
}
