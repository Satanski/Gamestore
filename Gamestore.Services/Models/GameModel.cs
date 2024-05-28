namespace Gamestore.Services.Models;

public record GameModel
{
    public Guid Id { get; set; }

    public string Key { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}
