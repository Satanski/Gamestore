using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Entities;

[Table("GameGenres")]
[PrimaryKey(nameof(GameId), nameof(GenreId))]
public class GameGenres
{
    public Guid GameId { get; set; }

    public Guid GenreId { get; set; }

    public Game Product { get; set; }

    public Genre Category { get; set; }
}
