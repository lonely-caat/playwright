using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Payments.Command.CreateBankPaymentMethod
{
    public class CreateBankPaymentMethodCommandValidator : AbstractValidator<CreateBankPaymentMethodCommand>
    {
        public CreateBankPaymentMethodCommandValidator()
        {
            RuleFor(x => x.ConsumerId).GreaterThan(0);

            RuleFor(x => x.BSB).NotEmpty();

            RuleFor(x => x.AccountName).NotEmpty();

            RuleFor(x => x.AccountNumber).NotEmpty();
        }
    }
}
