using Gamestore.BLL.Filtering.Handlers;

namespace Gamestore.BLL.Filtering;

public class GameProcessingPipelineDirector(
    IGameProcessingPipelineBuilder builder,
    GenreFilterHandler genreFilterHandler,
    NameFilterHandler nameFilterHandler,
    PaginationFilterHandler paginationFilterHandler,
    PlatformFilterHandler platformFilterHandler,
    PriceFilterHandler priceFilterHandler,
    PublishDateHandler publishDateHandler,
    PublisherFilterHandler publisherFilterHandler,
    SortingHandler sortingHandler) : IGameProcessingPipelineDirector
{
    private readonly IGameProcessingPipelineBuilder _builder = builder;

    public IGameProcessingPipelineService ConstructGameCollectionOperationService()
    {
        return _builder
            .AddHandler(genreFilterHandler)
            .AddHandler(platformFilterHandler)
            .AddHandler(publisherFilterHandler)
            .AddHandler(priceFilterHandler)
            .AddHandler(publishDateHandler)
            .AddHandler(nameFilterHandler)
            .AddHandler(sortingHandler)
            .AddHandler(paginationFilterHandler)
            .Build();
    }
}
