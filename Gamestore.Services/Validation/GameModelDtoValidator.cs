using FluentValidation;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Validation;

internal class GameModelDtoValidator : AbstractValidator<GameModelDto>
{
    internal GameModelDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Missing Name");
        RuleFor(x => x.Key).NotEmpty().WithMessage("Missing Key");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Price should be provided");
        RuleFor(x => x.Discontinued).NotEmpty().WithMessage("Discount should be provided");
        RuleFor(x => x.Platforms).NotEmpty().WithMessage("Game platforms should be provided");
        RuleFor(x => x.Genres).NotEmpty().WithMessage("Game genres should be provided");
    }
}
