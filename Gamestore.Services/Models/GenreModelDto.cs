namespace Gamestore.Services.Models;

public record GenreModelDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid? ParentGenreId { get; set; } = null!;
}
