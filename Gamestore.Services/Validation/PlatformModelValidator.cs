using FluentValidation;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Validation;

internal class PlatformModelValidator : AbstractValidator<PlatformModel>
{
    internal PlatformModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Type).NotEmpty().WithMessage("Missing type");
        RuleFor(x => x.Type).MustAsync(async (type, cancellation) =>
        {
            var platforms = await unitOfWork.PlatformRepository.GetAllAsync();
            var existingPlatform = platforms.Where(x => x.Type == type);
            return !existingPlatform.Any();
        }).WithMessage("This platform already exists");
    }
}
