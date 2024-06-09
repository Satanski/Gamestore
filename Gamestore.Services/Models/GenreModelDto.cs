namespace Gamestore.Services.Models;

public record GenreModelDto
{
    public string Name { get; set; }

    public Guid? ParentGenreId { get; set; } = null!;
}
