using Gamestore.BLL.BanHandler;
using Gamestore.BLL.Filtering.Handlers;

namespace Gamestore.BLL.Filtering;

public class FilterServiceDirector(
    IFilterServiceBuilder builder,
    IGenreFilterHandler genreFilterHandler,
    INameFilterHandler nameFilterHandler,
    IPaginationFilterHandler paginationFilterHandler,
    IPlatformFilterHandler platformFilterHandler,
    IPriceFilterHandler priceFilterHandler,
    IPublishDateHandler publishDateHandler,
    IPublisherFilterHandler publisherFilterHandler,
    ISortingHandler sortingHandler) : IFilterServiceDirector
{
    private readonly IFilterServiceBuilder _builder = builder;

    public FilterService ConstructFilterService()
    {
        return _builder
            .AddHandler((IFilterHandler)genreFilterHandler)
            .AddHandler((IFilterHandler)platformFilterHandler)
            .AddHandler((IFilterHandler)publisherFilterHandler)
            .AddHandler((IFilterHandler)priceFilterHandler)
            .AddHandler((IFilterHandler)publishDateHandler)
            .AddHandler((IFilterHandler)nameFilterHandler)
            .AddHandler((IFilterHandler)sortingHandler)
            .AddHandler((IFilterHandler)paginationFilterHandler)
            .Build();
    }
}
