using FluentValidation;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Validation;

internal class GenreModelDtoValidator : AbstractValidator<GenreModelDto>
{
    internal GenreModelDtoValidator(IUnitOfWork unitOfWork)
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
    }
}
