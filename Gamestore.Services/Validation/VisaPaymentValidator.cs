using FluentValidation;
using Gamestore.BLL.Models.Payment;

namespace Gamestore.BLL.Validation;

internal class VisaPaymentValidator : AbstractValidator<PaymentModelDto>
{
    internal VisaPaymentValidator()
    {
        RuleFor(x => x.Model.CardNumber).NotEmpty().WithMessage("Missing card number");
        RuleFor(x => x.Model.MonthExpire).NotEmpty().WithMessage("Missing expiration month");
        RuleFor(x => x.Model.YearExpire).NotEmpty().WithMessage("Missing expiration year");
        RuleFor(x => x.Model.Cvv2).NotEmpty().WithMessage("Missing Cvv2");
        RuleFor(x => x.Model.Holder).NotEmpty().WithMessage("Missing card holder name");
    }
}
