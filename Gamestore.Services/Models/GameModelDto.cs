using Gamestore.BLL.Helpers;
using Gamestore.BLL.Models;

namespace Gamestore.Services.Models;

public record GameModelDto
{
    private string _key;

    public Guid? Id { get; set; }

    public string? Key
    {
        get
        {
            return _key;
        }

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                _key = AutoGenrateGameKeyHelpers.GenerateGameKey(Name);
            }
            else
            {
                _key = value;
            }
        }
    }

    public string Name { get; set; }

    public double Price { get; set; }

    public int UnitInStock { get; set; }

    public int Discontinued { get; set; }

    public string Description { get; set; }

    public DateOnly PublishDate { get; set; }

    public PublisherModelDto? Publisher { get; set; }

    public List<GenreModelDto>? Genres { get; set; }

    public List<PlatformModelDto>? Platforms { get; set; }
}
