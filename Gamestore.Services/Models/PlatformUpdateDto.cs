using Gamestore.Services.Models;

namespace Gamestore.BLL.Models;

public record PlatformUpdateDto
{
    public PlatformModel Platform { get; set; }
}
