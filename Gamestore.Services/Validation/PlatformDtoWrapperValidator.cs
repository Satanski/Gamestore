using FluentValidation;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Validation;

internal class PlatformDtoWrapperValidator : AbstractValidator<PlatformModelDto>
{
    internal PlatformDtoWrapperValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Type).NotEmpty().WithMessage("Missing type");
        RuleFor(x => x.Type).Must(type =>
        {
            return !string.IsNullOrEmpty(type);
        }).WithMessage("Type can't be an empty string");
        RuleFor(x => x.Type).MustAsync(async (type, cancellation) =>
        {
            var platforms = await unitOfWork.PlatformRepository.GetAllAsync();
            var exisitngPlatforms = platforms.Where(x => x.Type == type);
            return !exisitngPlatforms.Any();
        }).WithMessage("This platform already exists");
    }
}
