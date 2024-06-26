using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering;

public class FilterService : IFilterService
{
    private readonly GenreFilterHandler _handlerChain;

    public FilterService()
    {
        var genreFilterHandler = new GenreFilterHandler();
        var platformFilterHandler = new PlatformFilterHandler();
        var publisherFilterHandler = new PublisherFilterHandler();

        genreFilterHandler.SetNext(platformFilterHandler);
        platformFilterHandler.SetNext(publisherFilterHandler);

        _handlerChain = genreFilterHandler;
    }

    public async Task<List<Game>> FilterGames(IUnitOfWork unitOfWork, List<Game> filteredGames, List<Guid> genresFilter, List<Guid> platformsFilter, List<Guid> publishersFilter)
    {
        return await _handlerChain.HandleAsync(unitOfWork, filteredGames, genresFilter, platformsFilter, publishersFilter);
    }
}
