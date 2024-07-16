using FluentValidation;
using Gamestore.BLL.Helpers;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Validation;

internal class GenreDtoWrapperUpdateValidator : AbstractValidator<GenreModelDto>
{
    internal GenreDtoWrapperUpdateValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Missing name");
        RuleFor(x => x.Name).Must(companyName =>
        {
            return !string.IsNullOrEmpty(companyName);
        }).WithMessage("Name can't be an empty string");

        RuleFor(x => new { x.Name, x.Id }).MustAsync(async (data, cancellation) =>
        {
            var genres = await unitOfWork.GenreRepository.GetAllAsync();
            var exisitngGenres = genres.Where(x => x.Name == data.Name && x.Id != data.Id);
            return !exisitngGenres.Any();
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
            var existingGenres = genres.Where(x => x.ParentCategoryId == data.Id && x.Id == data.ParentGenreId);
            return !existingGenres.Any();
        }).WithMessage("You can't set a parent genre that have this genre as a parent genre.");
        RuleFor(x => new { x.Id, x.ParentGenreId }).MustAsync(async (data, cancellation) =>
        {
            var genres = await unitOfWork.GenreRepository.GetAllAsync();
            List<Category> forbiddenList = [];

            if (data.Id != null)
            {
                ValidationHelpers.CyclicReferenceHelper(genres, forbiddenList, (Guid)data.Id);
            }

            return !forbiddenList.Exists(x => x.Id == data.ParentGenreId);
        }).WithMessage("Cyclic references not allowed");
        RuleFor(x => new { x.Id, x.ParentGenreId }).Must((data, cancellation) =>
        {
            return data.Id != data.ParentGenreId;
        }).WithMessage("You can't set the ganre as self parent genre.");
    }
}
