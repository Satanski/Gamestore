namespace Gamestore.MongoRepository.LoggingModels;

public class MongoGenre
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string? ParentGenreId { get; set; } = null!;
}
