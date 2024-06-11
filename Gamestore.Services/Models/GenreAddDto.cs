using Gamestore.Services.Models;

namespace Gamestore.BLL.Models;

public record GenreAddDto
{
    public GenreModelDto Genre { get; set; }
}
