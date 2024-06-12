using FluentValidation;
using Gamestore.BLL.Models;

namespace Gamestore.BLL.Validation;

internal class GameUpdateDtoValidator : AbstractValidator<GameUpdateDto>
{
    internal GameUpdateDtoValidator()
    {
        RuleFor(x => x.Game).NotNull().WithMessage("Missing Game object");
        RuleFor(x => x.Publisher).NotEmpty().WithMessage("Publisher should be provided");
        RuleFor(x => x.Platforms).NotEmpty().WithMessage("Game platforms should be provided");
        RuleFor(x => x.Genres).NotEmpty().WithMessage("Game genres should be provided");
    }
}
