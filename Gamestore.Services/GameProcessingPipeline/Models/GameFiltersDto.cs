using System.Text.Json.Serialization;

namespace Gamestore.BLL.Filtering.Models;

public class GameFiltersDto
{
    public List<Guid> Genres { get; set; } = [];

    public List<Guid> Platforms { get; set; } = [];

    public List<Guid> Publishers { get; set; } = [];

    public int Page { get; set; } = 1;

    public int? MinPrice { get; set; }

    public int? MaxPrice { get; set; }

    public string? DatePublishing { get; set; }

    public string? Sort { get; set; }

    public string? PageCount { get; set; }

    public string? Name { get; set; }

    [JsonIgnore]
    public int NumberOfPagesAfterFiltration { get; set; } = 0;

    [JsonIgnore]
    public int NumberOfGamesFromPreviousSource { get; set; } = 0;

    [JsonIgnore]
    public int NumberOfDisplayedGamesFromPreviousSource { get; internal set; }
}
