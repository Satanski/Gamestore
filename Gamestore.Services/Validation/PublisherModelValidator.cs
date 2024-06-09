using FluentValidation;
using Gamestore.BLL.Models;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Validation;

internal class PublisherModelValidator : AbstractValidator<PublisherModel>
{
    public PublisherModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.CompanyName).NotNull().WithMessage("Null CompanyName");
        RuleFor(x => new { x.CompanyName, x.Id }).MustAsync(async (data, cancellation) =>
        {
            var publishers = await unitOfWork.PublisherRepository.GetAllAsync();
            var existingPublisher = publishers.Where(x => x.CompanyName == data.CompanyName && x.Id != data.Id);
            return !existingPublisher.Any();
        }).WithMessage("Publisher with given Name already exists");
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) =>
        {
            var existingPublisher = await unitOfWork.PublisherRepository.GetByIdAsync(id);
            return existingPublisher != null;
        }).WithMessage("Publisher with given id doesn't exist");
    }
}
