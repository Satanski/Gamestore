using Gamestore.Services.Models;

namespace Gamestore.BLL.Filtering;

public class FilteredGamesDto
{
    public List<GameModelDto> Games { get; set; } = [];

    public int TotalPages { get; set; }

    public int CurrentPage { get; set; }
}
