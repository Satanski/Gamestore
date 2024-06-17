using FluentValidation;
using Gamestore.BLL.Helpers;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Validation;

internal class GenreDtoWrapperAddValidator : AbstractValidator<GenreModelDto>
{
    internal GenreDtoWrapperAddValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Missing name");
        RuleFor(x => x.Name).Must(companyName =>
        {
            return !string.IsNullOrEmpty(companyName);
        }).WithMessage("Name can't be an empty string");

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
        RuleFor(x => new { x.Id, x.ParentGenreId }).MustAsync(async (data, cancellation) =>
        {
            var genres = await unitOfWork.GenreRepository.GetAllAsync();
            List<Genre> forbiddenList = [];

            if (data.Id != null)
            {
                ValidationHelpers.CyclicReferenceHelper(genres, forbiddenList, (Guid)data.Id);
            }

            return !forbiddenList.Exists(x => x.Id == data.ParentGenreId);
        }).WithMessage("Cyclic references not allowed");
    }
}
