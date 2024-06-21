using Gamestore.DAL.Enums;

namespace Gamestore.BLL.Models;

public record OrderModelDto
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime Date { get; set; }

    public List<OrderGameModelDto> OrderGames { get; set; }
}
