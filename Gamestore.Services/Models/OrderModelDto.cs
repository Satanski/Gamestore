using Gamestore.DAL.Enums;

namespace Gamestore.BLL.Models;

public record OrderModelDto
{
    public Guid Id { get; set; }

    public string CustomerId { get; set; }

    public OrderStatus Status { get; set; }

    public string Date { get; set; }

    public List<OrderGameModelDto> OrderGames { get; set; }
}
