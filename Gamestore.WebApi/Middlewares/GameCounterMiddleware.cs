using Gamestore.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Gamestore.WebApi.Middlewares;

public class GameCounterMiddleware(RequestDelegate next, IMemoryCache cache)
{
    private const string Key = "x-total-number-of-games";

    public Task InvokeAsync(HttpContext httpContext, IConfiguration config, IGameService gameService)
    {
        var section = config.GetSection("NumberOfGamesHeaderCacheTime");

        if (!int.TryParse(section.Value, out var cacheTime))
        {
            cacheTime = 0;
        }

        httpContext.Response.OnStarting(async () =>
        {
            var numberOfGames = await cache.GetOrCreate("NumberOfGames", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheTime);
                var games = await gameService.GetAllGamesAsync();
                return games.Count();
            });

            httpContext.Response.Headers.Append(Key, numberOfGames.ToString());
        });

        return next(httpContext);
    }
}
