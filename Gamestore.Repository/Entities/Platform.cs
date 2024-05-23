using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gamestore.Repository.Entities;

[Table("Platforms")]
[Index(nameof(Type), IsUnique = true)]

public class Platform
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Type { get; set; }

    public ICollection<GamePlatform> GamePlatforms { get; set; }
}
