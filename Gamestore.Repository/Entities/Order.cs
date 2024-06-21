using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gamestore.DAL.Enums;

namespace Gamestore.DAL.Entities;

[Table("Orders")]
public class Order
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid CustomerId { get; set; }

    [Required]
    public OrderStatus Status { get; set; }

    public DateTime Date { get; set; }

    public List<OrderGame> OrderGames { get; set; }
}
