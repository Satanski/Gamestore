using Gamestore.Services.Models;

namespace Gamestore.BLL.Models;

public record GameUpdateDto
{
    public GameModel Game { get; set; }
}
