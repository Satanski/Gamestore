namespace Gamestore.BLL.Filtering;

public interface IGameProcessingPipelineDirector
{
    IGameProcessingPipelineService ConstructGameCollectionOperationService();
}
