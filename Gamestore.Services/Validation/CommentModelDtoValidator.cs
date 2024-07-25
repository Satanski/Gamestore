using FluentValidation;
using Gamestore.BLL.Models;

namespace Gamestore.BLL.Validation;

internal class CommentModelDtoValidator : AbstractValidator<CommentModelDto>
{
    internal CommentModelDtoValidator()
    {
        RuleFor(x => x.Comment.Body).NotNull().WithMessage("Missing body");
    }
}
