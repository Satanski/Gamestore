namespace Gamestore.BLL.Models;

public record OrderDetailsDto
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public string Key { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

    public int Quantity { get; set; }

    public int Discount { get; set; }
}
