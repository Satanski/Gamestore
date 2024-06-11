using Gamestore.Services.Models;

namespace Gamestore.BLL.Models;

public record GenreUpdateDto
{
    public GenreModel Genre { get; set; }
}
