namespace Gamestore.Services.Models;

public record GameModelDto
{
    public Guid Id { get; set; }

    public string Key { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public List<GenreModelDto> Genres { get; set; }

    public List<GenreModelDto> Plaftorms { get; set; }
}
