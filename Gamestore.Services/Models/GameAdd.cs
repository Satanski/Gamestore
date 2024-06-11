namespace Gamestore.Services.Models;

public record GameAdd
{
    public string Key { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

    public int UnitInStock { get; set; }

    public int Discount { get; set; }

    public string Description { get; set; }
}
