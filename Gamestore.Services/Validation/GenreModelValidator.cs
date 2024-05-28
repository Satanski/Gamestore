using FluentValidation;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Validation;

internal class GenreModelValidator : AbstractValidator<GenreModel>
{
    public GenreModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Missing name");
        RuleFor(x => x.Name).MustAsync(async (name, cancellation) =>
        {
            var genres = await unitOfWork.GenreRepository.GetAllAsync();
            var exisitngGenres = genres.Where(x => x.Name == name);
            return exisitngGenres == null;
        }).WithMessage("This genre already exists");
    }
}
