﻿using FluentValidation;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Validation;

internal class GameAddValidator : AbstractValidator<GameAdd>
{
    internal GameAddValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Name).NotNull().WithMessage("Missing name");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Missing price");
        RuleFor(x => x.UnitInStock).NotEmpty().WithMessage("Missing units in stock");
        RuleFor(x => x.Discontinued).NotEmpty().WithMessage("Missing discount");
        RuleFor(x => x.Key).NotEmpty().WithMessage("Missing key");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Missing description");
        RuleFor(x => x.Name).MustAsync(async (name, cancellation) =>
        {
            var games = await unitOfWork.GameRepository.GetAllAsync();
            var exisitngGenres = games.Where(x => x.Name == name);
            return !exisitngGenres.Any();
        }).WithMessage("Game with this name already exists");
        RuleFor(x => x.Key).MustAsync(async (key, cancellation) =>
        {
            var games = await unitOfWork.GameRepository.GetAllAsync();
            var exisitngGenres = games.Where(x => x.Key == key);
            return !exisitngGenres.Any();
        }).WithMessage("Game with this key already exists");
    }
}