using FluentValidation;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Validation;

internal class PlatformModelDtoValidator : AbstractValidator<PlatformModelDto>
{
    public PlatformModelDtoValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Type).NotEmpty().WithMessage("Missing type");
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) =>
        {
            var existingPlatform = await unitOfWork.PlatformRepository.GetByIdAsync(id);
            return existingPlatform != null;
        }).WithMessage("No platform with given id");
    }
}
