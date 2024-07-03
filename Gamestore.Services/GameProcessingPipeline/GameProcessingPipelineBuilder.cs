using Gamestore.BLL.BanHandler;

namespace Gamestore.BLL.Filtering;

public class GameProcessingPipelineBuilder : IGameProcessingPipelineBuilder
{
    private IGameProcessingPipelineHandler _firstHandler;
    private IGameProcessingPipelineHandler _lastHandler;

    public GameProcessingPipelineBuilder AddHandler(IGameProcessingPipelineHandler handler)
    {
        if (_firstHandler == null)
        {
            _firstHandler = handler;
            _lastHandler = handler;
        }
        else
        {
            _lastHandler.SetNext(handler);
            _lastHandler = handler;
        }

        return this;
    }

    public IGameProcessingPipelineService Build()
    {
        return new GameProcessingPipelineService(_firstHandler);
    }
}
