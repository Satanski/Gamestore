using FluentValidation;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Validation;

internal class GenreModelValidator : AbstractValidator<GenreModel>
{
    internal GenreModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Missing name");
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) =>
        {
            var genres = await unitOfWork.GenreRepository.GetAllAsync();
            var exisitngGenres = genres.Where(x => x.Id == id);
            return exisitngGenres.Any();
        }).WithMessage("This genre doesn't exist");
        RuleFor(x => x.ParentGenreId).MustAsync(async (id, cancellation) =>
        {
            if (id != null)
            {
                var genres = await unitOfWork.GenreRepository.GetAllAsync();
                var exisitngGenres = genres.Where(x => x.Id == id);
                return exisitngGenres.Any();
            }
            else
            {
                return true;
            }
        }).WithMessage("This parent genre doesn't exist");
        RuleFor(x => new { x.Id, x.ParentGenreId }).MustAsync(async (data, cancellation) =>
        {
            var genres = await unitOfWork.GenreRepository.GetAllAsync();
            var exisitngGenres = genres.Where(x => x.ParentGenreId == data.Id && x.Id == data.ParentGenreId);
            return !exisitngGenres.Any();
        }).WithMessage("You can't set a parent genre that have this genre as a parent genre.");
    }
}
