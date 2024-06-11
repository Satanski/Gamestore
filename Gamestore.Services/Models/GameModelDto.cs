using Gamestore.BLL.Models;

namespace Gamestore.Services.Models;

public record GameModelDto
{
    public Guid Id { get; set; }

    public string Key { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

    public int UnitInStock { get; set; }

    public int Discount { get; set; }

    public string Description { get; set; }

    public PublisherModelDto Publisher { get; set; }

    public List<GenreModel> Genres { get; set; }

    public List<PlatformModel> Platforms { get; set; }
}
