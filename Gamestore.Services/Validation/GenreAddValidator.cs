using FluentValidation;
using Gamestore.BLL.Models;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Validation;

internal class GenreAddValidator : AbstractValidator<GenreAdd>
{
    internal GenreAddValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Missing name");
        RuleFor(x => x.Name).MustAsync(async (name, cancellation) =>
        {
            var genres = await unitOfWork.GenreRepository.GetAllAsync();
            var exisitngGenres = genres.Where(x => x.Name == name);
            return !exisitngGenres.Any();
        }).WithMessage("This genre already exists");
        RuleFor(x => x.ParentGenreId).MustAsync(async (id, cancellation) =>
        {
            var genres = await unitOfWork.GenreRepository.GetAllAsync();
            var exisitngGenres = genres.Where(x => x.Id == id);
            return exisitngGenres.Any() || id == null;
        }).WithMessage("This parent genre doesn't exist");
    }
}
