namespace Gamestore.Services.Models;

public class DetailedGameModel
{
    public Guid Id { get; set; }

    public string Key { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public List<Guid> Genres { get; set; }

    public List<Guid> Plaftorms { get; set; }
}
