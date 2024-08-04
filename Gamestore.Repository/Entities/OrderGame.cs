using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gamestore.DAL.Entities;

[Table("OrderGames")]
[PrimaryKey(nameof(OrderId), nameof(GameId))]
public class OrderGame
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public Guid OrderId { get; set; }

    [Required]
    public Guid GameId { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public int Quantity { get; set; }

    public int? Discount { get; set; }

    public Order Order { get; set; }

    public Game Game { get; set; }
}
