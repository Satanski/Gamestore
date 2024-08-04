using Gamestore.BLL.Helpers;
using Gamestore.BLL.Models;

namespace Gamestore.Services.Models;

public record GameModelDto
{
    private string _key;

    public Guid? Id { get; set; }

    public string? Key
    {
        get => _key;

        set
        {
            _key = string.IsNullOrEmpty(value) ? AutoGenrateGameKeyHelpers.GenerateGameKey(Name) : value;
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

    public virtual bool Equals(GameModelDto? other)
    {
        if (!ReferenceEquals(this, other))
        {
            if (other is null)
            {
                return false;
            }

            return Id == other.Id && Key == other.Key;
        }

        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Price);
    }
}
