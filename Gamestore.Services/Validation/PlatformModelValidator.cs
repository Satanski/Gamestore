using FluentValidation;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Validation;

public class PlatformModelValidator : AbstractValidator<PlatformModel>
{
    public PlatformModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Type).NotEmpty().WithMessage("Missing type");
        RuleFor(x => x.Type).MustAsync(async (type, cancellation) =>
        {
            var platforms = await unitOfWork.PlatformRepository.GetAllAsync();
            var exisitngPlatform = platforms.Where(x => x.Type == type);
            return !exisitngPlatform.Any();
        }).WithMessage("This platform already exists");
    }
}
