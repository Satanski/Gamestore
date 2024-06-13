using FluentValidation;
using Gamestore.BLL.Models;

namespace Gamestore.BLL.Validation;

internal class GameDtoWrapperValidator : AbstractValidator<GameDtoWrapper>
{
    internal GameDtoWrapperValidator()
    {
        RuleFor(x => x.Game).NotNull().WithMessage("Missing Game object");
        RuleFor(x => x.Publisher).NotEmpty().WithMessage("Publisher should be provided");
        RuleFor(x => x.Platforms).NotEmpty().WithMessage("Game platforms should be provided");
        RuleFor(x => x.Genres).NotEmpty().WithMessage("Game genres should be provided");
        RuleFor(x => x.Game.Name).NotNull().WithMessage("Missing name");
        RuleFor(x => x.Game.Discontinued).GreaterThanOrEqualTo(0).WithMessage("Discount lower then 0");
        RuleFor(x => x.Game.Discontinued).LessThanOrEqualTo(100).WithMessage("Discount greater then 100");
        RuleFor(x => x.Game.Price).GreaterThan(0).WithMessage("Missing Game object");
        RuleFor(x => x.Game.UnitInStock).GreaterThanOrEqualTo(0).WithMessage("Missing Game object");
        RuleFor(x => x.Game.Name).Must(companyName =>
        {
            return !string.IsNullOrEmpty(companyName);
        }).WithMessage("Name can't be an empty string");
    }
}
