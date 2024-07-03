namespace Gamestore.BLL.Filtering.Models;

public class GameFiltersDto
{
    public List<Guid> GenresFilter { get; set; } = [];

    public List<Guid> PlatformsFilter { get; set; } = [];

    public List<Guid> PublishersFilter { get; set; } = [];

    public int Page { get; set; } = 1;

    public int? MinPrice { get; set; }

    public int? MaxPrice { get; set; }

    public string? DatePublishing { get; set; }

    public string? Sort { get; set; }

    public string? PageCount { get; set; }

    public string? Name { get; set; }

    public int? NumberOfPagesAfterFiltration { get; set; }
}
