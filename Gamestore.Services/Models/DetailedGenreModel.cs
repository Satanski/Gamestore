namespace Gamestore.Services.Models;

public class DetailedGenreModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid? ParentGenreId { get; set; } = null!;
}
