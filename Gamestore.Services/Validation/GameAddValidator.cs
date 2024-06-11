using FluentValidation;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Validation;

internal class GameAddValidator : AbstractValidator<GameAdd>
{
    internal GameAddValidator()
    {
        RuleFor(x => x.Name).NotNull().WithMessage("Missing name");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Missing price");
        RuleFor(x => x.UnitInStock).NotEmpty().WithMessage("Missing units in stock");
        RuleFor(x => x.Discount).NotEmpty().WithMessage("Missing discount");
        RuleFor(x => x.Key).NotEmpty().WithMessage("Missing key");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Missing description");
    }
}
