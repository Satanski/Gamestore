using Gamestore.Services.Models;

namespace Gamestore.BLL.Models;

public record OrderGameDto
{
    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public double Price { get; set; }

    public int Quantity { get; set; }

    public int? Discount { get; set; }

    public OrderModelDto Order { get; set; }

    public GameModelDto Product { get; set; }
}
