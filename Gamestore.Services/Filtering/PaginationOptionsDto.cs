namespace Gamestore.BLL.Filtering;

public static class PaginationOptionsDto
{
    public static List<string> PaginationOptions { get; set; } = ["10", "20", "50", "100", "all"];
}
