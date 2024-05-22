using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Repository.Entities;

[Table("GamePlatforms")]
[PrimaryKey(nameof(GameId), nameof(Platform))]
public class GamePlatform
{
    public Guid GameId { get; set; }

    public Guid Platform { get; set; }
}
