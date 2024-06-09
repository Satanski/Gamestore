using FluentValidation;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Validation;

internal class GameUpdateModelValidator : AbstractValidator<GameUpdateModel>
{
    internal GameUpdateModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Null Id");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Missing Name");
        RuleFor(x => x.Key).NotEmpty().WithMessage("Missing Key");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Price should be provided");
        RuleFor(x => x.Discount).NotEmpty().WithMessage("Discount should be provided");
        RuleFor(x => x.Platforms).NotEmpty().WithMessage("Game platforms should be provided");
        RuleFor(x => x.Genres).NotEmpty().WithMessage("Game genres should be provided");
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) =>
           {
               var existingGame = await unitOfWork.GameRepository.GetByIdAsync(id);
               return existingGame != null;
           }).WithMessage("Game with given id doesn't exists");
    }
}
