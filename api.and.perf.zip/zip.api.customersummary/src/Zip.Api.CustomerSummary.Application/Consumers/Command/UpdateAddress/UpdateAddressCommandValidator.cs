using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateAddress
{
    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressCommandValidator()
        {
            RuleFor(x => x.Address)
                .SetValidator(new AddressValidator());

            RuleFor(x => x.ConsumerId)
                .GreaterThan(0);
        }
    }
}
