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

    public DateTime OrderDate { get; set; }

    public DateTime? RequiredDate { get; set; }

    public DateTime? ShippedDate { get; set; }

    public Guid? EmployeeId { get; set; }

    public Guid? ShipVia { get; set; }

    public float? Freight { get; set; }

    public string? ShipName { get; set; }

    public string? ShipAddress { get; set; }

    public string? ShipCity { get; set; }

    public string? ShipRegion { get; set; }

    public string? ShipPostalCode { get; set; }

    public string? ShipCountry { get; set; }

    public List<OrderGame> OrderGames { get; set; }
}
