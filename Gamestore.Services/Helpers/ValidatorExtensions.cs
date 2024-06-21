using Gamestore.BLL.Models;
using Gamestore.BLL.Models.Payment;
using Gamestore.BLL.Validation;

namespace Gamestore.BLL.Helpers;

internal static class ValidatorExtensions
{
    internal static async Task ValidatePublisher(this PublisherDtoWrapperValidator validator, PublisherDtoWrapper publisherModel)
    {
        var result = await validator.ValidateAsync(publisherModel.Publisher);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }
    }

    internal static async Task ValidatePlatform(this PlatformDtoWrapperValidator validator, PlatformDtoWrapper platformModel)
    {
        var result = await validator.ValidateAsync(platformModel.Platform);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }
    }

    internal static async Task ValidateGenreForAdding(this GenreDtoWrapperAddValidator validator, GenreDtoWrapper genreModel)
    {
        var result = await validator.ValidateAsync(genreModel.Genre);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }
    }

    internal static async Task ValidateGenreForUpdating(this GenreDtoWrapperUpdateValidator validator, GenreDtoWrapper genreModel)
    {
        var result = await validator.ValidateAsync(genreModel.Genre);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }
    }

    internal static async Task ValidateGame(this GameDtoWrapperValidator validator, GameDtoWrapper gameModel)
    {
        var result = await validator.ValidateAsync(gameModel);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }
    }

    internal static async Task ValidateVisaPayment(this VisaPaymentValidator validator, PaymentModelDto payment)
    {
        var result = await validator.ValidateAsync(payment);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }
    }
}
