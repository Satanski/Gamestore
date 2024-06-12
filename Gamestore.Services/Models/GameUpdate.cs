namespace Gamestore.Services.Models;

public record GameUpdate
{
    public Guid Id { get; set; }

    public string Key { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

    public int UnitInStock { get; set; }

    public int Discontinued { get; set; }

    public string Description { get; set; }
}
