using Gamestore.Services.Models;

namespace Gamestore.BLL.Models;

public record GameDtoWrapper
{
    public GameModelDto Game { get; set; }

    public Guid Publisher { get; set; }

    public List<Guid> Genres { get; set; }

    public List<Guid> Platforms { get; set; }
}
