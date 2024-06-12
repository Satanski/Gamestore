using FluentValidation;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Validation;

internal class GameUpdateValidator : AbstractValidator<GameUpdate>
{
    internal GameUpdateValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Name).NotNull().WithMessage("Missing name");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Missing price");
        RuleFor(x => x.UnitInStock).NotEmpty().WithMessage("Missing units in stock");
        RuleFor(x => x.Discontinued).NotEmpty().WithMessage("Missing discount");
        RuleFor(x => x.Key).NotEmpty().WithMessage("Missing key");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Missing description");
        RuleFor(x => new { x.Name, x.Id }).MustAsync(async (data, cancellation) =>
        {
            var games = await unitOfWork.GameRepository.GetAllAsync();
            var exisitngGenres = games.Where(x => x.Name == data.Name && x.Id != data.Id);
            return !exisitngGenres.Any();
        }).WithMessage("Other game with this name already exists");
        RuleFor(x => new { x.Key, x.Id }).MustAsync(async (data, cancellation) =>
        {
            var games = await unitOfWork.GameRepository.GetAllAsync();
            var exisitngGenres = games.Where(x => x.Key == data.Key && x.Id != data.Id);
            return !exisitngGenres.Any();
        }).WithMessage("Other game with this key already exists");
    }
}
