using FluentValidation;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateAddress
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Country)
                .NotNull();

            RuleFor(x => x.PostCode)
                .NotEmpty();

            RuleFor(x => x.State)
                .NotEmpty();

            RuleFor(x => x.StreetName)
                .NotEmpty();

            RuleFor(x => x.StreetNumber)
                .NotEmpty();

            RuleFor(x => x.Suburb)
                .NotEmpty();
        }
    }
}
