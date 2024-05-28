using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gamestore.DAL.Entities;

[Table("Games")]
[Index(nameof(Key), IsUnique = true)]
public class Game
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Key { get; set; }

    public string? Description { get; set; } = null!;

    public List<GameGenre> GameGenres { get; set; }

    public List<GamePlatform> GamePlatforms { get; set; }
}
