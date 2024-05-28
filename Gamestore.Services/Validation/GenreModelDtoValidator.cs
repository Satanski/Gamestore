using FluentValidation;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Validation;

internal class GenreModelDtoValidator : AbstractValidator<GenreModelDto>
{
    public GenreModelDtoValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Missing name");
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) =>
        {
            var existingGenre = await unitOfWork.GenreRepository.GetByIdAsync(id);
            return existingGenre != null;
        }).WithMessage("No genre with given id");
    }
}
