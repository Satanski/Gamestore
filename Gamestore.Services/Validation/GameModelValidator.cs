using FluentValidation;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Validation;

internal class GameModelValidator : AbstractValidator<GameModel>
{
    internal GameModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Missing Name");
        RuleFor(x => x.Key).NotEmpty().WithMessage("Missing Key");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Price should be provided");
        RuleFor(x => x.Discount).NotEmpty().WithMessage("Discount should be provided");
        RuleFor(x => x.PublisherId).NotEmpty().WithMessage("Publisher id should be provided");
        RuleFor(x => x.Platforms).NotEmpty().WithMessage("Game platforms should be provided");
        RuleFor(x => x.Genres).NotEmpty().WithMessage("Game genres should be provided");
    }
}
