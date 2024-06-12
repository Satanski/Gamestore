namespace Gamestore.BLL.Models;

public record GenreAdd
{
    public string Name { get; set; }

    public Guid? ParentGenreId { get; set; }
}
