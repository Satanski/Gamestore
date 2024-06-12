namespace Gamestore.BLL.Models;

public record GenreUpdate
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid? ParentGenreId { get; set; }
}
