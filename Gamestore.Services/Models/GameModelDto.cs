namespace Gamestore.Services.Models;

public record GameModelDto
{
    public Guid Id { get; set; }

    public string Key { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public List<GenreModelDto> GameGenres { get; set; }

    public List<GenreModelDto> GamePlaftorms { get; set; }
}
