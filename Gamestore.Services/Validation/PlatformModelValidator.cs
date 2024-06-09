using FluentValidation;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Validation;

internal class PlatformModelValidator : AbstractValidator<PlatformModel>
{
    internal PlatformModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Type).NotEmpty().WithMessage("Missing type");
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) =>
        {
            var platforms = await unitOfWork.PlatformRepository.GetAllAsync();
            var existingPlatform = platforms.Where(x => x.Id == id);
            return existingPlatform.Any();
        }).WithMessage("This platform doesn't exist");
    }
}
