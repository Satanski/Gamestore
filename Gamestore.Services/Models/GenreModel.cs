namespace Gamestore.Services.Models;

public record GenreModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid? ParentGenreId { get; set; } = null!;
}
