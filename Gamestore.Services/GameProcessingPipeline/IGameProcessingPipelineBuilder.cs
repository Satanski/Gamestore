using Gamestore.BLL.BanHandler;

namespace Gamestore.BLL.Filtering;

public interface IGameProcessingPipelineBuilder
{
    IGameProcessingPipelineBuilder AddHandler(IGameProcessingPipelineHandler handler);

    IGameProcessingPipelineService Build();
}
