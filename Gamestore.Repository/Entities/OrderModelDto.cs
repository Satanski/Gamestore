using Gamestore.DAL.Enums;

namespace Gamestore.DAL.Entities;

public record OrderModelDto
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime Date { get; set; }

    public List<OrderGame> OrderGames { get; set; }
}
