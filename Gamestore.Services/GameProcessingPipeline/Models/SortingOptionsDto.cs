namespace Gamestore.BLL.Filtering.Models;

public static class SortingOptionsDto
{
    public static List<string> SortingOptions { get; set; } = ["Most popular", "Most commented", "Price ASC", "Price DESC", "New"];
}
