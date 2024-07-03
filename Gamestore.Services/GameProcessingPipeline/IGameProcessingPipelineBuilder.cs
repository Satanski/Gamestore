using Gamestore.BLL.BanHandler;

namespace Gamestore.BLL.Filtering;

public interface IGameProcessingPipelineBuilder
{
    GameProcessingPipelineBuilder AddHandler(IGameProcessingPipelineHandler handler);

    IGameProcessingPipelineService Build();
}
