using FluentValidation;
using Gamestore.BLL.Models;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Validation;

internal class PublisherDtoWrapperValidator : AbstractValidator<PublisherModelDto>
{
    public PublisherDtoWrapperValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.CompanyName).NotNull().WithMessage("Null CompanyName");
        RuleFor(x => x.CompanyName).Must(companyName =>
        {
            return !string.IsNullOrEmpty(companyName);
        }).WithMessage("Company name can't be an empty string");
        RuleFor(x => x.CompanyName).MustAsync(async (companyName, cancellation) =>
        {
            var existingPublisher = await unitOfWork.PublisherRepository.GetByCompanyNameAsync(companyName);
            return existingPublisher == null;
        }).WithMessage("Publisher with given Name already exists");
    }
}
