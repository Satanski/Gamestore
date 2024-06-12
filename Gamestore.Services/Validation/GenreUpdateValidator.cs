using FluentValidation;
using Gamestore.BLL.Models;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Validation;

internal class GenreUpdateValidator : AbstractValidator<GenreUpdate>
{
    internal GenreUpdateValidator(IUnitOfWork unitOfWork)
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
            var genres = await unitOfWork.GenreRepository.GetAllAsync();
            var exisitngGenres = genres.Where(x => x.Id == id);
            return exisitngGenres.Any() || id == null;
        }).WithMessage("This parent genre doesn't exist");
        RuleFor(x => new { x.Id, x.ParentGenreId }).MustAsync(async (data, cancellation) =>
        {
            var genres = await unitOfWork.GenreRepository.GetAllAsync();
            var existingGenres = genres.Where(x => x.ParentGenreId == data.Id && x.Id == data.ParentGenreId);
            return !existingGenres.Any();
        }).WithMessage("You can't set a parent genre that have this genre as a parent genre.");
        RuleFor(x => new { x.Id, x.ParentGenreId }).Must((data, cancellation) =>
        {
            return data.Id != data.ParentGenreId;
        }).WithMessage("You can't set the ganre as self parent genre.");
    }
}
