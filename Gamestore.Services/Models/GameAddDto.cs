using Gamestore.Services.Models;

namespace Gamestore.BLL.Models;

public record GameAddDto
{
    public GameAdd Game { get; set; }

    public Guid Publisher { get; set; }

    public List<Guid> Genres { get; set; }

    public List<Guid> Platforms { get; set; }
}
