namespace Gamestore.Services.Models;

public class GenreModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid ParentGenreId { get; set; }
}
