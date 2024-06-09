namespace Gamestore.Services.Models;

public record GameUpdateModel
{
    public Guid Id { get; set; }

    public string Key { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

    public int UnitInStock { get; set; }

    public int Discount { get; set; }

    public Guid PublisherId { get; set; }

    public string Description { get; set; }

    public List<Guid> Genres { get; set; }

    public List<Guid> Platforms { get; set; }
}
