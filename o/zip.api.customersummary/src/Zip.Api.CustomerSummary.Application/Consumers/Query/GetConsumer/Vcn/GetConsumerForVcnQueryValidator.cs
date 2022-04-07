using System;
using FluentValidation;
using Zip.Api.CustomerSummary.Domain.Entities.Products;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.Vcn
{
    public class GetConsumerForVcnQueryValidator : AbstractValidator<GetConsumerForVcnQuery>
    {
        public GetConsumerForVcnQueryValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Invalid Card Id");
            RuleFor(x => x.Product).NotEmpty().Must(product => IsValidProduct(product)).WithMessage("Invalid Product");
        }

        private bool IsValidProduct(string product)
        {
            return Enum.IsDefined(typeof(ProductClassification), product);
        }

    }
}
