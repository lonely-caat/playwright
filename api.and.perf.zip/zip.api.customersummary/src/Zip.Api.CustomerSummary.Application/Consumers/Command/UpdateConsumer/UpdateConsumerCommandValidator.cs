using System;
using FluentValidation;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateAddress;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateConsumer
{
    public class UpdateConsumerCommandValidator : AbstractValidator<UpdateConsumerCommand>
    {
        public UpdateConsumerCommandValidator()
        {
            RuleFor(x => x.ConsumerId)
                .GreaterThan(0);

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.DateOfBirth)
                .Must(x => x <= DateTime.Now.Date.AddYears(-18))
                .WithErrorCode("DOB")
                .WithMessage("The customer's age must be over 18.");

            RuleFor(x => x.Address)
                .NotNull()
                .SetValidator(new AddressValidator());
        } 
    }
}
