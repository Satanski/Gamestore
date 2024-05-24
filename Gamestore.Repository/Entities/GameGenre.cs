using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Entities;

[Table("GameGenres")]
[PrimaryKey(nameof(GameId), nameof(GenreId))]
public class GameGenre
{
    public Guid GameId { get; set; }

    public Guid GenreId { get; set; }

    public Game Game { get; set; }

    public Genre Genre { get; set; }
}
