using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Entities;

[Table("GamePlatforms")]
[PrimaryKey(nameof(GameId), nameof(PlatformId))]
public class GamePlatform
{
    public Guid GameId { get; set; }

    public Guid PlatformId { get; set; }

    public Game Game { get; set; }

    public Platform Platform { get; set; }
}
