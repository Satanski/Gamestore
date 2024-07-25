using Gamestore.Services.Models;

namespace Gamestore.BLL.Models;

public record OrderGameModelDto
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public double Price { get; set; }

    public int Quantity { get; set; }

    public int? Discount { get; set; }

    public GameModelDto Product { get; set; }
}
