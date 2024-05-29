using Gamestore.DAL.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Gamestore.WebApi.Middlewares;

public class GameCounterMiddlerware(RequestDelegate next, IMemoryCache cache)
{
    private readonly RequestDelegate _next = next;
    private readonly IMemoryCache _cache = cache;

    public async Task InvokeAsync(HttpContext httpContext, GamestoreContext dbContext)
    {
        httpContext.Response.OnStarting(() =>
        {
            var numberOfGames = _cache.GetOrCreate("NumberOfGames", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return dbContext.Games.Count();
            });

            httpContext.Response.Headers.Append("x-total-number-of-games", numberOfGames.ToString());
            return Task.CompletedTask;
        });

        await _next(httpContext);
    }
}
